using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DC4Ever
{
	public unsafe class Debugger : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TabControl tabControl1;
		
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tb_r1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tb_r0;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tb_r6;
		private System.Windows.Forms.TextBox tb_r5;
		private System.Windows.Forms.TextBox tb_r4;
		private System.Windows.Forms.TextBox tb_r7;
		private System.Windows.Forms.TextBox tb_r3;
		private System.Windows.Forms.TextBox tb_r2;
		private System.Windows.Forms.TextBox tb_r14;
		private System.Windows.Forms.TextBox tb_r13;
		private System.Windows.Forms.TextBox tb_r12;
		private System.Windows.Forms.TextBox tb_r15;
		private System.Windows.Forms.TextBox tb_r11;
		private System.Windows.Forms.TextBox tb_r10;
		private System.Windows.Forms.TextBox tb_r9;
		private System.Windows.Forms.TextBox tb_r8;
		private System.Windows.Forms.ListBox lstDisasm;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox txtPMAddr;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.TextBox txtPMsz;
		private System.Windows.Forms.TextBox txtPMVal;
		private System.Windows.Forms.TextBox tb_pc;
		private System.Windows.Forms.ListBox lstCallStack;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.ListBox lstBrkPoints;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.TextBox txtBrkAddr;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tb_pr;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label lbl1021;
		private System.Windows.Forms.TextBox tb_spc;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox tb_disasm;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox tb_fr15;
		private System.Windows.Forms.TextBox tb_fr14;
		private System.Windows.Forms.TextBox tb_fr13;
		private System.Windows.Forms.TextBox tb_fr12;
		private System.Windows.Forms.TextBox tb_fr11;
		private System.Windows.Forms.TextBox tb_fr10;
		private System.Windows.Forms.TextBox tb_fr9;
		private System.Windows.Forms.TextBox tb_fr8;
		private System.Windows.Forms.TextBox tb_fr7;
		private System.Windows.Forms.TextBox tb_fr6;
		private System.Windows.Forms.TextBox tb_fr5;
		private System.Windows.Forms.TextBox tb_fr4;
		private System.Windows.Forms.TextBox tb_fr3;
		private System.Windows.Forms.TextBox tb_fr2;
		private System.Windows.Forms.TextBox tb_fr1;
		private System.Windows.Forms.TextBox tb_fr0;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.TextBox textBox13;
		private System.Windows.Forms.TextBox textBox14;
		private System.Windows.Forms.TextBox textBox15;
		private System.Windows.Forms.TabPage tabPage2;
		
		private System.Windows.Forms.TextBox textBox16;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtPMsz = new System.Windows.Forms.TextBox();
			this.txtPMVal = new System.Windows.Forms.TextBox();
			this.txtPMAddr = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.txtBrkAddr = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label36 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.label48 = new System.Windows.Forms.Label();
			this.label49 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.label51 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.textBox13 = new System.Windows.Forms.TextBox();
			this.textBox14 = new System.Windows.Forms.TextBox();
			this.textBox15 = new System.Windows.Forms.TextBox();
			this.textBox16 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.tb_fr15 = new System.Windows.Forms.TextBox();
			this.tb_fr14 = new System.Windows.Forms.TextBox();
			this.tb_fr13 = new System.Windows.Forms.TextBox();
			this.tb_fr12 = new System.Windows.Forms.TextBox();
			this.tb_fr11 = new System.Windows.Forms.TextBox();
			this.tb_fr10 = new System.Windows.Forms.TextBox();
			this.tb_fr9 = new System.Windows.Forms.TextBox();
			this.tb_fr8 = new System.Windows.Forms.TextBox();
			this.tb_fr7 = new System.Windows.Forms.TextBox();
			this.tb_fr6 = new System.Windows.Forms.TextBox();
			this.tb_fr5 = new System.Windows.Forms.TextBox();
			this.tb_fr4 = new System.Windows.Forms.TextBox();
			this.tb_fr3 = new System.Windows.Forms.TextBox();
			this.tb_fr2 = new System.Windows.Forms.TextBox();
			this.tb_fr1 = new System.Windows.Forms.TextBox();
			this.tb_fr0 = new System.Windows.Forms.TextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label19 = new System.Windows.Forms.Label();
			this.tb_disasm = new System.Windows.Forms.TextBox();
			this.tb_spc = new System.Windows.Forms.TextBox();
			this.lbl1021 = new System.Windows.Forms.Label();
			this.tb_pr = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.tb_pc = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_r14 = new System.Windows.Forms.TextBox();
			this.tb_r13 = new System.Windows.Forms.TextBox();
			this.tb_r12 = new System.Windows.Forms.TextBox();
			this.tb_r15 = new System.Windows.Forms.TextBox();
			this.tb_r11 = new System.Windows.Forms.TextBox();
			this.tb_r10 = new System.Windows.Forms.TextBox();
			this.tb_r9 = new System.Windows.Forms.TextBox();
			this.tb_r8 = new System.Windows.Forms.TextBox();
			this.tb_r6 = new System.Windows.Forms.TextBox();
			this.tb_r5 = new System.Windows.Forms.TextBox();
			this.tb_r4 = new System.Windows.Forms.TextBox();
			this.tb_r7 = new System.Windows.Forms.TextBox();
			this.tb_r3 = new System.Windows.Forms.TextBox();
			this.tb_r2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tb_r1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tb_r0 = new System.Windows.Forms.TextBox();
			this.lstBrkPoints = new System.Windows.Forms.ListBox();
			this.button4 = new System.Windows.Forms.Button();
			this.lstDisasm = new System.Windows.Forms.ListBox();
			this.lstCallStack = new System.Windows.Forms.ListBox();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(12, 537);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(53, 22);
			this.button1.TabIndex = 0;
			this.button1.Text = "Run";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(703, 536);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(53, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Step";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(71, 537);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(53, 22);
			this.button3.TabIndex = 2;
			this.button3.Text = "Stop";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(572, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(239, 518);
			this.tabControl1.TabIndex = 4;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtPMsz);
			this.tabPage1.Controls.Add(this.txtPMVal);
			this.tabPage1.Controls.Add(this.txtPMAddr);
			this.tabPage1.Controls.Add(this.button6);
			this.tabPage1.Controls.Add(this.button5);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(231, 492);
			this.tabPage1.TabIndex = 5;
			this.tabPage1.Text = "Patch Mem";
			// 
			// txtPMsz
			// 
			this.txtPMsz.Location = new System.Drawing.Point(208, 14);
			this.txtPMsz.Name = "txtPMsz";
			this.txtPMsz.Size = new System.Drawing.Size(16, 20);
			this.txtPMsz.TabIndex = 4;
			this.txtPMsz.Text = "";
			// 
			// txtPMVal
			// 
			this.txtPMVal.Location = new System.Drawing.Point(117, 14);
			this.txtPMVal.Name = "txtPMVal";
			this.txtPMVal.Size = new System.Drawing.Size(85, 20);
			this.txtPMVal.TabIndex = 3;
			this.txtPMVal.Text = "";
			// 
			// txtPMAddr
			// 
			this.txtPMAddr.Location = new System.Drawing.Point(3, 14);
			this.txtPMAddr.Name = "txtPMAddr";
			this.txtPMAddr.Size = new System.Drawing.Size(103, 20);
			this.txtPMAddr.TabIndex = 2;
			this.txtPMAddr.Text = "";
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(155, 40);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(69, 21);
			this.button6.TabIndex = 1;
			this.button6.Text = "Write";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(3, 40);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(69, 21);
			this.button5.TabIndex = 0;
			this.button5.Text = "Read";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.txtBrkAddr);
			this.tabPage6.Controls.Add(this.button9);
			this.tabPage6.Controls.Add(this.button8);
			this.tabPage6.Controls.Add(this.button7);
			this.tabPage6.Controls.Add(this.lstBrkPoints);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(231, 492);
			this.tabPage6.TabIndex = 6;
			this.tabPage6.Text = "Breakpoints";
			// 
			// txtBrkAddr
			// 
			this.txtBrkAddr.Location = new System.Drawing.Point(70, 347);
			this.txtBrkAddr.Name = "txtBrkAddr";
			this.txtBrkAddr.Size = new System.Drawing.Size(94, 20);
			this.txtBrkAddr.TabIndex = 4;
			this.txtBrkAddr.Text = "";
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(4, 347);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(60, 22);
			this.button9.TabIndex = 3;
			this.button9.Text = "Add-Addr";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(164, 323);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(60, 21);
			this.button8.TabIndex = 2;
			this.button8.Text = "Remove";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(4, 322);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(60, 22);
			this.button7.TabIndex = 1;
			this.button7.Text = "Add";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// tabPage5
			// 
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(231, 492);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "MMR/misc";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox3);
			this.tabPage3.Controls.Add(this.groupBox2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(231, 492);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "CPU GBR/float";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label36);
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.Controls.Add(this.label38);
			this.groupBox3.Controls.Add(this.label39);
			this.groupBox3.Controls.Add(this.label40);
			this.groupBox3.Controls.Add(this.label41);
			this.groupBox3.Controls.Add(this.label42);
			this.groupBox3.Controls.Add(this.label43);
			this.groupBox3.Controls.Add(this.label44);
			this.groupBox3.Controls.Add(this.label45);
			this.groupBox3.Controls.Add(this.label46);
			this.groupBox3.Controls.Add(this.label47);
			this.groupBox3.Controls.Add(this.label48);
			this.groupBox3.Controls.Add(this.label49);
			this.groupBox3.Controls.Add(this.label50);
			this.groupBox3.Controls.Add(this.label51);
			this.groupBox3.Controls.Add(this.textBox1);
			this.groupBox3.Controls.Add(this.textBox2);
			this.groupBox3.Controls.Add(this.textBox3);
			this.groupBox3.Controls.Add(this.textBox4);
			this.groupBox3.Controls.Add(this.textBox5);
			this.groupBox3.Controls.Add(this.textBox6);
			this.groupBox3.Controls.Add(this.textBox7);
			this.groupBox3.Controls.Add(this.textBox8);
			this.groupBox3.Controls.Add(this.textBox9);
			this.groupBox3.Controls.Add(this.textBox10);
			this.groupBox3.Controls.Add(this.textBox11);
			this.groupBox3.Controls.Add(this.textBox12);
			this.groupBox3.Controls.Add(this.textBox13);
			this.groupBox3.Controls.Add(this.textBox14);
			this.groupBox3.Controls.Add(this.textBox15);
			this.groupBox3.Controls.Add(this.textBox16);
			this.groupBox3.Location = new System.Drawing.Point(4, 245);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(220, 238);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Bank 1 (fr)";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(126, 208);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(26, 16);
			this.label36.TabIndex = 39;
			this.label36.Text = "xr15";
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(126, 178);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(26, 16);
			this.label37.TabIndex = 38;
			this.label37.Text = "xr14";
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(126, 152);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(26, 16);
			this.label38.TabIndex = 37;
			this.label38.Text = "xr13";
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(126, 126);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(26, 16);
			this.label39.TabIndex = 36;
			this.label39.Text = "xr12";
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(126, 100);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(26, 16);
			this.label40.TabIndex = 35;
			this.label40.Text = "xr11";
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(126, 78);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(26, 16);
			this.label41.TabIndex = 34;
			this.label41.Text = "xr10";
			// 
			// label42
			// 
			this.label42.AutoSize = true;
			this.label42.Location = new System.Drawing.Point(126, 52);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(20, 16);
			this.label42.TabIndex = 33;
			this.label42.Text = "xr9";
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.Location = new System.Drawing.Point(126, 26);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(20, 16);
			this.label43.TabIndex = 32;
			this.label43.Text = "xr8";
			// 
			// label44
			// 
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(6, 204);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(20, 16);
			this.label44.TabIndex = 31;
			this.label44.Text = "xr7";
			// 
			// label45
			// 
			this.label45.AutoSize = true;
			this.label45.Location = new System.Drawing.Point(6, 178);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(20, 16);
			this.label45.TabIndex = 30;
			this.label45.Text = "xr6";
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(6, 152);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(20, 16);
			this.label46.TabIndex = 29;
			this.label46.Text = "xr5";
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(6, 126);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(20, 16);
			this.label47.TabIndex = 28;
			this.label47.Text = "xr4";
			// 
			// label48
			// 
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(6, 100);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(20, 16);
			this.label48.TabIndex = 27;
			this.label48.Text = "xr3";
			// 
			// label49
			// 
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(6, 74);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(20, 16);
			this.label49.TabIndex = 26;
			this.label49.Text = "xr2";
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Location = new System.Drawing.Point(6, 48);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(20, 16);
			this.label50.TabIndex = 25;
			this.label50.Text = "xr1";
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Location = new System.Drawing.Point(6, 22);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(20, 16);
			this.label51.TabIndex = 24;
			this.label51.Text = "xr0";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(155, 201);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(63, 20);
			this.textBox1.TabIndex = 23;
			this.textBox1.Text = "0";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(155, 175);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(63, 20);
			this.textBox2.TabIndex = 22;
			this.textBox2.Text = "0";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(155, 149);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(63, 20);
			this.textBox3.TabIndex = 21;
			this.textBox3.Text = "0";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(155, 123);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(63, 20);
			this.textBox4.TabIndex = 20;
			this.textBox4.Text = "0";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(155, 97);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(63, 20);
			this.textBox5.TabIndex = 19;
			this.textBox5.Text = "0";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(155, 71);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(63, 20);
			this.textBox6.TabIndex = 18;
			this.textBox6.Text = "0";
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(155, 45);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(63, 20);
			this.textBox7.TabIndex = 17;
			this.textBox7.Text = "0";
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(155, 19);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(63, 20);
			this.textBox8.TabIndex = 16;
			this.textBox8.Text = "0";
			// 
			// textBox9
			// 
			this.textBox9.Location = new System.Drawing.Point(42, 201);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new System.Drawing.Size(63, 20);
			this.textBox9.TabIndex = 15;
			this.textBox9.Text = "0";
			// 
			// textBox10
			// 
			this.textBox10.Location = new System.Drawing.Point(42, 175);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(63, 20);
			this.textBox10.TabIndex = 14;
			this.textBox10.Text = "0";
			// 
			// textBox11
			// 
			this.textBox11.Location = new System.Drawing.Point(42, 149);
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new System.Drawing.Size(63, 20);
			this.textBox11.TabIndex = 13;
			this.textBox11.Text = "0";
			// 
			// textBox12
			// 
			this.textBox12.Location = new System.Drawing.Point(42, 123);
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new System.Drawing.Size(63, 20);
			this.textBox12.TabIndex = 12;
			this.textBox12.Text = "0";
			// 
			// textBox13
			// 
			this.textBox13.Location = new System.Drawing.Point(42, 97);
			this.textBox13.Name = "textBox13";
			this.textBox13.Size = new System.Drawing.Size(63, 20);
			this.textBox13.TabIndex = 11;
			this.textBox13.Text = "0";
			// 
			// textBox14
			// 
			this.textBox14.Location = new System.Drawing.Point(42, 71);
			this.textBox14.Name = "textBox14";
			this.textBox14.Size = new System.Drawing.Size(63, 20);
			this.textBox14.TabIndex = 10;
			this.textBox14.Text = "0";
			// 
			// textBox15
			// 
			this.textBox15.Location = new System.Drawing.Point(42, 45);
			this.textBox15.Name = "textBox15";
			this.textBox15.Size = new System.Drawing.Size(63, 20);
			this.textBox15.TabIndex = 9;
			this.textBox15.Text = "0";
			// 
			// textBox16
			// 
			this.textBox16.Location = new System.Drawing.Point(42, 19);
			this.textBox16.Name = "textBox16";
			this.textBox16.Size = new System.Drawing.Size(63, 20);
			this.textBox16.TabIndex = 8;
			this.textBox16.Text = "0";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label35);
			this.groupBox2.Controls.Add(this.label34);
			this.groupBox2.Controls.Add(this.label33);
			this.groupBox2.Controls.Add(this.label32);
			this.groupBox2.Controls.Add(this.label31);
			this.groupBox2.Controls.Add(this.label30);
			this.groupBox2.Controls.Add(this.label29);
			this.groupBox2.Controls.Add(this.label28);
			this.groupBox2.Controls.Add(this.label27);
			this.groupBox2.Controls.Add(this.label26);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.label22);
			this.groupBox2.Controls.Add(this.label21);
			this.groupBox2.Controls.Add(this.label20);
			this.groupBox2.Controls.Add(this.tb_fr15);
			this.groupBox2.Controls.Add(this.tb_fr14);
			this.groupBox2.Controls.Add(this.tb_fr13);
			this.groupBox2.Controls.Add(this.tb_fr12);
			this.groupBox2.Controls.Add(this.tb_fr11);
			this.groupBox2.Controls.Add(this.tb_fr10);
			this.groupBox2.Controls.Add(this.tb_fr9);
			this.groupBox2.Controls.Add(this.tb_fr8);
			this.groupBox2.Controls.Add(this.tb_fr7);
			this.groupBox2.Controls.Add(this.tb_fr6);
			this.groupBox2.Controls.Add(this.tb_fr5);
			this.groupBox2.Controls.Add(this.tb_fr4);
			this.groupBox2.Controls.Add(this.tb_fr3);
			this.groupBox2.Controls.Add(this.tb_fr2);
			this.groupBox2.Controls.Add(this.tb_fr1);
			this.groupBox2.Controls.Add(this.tb_fr0);
			this.groupBox2.Location = new System.Drawing.Point(4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(220, 238);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Bank 0 (fr)";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(126, 208);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(24, 16);
			this.label35.TabIndex = 39;
			this.label35.Text = "fr15";
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(126, 178);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(24, 16);
			this.label34.TabIndex = 38;
			this.label34.Text = "fr14";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(126, 149);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(24, 16);
			this.label33.TabIndex = 37;
			this.label33.Text = "fr13";
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(126, 126);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(24, 16);
			this.label32.TabIndex = 36;
			this.label32.Text = "fr12";
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(126, 100);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(24, 16);
			this.label31.TabIndex = 35;
			this.label31.Text = "fr11";
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(126, 78);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(24, 16);
			this.label30.TabIndex = 34;
			this.label30.Text = "fr10";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(126, 52);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(17, 16);
			this.label29.TabIndex = 33;
			this.label29.Text = "fr9";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(126, 26);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(17, 16);
			this.label28.TabIndex = 32;
			this.label28.Text = "fr8";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(6, 204);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(17, 16);
			this.label27.TabIndex = 31;
			this.label27.Text = "fr7";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(6, 178);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(17, 16);
			this.label26.TabIndex = 30;
			this.label26.Text = "fr6";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(6, 152);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(17, 16);
			this.label25.TabIndex = 29;
			this.label25.Text = "fr5";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(6, 126);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(17, 16);
			this.label24.TabIndex = 28;
			this.label24.Text = "fr4";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(6, 100);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(17, 16);
			this.label23.TabIndex = 27;
			this.label23.Text = "fr3";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(6, 74);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(17, 16);
			this.label22.TabIndex = 26;
			this.label22.Text = "fr2";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(6, 48);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(17, 16);
			this.label21.TabIndex = 25;
			this.label21.Text = "fr1";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(6, 22);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(17, 16);
			this.label20.TabIndex = 24;
			this.label20.Text = "fr0";
			// 
			// tb_fr15
			// 
			this.tb_fr15.Location = new System.Drawing.Point(155, 201);
			this.tb_fr15.Name = "tb_fr15";
			this.tb_fr15.Size = new System.Drawing.Size(63, 20);
			this.tb_fr15.TabIndex = 23;
			this.tb_fr15.Text = "0";
			// 
			// tb_fr14
			// 
			this.tb_fr14.Location = new System.Drawing.Point(155, 175);
			this.tb_fr14.Name = "tb_fr14";
			this.tb_fr14.Size = new System.Drawing.Size(63, 20);
			this.tb_fr14.TabIndex = 22;
			this.tb_fr14.Text = "0";
			// 
			// tb_fr13
			// 
			this.tb_fr13.Location = new System.Drawing.Point(155, 149);
			this.tb_fr13.Name = "tb_fr13";
			this.tb_fr13.Size = new System.Drawing.Size(63, 20);
			this.tb_fr13.TabIndex = 21;
			this.tb_fr13.Text = "0";
			// 
			// tb_fr12
			// 
			this.tb_fr12.Location = new System.Drawing.Point(155, 123);
			this.tb_fr12.Name = "tb_fr12";
			this.tb_fr12.Size = new System.Drawing.Size(63, 20);
			this.tb_fr12.TabIndex = 20;
			this.tb_fr12.Text = "0";
			// 
			// tb_fr11
			// 
			this.tb_fr11.Location = new System.Drawing.Point(155, 97);
			this.tb_fr11.Name = "tb_fr11";
			this.tb_fr11.Size = new System.Drawing.Size(63, 20);
			this.tb_fr11.TabIndex = 19;
			this.tb_fr11.Text = "0";
			// 
			// tb_fr10
			// 
			this.tb_fr10.Location = new System.Drawing.Point(155, 71);
			this.tb_fr10.Name = "tb_fr10";
			this.tb_fr10.Size = new System.Drawing.Size(63, 20);
			this.tb_fr10.TabIndex = 18;
			this.tb_fr10.Text = "0";
			// 
			// tb_fr9
			// 
			this.tb_fr9.Location = new System.Drawing.Point(155, 45);
			this.tb_fr9.Name = "tb_fr9";
			this.tb_fr9.Size = new System.Drawing.Size(63, 20);
			this.tb_fr9.TabIndex = 17;
			this.tb_fr9.Text = "0";
			// 
			// tb_fr8
			// 
			this.tb_fr8.Location = new System.Drawing.Point(155, 19);
			this.tb_fr8.Name = "tb_fr8";
			this.tb_fr8.Size = new System.Drawing.Size(63, 20);
			this.tb_fr8.TabIndex = 16;
			this.tb_fr8.Text = "0";
			// 
			// tb_fr7
			// 
			this.tb_fr7.Location = new System.Drawing.Point(42, 201);
			this.tb_fr7.Name = "tb_fr7";
			this.tb_fr7.Size = new System.Drawing.Size(63, 20);
			this.tb_fr7.TabIndex = 15;
			this.tb_fr7.Text = "0";
			// 
			// tb_fr6
			// 
			this.tb_fr6.Location = new System.Drawing.Point(42, 175);
			this.tb_fr6.Name = "tb_fr6";
			this.tb_fr6.Size = new System.Drawing.Size(63, 20);
			this.tb_fr6.TabIndex = 14;
			this.tb_fr6.Text = "0";
			// 
			// tb_fr5
			// 
			this.tb_fr5.Location = new System.Drawing.Point(42, 149);
			this.tb_fr5.Name = "tb_fr5";
			this.tb_fr5.Size = new System.Drawing.Size(63, 20);
			this.tb_fr5.TabIndex = 13;
			this.tb_fr5.Text = "0";
			// 
			// tb_fr4
			// 
			this.tb_fr4.Location = new System.Drawing.Point(42, 123);
			this.tb_fr4.Name = "tb_fr4";
			this.tb_fr4.Size = new System.Drawing.Size(63, 20);
			this.tb_fr4.TabIndex = 12;
			this.tb_fr4.Text = "0";
			// 
			// tb_fr3
			// 
			this.tb_fr3.Location = new System.Drawing.Point(42, 97);
			this.tb_fr3.Name = "tb_fr3";
			this.tb_fr3.Size = new System.Drawing.Size(63, 20);
			this.tb_fr3.TabIndex = 11;
			this.tb_fr3.Text = "0";
			// 
			// tb_fr2
			// 
			this.tb_fr2.Location = new System.Drawing.Point(42, 71);
			this.tb_fr2.Name = "tb_fr2";
			this.tb_fr2.Size = new System.Drawing.Size(63, 20);
			this.tb_fr2.TabIndex = 10;
			this.tb_fr2.Text = "0";
			// 
			// tb_fr1
			// 
			this.tb_fr1.Location = new System.Drawing.Point(42, 45);
			this.tb_fr1.Name = "tb_fr1";
			this.tb_fr1.Size = new System.Drawing.Size(63, 20);
			this.tb_fr1.TabIndex = 9;
			this.tb_fr1.Text = "0";
			// 
			// tb_fr0
			// 
			this.tb_fr0.Location = new System.Drawing.Point(42, 19);
			this.tb_fr0.Name = "tb_fr0";
			this.tb_fr0.Size = new System.Drawing.Size(63, 20);
			this.tb_fr0.TabIndex = 8;
			this.tb_fr0.Text = "0";
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(231, 492);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "CPU/misc";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(6, 285);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(29, 16);
			this.label19.TabIndex = 8;
			this.label19.Text = "CUR";
			// 
			// tb_disasm
			// 
			this.tb_disasm.Location = new System.Drawing.Point(35, 282);
			this.tb_disasm.Name = "tb_disasm";
			this.tb_disasm.Size = new System.Drawing.Size(77, 20);
			this.tb_disasm.TabIndex = 7;
			this.tb_disasm.Text = "";
			// 
			// tb_spc
			// 
			this.tb_spc.Location = new System.Drawing.Point(138, 282);
			this.tb_spc.Name = "tb_spc";
			this.tb_spc.Size = new System.Drawing.Size(77, 20);
			this.tb_spc.TabIndex = 6;
			this.tb_spc.Text = "0";
			// 
			// lbl1021
			// 
			this.lbl1021.AutoSize = true;
			this.lbl1021.Location = new System.Drawing.Point(113, 285);
			this.lbl1021.Name = "lbl1021";
			this.lbl1021.Size = new System.Drawing.Size(27, 16);
			this.lbl1021.TabIndex = 5;
			this.lbl1021.Text = "SPC";
			// 
			// tb_pr
			// 
			this.tb_pr.Location = new System.Drawing.Point(138, 252);
			this.tb_pr.Name = "tb_pr";
			this.tb_pr.Size = new System.Drawing.Size(77, 20);
			this.tb_pr.TabIndex = 4;
			this.tb_pr.Text = "0";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(119, 255);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(20, 16);
			this.label18.TabIndex = 3;
			this.label18.Text = "PR";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(12, 255);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(20, 16);
			this.label17.TabIndex = 2;
			this.label17.Text = "PC";
			// 
			// tb_pc
			// 
			this.tb_pc.Location = new System.Drawing.Point(35, 252);
			this.tb_pc.Name = "tb_pc";
			this.tb_pc.Size = new System.Drawing.Size(79, 20);
			this.tb_pc.TabIndex = 1;
			this.tb_pc.Text = "0";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tb_r14);
			this.groupBox1.Controls.Add(this.tb_r13);
			this.groupBox1.Controls.Add(this.tb_r12);
			this.groupBox1.Controls.Add(this.tb_r15);
			this.groupBox1.Controls.Add(this.tb_r11);
			this.groupBox1.Controls.Add(this.tb_r10);
			this.groupBox1.Controls.Add(this.tb_r9);
			this.groupBox1.Controls.Add(this.tb_r8);
			this.groupBox1.Controls.Add(this.tb_r6);
			this.groupBox1.Controls.Add(this.tb_r5);
			this.groupBox1.Controls.Add(this.tb_r4);
			this.groupBox1.Controls.Add(this.tb_r7);
			this.groupBox1.Controls.Add(this.tb_r3);
			this.groupBox1.Controls.Add(this.tb_r2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tb_r1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.tb_r0);
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(215, 235);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "GPR/Int";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(108, 204);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(25, 16);
			this.label16.TabIndex = 31;
			this.label16.Text = "R15";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(108, 178);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(25, 16);
			this.label15.TabIndex = 30;
			this.label15.Text = "R14";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(108, 152);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(25, 16);
			this.label14.TabIndex = 29;
			this.label14.Text = "R13";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(108, 126);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(25, 16);
			this.label13.TabIndex = 28;
			this.label13.Text = "R12";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(108, 104);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(25, 16);
			this.label12.TabIndex = 27;
			this.label12.Text = "R11";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(108, 74);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(25, 16);
			this.label11.TabIndex = 26;
			this.label11.Text = "R10";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(114, 48);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(19, 16);
			this.label10.TabIndex = 25;
			this.label10.Text = "R9";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(114, 22);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(19, 16);
			this.label9.TabIndex = 24;
			this.label9.Text = "R8";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 204);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(19, 16);
			this.label8.TabIndex = 23;
			this.label8.Text = "R7";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 178);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(19, 16);
			this.label7.TabIndex = 22;
			this.label7.Text = "R6";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(19, 16);
			this.label6.TabIndex = 21;
			this.label6.Text = "R5";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(19, 16);
			this.label5.TabIndex = 20;
			this.label5.Text = "R4";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(19, 16);
			this.label4.TabIndex = 19;
			this.label4.Text = "R3";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 16);
			this.label3.TabIndex = 18;
			this.label3.Text = "R2";
			// 
			// tb_r14
			// 
			this.tb_r14.Location = new System.Drawing.Point(132, 175);
			this.tb_r14.Name = "tb_r14";
			this.tb_r14.Size = new System.Drawing.Size(77, 20);
			this.tb_r14.TabIndex = 17;
			this.tb_r14.Text = "0";
			// 
			// tb_r13
			// 
			this.tb_r13.Location = new System.Drawing.Point(132, 149);
			this.tb_r13.Name = "tb_r13";
			this.tb_r13.Size = new System.Drawing.Size(77, 20);
			this.tb_r13.TabIndex = 16;
			this.tb_r13.Text = "0";
			this.tb_r13.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
			// 
			// tb_r12
			// 
			this.tb_r12.Location = new System.Drawing.Point(132, 123);
			this.tb_r12.Name = "tb_r12";
			this.tb_r12.Size = new System.Drawing.Size(77, 20);
			this.tb_r12.TabIndex = 15;
			this.tb_r12.Text = "0";
			// 
			// tb_r15
			// 
			this.tb_r15.Location = new System.Drawing.Point(132, 201);
			this.tb_r15.Name = "tb_r15";
			this.tb_r15.Size = new System.Drawing.Size(77, 20);
			this.tb_r15.TabIndex = 14;
			this.tb_r15.Text = "0";
			// 
			// tb_r11
			// 
			this.tb_r11.Location = new System.Drawing.Point(132, 97);
			this.tb_r11.Name = "tb_r11";
			this.tb_r11.Size = new System.Drawing.Size(77, 20);
			this.tb_r11.TabIndex = 13;
			this.tb_r11.Text = "0";
			// 
			// tb_r10
			// 
			this.tb_r10.Location = new System.Drawing.Point(132, 71);
			this.tb_r10.Name = "tb_r10";
			this.tb_r10.Size = new System.Drawing.Size(77, 20);
			this.tb_r10.TabIndex = 12;
			this.tb_r10.Text = "0";
			// 
			// tb_r9
			// 
			this.tb_r9.Location = new System.Drawing.Point(132, 45);
			this.tb_r9.Name = "tb_r9";
			this.tb_r9.Size = new System.Drawing.Size(77, 20);
			this.tb_r9.TabIndex = 11;
			this.tb_r9.Text = "0";
			// 
			// tb_r8
			// 
			this.tb_r8.Location = new System.Drawing.Point(132, 19);
			this.tb_r8.Name = "tb_r8";
			this.tb_r8.Size = new System.Drawing.Size(77, 20);
			this.tb_r8.TabIndex = 10;
			this.tb_r8.Text = "0";
			// 
			// tb_r6
			// 
			this.tb_r6.Location = new System.Drawing.Point(29, 175);
			this.tb_r6.Name = "tb_r6";
			this.tb_r6.Size = new System.Drawing.Size(80, 20);
			this.tb_r6.TabIndex = 9;
			this.tb_r6.Text = "0";
			this.tb_r6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
			// 
			// tb_r5
			// 
			this.tb_r5.Location = new System.Drawing.Point(29, 149);
			this.tb_r5.Name = "tb_r5";
			this.tb_r5.Size = new System.Drawing.Size(80, 20);
			this.tb_r5.TabIndex = 8;
			this.tb_r5.Text = "0";
			// 
			// tb_r4
			// 
			this.tb_r4.Location = new System.Drawing.Point(29, 123);
			this.tb_r4.Name = "tb_r4";
			this.tb_r4.Size = new System.Drawing.Size(80, 20);
			this.tb_r4.TabIndex = 7;
			this.tb_r4.Text = "0";
			// 
			// tb_r7
			// 
			this.tb_r7.Location = new System.Drawing.Point(29, 201);
			this.tb_r7.Name = "tb_r7";
			this.tb_r7.Size = new System.Drawing.Size(80, 20);
			this.tb_r7.TabIndex = 6;
			this.tb_r7.Text = "0";
			this.tb_r7.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
			// 
			// tb_r3
			// 
			this.tb_r3.Location = new System.Drawing.Point(29, 97);
			this.tb_r3.Name = "tb_r3";
			this.tb_r3.Size = new System.Drawing.Size(80, 20);
			this.tb_r3.TabIndex = 5;
			this.tb_r3.Text = "0";
			// 
			// tb_r2
			// 
			this.tb_r2.Location = new System.Drawing.Point(29, 71);
			this.tb_r2.Name = "tb_r2";
			this.tb_r2.Size = new System.Drawing.Size(80, 20);
			this.tb_r2.TabIndex = 4;
			this.tb_r2.Text = "0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "R1";
			// 
			// tb_r1
			// 
			this.tb_r1.Location = new System.Drawing.Point(29, 45);
			this.tb_r1.Name = "tb_r1";
			this.tb_r1.Size = new System.Drawing.Size(80, 20);
			this.tb_r1.TabIndex = 2;
			this.tb_r1.Text = "0";
			this.tb_r1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "R0";
			// 
			// tb_r0
			// 
			this.tb_r0.Location = new System.Drawing.Point(29, 19);
			this.tb_r0.Name = "tb_r0";
			this.tb_r0.Size = new System.Drawing.Size(80, 20);
			this.tb_r0.TabIndex = 0;
			this.tb_r0.Text = "0";
			// 
			// lstBrkPoints
			// 
			this.lstBrkPoints.Location = new System.Drawing.Point(4, 0);
			this.lstBrkPoints.Name = "lstBrkPoints";
			this.lstBrkPoints.Size = new System.Drawing.Size(220, 316);
			this.lstBrkPoints.TabIndex = 0;
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(758, 536);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(53, 22);
			this.button4.TabIndex = 5;
			this.button4.Text = "Skip";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// lstDisasm
			// 
			this.lstDisasm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstDisasm.IntegralHeight = false;
			this.lstDisasm.Location = new System.Drawing.Point(12, 12);
			this.lstDisasm.Name = "lstDisasm";
			this.lstDisasm.Size = new System.Drawing.Size(540, 383);
			this.lstDisasm.TabIndex = 6;
			// 
			// lstCallStack
			// 
			this.lstCallStack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstCallStack.IntegralHeight = false;
			this.lstCallStack.Location = new System.Drawing.Point(12, 407);
			this.lstCallStack.Name = "lstCallStack";
			this.lstCallStack.Size = new System.Drawing.Size(550, 121);
			this.lstCallStack.TabIndex = 7;
			this.lstCallStack.SelectedIndexChanged += new System.EventHandler(this.lstCallStack_SelectedIndexChanged);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.LargeChange = 100;
			this.vScrollBar1.Location = new System.Drawing.Point(555, 12);
			this.vScrollBar1.Maximum = 10000;
			this.vScrollBar1.Minimum = 1;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 382);
			this.vScrollBar1.TabIndex = 8;
			this.vScrollBar1.Value = 5000;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// button10
			// 
			this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button10.Location = new System.Drawing.Point(580, 536);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(83, 23);
			this.button10.TabIndex = 9;
			this.button10.Text = "Console on/off";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button11.Location = new System.Drawing.Point(482, 537);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(69, 20);
			this.button11.TabIndex = 10;
			this.button11.Text = "Render NOW!";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button12
			// 
			this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button12.Location = new System.Drawing.Point(130, 537);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(53, 22);
			this.button12.TabIndex = 11;
			this.button12.Text = "Call Strt";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label19);
			this.tabPage2.Controls.Add(this.tb_disasm);
			this.tabPage2.Controls.Add(this.tb_spc);
			this.tabPage2.Controls.Add(this.lbl1021);
			this.tabPage2.Controls.Add(this.tb_pr);
			this.tabPage2.Controls.Add(this.label18);
			this.tabPage2.Controls.Add(this.label17);
			this.tabPage2.Controls.Add(this.tb_pc);
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(227, 372);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "CPU GPR/int";
			// 
			// Debugger
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(819, 563);
			this.Controls.Add(this.button12);
			this.Controls.Add(this.button11);
			this.Controls.Add(this.button10);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.lstCallStack);
			this.Controls.Add(this.lstDisasm);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Debugger";
			this.Text = "Debugger";
			this.Load += new System.EventHandler(this.Debugger_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion


		uint[] R_b = new uint[16];
		uint R_pr=0, R_pc=0, R_spc=0;
		TextBox[] tb_r = new TextBox[16];
		TextBox[] tb_fr = new TextBox[16];
		TextBox[] tb_xr = new TextBox[16];
		float[] R_fr = new float[16];
		float[] R_xr = new float[16];
		double[] R_dr = new double[8];

		//List<uint> brkpoints = new List<uint>();
		ArrayList brkpoints = new ArrayList();

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
					if (((uint)brkpoints[i2]) == sh4.pc)
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
				ta.DoEvents();
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
					if (i == sh4.pc)
						lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":-->	" + Convert.ToString(mem.read(i, 3), 16).ToUpper() + "	" + sh4_disasm.DisasmOpcode(mem.read(i, 3), i));
					else if (i == center)
						lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":+++	" + Convert.ToString(mem.read(i, 3), 16).ToUpper() + "	" + sh4_disasm.DisasmOpcode(mem.read(i, 3), i));
					else
					{
						for (int i2 = 0; i2 < csfa.Length; i2++)
							if (csfa[i2].calladr == i)
							{
								lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":***	" + Convert.ToString(mem.read(i, 3), 16).ToUpper() + "	" + sh4_disasm.DisasmOpcode(mem.read(i, 3), i));
								goto ntd;
							}
							else if (csfa[i2].retadr == i)
							{
								lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":___	" + Convert.ToString(mem.read(i, 3), 16).ToUpper() + "	" + sh4_disasm.DisasmOpcode(mem.read(i, 3), i));
								goto ntd;
							}

						lstDisasm.Items.Add("0x" + Convert.ToString(i, 16).ToUpper() + ":   	" + Convert.ToString(mem.read(i, 3), 16).ToUpper() + "	" + sh4_disasm.DisasmOpcode(mem.read(i, 3), i));
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
				if (sh4.r[i] != R_b[i])
				{
					R_b[i] = sh4.r[i];
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

			if (sh4.pc != R_pc)
			{
				R_pc = sh4.pc;
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

			if (sh4.spc != R_spc)
			{
				R_spc = sh4.spc;
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

			if (sh4.pr != R_pr)
			{
				R_pr = sh4.pr;
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

			if (sh4.fpscr.PR == 0)
			{
				for (int i = 0; i <= 0xF; i++)
				{
					if (sh4.xr[i] != R_xr[i])
					{
						R_xr[i] = sh4.xr[i];
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
					if (sh4.fr[i] != R_fr[i])
					{
						R_fr[i] = sh4.fr[i];
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
					if (sh4.GetDR((uint)i) != R_dr[i])
					{
						R_dr[i] = sh4.GetDR((uint)i);
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

			Disassemble(sh4.pc);
			#region Update Call stack

			object[] tob=CallStackTrace.cstCallStack.ToArray();

			csfa= new callstack_frame[tob.Length] ;
			for (int i=0;i<tob.Length;i++)
			{
				csfa[i]=(callstack_frame)tob[i];
			}

			lstCallStack.SuspendLayout();
			lstCallStack.Items.Clear();

			if (csfa.Length==0)
				lstCallStack.Items.Add(new sedhold(sh4_disasm.GetProcedureNameFromAddr(sh4.dc_boot_vec) + "+" + Convert.ToString((sh4.pc & 0x1FFFFFFF)-(sh4.dc_boot_vec & 0x1FFFFFFF),16) + " (curent)", sh4.pc, false));
			else
				lstCallStack.Items.Add(new sedhold(sh4_disasm.GetProcedureNameFromAddr(csfa[0].calladr) + "+" + Convert.ToString((sh4.pc & 0x1FFFFFFF) - (csfa[0].calladr & 0x1FFFFFFF), 16) + " (curent)", sh4.pc, false));

			for (uint i = 0; i < csfa.Length; i++)
			{
				lstCallStack.Items.Add(new sedhold(csfa[i].ToString(),i,true));
			}

			lstCallStack.ResumeLayout();

			#endregion
			UpdateSh4Regs();
			tb_pc.Text = "0x"+Convert.ToString(sh4.pc, 16);

		}

		private unsafe void button3_Click(object sender, EventArgs e)
		{
			if (mode == 0)
			{
				for (int i = 0; i <= 0xF; i++)
				{
					R_b[i] = sh4.r[i];
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
					R_b[i] = sh4.r[i];
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
				txtPMVal.Text = "0x"+Convert.ToString(mem.read(addr, sz), 16);
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
				mem.write(addr, dat, sz);
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
			mem.stop_textout = !mem.stop_textout;
		}

		private void button11_Click(object sender, EventArgs e)
		{
#if!zezuExt
			pvr.clc_pvr_renderdone = 1;//render now
#else
			mem.write(0xA0000000 | (0x5f8000 + 0x05 * 4),1,4);
#endif
		}

		private void button12_Click(object sender, EventArgs e)
		{
			Disassemble(((sedhold)lstCallStack.SelectedItem).md ?
			csfa[((sedhold)lstCallStack.SelectedItem).ndat].startadr :
			csfa[0].startadr);
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}