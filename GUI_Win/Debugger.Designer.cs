namespace DC4Ever
{
	partial class Debugger
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tb_pc = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
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
			this.lstBrkPoints = new System.Windows.Forms.ListBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.button4 = new System.Windows.Forms.Button();
			this.lstDisasm = new System.Windows.Forms.ListBox();
			this.lstCallStack = new System.Windows.Forms.ListBox();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(12, 417);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(53, 22);
			this.button1.TabIndex = 0;
			this.button1.Text = "Run";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(678, 416);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(53, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Step";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(71, 417);
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
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Location = new System.Drawing.Point(547, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(235, 398);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tb_pc);
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(227, 372);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "CPU GPR/int";
			this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
			// 
			// tb_pc
			// 
			this.tb_pc.Location = new System.Drawing.Point(35, 259);
			this.tb_pc.Name = "tb_pc";
			this.tb_pc.Size = new System.Drawing.Size(79, 20);
			this.tb_pc.TabIndex = 1;
			// 
			// groupBox1
			// 
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
			// tb_r14
			// 
			this.tb_r14.Location = new System.Drawing.Point(132, 175);
			this.tb_r14.Name = "tb_r14";
			this.tb_r14.Size = new System.Drawing.Size(77, 20);
			this.tb_r14.TabIndex = 17;
			// 
			// tb_r13
			// 
			this.tb_r13.Location = new System.Drawing.Point(132, 149);
			this.tb_r13.Name = "tb_r13";
			this.tb_r13.Size = new System.Drawing.Size(77, 20);
			this.tb_r13.TabIndex = 16;
			this.tb_r13.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
			// 
			// tb_r12
			// 
			this.tb_r12.Location = new System.Drawing.Point(132, 123);
			this.tb_r12.Name = "tb_r12";
			this.tb_r12.Size = new System.Drawing.Size(77, 20);
			this.tb_r12.TabIndex = 15;
			// 
			// tb_r15
			// 
			this.tb_r15.Location = new System.Drawing.Point(132, 201);
			this.tb_r15.Name = "tb_r15";
			this.tb_r15.Size = new System.Drawing.Size(77, 20);
			this.tb_r15.TabIndex = 14;
			// 
			// tb_r11
			// 
			this.tb_r11.Location = new System.Drawing.Point(132, 97);
			this.tb_r11.Name = "tb_r11";
			this.tb_r11.Size = new System.Drawing.Size(77, 20);
			this.tb_r11.TabIndex = 13;
			// 
			// tb_r10
			// 
			this.tb_r10.Location = new System.Drawing.Point(132, 71);
			this.tb_r10.Name = "tb_r10";
			this.tb_r10.Size = new System.Drawing.Size(77, 20);
			this.tb_r10.TabIndex = 12;
			// 
			// tb_r9
			// 
			this.tb_r9.Location = new System.Drawing.Point(132, 45);
			this.tb_r9.Name = "tb_r9";
			this.tb_r9.Size = new System.Drawing.Size(77, 20);
			this.tb_r9.TabIndex = 11;
			// 
			// tb_r8
			// 
			this.tb_r8.Location = new System.Drawing.Point(132, 19);
			this.tb_r8.Name = "tb_r8";
			this.tb_r8.Size = new System.Drawing.Size(77, 20);
			this.tb_r8.TabIndex = 10;
			// 
			// tb_r6
			// 
			this.tb_r6.Location = new System.Drawing.Point(29, 175);
			this.tb_r6.Name = "tb_r6";
			this.tb_r6.Size = new System.Drawing.Size(80, 20);
			this.tb_r6.TabIndex = 9;
			this.tb_r6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
			// 
			// tb_r5
			// 
			this.tb_r5.Location = new System.Drawing.Point(29, 149);
			this.tb_r5.Name = "tb_r5";
			this.tb_r5.Size = new System.Drawing.Size(80, 20);
			this.tb_r5.TabIndex = 8;
			// 
			// tb_r4
			// 
			this.tb_r4.Location = new System.Drawing.Point(29, 123);
			this.tb_r4.Name = "tb_r4";
			this.tb_r4.Size = new System.Drawing.Size(80, 20);
			this.tb_r4.TabIndex = 7;
			// 
			// tb_r7
			// 
			this.tb_r7.Location = new System.Drawing.Point(29, 201);
			this.tb_r7.Name = "tb_r7";
			this.tb_r7.Size = new System.Drawing.Size(80, 20);
			this.tb_r7.TabIndex = 6;
			this.tb_r7.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
			// 
			// tb_r3
			// 
			this.tb_r3.Location = new System.Drawing.Point(29, 97);
			this.tb_r3.Name = "tb_r3";
			this.tb_r3.Size = new System.Drawing.Size(80, 20);
			this.tb_r3.TabIndex = 5;
			// 
			// tb_r2
			// 
			this.tb_r2.Location = new System.Drawing.Point(29, 71);
			this.tb_r2.Name = "tb_r2";
			this.tb_r2.Size = new System.Drawing.Size(80, 20);
			this.tb_r2.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "R1";
			// 
			// tb_r1
			// 
			this.tb_r1.Location = new System.Drawing.Point(29, 45);
			this.tb_r1.Name = "tb_r1";
			this.tb_r1.Size = new System.Drawing.Size(80, 20);
			this.tb_r1.TabIndex = 2;
			this.tb_r1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "R0";
			// 
			// tb_r0
			// 
			this.tb_r0.Location = new System.Drawing.Point(29, 19);
			this.tb_r0.Name = "tb_r0";
			this.tb_r0.Size = new System.Drawing.Size(80, 20);
			this.tb_r0.TabIndex = 0;
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
			this.tabPage1.Size = new System.Drawing.Size(227, 372);
			this.tabPage1.TabIndex = 5;
			this.tabPage1.Text = "Patch Mem";
			// 
			// txtPMsz
			// 
			this.txtPMsz.Location = new System.Drawing.Point(208, 14);
			this.txtPMsz.Name = "txtPMsz";
			this.txtPMsz.Size = new System.Drawing.Size(16, 20);
			this.txtPMsz.TabIndex = 4;
			// 
			// txtPMVal
			// 
			this.txtPMVal.Location = new System.Drawing.Point(117, 14);
			this.txtPMVal.Name = "txtPMVal";
			this.txtPMVal.Size = new System.Drawing.Size(85, 20);
			this.txtPMVal.TabIndex = 3;
			// 
			// txtPMAddr
			// 
			this.txtPMAddr.Location = new System.Drawing.Point(3, 14);
			this.txtPMAddr.Name = "txtPMAddr";
			this.txtPMAddr.Size = new System.Drawing.Size(103, 20);
			this.txtPMAddr.TabIndex = 2;
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
			this.tabPage6.Size = new System.Drawing.Size(227, 372);
			this.tabPage6.TabIndex = 6;
			this.tabPage6.Text = "Breakpoints";
			// 
			// txtBrkAddr
			// 
			this.txtBrkAddr.Location = new System.Drawing.Point(70, 347);
			this.txtBrkAddr.Name = "txtBrkAddr";
			this.txtBrkAddr.Size = new System.Drawing.Size(94, 20);
			this.txtBrkAddr.TabIndex = 4;
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
			// lstBrkPoints
			// 
			this.lstBrkPoints.FormattingEnabled = true;
			this.lstBrkPoints.Location = new System.Drawing.Point(4, 0);
			this.lstBrkPoints.Name = "lstBrkPoints";
			this.lstBrkPoints.Size = new System.Drawing.Size(220, 316);
			this.lstBrkPoints.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(227, 372);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "CPU GBR/float";
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(227, 372);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "CPU/misc";
			// 
			// tabPage5
			// 
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(227, 372);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "MMR/misc";
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(733, 416);
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
			this.lstDisasm.FormattingEnabled = true;
			this.lstDisasm.IntegralHeight = false;
			this.lstDisasm.Location = new System.Drawing.Point(12, 12);
			this.lstDisasm.Name = "lstDisasm";
			this.lstDisasm.Size = new System.Drawing.Size(515, 263);
			this.lstDisasm.TabIndex = 6;
			// 
			// lstCallStack
			// 
			this.lstCallStack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstCallStack.FormattingEnabled = true;
			this.lstCallStack.IntegralHeight = false;
			this.lstCallStack.Location = new System.Drawing.Point(12, 287);
			this.lstCallStack.Name = "lstCallStack";
			this.lstCallStack.Size = new System.Drawing.Size(525, 121);
			this.lstCallStack.TabIndex = 7;
			this.lstCallStack.SelectedIndexChanged += new System.EventHandler(this.lstCallStack_SelectedIndexChanged);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.LargeChange = 100;
			this.vScrollBar1.Location = new System.Drawing.Point(530, 12);
			this.vScrollBar1.Maximum = 10000;
			this.vScrollBar1.Minimum = 1;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 262);
			this.vScrollBar1.TabIndex = 8;
			this.vScrollBar1.Value = 5000;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(555, 416);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(83, 23);
			this.button10.TabIndex = 9;
			this.button10.Text = "Console on/off";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(457, 417);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(69, 20);
			this.button11.TabIndex = 10;
			this.button11.Text = "Render NOW!";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// Debugger
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 443);
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
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
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
	}
}