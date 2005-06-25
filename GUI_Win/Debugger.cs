using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DC4Ever
{
	public unsafe partial class Debugger : Form
	{
		uint[] R_b = new uint[16];
		uint R_pr=0, R_pc=0, R_spc=0;
		TextBox[] tb_r = new TextBox[16];
		TextBox[] tb_fr = new TextBox[16];
		TextBox[] tb_xr = new TextBox[16];
		float[] R_fr = new float[16];
		float[] R_xr = new float[16];
		double[] R_dr = new double[8];

		List<uint> brkpoints = new List<uint>();

		public uint mode = 0;
		public Debugger()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			mode = 0;
		}

		public bool CpuStep()
		{


			if (brkpoints.Count > 0)
			{
				for (int i2 = 0; i2 < brkpoints.Count; i2++)
				{
					if (brkpoints[i2] == emu.pc)
					{
						mode = 1;
						Show();
					}
				}
			}
			if (mode == 0)
				return false;//running
			if (Visible==false)
				return false;//running

			UpdateDbg();

			while (mode == 1 && Visible==true)
			{
				emu.DoEvents();
				System.Threading.Thread.Sleep(10);


			}

			//step
			if (mode == 2)
			{
				mode = 1;
				return false;
			}

			//step
			if (mode == 3)
			{
				mode = 1;
				return true;
			}

			return false;

		}

		class sedhold
		{
			public string StrDat;
			public uint ndat;
			public bool md;
			public override string ToString()
			{
				return StrDat;
			}
			public sedhold(string str, uint num, bool mda)
			{
				StrDat = str;
				ndat = num;
				md = mda;
			}
		}
		uint lastdisasmadr = 0;
		unsafe void Disassemble(uint center)
		{
			if (lastdisasmadr != center)
			{
				lastdisasmadr = center;
				tb_disasm.Text = "0x" + Convert.ToString(center, 16);
				#region Disassem
				lstDisasm.SuspendLayout();
				lstDisasm.Items.Clear();
				uint ma = ((uint)(lstDisasm.Height / lstDisasm.ItemHeight)) & 0xFFFFE;
				for (uint i = center - ma; i < center + ma; i += 2)
				{
					if (i == emu.pc)
						lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":-->	" + Convert.ToString(emu.read(i, 3), 16).ToUpper() + "	" + emu.DisasmOpcode(emu.read(i, 3), i));
					else if (i == center)
						lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":+++	" + Convert.ToString(emu.read(i, 3), 16).ToUpper() + "	" + emu.DisasmOpcode(emu.read(i, 3), i));
					else
					{
						for (int i2 = 0; i2 < csfa.Length; i2++)
							if (csfa[i2].calladr == i)
							{
								lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":***	" + Convert.ToString(emu.read(i, 3), 16).ToUpper() + "	" + emu.DisasmOpcode(emu.read(i, 3), i));
								goto ntd;
							}
							else if (csfa[i2].retadr == i)
							{
								lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":___	" + Convert.ToString(emu.read(i, 3), 16).ToUpper() + "	" + emu.DisasmOpcode(emu.read(i, 3), i));
								goto ntd;
							}

						lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":   	" + Convert.ToString(emu.read(i, 3), 16).ToUpper() + "	" + emu.DisasmOpcode(emu.read(i, 3), i));
					ntd: ;
						//
					}
				}
				lstDisasm.ResumeLayout();
				#endregion
			}
		}

		callstack_frame[] csfa=new callstack_frame[0];

		unsafe void UpdateSh4Regs()
		{
			#region Update gpr/int
			for (int i = 0; i <= 0xF; i++)
			{
				if (emu.r[i] != R_b[i])
				{
					R_b[i] = emu.r[i];
					tb_r[i].ForeColor = Color.Blue;
					tb_r[i].Text = "0x" + Convert.ToString(R_b[i], 16);
				}
				else
				{
					if (tb_r[i].ForeColor == Color.Blue)
					{
						tb_r[i].ForeColor = Color.Black;
					}
				}
			}

			if (emu.pc != R_pc)
			{
				R_pc = emu.pc;
				tb_pc.ForeColor = Color.Blue;
				tb_pc.Text = "0x" + Convert.ToString(R_pc, 16);
			}
			else
			{
				if (tb_pc.ForeColor == Color.Blue)
				{
					tb_pc.ForeColor = Color.Black;
				}
			}

			if (emu.spc != R_spc)
			{
				R_spc = emu.spc;
				tb_spc.ForeColor = Color.Blue;
				tb_spc.Text = "0x" + Convert.ToString(R_spc, 16);
			}
			else
			{
				if (tb_spc.ForeColor == Color.Blue)
				{
					tb_spc.ForeColor = Color.Black;
				}
			}

			if (emu.pr != R_pr)
			{
				R_pr = emu.pr;
				tb_pr.ForeColor = Color.Blue;
				tb_pr.Text = "0x" + Convert.ToString(R_pr, 16);
			}
			else
			{
				if (tb_pr.ForeColor == Color.Blue)
				{
					tb_pr.ForeColor = Color.Black;
				}
			}

			if (emu.fpscr.PR == 0)
			{
				for (int i = 0; i <= 0xF; i++)
				{
					if (emu.xr[i] != R_xr[i])
					{
						R_xr[i] = emu.xr[i];
						tb_xr[i].ForeColor = Color.Blue;
						tb_xr[i].Text = Convert.ToString(R_xr[i]) + "f";
					}
					else
					{
						if (tb_xr[i].ForeColor == Color.Blue)
						{
							tb_xr[i].ForeColor = Color.Black;
						}
					}
				}

				for (int i = 0; i <= 0xF; i++)
				{
					if (emu.fr[i] != R_fr[i])
					{
						R_fr[i] = emu.fr[i];
						tb_fr[i].ForeColor = Color.Blue;
						tb_fr[i].Text = Convert.ToString(R_fr[i]) + "f";
					}
					else
					{
						if (tb_fr[i].ForeColor == Color.Blue)
						{
							tb_fr[i].ForeColor = Color.Black;
						}
					}
				}
			}
			else
			{
				for (int i = 0; i <= 0x7; i++)
				{
					if (emu.dr[i] != R_dr[i])
					{
						R_dr[i] = emu.dr[i];
						tb_fr[i].ForeColor = Color.Blue;
						tb_fr[i].Text = Convert.ToString(R_dr[i])+"d";
					}
					else
					{
						if (tb_fr[i].ForeColor == Color.Blue)
						{
							tb_fr[i].ForeColor = Color.Black;
						}
					}
				}
			}
			#endregion
		}
		unsafe void UpdateDbg()
		{

			Disassemble(emu.pc);
			#region Update Call stack

			csfa= emu.cstCallStack.ToArray();

			lstCallStack.SuspendLayout();
			lstCallStack.Items.Clear();

			if (csfa.Length==0)
				lstCallStack.Items.Add(new sedhold(emu.GetProcedureNameFromAddr(emu.dc_boot_vec) + "+" + Convert.ToString((emu.pc & 0x1FFFFFFF)-(emu.dc_boot_vec & 0x1FFFFFFF),16) + " (curent)", emu.pc, false));
			else
				lstCallStack.Items.Add(new sedhold(emu.GetProcedureNameFromAddr(csfa[0].calladr) + "+" + Convert.ToString((emu.pc & 0x1FFFFFFF) - (csfa[0].calladr & 0x1FFFFFFF), 16) + " (curent)", emu.pc, false));

			for (uint i = 0; i < csfa.Length; i++)
			{
				lstCallStack.Items.Add(new sedhold(csfa[i].ToString(),i,true));
			}

			lstCallStack.ResumeLayout();

			#endregion
			UpdateSh4Regs();
			tb_pc.Text = "0x"+Convert.ToString(emu.pc, 16);

		}

		private unsafe void button3_Click(object sender, EventArgs e)
		{
			if (mode == 0)
			{
				for (int i = 0; i <= 0xF; i++)
				{
					R_b[i] = emu.r[i];
					tb_r[i].ForeColor = Color.Black;
					tb_r[i].Text = "0x" + Convert.ToString(R_b[i], 16);
				}
				UpdateDbg();
			}
			mode = 1;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (mode == 0)
			{
				for (int i = 0; i <= 0xF; i++)
				{
					R_b[i] = emu.r[i];
					tb_r[i].ForeColor = Color.Black;
					tb_r[i].Text = "0x" + Convert.ToString(R_b[i], 16);
				}
			}
			mode = 2;
		}

		private void Debugger_Hide(object sender, EventArgs e)
		{
			mode = 0;
		}

		private void Debugger_Load(object sender, EventArgs e)
		{
			tb_r[0] = tb_r0;
			tb_r[1] = tb_r1;
			tb_r[2] = tb_r2;
			tb_r[3] = tb_r3;
			tb_r[4] = tb_r4;
			tb_r[5] = tb_r5;
			tb_r[6] = tb_r6;
			tb_r[7] = tb_r7;
			tb_r[8] = tb_r8;
			tb_r[9] = tb_r9;
			tb_r[10] = tb_r10;
			tb_r[11] = tb_r11;
			tb_r[12] = tb_r12;
			tb_r[13] = tb_r13;
			tb_r[14] = tb_r14;
			tb_r[15] = tb_r15;

			tb_fr[0] = tb_fr0;
			tb_fr[1] = tb_fr1;
			tb_fr[2] = tb_fr2;
			tb_fr[3] = tb_fr3;
			tb_fr[4] = tb_fr4;
			tb_fr[5] = tb_fr5;
			tb_fr[6] = tb_fr6;
			tb_fr[7] = tb_fr7;
			tb_fr[8] = tb_fr8;
			tb_fr[9] = tb_fr9;
			tb_fr[10] = tb_fr10;
			tb_fr[11] = tb_fr11;
			tb_fr[12] = tb_fr12;
			tb_fr[13] = tb_fr13;
			tb_fr[14] = tb_fr14;
			tb_fr[15] = tb_fr15;

			tb_xr[0] = textBox16;
			tb_xr[1] = textBox15;
			tb_xr[2] = textBox14;
			tb_xr[3] = textBox13;
			tb_xr[4] = textBox12;
			tb_xr[5] = textBox11;
			tb_xr[6] = textBox10;
			tb_xr[7] = textBox9;
			tb_xr[8] = textBox8;
			tb_xr[9] = textBox7;
			tb_xr[10] = textBox6;
			tb_xr[11] = textBox5;
			tb_xr[12] = textBox4;
			tb_xr[13] = textBox3;
			tb_xr[14] = textBox2;
			tb_xr[15] = textBox1;
		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

		private void tabPage2_Click(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			mode = 3;
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox6_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged_1(object sender, EventArgs e)
		{

		}

		private void button5_Click(object sender, EventArgs e)
		{
			try
			{
				uint addr = Convert.ToUInt32(txtPMAddr.Text,16);
				//uint dat = Convert.ToUInt32(txtPMVal);
				int sz = Convert.ToInt32(txtPMsz.Text,10);
				txtPMVal.Text = "0x"+Convert.ToString(emu.read(addr, sz), 16);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error;\n"+ex.ToString());
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				uint addr = Convert.ToUInt32(txtPMAddr.Text,16);
				uint dat = Convert.ToUInt32(txtPMVal.Text, 16);
				int sz = Convert.ToInt32(txtPMsz.Text, 10);
				emu.write(addr, dat, sz);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error;\n" + ex.ToString());
			}
		}

		private void lstCallStack_SelectedIndexChanged(object sender, EventArgs e)
		{
			Disassemble(((sedhold)lstCallStack.SelectedItem).md?
				csfa[((sedhold)lstCallStack.SelectedItem).ndat].retadr :
				((sedhold)lstCallStack.SelectedItem).ndat);
		}
		bool insc = false;
		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			if (!insc && e.Type == ScrollEventType.EndScroll)
			{
				insc = true;

				int offset = e.NewValue - 5000;
				e.NewValue = 5000;
				if (offset != 0)
				{
					Disassemble((uint)(lastdisasmadr + (offset << 1)) & 0xFFFFFFFE);
				}
				insc = false;
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			try
			{

				brkpoints.Remove((uint)lstBrkPoints.SelectedItem);
				lstBrkPoints.Items.Remove(lstBrkPoints.SelectedItem);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error;\n" + ex.ToString());
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			try
			{

				brkpoints.Add(lastdisasmadr);
				lstBrkPoints.Items.Add(lastdisasmadr);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error;\n" + ex.ToString());
			}
		}
		public void SetBP(uint addr)
		{
			if (!brkpoints.Contains(addr))
				brkpoints.Add(addr);
		}
		public void RemBP(uint addr)
		{
			brkpoints.Remove(addr);
		}
		private void button9_Click(object sender, EventArgs e)
		{
			//0x8c01eef8
			try
			{
				uint addr = Convert.ToUInt32(txtBrkAddr.Text, 16);
				brkpoints.Add(addr);
				lstBrkPoints.Items.Add(addr);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error;\n" + ex.ToString());
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			emu.stop_textout = !emu.stop_textout;
		}

		private void button11_Click(object sender, EventArgs e)
		{
#if!zezuExt
			emu.clc_pvr_renderdone = 1;//render now
#else
			emu.write(0xA0000000 | (0x5f8000 + 0x05 * 4),1,4);
#endif
		}

		private void button12_Click(object sender, EventArgs e)
		{
			Disassemble(((sedhold)lstCallStack.SelectedItem).md ?
			csfa[((sedhold)lstCallStack.SelectedItem).ndat].startadr :
			csfa[0].startadr);
		}
	}
}