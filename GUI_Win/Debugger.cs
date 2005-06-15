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
		TextBox[] tb_r = new TextBox[16];
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
			lastdisasmadr = center;
			#region Disassem
			lstDisasm.SuspendLayout();
			lstDisasm.Items.Clear();
			uint ma = ((uint)(lstDisasm.Height / lstDisasm.ItemHeight)) & 0xFFFFE;
			for (uint i = center - ma; i < center + ma; i += 2)
			{
				if (i == emu.pc)
					lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":-->	" + emu.DisasmOpcode(emu.read(i, 3), i));
				else if (i == center)
					lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":+++	" + emu.DisasmOpcode(emu.read(i, 3), i));
				else
				{
					for (int i2=0;i2<csfa.Length;i2++)
						if (csfa[i2].calladr == i)
						{
							lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":***	" + emu.DisasmOpcode(emu.read(i, 3), i));
							goto ntd;
						}
						else if (csfa[i2].retadr == i)
						{
							lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":___	" + emu.DisasmOpcode(emu.read(i, 3), i));
							goto ntd;
						}
				
					lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":   	" + emu.DisasmOpcode(emu.read(i, 3), i));
				ntd: ;
					//
				}
			}
			lstDisasm.ResumeLayout();
			#endregion
		}

		callstack_frame[] csfa=new callstack_frame[0];

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
			#region Update gpr
			for (int i = 0; i <= 0xF; i++)
			{
				if (emu.r[i] != R_b[i])
				{
					R_b[i] = emu.r[i];
					tb_r[i].ForeColor = Color.Blue;
					tb_r[i].Text = "0x"+Convert.ToString(R_b[i], 16);
				}
				else
				{
					if (tb_r[i].ForeColor == Color.Blue)
					{
						tb_r[i].ForeColor = Color.Black;
					}
				}
			}
			#endregion
			
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
					lastdisasmadr = (uint)(lastdisasmadr + (offset<<1));
					Disassemble(lastdisasmadr & 0xFFFFFFFE);
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
			emu.clc_pvr_renderdone = 1;//render now
		}
	}
}