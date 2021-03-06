//This is a part of the DC4Ever emulator
//You are not allowed to release modified(or unmodified) versions
//without asking me (drk||Raziel).
//For Suggestions ect please e-mail at : stef_mp@yahoo.gr
//Note : This is just to prove that Fast emulation is possible with 
//	C# /.net ...And yes , this code could be writen at VB.net and 
//	run more or less at the same speed on dynarec mode
//	This code requires C#.net 2.0 (Get the C# epxress 2005 Beta from microsoft)
//

//Managed Recompiler
//Generetes CIL code from dc opcodes
//#define interpreter
//define the above to disable it
#if !interpreter
#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

#endregion
namespace DC4Ever
{
    #region delegate
    //lota params ehh??
    public unsafe delegate uint DynaRecCall(uint* reg0, uint* reg1, uint* reg2,
                                            uint* reg3, uint* reg4, uint* reg5,
                                            uint* reg6, uint* reg7, uint* reg8,
                                            uint* reg9, uint* reg10, uint* reg11,
                                            uint* reg12, uint* reg13, uint* reg14,
                                            uint* reg15, uint* reg16, uint* reg17,
                                            uint* reg18, uint* reg19, uint* reg20,
                                            uint* reg21, uint* reg22, uint* reg23,
                                            float* reg24, float* reg25, float* reg26,
                                            float* reg27, float* reg28, float* reg29,
                                            float* reg30, float* reg31, float* reg32,
                                            float* reg33, float* reg34, float* reg35,
                                            float* reg36, float* reg37, float* reg38,
                                            float* reg39, float* reg40, float* reg41,
                                            float* reg42, float* reg43, float* reg44,
                                            float* reg45, float* reg46, float* reg47,
                                            float* reg48, float* reg49, float* reg50,
                                            float* reg51, float* reg52, float* reg53,
                                            float* reg54, float* reg55, ref uint reg56,
                                            ref uint reg57, ref uint reg58, ref uint reg59,
                                            ref uint reg60, ref uint reg61, ref uint reg62,
                                            ref uint reg63, ref uint reg64, ref uint reg65,
                                            ref uint reg66, ref uint reg67, ref uint reg68);
    #endregion 
    #if nrt 
    public unsafe static partial  class emu
 #else  
    public unsafe partial  class emu 
 #endif
    {
        #region Dynarec Registers Param offsets
        //public static uint[] r = new uint[16];
        public const int l_rbase = 0;
        //public static uint[] r_bank = new uint[8];
        public const int l_r_bbase = l_rbase + 16;
        //public static float[] fr=new float[16];//fp regs set 1
        public const int l_fr_base = l_r_bbase + 8;
        //public static float[] xr=new float[16];//fp regs set 2
        public const int l_xr_base = l_fr_base + 16;
        //gbr,ssr,spc,sgr,dbr,vbr;
        public const int l_gbri = l_xr_base + 16;
        public const int l_ssri = l_gbri + 1;
        public const int l_spci = l_ssri + 1;
        public const int l_sgri = l_spci + 1;
        public const int l_dbri = l_sgri + 1;
        public const int l_vbri = l_dbri + 1;
        //public static uint mach,macl,pr,fpul;
        public const int l_machi = l_vbri + 1;
        public const int l_macli = l_machi + 1;
        public const int l_pri = l_macli + 1;
        public const int l_fpuli = l_pri + 1;
        //public static uint pc;
        public const int l_pci = l_fpuli + 1;
        //sr and fpscr clases
        public const int l_sri = l_pci + 1;
        public const int l_fpscri = l_sri + 1;
        public const int pCount = l_fpscri + 1;//count of all registers ;)
        public const int tuint = pCount;
        public const int tflt = pCount + 1;
        public const int tclk = tflt + 1;
        public const int tdfnl = tclk + 1;

        public static bool[] bLoaded = new bool[pCount];
        public static bool[] bEdited = new bool[pCount];
        #endregion
        public static uint ch,cm;  
        static uint br_8_b_level;
        static uint br_8_f_level;
        public static uint br_8_b_level_max = 35;//maximum conditional inlines
        public static uint br_8_f_level_max = 35;//maximum conditional inlines
        public static uint BlockCycles=0;
        public static uint BlockLen = 0;
        static Random rnd = new Random();
        public const int RecMB =2*kb;//2k blocks 
        public static CodeCacheEntry[] DynaCache = new CodeCacheEntry[RecMB];//code cache
        static MethodInfo writemeth = typeof(emu).GetMethod("write");
        static MethodInfo readmeth = typeof(emu).GetMethod("read");
        static MethodInfo Cosmeth = typeof(Math).GetMethod("Cos");
        static MethodInfo Sinmeth = typeof(Math).GetMethod("Sin");
        static MethodInfo UpdateMeth = typeof(emu).GetMethod("UpdateSystem");
        static Label lbl_short_exit;
        static bool RecBlockEnd;
        
        /// <summary>
        /// Code Buffer id index
        /// </summary>
        static int CBind = 0;
        //    test.FinaliseIl();
        //    DynaRecCall tb=test.GetCodeBuffer();
        //    uint tst=0,nil=0;
        //
        //    tb(ref tst, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil,
        //       ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil,
        //       ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil,
        //       ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil,
        //       ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil, ref nil,
        //       ref nil, ref nil);
        static uint rec_special_pc = 0;
        /// <summary>
        /// Outputs the msil code for 1 opcode...
        /// </summary>
        /// <param name="Opcode to generate"></param>
        /// <param name="IlCoder object to use for generation"></param>
        /// <returns>Returns number of cycles that the opcode \n Given takes to execure.\n If 0 then the buffer must be finalized after this call </returns>
        public static unsafe uint RecSingle(uint opcode, IlCoder to)
        {
            #region opcode msil emit
            recIltclkMark(ccount[opcode], to.il );
            switch (opcode >> 12)//proc opcode
            {
                case 0x0://finished rec
                    #region case 0x0

                    switch (opcode & 0xf)
                    {
                        case 0x0://0000
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                        case 0x1://0001
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                        case 0x2://0010
                            #region case 0x2 multi opcodes
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000
                                    //i0000_nnnn_0000_0010();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] = sr.reg; 
                                    ldloc(l_sri,to.il );//load sr
                                    stloc(l_rbase + n,to.il);//store it to rn
                                    return ccount[opcode];
                                    //break;
                                case 0x1://0001
                                    //i0000_nnnn_0001_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                                case 0x2://0010
                                    //i0000_nnnn_0010_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                                case 0x3://0011
                                    //i0000_nnnn_0011_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                                case 0x4://0100
                                    //i0000_nnnn_0100_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x8://1000
                                    //i0000_nnnn_1000_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x9://1001
                                    //i0000_nnnn_1001_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xA://1010
                                    //i0000_nnnn_1010_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xB://1011
                                    //i0000_nnnn_1011_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xC://1100
                                    //i0000_nnnn_1100_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xD://1101
                                    //i0000_nnnn_1101_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xE://1110
                                    //i0000_nnnn_1110_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xF://1111
                                    //i0000_nnnn_1111_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x3://0011
                            #region case 0x3 multi opcodes
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000
                                    //i0000_nnnn_0000_0011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0010
                                    //i0000_nnnn_0010_0011();
                                    n = (uint)(opcode >> 8) & 0x0F;
                                    //delayslot = r[n] + pc + 4;
                                    //pc_funct = 2;//delay 1
                                    to.il.Emit(OpCodes.Ldc_I4, pc + 4);   //load the value that we must jump , part 1
                                    ldloc(l_rbase + n,to.il);   //load the value that we must jump , part 2 and add them
                                    to.il.Emit(OpCodes.Add);                //add them 
                                    stloc (l_pci,to.il );     //store the result to pc register
                                    uint temp = RecSingle(RecNextOpcode(), to);// do the delayslot instruction
                                    RecBlockEnd = true;
                                    return (uint)(temp + ccount[opcode]);
                                    //break;
                                case 0x8://1000
                                    //i0000_nnnn_1000_0011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x9://1001
                                    //i0000_nnnn_1001_0011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xA://1010
                                    //i0000_nnnn_1010_0011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xB://1011
                                    //i0000_nnnn_1011_0011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xC://1100
                                    //i0000_nnnn_1100_0011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x4://0100
                            //i0000_nnnn_mmmm_0100();
                            break;
                        case 0x5://0101
                            //i0000_nnnn_mmmm_0101();
                            n = (uint)(opcode >> 8) & 0x0F;
                            m = (uint)(opcode >> 4) & 0x0F;
                            //write(r[0] + r[n], r[m] & 0xFFFF, 2);
                            ldloc (l_rbase,to.il);//values to add
                            ldloc(l_rbase + n,to.il);
                            to.il.Emit(OpCodes.Add);//add them , this is param 1
                            ldloc(l_rbase + m,to.il);//param 2
                            to.il.Emit(OpCodes.Ldc_I4_2); //param 3
                            to.il.Emit(OpCodes.Call, writemeth);
                            return ccount[opcode];
                            //break;
                        case 0x6://0110
                            //i0000_nnnn_mmmm_0110();
                            break;
                        case 0x7://0111
                            //i0000_nnnn_mmmm_0111();
                            n = (uint)(opcode >> 8) & 0x0F;
                            m = (uint)(opcode >> 4) & 0x0F;
                            //macl = (uint)(((int)r[n] * (int)r[m]) & 0xFFFFFFFF);
                            ldloc(l_rbase + n,to.il );//values to mul
                            ldloc(l_rbase + m,to.il);
                            to.il.Emit(OpCodes.Mul);//mul them , push rez
                            stloc(l_macli,to.il );//store to macl
                            return ccount[opcode];
                            //break;
                        case 0x8://1000
                            #region case 0x8 multi opcodes
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000
                                    ////i0000_0000_0000_1000();
                                    break;
                                case 0x1://0001
                                    ////i0000_0000_0001_1000();
                                    break;
                                case 0x2://0010
                                    ////i0000_0000_0010_1000();
                                    break;
                                case 0x3://0011
                                    //i0000_0000_0011_1000();
                                    break;
                                case 0x4://0100
                                    //i0000_0000_0100_1000();
                                    break;
                                case 0x5://0101
                                    //i0000_0000_0101_1000();
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x9://1001
                            #region case 0x9 multi opcodes
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000 heeheheeh nop
                                    //i0000_0000_0000_1001();
                                    return ccount[opcode];
                                    //break;
                                case 0x1://0001
                                    //i0000_0000_0001_1001();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0010
                                    //i0000_nnnn_0010_1001();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0xA://1010
                            #region case 0xA multi opcodes
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000
                                    //i0000_nnnn_0000_1010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x1://0001
                                    //i0000_nnnn_0001_1010();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] = macl;
                                    ldloc(l_macli,to.il);//push macl
                                    stloc (l_rbase + n,to.il );//pop it to reg n
                                    return ccount[opcode];
                                    //break;
                                case 0x2://0010
                                    //i0000_nnnn_0010_1010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x5://0101
                                    //i0000_nnnn_0101_1010();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] = (uint)(int)fpul;
                                    ldloc (l_fpuli,to.il);//push fpul
                                    stloc (l_rbase + n,to.il);//pop it to reg n
                                    return ccount[opcode];
                                    //break;
                                case 0x6://0110
                                    //i0000_nnnn_0110_1010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0xB://1011
                            #region case 0xB multi opcodes
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000
                                    //i0000_0000_0000_1011();
                                    //delayslot = pr;
                                    //pc_funct = 2;//delay slot 1
                                    ldloc (l_pri,to.il);//push pr
                                    stloc (l_pci,to.il);//pop it to pc
                                    RecBlockEnd = true;//end of block
                                    return RecSingle(RecNextOpcode(), to) +ccount[opcode];// do the delayslot instruction and ret
                                    //break;
                                case 0x1://0001
                                    //i0000_0000_0001_1011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0010
                                    //i0000_0000_0010_1011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0xC://1100
                            //i0000_nnnn_mmmm_1100();
                            n = (uint)(opcode >> 8) & 0x0F;
                            m = (uint)(opcode >> 4) & 0x0F;
                            //r[n] = (uint)(sbyte)read(r[0] + r[m], 1);
                            ldloc (l_rbase ,to.il);//push r0
                            ldloc (l_rbase + m,to.il);//push rm
                            to.il.Emit(OpCodes.Add);            //add them and push rez
                            to.il.Emit(OpCodes.Conv_I1);        //convert to singed
                            stloc (l_rbase + n,to.il);//save it to rn
                            return ccount[opcode];
                            //break;
                        case 0xD://1101
                            //i0000_nnnn_mmmm_1101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xE://1110
                            //i0000_nnnn_mmmm_1110();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xF://1111
                            //i0000_nnnn_mmmm_1111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                    }
                    #endregion
                    break;
                case 0x1://finished rec
                    //i0001_nnnn_mmmm_iiii();
                    n = (uint)(opcode >> 8) & 0x0F;
                    m = (uint)(opcode >> 4) & 0x0F;
                    disp = opcode & 0x0F;
                    //write(r[n] + disp * 4, r[m], 4); do this in msil
                    ldloc (l_rbase + n,to.il);//load register
                    to.il.Emit(OpCodes.Ldc_I4, disp * 4);//load value to add
                    to.il.Emit(OpCodes.Add);//add them 
                    ldloc(l_rbase + m, to.il);//load value to write (param #2)
                    to.il.Emit(OpCodes.Ldc_I4_4);//load 4 for 4 bytes write (param #3)
                    to.il.Emit(OpCodes.Call,writemeth );//call write
                    return ccount[opcode];
                    //break;
                case 0x2://finished rec
                    #region case 0x2
                    switch (opcode & 0xf)
                    {
                        case 0x0://0000
                            //i0010_nnnn_mmmm_0000();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //write(r[n], r[m] & 0xFF, 1);
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Conv_U1);           //convert to byte
                            to.il.Emit(OpCodes.Ldc_I4_1);//load 1
                            to.il.Emit(OpCodes.Call, writemeth);///do the write
                            return ccount[opcode];//return
                            //break;
                        case 0x1://0001
                            //i0010_nnnn_mmmm_0001();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //write(r[n], r[m] & 0xFFFF, 2);
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Conv_U2);           //convert to word
                            to.il.Emit(OpCodes.Ldc_I4_2);//load 2 (word write)
                            to.il.Emit(OpCodes.Call, writemeth);//do the write
                            return ccount[opcode];//return
                            //break;
                        case 0x2://0010
                            //i0010_nnnn_mmmm_0010();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //write(r[n], r[m], 4);//at r[n],r[m]
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Ldc_I4_4);//load 4 (dword)
                            to.il.Emit(OpCodes.Call, writemeth);//do the write
                            return ccount[opcode];//return
                            //break;
                        case 0x4://0100
                            //i0010_nnnn_mmmm_0100();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x5://0101
                            //i0010_nnnn_mmmm_0101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x6://0110
                            //i0010_nnnn_mmmm_0110();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] -= 4;
                            //write(r[n], r[m], 4);
                            ldloc(l_rbase + n, to.il);//load n
                            to.il.Emit(OpCodes.Ldc_I4_S, 4);       //load -4 
                            to.il.Emit(OpCodes.Sub);               //do the -
                            stloc(l_rbase + n, to.il);//store n

                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Ldc_I4_4);//load 4 (dword)
                            to.il.Emit(OpCodes.Call, writemeth);//do the write
                            return ccount[opcode];//return
                            //break;
                        case 0x7://0111
                            //i0010_nnnn_mmmm_0111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x8://1000
                            //i0010_nnnn_mmmm_1000();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //dc.dcon.WriteLine("non emulated opcode on recompiler mode...");
                            //if ((r[n] & r[m]) > 0)
                            //    sr.T = 0;
                            //else
                            //    sr.T = 1;
                            locarg(l_sri, to.il);
                            Label T0 = to.il.DefineLabel();
                            Label fend = to.il.DefineLabel();
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.And);//bitwise and , pushed result
                            to.il.Emit(OpCodes.Brtrue_S , T0);// if not 0 then them jusm T0
                            to.il.Emit(OpCodes.Ldc_I4, sr_T_bit_set);
                            ldloc(l_sri, to.il);//sr register has t in it
                            to.il.Emit(OpCodes.Or);          //bitwise or to set T in sr
                            stloc(l_sri, to.il);//store the rez back to sr
                            to.il.Emit(OpCodes.Br_S, fend);//finished , goto to end
                            to.il.MarkLabel(T0);//set t to 0
                            to.il.Emit(OpCodes.Ldc_I4, sr_T_bit_reset);
                            ldloc(l_sri, to.il);//sr register has t in it
                            to.il.Emit(OpCodes.And);          //bitwise And to unset T in sr
                            stloc(l_sri, to.il);//store the rez back to sr
                            to.il.MarkLabel(fend);//function end
                            return ccount[opcode];//return
                            //break;
                        case 0x9://1001
                            //i0010_nnnn_mmmm_1001();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] &= r[m];
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.And);               //bitwise and
                            stloc(l_rbase + n, to.il);//store n
                            return ccount[opcode];                 //return
                            //break;
                        case 0xA://1010
                            //i0010_nnnn_mmmm_1010();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] ^= r[m];
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Xor);               //bitwise Xor
                            stloc(l_rbase + n, to.il);//store n
                            return ccount[opcode];                 //return
                            //break;
                        case 0xB://1011
                            //i0010_nnnn_mmmm_1011();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] |= r[m];
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Or);               //bitwise or
                            stloc(l_rbase + n, to.il);//store n
                            return ccount[opcode];                 //return
                            //break;
                        case 0xC://1100
                            //i0010_nnnn_mmmm_1100();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xD://1101
                            //i0010_nnnn_mmmm_1101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xE://1110
                            //i0010_nnnn_mmmm_1110();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xF://1111
                            //i0010_nnnn_mmmm_1111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        default:
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
                case 0x3://finished rec
                    #region case 0x3
                    switch (opcode & 0xf)
                    {
                        case 0x0://0000
                            //i0011_nnnn_mmmm_0000();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x2://0010
                            //i0011_nnnn_mmmm_0010();
                            locarg(l_sri, to.il);
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //if (r[n] >= r[m])
                            //    sr.T = 1;
                            //else
                            //    sr.T = 0;
                            Label T0 = to.il.DefineLabel();
                            Label fend = to.il.DefineLabel();
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Bge_S, T0);// if > then set t=1
                            to.il.Emit(OpCodes.Ldc_I4, sr_T_bit_set);
                            ldloc(l_sri, to.il);//sr register has t in it
                            to.il.Emit(OpCodes.Or);          //bitwise or to set T in sr
                            stloc(l_sri, to.il);//store the rez back to sr
                            to.il.Emit(OpCodes.Br_S, fend);//finished , goto to end
                            to.il.MarkLabel(T0);//set t to 0
                            to.il.Emit(OpCodes.Ldc_I4, sr_T_bit_reset);
                            ldloc(l_sri, to.il);//sr register has t in it
                            to.il.Emit(OpCodes.And);          //bitwise And to unset T in sr
                            stloc(l_sri, to.il);//store the rez back to sr
                            to.il.MarkLabel(fend);//function end
                            return ccount[opcode];//return
                            //break;
                        case 0x3://0011
                            //i0011_nnnn_mmmm_0011();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x4://0100
                            //i0011_nnnn_mmmm_0100();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x5://0101
                            //i0011_nnnn_mmmm_0101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x6://0110
                            //i0011_nnnn_mmmm_0110();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            locarg(l_sri, to.il);
                            //if (r[n] > r[m])
                            //    sr.T = 1;
                            //else
                             //    sr.T = 0;
                            T0 = to.il.DefineLabel();
                            fend = to.il.DefineLabel();
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Bgt_S, T0);// if > then set t=1
                            to.il.Emit(OpCodes.Ldc_I4, sr_T_bit_set);
                            ldloc(l_sri, to.il);//sr register has t in it
                            to.il.Emit(OpCodes.Or);          //bitwise or to set T in sr
                            stloc(l_sri, to.il);//store the rez back to sr
                            to.il.Emit(OpCodes.Br_S, fend);//finished , goto to end
                            to.il.MarkLabel(T0);//set t to 0
                            to.il.Emit(OpCodes.Ldc_I4, sr_T_bit_reset);
                            ldloc(l_sri, to.il);//sr register has t in it
                            to.il.Emit(OpCodes.And);          //bitwise And to unset T in sr
                            stloc(l_sri, to.il);//store the rez back to sr
                            to.il.MarkLabel(fend);//function end
                            return ccount[opcode];//return
                            //break;
                        case 0x7://0111
                            //i0011_nnnn_mmmm_0111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x8://1000
                            //i0011_nnnn_mmmm_1000();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //rn = (int)r[n];
                            //rm = (int)r[m];
                            //rn -= rm;
                            //r[n] = (uint)rn;
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Sub);// substarct them
                            stloc(l_rbase + n, to.il);//store n
                            return ccount[opcode];//return
                            //break;
                        case 0xA://1010
                            //i0011_nnnn_mmmm_1010();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xB://1011
                            //i0011_nnnn_mmmm_1011();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xC://1100
                            //i0011_nnnn_mmmm_1100();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //rm = (int)r[m];
                            //rn = (int)r[n];
                            //rn += rm;
                            //r[n] = (uint)rn;
                            ldloc(l_rbase + n, to.il);//load n
                            ldloc(l_rbase + m, to.il);//load m
                            to.il.Emit(OpCodes.Add);// substarct them
                            stloc(l_rbase + n, to.il);//store n
                            return ccount[opcode];//return
                            //break;
                        case 0xD://1101
                            //i0011_nnnn_mmmm_1101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xE://1110
                            //i0011_nnnn_mmmm_1110();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xF://1111
                            //i0011_nnnn_mmmm_1111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        default:
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
                case 0x4://finished rec
                    #region case 0x4
                    switch (opcode & 0xf)
                    {
                        case 0x0://0000 rec
                            #region 0x0 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0000
                                    //i0100_nnnn_0000_0000();
                                    break;
                                case 0x1://0100_xxxx_0001_0000
                                    //i0100_nnnn_0001_0000();
                                    n = (opcode >> 8) & 0x0F;
                                    //rn = (int)(r[n]);
                                    //--rn;
                                    //if (rn == 0)
                                    //    sr.T = 1;
                                    //else
                                    //    sr.T = 0;
                                    //r[n] = (uint)rn;
                                    ldloc(l_rbase + n, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_1);         //load 1  
                                    to.il.Emit(OpCodes.Sub);              //do the sub  
                                    to.il.Emit(OpCodes.Dup);              //duplicate klas stack item
                                    stloc(l_rbase + n, to.il);//store
                                    to.il.Emit(OpCodes.Ldc_I4_0);         // if register=0
                                    SetTeq(to.il, 1, 0);                  //then t=1 else t=0  
                                    return ccount[opcode];//return
                                    //break;
                                case 0x2://0100_xxxx_0010_0000
                                    //i0100_nnnn_0010_0000();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x1://0001 rec
                            #region 0x1 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0001
                                    //i0100_nnnn_0000_0001();
                                    n = (opcode >> 8) & 0x0F;
                                    //sr.T = r[n] & 0x1;
                                    ldloc(l_rbase + n, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_1);         //load 1
                                    to.il.Emit(OpCodes.And);              //And them
                                    to.il.Emit(OpCodes.Ldc_I4_0);         //load 0
                                    SetTeq(to.il, 1, 0);                  // if and=1 then t=1 else t=0
                                    //r[n] >>= 1;
                                    ldloc(l_rbase + n, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_1);         //by 1  
                                    to.il.Emit(OpCodes.Shr_Un);           //unsined shift right by 1  
                                    stloc(l_rbase + n, to.il);//store the register
                                    return ccount[opcode];//return
                                    //break;
                                case 0x1://0100_xxxx_0001_0001
                                    //i0100_nnnn_0001_0001();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0100_xxxx_0010_0001
                                    //i0100_nnnn_0010_0001();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x2://0010 rec
                            #region 0x2 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0010
                                    //i0100_nnnn_0000_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x1://0100_xxxx_0001_0010
                                    //i0100_nnnn_0001_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0100_xxxx_0010_0010
                                    //i0100_nnnn_0010_0010();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] -= 4;
                                    ldloc(l_rbase + n, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);//load the 4
                                    to.il.Emit(OpCodes.Sub);           //sub them
                                    stloc(l_rbase + n, to.il);//store the register
                                    //write(r[n], pr, 4);
                                    ldloc(l_rbase + n, to.il); //load the register
                                    ldloc(l_pri, to.il);       //load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);//load the 4
                                    to.il.Emit(OpCodes.Call, writemeth);    //call teh function
                                    return ccount[opcode];//return
                                    //break;
                                case 0x5://0100_xxxx_0101_0010
                                    //i0100_nnnn_0101_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x6://0100_xxxx_0110_0010
                                    //i0100_nnnn_0110_0010();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] -= 4;
                                    ldloc(l_rbase + n, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);//load the 4
                                    to.il.Emit(OpCodes.Sub);           //sub them
                                    stloc(l_rbase + n, to.il);//store the register
                                    //write(r[n], fpscr.reg, 4);
                                    ldloc(l_rbase + n, to.il); //load the register
                                    ldloc(l_fpscri, to.il);   //load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);           //load the 4
                                    to.il.Emit(OpCodes.Call, writemeth);    //call the function
                                    return ccount[opcode];//return
                                    //break;
                                case 0x8://0100_xxxx_1000_0010
                                    //i0100_nnnn_1000_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x9://0100_xxxx_1001_0010
                                    //i0100_nnnn_1001_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xA://0100_xxxx_1010_0010
                                    //i0100_nnnn_1010_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xB://0100_xxxx_1011_0010
                                    //i0100_nnnn_1011_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xC://0100_xxxx_1100_0010
                                    //i0100_nnnn_1100_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xD://0100_xxxx_1101_0010
                                    //i0100_nnnn_1101_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xE://0100_xxxx_1110_0010
                                    //i0100_nnnn_1110_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xF://0100_xxxx_1111_0010
                                    //i0100_nnnn_1111_0010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x3://0011 rec
                            #region 0x3 multi

                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0011
                                    //i0100_nnnn_0000_0011();
                                    break;
                                case 0x1://0100_xxxx_0001_0011
                                    //i0100_nnnn_0001_0011();
                                    break;
                                case 0x2://0100_xxxx_0010_0011
                                    //i0100_nnnn_0010_0011();
                                    break;
                                case 0x3://0100_xxxx_0011_0011
                                    //i0100_nnnn_0011_0011();
                                    break;
                                case 0x4://0100_xxxx_0100_0011
                                    //i0100_nnnn_0100_0011();
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x4://0100 rec
                            #region 0x4 multi

                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0100
                                    //i0100_nnnn_0000_0100();
                                    break;
                                case 0x2://0100_xxxx_0010_0100
                                    //i0100_nnnn_0010_0100();
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x5://0101 rec
                            #region 0x5 multi

                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0101
                                    //i0100_nnnn_0000_0101();
                                    break;
                                case 0x1://0100_xxxx_0001_0101
                                    //i0100_nnnn_0001_0101();
                                    break;
                                case 0x2://0100_xxxx_0010_0101
                                    //i0100_nnnn_0010_0101();
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x6://0110 rec
                            #region 0x6 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0110
                                    //i0100_nnnn_0000_0110();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x1://0100_xxxx_0001_0110
                                    //i0100_nnnn_0001_0110();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0100_xxxx_0010_0110
                                    //i0100_nnnn_0010_0110();
                                    m = (opcode >> 8) & 0x0F;
                                    //pr = read(r[m], 4);
                                    ldloc(l_rbase + m, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);          //load the 4
                                    to.il.Emit(OpCodes.Call ,readmeth);    //call read
                                    stloc (l_pri,to.il);      //store the return val
                                    //r[m] += 4;
                                    ldloc(l_rbase + m, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);          //load the 4
                                    to.il.Emit(OpCodes.Add );              //Add them
                                    stloc(l_rbase + m, to.il);//store the register
                                    return ccount[opcode];//return
                                    //break;
                                case 0x5://0100_xxxx_0101_0110
                                    //i0100_nnnn_0101_0110();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x6://0100_xxxx_0110_0110
                                    //i0100_nnnn_0110_0110();
                                    m = (opcode >> 8) & 0x0F;
                                    //fpscr.reg = read(r[m], 4);
                                    ldloc(l_rbase + m, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);          //load the 4
                                    to.il.Emit(OpCodes.Call, readmeth);    //call read
                                    stloc(l_fpscri, to.il);      //store the return val
                                    //r[m] += 4;
                                    ldloc(l_rbase + m, to.il);//load the register
                                    to.il.Emit(OpCodes.Ldc_I4_4);          //load the 4
                                    to.il.Emit(OpCodes.Add);              //Add them
                                    stloc(l_rbase + m, to.il);//store the register
                                    return ccount[opcode];//return
                                    //break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x7://0111 rec
                            #region 0x7 multi
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_0111
                                    //i0100_nnnn_0000_0111();
                                    break;
                                case 0x1://0100_xxxx_0001_0111
                                    //i0100_nnnn_0001_0111();
                                    break;
                                case 0x2://0100_xxxx_0010_0111
                                    //i0100_nnnn_0010_0111();
                                    break;
                                case 0x3://0100_xxxx_0011_0111
                                    //i0100_nnnn_0011_0111();
                                    break;
                                case 0x4://0100_xxxx_0100_0111
                                    //i0100_nnnn_0100_0111();
                                    break;
                                case 0x8://0100_xxxx_1000_0111
                                    //i0100_nnnn_1000_0111();
                                    break;
                                case 0x9://0100_xxxx_1001_0111
                                    //i0100_nnnn_1001_0111();
                                    break;
                                case 0xA://0100_xxxx_1010_0111
                                    //i0100_nnnn_1010_0111();
                                    break;
                                case 0xB://0100_xxxx_1011_0111
                                    //i0100_nnnn_1011_0111();
                                    break;
                                case 0xC://0100_xxxx_1100_0111
                                    //i0100_nnnn_1100_0111();
                                    break;
                                case 0xD://0100_xxxx_1101_0111
                                    //i0100_nnnn_1101_0111();
                                    break;
                                case 0xE://0100_xxxx_1110_0111
                                    //i0100_nnnn_1110_0111();
                                    break;
                                case 0xF://0100_xxxx_1111_0111
                                    //i0100_nnnn_1111_0111();
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x8://1000 rec
                            #region 0x8 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_1000
                                    //i0100_nnnn_0000_1000();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] <<= 2;
                                    ldloc(l_rbase + n, to.il); //number
                                    to.il.Emit(OpCodes.Ldc_I4_2);           //by2
                                    to.il.Emit(OpCodes.Shl);                //swift
                                    stloc(l_rbase + n, to.il); //and store      
                                    return ccount[opcode];//return
                                    //break;
                                case 0x1://0100_xxxx_0001_1000
                                    //i0100_nnnn_0001_1000();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] <<= 8;
                                    ldloc(l_rbase + n, to.il); //number
                                    to.il.Emit(OpCodes.Ldc_I4_8);           //by8
                                    to.il.Emit(OpCodes.Shl);                //swift
                                    stloc(l_rbase + n, to.il); //and store   
                                    return ccount[opcode];//return
                                    //break;
                                case 0x2://0100_xxxx_0010_1000
                                    //i0100_nnnn_0010_1000();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] <<= 16;
                                    ldloc(l_rbase + n, to.il); //number
                                    to.il.Emit(OpCodes.Ldc_I4,16);           //by16
                                    to.il.Emit(OpCodes.Shl);                //swift
                                    stloc(l_rbase + n, to.il); //and store   
                                    return ccount[opcode];//return
                                    //break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0x9://1001 rec
                            #region 0x9 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_1001
                                    //i0100_nnnn_0000_1001();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] >>= 2;
                                    ldloc(l_rbase + n, to.il); //number
                                    to.il.Emit(OpCodes.Ldc_I4_2);           //by2
                                    to.il.Emit(OpCodes.Shr_Un);                //shift
                                    stloc(l_rbase + n, to.il); //and store   
                                    return ccount[opcode];//return
                                    //break;
                                case 0x1://0100_xxxx_0001_1001
                                    //i0100_nnnn_0001_1001();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0100_xxxx_0010_1001
                                    //i0100_nnnn_0010_1001();
                                    n = (opcode >> 8) & 0x0F;
                                    //r[n] >>= 16;
                                    ldloc(l_rbase + n, to.il); //number
                                    to.il.Emit(OpCodes.Ldc_I4,16);           //by16
                                    to.il.Emit(OpCodes.Shr_Un);                //shift
                                    stloc(l_rbase + n, to.il); //and store   
                                    return ccount[opcode];//return
                                    //break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;

                        case 0xA://1010 rec
                            #region 0x9 multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_1010
                                    //i0100_nnnn_0000_1010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x1://0100_xxxx_0001_1010
                                    //i0100_nnnn_0001_1010();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0100_xxxx_0010_1010
                                    //i0100_nnnn_0010_1010();
                                    m = (opcode >> 8) & 0x0F;
                                    //pr = r[m];
                                    ldloc(l_rbase + m, to.il); //load
                                    stloc(l_pri, to.il); //and store  
                                    return ccount[opcode];//return
                                    //break;
                                case 0x5://0100_xxxx_0101_1010
                                    //i0100_nnnn_0101_1010();
                                    m = (opcode >> 8) & 0x0F;
                                    //fpul = r[m];
                                    ldloc(l_rbase + m, to.il); //load
                                    stloc(l_fpuli, to.il); //and store 
                                    return ccount[opcode];//return
                                    //break;
                                case 0x6://0100_xxxx_0110_1010
                                    //i0100_nnnn_0110_1010();
                                    m = (opcode >> 8) & 0x0F;
                                    //fpscr.reg = r[m];
                                    dc.dcon.WriteLine("Write to fpscr is not totaly corect , not lanching event changes..");
                                    ldloc(l_rbase + m, to.il); //load
                                    stloc(l_fpscri, to.il); //and store 
                                    return ccount[opcode];//return
                                    //break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;

                        case 0xB://1011 rec
                            #region 0xB multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_1011
                                    //i0100_nnnn_0000_1011();
                                    n = (opcode >> 8) & 0x0F;
                                    //pr = pc + 4;
                                    //delayslot = r[n];
                                    //pc_funct = 2;//jump with delay
                                    to.il.Emit(OpCodes.Ldc_I4, pc + 4);   //load the value that we must return  
                                    stloc(l_pri, to.il);   //store it to pr
                                    ldloc(l_rbase + n, to.il);//load jump address                                    
                                    stloc(l_pci, to.il);     //store it to the pc register
                                    uint temp = RecSingle(RecNextOpcode(), to);// do the delayslot instruction
                                    temp+=ccount[opcode];
                                    RecBlockEnd = true;
                                    return temp;
                                    //break;
                                case 0x1://0100_xxxx_0001_1011
                                    //i0100_nnnn_0001_1011();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0100_xxxx_0010_1011
                                    //i0100_nnnn_0010_1011();
                                    n = (opcode >> 8) & 0x0F;
                                    //delayslot = r[n];
                                    //pc_funct = 2;//jump with delay 1
                                    ldloc(l_rbase + n, to.il);//load jump address                                    
                                    stloc(l_pci, to.il);     //store it to the pc register
                                    uint temp2 = RecSingle(RecNextOpcode(), to);// do the delayslot instruction
                                    temp2 += ccount[opcode];
                                    RecBlockEnd = true;
                                    return temp2;
                                    //break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0xC://1100 rec
                            //i0100_nnnn_mmmm_1100();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xD://1101 rec
                            //i0100_nnnn_mmmm_1101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xE://1110 rec
                            #region 0xE multi
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0100_xxxx_0000_1110
                                    //i0100_nnnn_0000_1110();
                                    break;
                                case 0x1://0100_xxxx_0001_1110
                                    //i0100_nnnn_0001_1110();
                                    break;
                                case 0x2://0100_xxxx_0010_1110
                                    //i0100_nnnn_0010_1110();
                                    break;
                                case 0x3://0100_xxxx_0011_1110
                                    //i0100_nnnn_0011_1110();
                                    break;
                                case 0x4://0100_xxxx_0100_1110
                                    //i0100_nnnn_0100_1110();
                                    break;
                                case 0x8://0100_xxxx_1000_1110
                                    //i0100_nnnn_1000_1110();
                                    break;
                                case 0x9://0100_xxxx_1001_1110
                                    //i0100_nnnn_1001_1110();
                                    break;
                                case 0xA://0100_xxxx_1010_1110
                                    //i0100_nnnn_1010_1110();
                                    break;
                                case 0xB://0100_xxxx_1011_1110
                                    //i0100_nnnn_1011_1110();
                                    break;
                                case 0xC://0100_xxxx_1100_1110
                                    //i0100_nnnn_1100_1110();
                                    break;
                                case 0xD://0100_xxxx_1101_1110
                                    //i0100_nnnn_1101_1110();
                                    break;
                                case 0xE://0100_xxxx_1110_1110
                                    //i0100_nnnn_1110_1110();
                                    break;
                                case 0xF://0100_xxxx_1111_1110
                                    //i0100_nnnn_1111_1110();
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0xF://1111
                            //i0100_nnnn_mmmm_1111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        default:
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
                case 0x5://finished rec
                    //i0101_nnnn_mmmm_iiii();
                    n = (opcode >> 8) & 0x0F;
                    m = (opcode >> 4) & 0x0F;
                    disp = (opcode & 0x0F) * 4;
                    //r[n] = read(r[m] + disp, 4); : do this is msil
                    ldloc(l_rbase + m, to.il); //load register m
                    to.il.Emit(OpCodes.Ldc_I4, disp);       //load disp
                    to.il.Emit(OpCodes.Add);                //add them
                    to.il.Emit(OpCodes.Ldc_I4_4);           //push 4 (param #2)
                    to.il.Emit(OpCodes.Call, readmeth);//call read
                    stloc(l_rbase + n, to.il); //store result onto register n
                    return ccount[opcode];
                    //break;
                case 0x6://finished
                    #region case 0x6
                    switch (opcode & 0xf)
                    {
                        case 0x0://0000
                            //i0110_nnnn_mmmm_0000();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] = (uint)(sbyte)read(r[m], 1);
                            ldloc(l_rbase + m, to.il); //add to read
                            to.il.Emit(OpCodes.Ldc_I4_1);           //1
                            to.il.Emit(OpCodes.Call,readmeth);      //call read
                            to.il.Emit(OpCodes.Conv_I1);            //make it sbyte extended
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0x1://0001
                            //i0110_nnnn_mmmm_0001();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x2://0010
                            //i0110_nnnn_mmmm_0010();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] = read(r[m], 4);
                            ldloc(l_rbase + m, to.il); //add to read
                            to.il.Emit(OpCodes.Ldc_I4_4);           //4
                            to.il.Emit(OpCodes.Call, readmeth);     //call read
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0x3://0011
                            //i0110_nnnn_mmmm_0011();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] = r[m];
                            ldloc(l_rbase + m, to.il); //load source register
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0x4://0100
                            //i0110_nnnn_mmmm_0100();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x5://0101
                            //i0110_nnnn_mmmm_0101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x6://0110
                            //i0110_nnnn_mmmm_0110();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;

                            //r[n] = read(r[m], 4);
                            ldloc(l_rbase + m, to.il); //add to read
                            to.il.Emit(OpCodes.Ldc_I4_4);           //4
                            to.il.Emit(OpCodes.Call, readmeth);      //call read
                            stloc(l_rbase + n, to.il); //and store to dest register
                            if (n != m)//if diferent reg then add 4 ;)
                            {
                                //r[m] += 4;
                                ldloc(l_rbase + m, to.il); //load reg
                                to.il.Emit(OpCodes.Ldc_I4_4);           //param #2 (4)
                                to.il.Emit(OpCodes.Add);                //add them
                                stloc(l_rbase + m, to.il); //and store it back
                            }
                            return ccount[opcode];//return
                            //break;
                        case 0x7://0111
                            //i0110_nnnn_mmmm_0111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x8://1000
                            //i0110_nnnn_mmmm_1000();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x9://1001
                            //i0110_nnnn_mmmm_1001();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] = ((r[m] >> 16) & 0xFFFF) | ((r[m] << 16) & 0xFFFF0000);
                            ldloc(l_rbase + m, to.il); //load m 
                            to.il.Emit(OpCodes.Ldc_I4,16);          //16
                            to.il.Emit(OpCodes.Shr_Un);             //>>16
                            to.il.Emit(OpCodes.Ldc_I4, 0xFFFF);     //0xFFFF
                            to.il.Emit(OpCodes.And);                //and 0xFFFF
                            //par 1 finished
                            ldloc(l_rbase + n, to.il); //load n 
                            to.il.Emit(OpCodes.Ldc_I4, 16);         //16
                            to.il.Emit(OpCodes.Shl);                //<<16
                            to.il.Emit(OpCodes.Ldc_I4, 0xFFFF0000); //0xFFFF0000
                            to.il.Emit(OpCodes.And);                //and 0xFFFF0000
                            //par 2 finished
                            //now or them
                            to.il.Emit(OpCodes.Or);
                            //and finaly store it
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0xA://1010
                            //i0110_nnnn_mmmm_1010();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xB://1011
                            //i0110_nnnn_mmmm_1011();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //rm = (int)r[m];
                            //r[n] = (uint)-rm;
                            to.il.Emit(OpCodes.Ldc_I4_0);           //load 0
                            ldloc(l_rbase + m, to.il); //load m register
                            to.il.Emit(OpCodes.Sub);                //sub them 
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0xC://1100
                            //i0110_nnnn_mmmm_1100();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] = r[m] & 0xFF;
                            ldloc(l_rbase + m, to.il); //load m register
                            to.il.Emit(OpCodes.Ldc_I4, 0xFF);       //load 0xFF
                            to.il.Emit(OpCodes.And);                //and them 
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0xD://1101
                            //i0110_nnnn_mmmm_1101();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //r[n] = r[m] & 0x0000FFFF;
                            ldloc(l_rbase + m, to.il); //load m register
                            to.il.Emit(OpCodes.Ldc_I4, 0x0000FFFF); //load 0x0000FFFF
                            to.il.Emit(OpCodes.And);                //and them 
                            stloc(l_rbase + n, to.il); //and store to dest register
                            return ccount[opcode];//return
                            //break;
                        case 0xE://1110
                            //i0110_nnnn_mmmm_1110();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xF://1111
                            //i0110_nnnn_mmmm_1111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        default:
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
                case 0x7://finished rec
                    //i0111_nnnn_iiii_iiii();
                    n = (opcode >> 8) & 0x0F;
                    rm = (int)(sbyte)(opcode & 0xFF);
                    //rn = ((int)r[n]) + rm;  :Do this in msil
                    //r[n] = (uint)rn;
                    ldloc(l_rbase + n, to.il);//load reg
                    to.il.Emit(OpCodes.Ldc_I4, rm); //load value to add (singed)
                    to.il.Emit(OpCodes.Add);        // add them 
                    stloc(l_rbase + n, to.il);//save the result back
                    return ccount[opcode];
                    //break;
                case 0x8://finished
                    #region case 0x8
                    switch ((opcode >> 8) & 0xf)
                    {
                        case 0x0://0000
                            //i1000_0000_mmmm_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x1://0001
                            //i1000_0001_mmmm_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x4://0100
                            //i1000_0100_mmmm_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x5://0101
                            //i1000_0101_mmmm_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x8://1000
                            //i1000_1000_iiii_iiii();
                            m = (uint)(sbyte)(opcode & 0xFF);
                            //if (r[0] == m)
                            //    sr.T = 1;
                            //else
                            //    sr.T = 0;
                            ldloc(l_rbase,to.il);//load r[0]
                            to.il.Emit(OpCodes.Ldc_I4,m);//load m
                            SetTeq(to.il, 1, 0);// if true then t=1 else t=0
                            return ccount[opcode];//return
                            //break;
                        case 0x9://1001
                            //i1000_1001_iiii_iiii();
                            //if (sr.T == 1)
                            //please note that the params are pushed "backwards"
                            //else set pc to this
                            to.il.Emit(OpCodes.Ldc_I4, pc+2);
                            //set pc to this
                            to.il.Emit(OpCodes.Ldc_I4, (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4);
                            to.il.Emit(OpCodes.Ldc_I4_1);    //is 1 then     
                            GetTBit(to.il);                     //if this
                            //do all the above
                            SetPCeq(to.il);
                            RecBlockEnd = true;
                            return ccount[opcode];//return
                            //break;
                        case 0xB://1011
                            //i1000_1011_iiii_iiii();
                            //if (sr.T == 0)
                            locarg(l_sri, to.il);
                            locarg(l_pci, to.il);
                            Label t1 = to.il.DefineLabel();
                            uint ccnt_old = BlockCycles;
                            BlockCycles += ccount[opcode];
                            uint _t_fut_pc1 = (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4;
                            if ( br_8_b_level < br_8_b_level_max)
                            {//ok we are not in the max inlining level , we can inline this one 
                                //do the delay slot
                                uint oldpc = pc;//save old pc
                                GetTBit(to.il);//get tbit
                                uint pcres = pc;
                                Label nojump = to.il.DefineLabel();
                                //inline code
                                pc =  _t_fut_pc1- 2;//-2 to fixup the next opcode read
                                //if t==0 then the jump is executed
                                to.il.Emit(OpCodes.Brtrue, nojump);//if it is <> than 0 then skip the jump code
                                br_8_b_level++;

                                //make up code that will lead pc to the next jump/br
                                //inline code until we reach a jump/br that must stop the block
                                while (RecBlockEnd == false)
                                {
                                    BlockCycles += RecSingle(RecNextOpcode(), to);
                                }
                                //to.il.Emit(OpCodes.Ldc_I4, (uint)((sbyte)(opcode & 0xFF)) * 2 + oldpc + 4);
                                //stloc (l_ pci);

                                //resume code
                                to.FinaliseIl();//ok we finalise this br case
                                to.il.MarkLabel(nojump);//if we did not did the jump then we will end up here directly
                                //RecBlockEnd = false;
                                pc = pcres;//resume recompilation of next opcodes
                                br_8_b_level--;
                                //RecBlockEnd = true;
                                BlockLen += BlockCycles - ccnt_old;
                                BlockCycles = ccnt_old;//restore block cycles
                            }
                            else//..uff to much inlinng , time to stop it
                            {

                                //if not jump is made
                                to.il.Emit(OpCodes.Ldc_I4, pc+2);
                                //if jump is made
                                to.il.Emit(OpCodes.Ldc_I4, (uint)((sbyte)(opcode & 0xFF)) * 2 + 4 + pc);
                                //if t=0 them do jump else do not
                                to.il.Emit(OpCodes.Ldc_I4_0);       //we want to be 0
                                GetTBit(to.il);                     //load tbit
                                rec_special_pc= (uint)((sbyte)(opcode & 0xFF)) * 2 + 4 + pc;
                                SetPCeq(to.il);// do all the above

                                RecBlockEnd = true;
                            }

                            return ccount[opcode]; ;//return
                            //break;
                        case 0xD://1101 inliner
                            //i1000_1101_iiii_iiii();
                            delayslot = (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4; // antes era disp = ...
                            //to.il.Emit(OpCodes.Ldc_I4, (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4);
                            //pc_funct = 2;//delay 1
                            //stloc (l_ pci);//store delayslot to pc register
                            uint temp = RecSingle(RecNextOpcode(), to);// do the delayslot instruction
                            temp += ccount[opcode];
                            //RecBlockEnd = true;
                            pc = delayslot-2;//since no conditional jump , we can inline it :)
                            return temp;//return
                            //break;
                        case 0xF://1111 inliner short
                            //i1000_1111_iiii_iiii();
                            //if (sr.T == 0)
                            //note that the params are from last to first
                            locarg(l_sri, to.il);
                            locarg(l_pci, to.il);
                            uint ccnt_o = BlockCycles;
                            BlockCycles+=ccount[opcode];
                            uint tc=0;
                            if (br_8_f_level < br_8_f_level_max)
                            {//we will inline all this
                                //do the delay slot
                                uint oldpc = pc;//save old pc
                                GetTBit(to.il);//get tbit
                                BlockCycles += RecSingle(RecNextOpcode(), to);
                                uint pcres = pc;
                                Label nojump = to.il.DefineLabel();
                                //inline code
                                pc = (uint)((sbyte)(opcode & 0xFF)) * 2 + oldpc + 4 - 2;//-2 to fixup the next opcode read
                                //if t==0 then the jump is executed
                                to.il.Emit(OpCodes.Brtrue, nojump);//if it is <> than 0 then skip the jump code
                                br_8_f_level++;
                                
                                //make up code that will lead pc to the next jump/br
                                //inline code until we reach a jump/br that must stop the block
                                while (RecBlockEnd == false)
                                {
                                    BlockCycles += RecSingle(RecNextOpcode(), to);
                                }
                                //to.il.Emit(OpCodes.Ldc_I4, (uint)((sbyte)(opcode & 0xFF)) * 2 + oldpc + 4);
                                //stloc (l_ pci);

                                //resume code
                                to.FinaliseIl();//ok we finalise this br case
                                to.il.MarkLabel(nojump);//if we did not did the jump then we will end up here directly
                                //RecBlockEnd = false;
                                pc = pcres;//resume recompilation of next opcodes
                                br_8_f_level--;
                                //RecBlockEnd = true;
                                BlockLen += BlockCycles - ccnt_o;
                                BlockCycles = ccnt_o;
                            }
                            else
                            {
                                //else pc = this..
                                to.il.Emit(OpCodes.Ldc_I4, pc+4);

                                //  delayslot = (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4;
                                //pc= this
                                to.il.Emit(OpCodes.Ldc_I4, (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4);
                                rec_special_pc = (uint)((sbyte)(opcode & 0xFF)) * 2 + pc + 4;
                                to.il.Emit(OpCodes.Ldc_I4_0);//is 0 then 

                                GetTBit(to.il);                  //if this 

                                SetPCeq(to.il);// do all the above
                                //  pc_funct = 2;//delay 1 instruction
                                // do the delayslot instruction
                                tc = RecSingle(RecNextOpcode(), to);
                                RecBlockEnd = true;
                            }
                            return tc+ccount[opcode];//return
                            //break;
                        default:
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
                case 0x9://finished rec
                    //i1001_nnnn_iiii_iiii();
                    n = (opcode >> 8) & 0x0F;
                    disp = (opcode & 0x00FF);
                    //r[n] = (uint)(short)read(disp * 2 + pc + 4, 2); do it in msil
                    to.il.Emit(OpCodes.Ldc_I4, 4 + disp * 2+pc);   //load the value addr 
                    to.il.Emit(OpCodes.Ldc_I4_2);               //2 bytes read (param #2)
                    to.il.Emit(OpCodes.Call, readmeth);//call read
                    to.il.Emit(OpCodes.Conv_I2);                //convert to int16s
                    stloc(l_rbase + n, to.il);     //store the result to n
                    return ccount[opcode];
                    //break;
                case 0xA://finished rec inliner
                    //i1010_iiii_iiii_iiii();
                    delayslot = (uint)(((short)((opcode & 0x0FFF) << 4)) >> 3) + pc + 4;//(short<<4,>>4(-1*2))
                    //pc_funct = 2;//jump delay 1
                    //to.il.Emit(OpCodes.Ldc_I4, delayslot);   //load the value that we must jump
                    //stloc (l_ pci);     //store the result to pc register
                    uint tmp=RecSingle(RecNextOpcode(), to);// do the delayslot instruction
                    //RecBlockEnd = true;
                    pc = delayslot-2; //since this is not a conditional jump , we can inline it ;)
                    return (uint)(tmp + ccount[opcode]);
                    //break;
                case 0xB://finished rec
                    //i1011_iiii_iiii_iiii();
                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                    break;
                case 0xC://finished rec
                    #region case 0xC
                    switch ((opcode >> 8) & 0xf)
                    {
                        case 0x0://0000
                            //i1100_0000_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x1://0001
                            //i1100_0001_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x2://0010
                            //i1100_0010_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x3://0011
                            //i1100_0011_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x4://0100
                            //i1100_0100_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x5://0101
                            //i1100_0101_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x6://0110
                            //i1100_0110_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x7://0111
                            //i1100_0111_iiii_iiii();
                            disp = (opcode & 0x00FF) * 4 + ((pc + 4) & 0xFFFFFFFC);
                            //r[0] = disp;
                            to.il.Emit(OpCodes.Ldc_I4, disp);   //load the value that we calculated
                            stloc(l_rbase,  to.il);     //store the result to r0 register
                            return ccount[opcode];
                            //break;
                        case 0x8://1000
                            //i1100_1000_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x9://1001
                            //i1100_1001_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xA://1010
                            //i1100_1010_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xB://1011
                            //i1100_1011_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xC://1100
                            //i1100_1100_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xD://1101
                            //i1100_1101_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xE://1110
                            //i1100_1110_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xF://1111
                            //i1100_1111_iiii_iiii();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        default:
                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
                case 0xD://finished rec
                    //i1101_nnnn_iiii_iiii();
                    n = (opcode >> 8) & 0x0F;
                    disp = (opcode & 0xFF) << 2;
                    //r[n] = read(disp + (pc & 0xFFFFFFFC) + 4, 4);
                    to.il.Emit(OpCodes.Ldc_I4, disp + (pc & 0xFFFFFFFC) + 4);//param #1
                    to.il.Emit(OpCodes.Ldc_I4_4);//param #2
                    to.il.Emit(OpCodes.Call, readmeth);// call readmem
                    stloc(l_rbase + n,  to.il);     //store the result to rn register
                    return ccount[opcode];
                    //break;
                case 0xE://finished rec
                    //i1110_nnnn_iiii_iiii();
                    n = (opcode >> 8) & 0x0F;
                    //r[n] = (uint)(sbyte)(opcode & 0xFF);//(uint)(sbyte)= signextend8 :)
                    to.il.Emit(OpCodes.Ldc_I4_S, opcode & 0xFF);//store it
                    to.il.Emit(OpCodes.Conv_I1);//convert it to s8
                    stloc(l_rbase + n, to.il);     //store the result to rn register
                    return ccount[opcode];
                    //break;
                case 0xF://finished - fix for fsca
                    #region case 0xf
                    switch (opcode & 0xf)
                    {
                        case 0x0://0000
                            //i1111_nnnn_mmmm_0000();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //fr[n] += fr[m];
                            ldloc(l_fr_base + n, to.il);
                            ldloc(l_fr_base + m, to.il);
                            to.il.Emit(OpCodes.Add);
                            stloc(l_fr_base + n, to.il);
                            return ccount[opcode];
                            //break;
                        case 0x1://0001
                            //i1111_nnnn_mmmm_0001();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x2://0010
                            //i1111_nnnn_mmmm_0010();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //fr[n] *= fr[m];
                            ldloc(l_fr_base + n, to.il);
                            ldloc(l_fr_base + m, to.il);
                            to.il.Emit(OpCodes.Mul);
                            stloc(l_fr_base + n, to.il);
                            return ccount[opcode];
                            //break;
                        case 0x3://0011
                            //i1111_nnnn_mmmm_0011();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //fr[n] /= fr[m];
                            ldloc(l_fr_base + n, to.il);
                            ldloc(l_fr_base + m, to.il);
                            to.il.Emit(OpCodes.Div);
                            stloc(l_fr_base + n, to.il);
                            return ccount[opcode];
                            //break;
                        case 0x4://0100
                            //i1111_nnnn_mmmm_0100();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x5://0101
                            //i1111_nnnn_mmmm_0101();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x6://0110
                            //i1111_nnnn_mmmm_0110();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x7://0111
                            //i1111_nnnn_mmmm_0111();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0x8://1000
                            //i1111_nnnn_mmmm_1000();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //uint ttmp = read(r[m], 4);
                            ldloc(l_rbase + m, to.il);//load reg
                            to.il.Emit(OpCodes.Ldc_I4_4 );         //load 4
                            to.il.Emit(OpCodes.Call,readmeth);     //call read
                            //fr[n] = *(float*)&ttmp;
                            u2fS(to.il);                    //copy to to float 
                            stloc(l_fr_base + n, to.il);
                            return ccount[opcode];
                            //break;
                        case 0x9://1001
                            //i1111_nnnn_mmmm_1001();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xA://1010
                            //i1111_nnnn_mmmm_1010();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            
                            //param #1
                            ldloc(l_rbase + n, to.il);//address

                            //fixed (float* p = &fr[m]) { write(r[n], *(uint*)p, 4); }
                            f2uL(l_fr_base + m, to.il);            //copy to to uint 
                            to.il.Emit(OpCodes.Ldc_I4_4);         //load 4
                            to.il.Emit(OpCodes.Call, writemeth);     //call write

                            return ccount[opcode];
                            //break;
                        case 0xB://1011
                            //i1111_nnnn_mmmm_1011();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        case 0xC://1100
                            //i1111_nnnn_mmmm_1100();
                            n = (opcode >> 8) & 0x0F;
                            m = (opcode >> 4) & 0x0F;
                            //fr[n] = fr[m];
                            ldloc(l_fr_base + m, to.il);
                            stloc(l_fr_base + n, to.il);
                            return ccount[opcode];
                            //break;
                        case 0xD://1101
                            #region 0xD multi
                            switch ((opcode >> 4) & 0xf)
                            {
                                case 0x0://0000
                                    //i1111_nnnn_0000_1101(); 
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x1://0001
                                    //i1111_nnnn_0001_1101(); 
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x2://0010
                                    //i1111_nnnn_0010_1101(); 
                                    n = (opcode >> 8) & 0x0F;
                                    //fr[n] = (float)(int)fpul;
                                    ldloc(l_fpuli, to.il);
                                    to.il.Emit(OpCodes.Conv_R4);
                                    stloc(l_fr_base + n, to.il);
                                    return ccount[opcode];
                                    //break;
                                case 0x3://0011
                                    //i1111_nnnn_0011_1101(); 
                                    m = (opcode >> 8) & 0x0F;
                                    //fpul = (uint)(int)fr[m];
                                    ldloc(l_fr_base + m, to.il);
                                    to.il.Emit(OpCodes.Conv_I4);
                                    stloc(l_fpuli, to.il);
                                    return ccount[opcode];
                                    //break;
                                case 0x4://0100
                                    //i1111_nnnn_0100_1101(); 
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x5://0101
                                    //i1111_nnnn_0101_1101();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x6://0110
                                    //i1111_nnnn_0110_1101();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x8://1000
                                    //i1111_nnnn_1000_1101(); 
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0x9://1001
                                    //i1111_nnnn_1001_1101();
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xA://1010
                                    //i1111_nnnn_1010_1101(); 
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xB://1011
                                    //i1111_nnnn_1011_1101(); 
                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                    break;
                                case 0xF://1111_xxxx_1111_1101
                                    #region 0xf multi
                                    //we have :
                                    //1111_nnn0_1111_1101
                                    //1111_nn01_1111_1101
                                    //1111_1011_1111_1101
                                    //1111_0011_1111_1101
                                    switch ((opcode >> 8) & 0x1)
                                    {
                                        case 0x0://1111_nnn0_1111_1101 - fsca DC special
                                            //i1111_nnn0_1111_1101();
                                            n = (opcode >> 9) & 0x07;
                                            //float x = (float)(2 * pi * (float)fpul / 65536.0);
                                            //fr[n * 2] = (float)System.Math.Sin(x);
                                            //fr[n * 2 + 1] = (float)System.Math.Cos(x);

                                            to.il.Emit(OpCodes.Ldc_R4, (float)(2 * pi));
                                            ldloc(l_fpuli, to.il);
                                            to.il.Emit(OpCodes.Conv_R4);
                                            to.il.Emit(OpCodes.Mul);
                                            to.il.Emit(OpCodes.Ldc_R4, (float)65536.0);
                                            to.il.Emit(OpCodes.Div);
                                            to.il.Emit(OpCodes.Dup);

                                            to.il.Emit(OpCodes.Call ,Sinmeth);
                                            stloc(l_fr_base + n * 2, to.il);

                                            to.il.Emit(OpCodes.Call, Cosmeth);
                                            stloc(l_fr_base + n * 2 + 1, to.il);

                                            return ccount[opcode];
                                            //break;
                                        case 0x1://1111_xxy1_1111_1101
                                            //if (opcode==0xfffd) {dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();break;}//1111_x111_1111_1101- invalid
                                            //1111_nn01_1111_1101
                                            //1111_1011_1111_1101
                                            //1111_0011_1111_1101
                                            if (((opcode >> 9) & 0x1) == 0)//1111_xxy1_1111_1101
                                            {
                                                dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                                break;//i1111_nn01_1111_1101();
                                            }
                                            else//1111_yy11_1111_1101
                                            {
                                                if (((opcode >> 10) & 0x3) == 0)//1111_yy11_1111_1101
                                                {
                                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                                    break;//i1111_0011_1111_1101();
                                                }
                                                else if (((opcode >> 10) & 0x3) == 2)//1111_yy11_1111_1101
                                                {
                                                    dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                                                    break;//i1111_1011_1111_1101();
                                                }
                                            }
                                            //1111_x111_1111_1101- invalid
                                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                            break;
                                        default:
                                            dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                            break;
                                    }
                                    #endregion
                                    break;
                                default:
                                    dc.dcon.WriteLine("Warning:Invalid opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper()); System.Windows.Forms.Application.DoEvents();
                                    break;
                            }
                            #endregion
                            break;
                        case 0xE://1110
                            //i1111_nnnn_mmmm_1110();
                            dc.dcon.WriteLine("Warning:Inimplemented opcode at pc " + System.Convert.ToString(pc, 16).ToUpper() + " with code (" + Convert.ToString(opcode, 16).ToUpper() + ") " + Convert.ToString(opcode, 2).ToUpper()); System.Windows.Forms.Application.DoEvents();

                            break;
                        default:
                            //dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
                            //System.Windows.Forms.Application.DoEvents();
                            break;
                    }
                    #endregion
                    break;
            }
            return 0;
            #endregion
        }

        //Memory copys from uint to float and reverse , L is for local , S is for Stack
        //local ones
        public static void f2uL(uint n,ILGenerator il)
        {
            ldloca((uint)n, il);
            il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer , push *(uint*)pointer
        }
        public static void u2fL(uint n,ILGenerator il)
        {
            ldloca(n, il);
            il.Emit(OpCodes.Ldobj, typeof(float));//pop pointer , push *(float*)pointer
        }
        //stack ones
        public static void f2uS(ILGenerator il)
        {
#if nrt
            il.Emit(OpCodes.Stloc_S ,tflt);      //pop value
#else
            il.Emit(OpCodes.Stloc, tflt);      //pop value
#endif
            ldloca(tflt, il);                    //push pointer 
            il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer , push *(uint*)pointer
        }
        public static void u2fS(ILGenerator il)
        {
#if nrt
            il.Emit(OpCodes.Stloc_S, tuint);      //pop value
#else
            il.Emit(OpCodes.Stloc, tuint);      //pop value
#endif
            ldloca(tuint, il);                    //push pointer 
            il.Emit(OpCodes.Ldobj, typeof(float));//pop pointer , push *(float*)pointer
        }

        //debug text out from recompiler
        public static void DoutB(uint Value)
        {
            dc.dcon.WriteLine("Before " + Value.ToString());
        }
        public static void DoutA(uint Value)
        {
            dc.dcon.WriteLine("After " + Value.ToString());
        }
        //helper functions to use for the recompiler
        public static uint RecNextOpcode()
        {
            pc += 2;//goto next opcode
            return read(pc, 2);//return it
        }
        public static uint RecExecuteBlock()
        {
            int bid=FindBlock(pc);
            if (bid >= 0)
            {//found , call it then ;)
                ch++;
                DynaCache[bid].nocs++;
                pc = DynaCache[bid].BufferEnd+2;
                if (r[2] < 1000)// a nice hack for roto and 256b ..
                {
                   // r[0] = 2768240648;
                   // r[1] = (uint)rnd.Next((int)0x5FFFFFFF);
                   // r[2] = 153598;
                }
                return DynaCache[bid].CodeBuffer( r+0,  r+1,  r+2,  r+3,  r+4,  r+5,  r+6,
                                                 r+7,  r+8,  r+9,  r+10,  r+11,  r+12,
                                                 r+13,  r+14,  r+15,  r_bank+0,  r_bank+1,  r_bank+2,
                                                 r_bank+3,  r_bank+4,  r_bank+5,  r_bank+6,  r_bank+7,
                                                 fr+0,  fr+1,  fr+2,  fr+3,  fr+4,  fr+5,  fr+6,
                                                 fr+7,  fr+8,  fr+9,  fr+10,  fr+11,  fr+12,
                                                 fr+13,  fr+14,  fr+15,  xr+0,  xr+1,  xr+2,
                                                 xr+3,  xr+4,  xr+5,  xr+6,  xr+7,  xr+8,  xr+9,  xr+10,
                                                 xr+11,  xr+12,  xr+13,  xr+14,  xr+15,ref gbr,
                                                 ref ssr, ref spc, ref sgr, ref dbr, ref vbr, ref mach, ref  macl,
                                                 ref pr, ref fpul, ref pc, ref sr, ref  fpscr);
                //block executed , pc and registers are updated
            }
            //dc.dcon.WriteLine("Cache miss");
            //doh .. not found , we must make this block....
            cm++;
            //uint ccnt = 0;
            BlockCycles = 0;//reset block cycle count
            uint baseA=pc;
            bid =(int) FindEmpty();//find next empty block
            //generate msil code
            RecBlockEnd = false;
            pc -= 2;
            IlCoder cc = new IlCoder(CBind++);
            //uint _t_clk_t = 0;
            rec_special_pc = 0;
            lbl_short_exit = cc.il.DefineLabel();
            while (!RecBlockEnd)
            {
                recAddLabel(cc.il);
                BlockCycles += RecSingle(RecNextOpcode(), cc);
               //recIltclkMark(_t_clk_t, cc.il);
            }
            if (rec_special_pc != 0 && recIsOnCurBlock(rec_special_pc))
            {
                recLabelJmp(cc.il, rec_special_pc);
            }
            //if (br_8_b_level != 0) System.Windows.Forms.MessageBox.Show("Error , br 8b level !=0");
            BlockLen += BlockCycles;
            cc.il.MarkLabel(lbl_short_exit);
            cc.FinaliseIl();//finalise the msil stream
            for (int i = 0; i < pCount; i++)
            {
                bEdited[i] = false;
                bLoaded[i] = false;
            }
            recClearLabel();
                                                
            //DynaCache[bid].CodeBuffer.Method.GetMethodBody().GetILAsByteArray().Length
            DynaCache[bid] = new CodeCacheEntry(cc.GetCodeBuffer(), baseA, pc, BlockLen);
            
            dc.dcon.WriteLine("Added opcode to cache #"  + bid.ToString()
                             + " from address " + Convert.ToString(baseA, 16) + " Length "
                             + Convert.ToString(BlockLen, 10) + " end " + Convert.ToString(pc, 16));
            BlockLen = 0;
            DynaCache[bid].nocs++;
            return DynaCache[bid].CodeBuffer( r+0,  r+1,  r+2,  r+3,  r+4,  r+5,  r+6,
                                       r+7,  r+8,  r+9,  r+10,  r+11,  r+12,
                                       r+13,  r+14,  r+15,  r_bank+0,  r_bank+1,  r_bank+2,
                                       r_bank+3,  r_bank+4,  r_bank+5,  r_bank+6,  r_bank+7,
                                       fr+0,  fr+1,  fr+2,  fr+3,  fr+4,  fr+5,  fr+6,
                                       fr+7,  fr+8,  fr+9,  fr+10,  fr+11,  fr+12,
                                       fr+13,  fr+14,  fr+15,  xr+0,  xr+1,  xr+2,
                                       xr+3,  xr+4,  xr+5,  xr+6,  xr+7,  xr+8,  xr+9,  xr+10,
                                       xr+11,  xr+12,  xr+13,  xr+14,  xr+15,  ref gbr,
                                       ref ssr, ref spc, ref sgr, ref dbr, ref vbr, ref mach, ref macl,
                                       ref pr, ref fpul, ref pc, ref sr, ref fpscr);
            //block executed , pc and registers are updated
        }

        public static void recIltclkMark(uint cycles, ILGenerator il)
        {
            il.Emit(OpCodes.Ldc_I4, cycles);                //load the value to add
            il.Emit(OpCodes.Dup);                           //duplicate it
            il.Emit(OpCodes.Ldloc_S, tclk);                 //load on stack the cur cycle count
            il.Emit(OpCodes.Add);                           //add them
            il.Emit(OpCodes.Stloc_S, tclk);                 //save back the result
                                                            //now , the stack has the duplicated entry
            il.EmitCall(OpCodes.Call, UpdateMeth, null);    //call the update function
            il.Emit(OpCodes.Pop);
            //il.Emit(OpCodes.Brtrue, lbl_short_exit);        //exit if needed from the block ...
        }
        //Find block that starts at baseAddress
        static int FindBlock(uint baseAddress)
        {
            for (int i = 0; i < RecMB; i++)
            {
                if (DynaCache[i].BaseAddress == baseAddress)
                    return i;
            }
            return -1;
        }
        //find next empty block , if there is no empty block , then least frequelnty used
        static uint FindEmpty()
        {
            uint lowest = 0xFFFFFFFF, lowid = 0xFFFFFFFF;
            for (uint i = 0; i < RecMB; i++)
            {
                if (DynaCache[i].BaseAddress == 0)
                    return i;
                if ((DynaCache[i].CTime-((uint)System.DateTime.Now.Ticks >> 2))>4)
                {//
                    if ((DynaCache[i].CTime - ((uint)System.DateTime.Now.Ticks >> 2)) < lowest)
                    {
                        lowid = i; lowest = (DynaCache[i].CTime - ((uint)System.DateTime.Now.Ticks >> 2));
                    }
                }
                else
                {// this block exist for too short time.. we can't realy say how frequetly it is used
                //    if (DynaCache[i].nocs < lowest)
                //    {
                //        lowid = i; lowest = DynaCache[i].nocs;
                //    }
                }
            }
            return lowid;
            //return 0;
        }

        //for future optimizations -  code generation emit warpers

        //to load the register from argument to local - not completed
        static void locarg(uint id, ILGenerator il)
        {/*
            if ((bLoaded[id] == false) && (id < pCount))
            {
                il.Emit(OpCodes.Ldarg_S, id);//load the pointer on the stack
                if ((id >= l_fr_base) && (id < l_gbri))
                    il.Emit(OpCodes.Ldobj, typeof(float));//pop pointer,push *pointer,float
                else
                    il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer,push *pointer,uint

                il.Emit(OpCodes.Stloc_S, id);
                bLoaded[id] = true;
            }
        */
        }
        //to load the loc reg to stack , if it is not loaded on local , load it from argument first- not completed
        static void ldloc(uint id, ILGenerator il)
        {
            /*if ((bLoaded[id] == false)&& (id<pCount))
            {
                il.Emit(OpCodes.Ldarg_S, id);//load the pointer on the stack
                if ((id>=l_fr_base ) && (id<l_gbri ))
                    il.Emit(OpCodes.Ldobj, typeof(float));//pop pointer,push *pointer,float
                else
                    il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer,push *pointer,uint

                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Stloc_S, id);
                bLoaded[id] = true;
            }
            else
            {*/
            il.Emit(OpCodes.Ldloc_S, id);
            //}
        }
        //the same above but for register address- not completed
        static void ldloca(uint id, ILGenerator il)
        {
            /*if ((id<pCount)&&(bLoaded[id] == false) )
            {
                il.Emit(OpCodes.Ldarg_S, id);//load the pointer on the stack
                if ((id >= l_fr_base) && (id < l_gbri))
                    il.Emit(OpCodes.Ldobj, typeof(float));//pop pointer,push *pointer,float
                else
                    il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer,push *pointer,uint

                il.Emit(OpCodes.Stloc_S, id);
                il.Emit(OpCodes.Ldloca_S, id);
                bLoaded[id] = true;
            }
            else
            {*/
            il.Emit(OpCodes.Ldloca_S, id);
            //}
        }
        //for store olny edited regs optimization -  completed
        static void stloc(uint id, ILGenerator il)
        {
            if (id < pCount)
                bEdited[id] = true;
#if nrt
            il.Emit(OpCodes.Stloc_S, id);
#else
            il.Emit(OpCodes.Stloc, id);
#endif
        }

        //Code generation helper functions

        /// <summary>
        /// Changes r bank - no check
        /// stack op - none
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        static void r_change(ILGenerator il)
        {

        }
        /// <summary>
        /// changes fr/xr bank - no check
        /// stack op - none
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        static void fr_change(ILGenerator il)
        {

        }
        /// <summary>
        /// sets Sr and fires any events needed - stack op - none 
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        static void SetSr(ILGenerator il)
        {

        }
        /// <summary>
        /// sets Fpscr and fires any events needed - stack op - none 
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        static void SetFpscr(ILGenerator il)
        {

        }
        //Set & get for TBit (in SR reg)
        /// <summary>
        /// Set TBit (in SR reg) , fixed cond
        /// stack op - Pop1
        /// </summary>
        /// <param name="il">If true then set T to 1 else to 0</param>
        /// <param name="nval">IlGenerator to output the code</param>
        public static void SetTBit(ILGenerator il, uint nCond)
        {
            if (nCond != 0)
            {
                il.Emit(OpCodes.Ldc_I4, sr_T_bit_set);
                ldloc(l_sri, il);//sr register has t in it
                il.Emit(OpCodes.Or);          //bitwise or to set T in sr
                stloc(l_sri, il);//store the rez back to sr
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, sr_T_bit_reset);
                ldloc(l_sri, il);//sr register has t in it
                il.Emit(OpCodes.And);          //bitwise And to unset T in sr
                stloc(l_sri, il);//store the rez back to sr
            }
        }
        /// <summary>
        /// Gets Tbit from SR and pushes it
        /// stack op - push1
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        public static void GetTBit(ILGenerator il)
        {
            il.Emit(OpCodes.Ldc_I4, sr_T_bit_set);
            //il.Emit(OpCodes.Ldloc_S, l_sri);//sr register has t in it
            ldloc(l_sri, il);
            il.Emit(OpCodes.And);          //bitwise and to get T from sr
            //we return the and result  
        }
        /// <summary>
        /// if t =  val then set t = tt else set t=tf 
        /// stack op - pop1_pop1
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        /// <param name="tt">Value to set if condition is true</param>
        /// <param name="tf">Value to set if condition is false</param>
        public static void SetTeq(ILGenerator il, uint tt, uint tf)
        {
            locarg(l_sri, il);
            Label TT = il.DefineLabel();
            Label fend = il.DefineLabel();
            il.Emit(OpCodes.Beq_S, TT);// if p1= p1 set t=tt
            SetTBit(il, tf);// else set it to TF
            il.Emit(OpCodes.Br_S, fend);//finished , goto to end
            il.MarkLabel(TT);//oh we must set it to TT
            SetTBit(il, tt);//set t to TT
            il.MarkLabel(fend);//function end
        }
        /// <summary>
        /// if t =  val then set t = tt else set t=tf
        /// stack op - pop1_pop1_pop1_pop1
        /// </summary>
        /// <param name="il">IlGenerator to output the code</param>
        public static void SetPCeq(ILGenerator il)
        {
            Label TT = il.DefineLabel();
            Label fend = il.DefineLabel();
            il.Emit(OpCodes.Beq_S, TT);// if p1= p1 set t=tt  
            il.Emit(OpCodes.Pop);
            stloc(l_pci, il);//else set it to TF
            il.Emit(OpCodes.Br_S, fend);//finished , goto to end
            il.MarkLabel(TT);//oh we must set it to TT            
            stloc(l_pci, il);//else set t to TT
            il.Emit(OpCodes.Pop);
            il.MarkLabel(fend);//function end
        }

        struct lbl_col
        {
            public Label lbl;
            public uint pc;
            public lbl_col(Label lbl, uint pc)
            {
                this.lbl = lbl;
                this.pc = pc;
            }
        }
        static List<lbl_col> lbllist = new List<lbl_col>();
        public static bool recIsOnCurBlock(uint Addr)
        {
            int ln = lbllist.Count;
            for (int i = 0; i < ln; i++)
            {
                if (lbllist[i].pc == Addr)
                {
                    return true;
                }
            }
            return false;
        }
        public static void recAddLabel(ILGenerator il)
        {
            Label t_lbl=il.DefineLabel();
            il.MarkLabel(t_lbl);
            lbllist.Add(new lbl_col(t_lbl,pc));
        }
        public static void recClearLabel()
        {
            lbllist.Clear();
        }
        public static void recLabelJmp(ILGenerator il,uint addr)
        {
            int ln = lbllist.Count;
            for (int i = 0; i < ln; i++)
            {
                if (lbllist[i].pc == addr)
                {
                    //il.Emit(OpCodes.Ldloc_S, tdfnl);
                    //il.Emit(OpCodes.Ldc_I4, 1);
                    //il.Emit(OpCodes.Add);
                    //il.Emit(OpCodes.Dup);
                    //il.Emit(OpCodes.Stloc_S, tdfnl);
                    //il.Emit(OpCodes.Dup);
                    Label tlabel = il.DefineLabel();

                    il.Emit(OpCodes.Ldloc_S, l_pci);
                    il.Emit(OpCodes.Ldc_I4, addr);
                    il.Emit(OpCodes.Bne_Un, tlabel);
                    //il.EmitCall(OpCodes.Call, typeof(emu).GetMethod("DoutA"), null);
                    il.Emit(OpCodes.Ldloc_S, tclk);
                    il.Emit(OpCodes.Ldc_I4, 10*mb);//no more that 1 mega cycle per call.. too bad ehh??
                    il.Emit(OpCodes.Blt , lbllist[i].lbl);
                    //il.Emit(OpCodes.Br,);
                    //il.Emit(OpCodes.Ldloc_S, tclk);
                    //il.EmitCall(OpCodes.Call, typeof(emu).GetMethod("DoutA"), null);
                    il.MarkLabel(tlabel);
                    return;
                }
            }
            
        }


    }

    /// <summary>
    /// Class to help the code generation
    /// </summary>
    public class IlCoder
    {
        public ILGenerator il;
        public DynamicMethod si;
        /// <summary>
        /// Creates the IlCoder Class and inits it.
        /// </summary>
        public IlCoder(int id)
        {
            Type[] siArgs=new Type[emu.pCount];
            for (int i = 0; i < emu.l_fr_base; i++)
            {
                siArgs[i] = typeof(uint*);
            }
            for (int i = emu.l_fr_base; i < emu.l_gbri; i++)
            {
                siArgs[i] = typeof(float*);
            }
            for (int i = emu.l_gbri; i < emu.pCount; i++)
            {
                siArgs[i] = typeof(uint).MakeByRefType();
            }
            //= { typeof(int).MakeByRefType(), typeof(int[]) }; 
            #if false 
		            ref uint reg0 ,  ref uint reg1 ,  ref uint reg2 ,  
                                                ref uint reg3 ,  ref uint reg4 ,  ref uint reg5 ,  
                                                ref uint reg6 ,  ref uint reg7 ,  ref uint reg8 ,
                                                ref uint reg9, ref uint reg10, ref uint reg11,
                                                ref uint reg12, ref uint reg13, ref uint reg14,
                                                ref uint reg15, ref uint reg16, ref uint reg17,
                                                ref uint reg18, ref uint reg19, ref uint reg20,
                                                ref uint reg21, ref uint reg22, ref uint reg23,
                                                ref float reg24, ref float reg25, ref float reg26,
                                                ref float reg27, ref float reg28, ref float reg29,
                                                ref float reg30, ref float reg31, ref float reg32,
                                                ref float reg33, ref float reg34, ref float reg35,
                                                ref float reg36, ref float reg37, ref float reg38,
                                                ref float reg39, ref float reg40, ref float reg41,
                                                ref float reg42, ref float reg43, ref float reg44,
                                                ref float reg45, ref float reg46, ref float reg47,
                                                ref float reg48, ref float reg49, ref float reg50,
                                                ref float reg51, ref float reg52, ref float reg53,
                                                ref float reg54, ref float reg55, ref uint reg56,
                                                ref uint reg57, ref uint reg58, ref uint reg59,
                                                ref uint reg60, ref uint reg61, ref uint reg62,
                                                ref uint reg63, ref uint reg64, ref uint reg65,
                                                ref uint reg66, ref uint reg67, ref uint reg68); /**/ 
	        #endif
            
            si = new DynamicMethod("DRM_" + id.ToString()  ,
                typeof(uint) ,
                siArgs,
                typeof(emu).Module);
            
            si.InitLocals = true;
            //si.
            il = si.GetILGenerator();
            
            //define all the params as locals...
            for (int i = 0; i < emu.l_fr_base; i++)
            {
                il.DeclareLocal(typeof(uint));
            }
            for (int i = emu.l_fr_base; i < emu.l_gbri; i++)
            {
                il.DeclareLocal(typeof(float));
            }
            for (int i = emu.l_gbri; i < emu.pCount; i++)
            {
                il.DeclareLocal(typeof(uint));
            }
            il.DeclareLocal(typeof(uint));//temp uint
            il.DeclareLocal(typeof(float));//temp float
            il.DeclareLocal(typeof(uint));//tclk
            il.DeclareLocal(typeof(uint));//tdfnl
            //done automaticaly when needed[not any more...]
            //load the params onto the locals
            for (int i = 0; i < emu.l_fr_base; i++)//
            {
                il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer,push *pointer
                il.Emit(OpCodes.Stloc_S, i);//store it on the local
            }
            for (int i = emu.l_fr_base; i < emu.l_gbri; i++)//
            {
                il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                il.Emit(OpCodes.Ldobj, typeof(float));//pop pointer,push *pointer
                il.Emit(OpCodes.Stloc_S, i);//store it on the local
            }
            for (int i = emu.l_gbri; i < emu.pCount; i++)
            {
                il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                il.Emit(OpCodes.Ldobj, typeof(uint));//pop pointer,push *pointer
                il.Emit(OpCodes.Stloc_S, i);//store it on the local
            }
            
        }
        /// <summary>
        /// Finalises the funtion code.
        /// After called no more emiting is permited in the current branch.
        /// [the code terminates the curent brach using a ret intruction]
        /// </summary>
        public void FinaliseIl()
        {
            //store all the local registers on the arguments
            for (int i = 0; i < emu.l_r_bbase; i++)//emu.fr_base
            {
                if (emu.bEdited[i])
                {
                    il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                    il.Emit(OpCodes.Ldloc_S, i);//load tha value to write
                    il.Emit(OpCodes.Stobj, typeof(uint));//store it in argument
                }
            }
            for (int i = emu.l_fr_base; i < emu.l_gbri; i++)//;
            {
                if (emu.bEdited[i])
                {
                    il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                    il.Emit(OpCodes.Ldloc_S, i);//load tha value to write
                    il.Emit(OpCodes.Stobj, typeof(float));//store it in argument
                }
            }
            for (int i = emu.l_gbri; i < emu.pCount; i++)
            {
                if (emu.bEdited[i])
                {
                    il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                    il.Emit(OpCodes.Ldloc_S, i);//load tha value to write
                    il.Emit(OpCodes.Stobj, typeof(uint));//store it in argument
                }
            }
            il.Emit(OpCodes.Ldloc_S, emu.tclk);
            il.Emit(OpCodes.Ret); //yeah the end here -> return to function caller..
        }
        /// <summary>
        /// Finalises the il stream - no ret
        /// </summary>
        private void FinaliseIlNoRet()
        {
            //store all the local registers on the arguments
            for (int i = 0; i < emu.l_r_bbase; i++)//emu.fr_base
            {
                if (emu.bEdited[i])
                {
                    il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                    il.Emit(OpCodes.Ldloc_S, i);//load tha value to write
                    il.Emit(OpCodes.Stobj, typeof(uint));//store it in argument
                }
            }
            for (int i = emu.l_fr_base; i < emu.l_gbri; i++)//;
            {
                if (emu.bEdited[i])
                {
                    il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                    il.Emit(OpCodes.Ldloc_S, i);//load tha value to write
                    il.Emit(OpCodes.Stobj, typeof(float));//store it in argument
                }
            }
            for (int i = emu.l_gbri; i < emu.pCount; i++)
            {
                if (emu.bEdited[i])
                {
                    il.Emit(OpCodes.Ldarg_S, i);//load the pointer on the stack
                    il.Emit(OpCodes.Ldloc_S, i);//load tha value to write
                    il.Emit(OpCodes.Stobj, typeof(uint));//store it in argument
                }
            }
        }
        /// <summary>
        /// Gets the function buffer as a delegate.
        /// The buffer must be finalised on all branches to be callable
        /// </summary>
        public DynaRecCall GetCodeBuffer()
        {
            return (DynaRecCall)si.CreateDelegate(typeof(DynaRecCall));
        }
    }

    /// <summary>
    /// Stores info for a CodeCache table entry.
    /// </summary>
    public struct CodeCacheEntry
    {
        /// <summary>
        /// The Generated code.
        /// </summary>
        public DynaRecCall CodeBuffer;
        /// <summary>
        /// Number of the calls doe to this Entry.
        /// </summary>
        public uint nocs;       //number of calls
        /// <summary>
        /// Base Address of the code buffer(recoplie_start address)
        /// </summary>
        public uint BaseAddress;//base address of the code buffer(recoplie_start address)
        /// <summary>
        /// End Address of the code buffer
        /// </summary>
        public uint BufferEnd;  //end of the code buffer
        /// <summary>
        /// Lenght of the buffer(in DreamCast opcodes)
        /// </summary>
        public uint BufferLen;  //number of instructions..
        /// <summary>
        /// If this buffer is valid then this is not set.
        /// If it is set then this buffer should be considered as empty
        /// </summary>
        public bool dirty;      //Does not reflect the current memory data..
        /// <summary>
        /// How many cycles takes the DreamCast's SH4 cpu to execute this buffer
        /// </summary>
        public uint DurCucles;  //how many emulated cpu cycles the codebuffer executes..
        /// <summary>
        /// The Time that this block was made
        /// </summary>
        public uint CTime;//The Time that this block was made
        /// <summary>
        /// Creates and inits the CodeCacheEntry object
        /// </summary>
        /// <param name="Code Buffer"></param>
        /// <param name="Base Address"></param>
        /// <param name="End Address"></param>
        /// <param name="Buffer Length"></param>
        /// <param name="Sh4 cycles"></param>
        public CodeCacheEntry(DynaRecCall buf, uint ba, uint be,uint nCycles)
        {
            CodeBuffer = buf;
            nocs = 0;
            BaseAddress = ba;
            BufferEnd = be;
            BufferLen = be - ba+1;
            CTime = (uint)System.DateTime.Now.Ticks>>2;
            dirty = false;
            DurCucles = nCycles;
        }
    }
}

#endif