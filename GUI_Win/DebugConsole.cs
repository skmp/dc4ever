// DEBUG CONSOLE EXAMPLE v1.1
// Code by Kévin Drapel - 2002
// http://www.calodox.org
//
// Thanks to Richard D for the TraceListener idea
//
// Do what you want with this source.
//

#define TRACE

using System;

using System.Diagnostics;
#if nrt
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
#endif


namespace System
{
    #if nrt
	/// <summary>
	/// Summary description for DebugConsole.
	/// </summary>
	public class DebugConsoleWrapper : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button BtnSave;
		private System.Windows.Forms.Button BtnClear;
		private System.Windows.Forms.SaveFileDialog SaveFileDlg;
		private System.Windows.Forms.CheckBox CheckScroll;
		private System.Diagnostics.DefaultTraceListener Tracer = new System.Diagnostics.DefaultTraceListener();
		private System.Windows.Forms.ColumnHeader Col1;
		private System.Windows.Forms.ColumnHeader Col2;
		private System.Windows.Forms.ListView OutputView;
		private System.Windows.Forms.CheckBox CheckTop;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ColumnHeader Col3;
		private ListViewItem.ListViewSubItem CurrentMsgItem = null;
		private int EventCounter=0;
		public StringBuilder Buffer = new StringBuilder();

		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		private System.ComponentModel.Container components = null;

		public DebugConsoleWrapper()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
            DC4Ever.emu.runsh = false;//stop cpu if it is running..
            Application.Exit ();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnClear = new System.Windows.Forms.Button();
			this.SaveFileDlg = new System.Windows.Forms.SaveFileDialog();
			this.CheckScroll = new System.Windows.Forms.CheckBox();
			this.OutputView = new System.Windows.Forms.ListView();
			this.Col1 = new System.Windows.Forms.ColumnHeader();
			this.Col2 = new System.Windows.Forms.ColumnHeader();
			this.Col3 = new System.Windows.Forms.ColumnHeader();
			this.CheckTop = new System.Windows.Forms.CheckBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnSave
			// 
			this.BtnSave.Location = new System.Drawing.Point(8, 16);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(64, 24);
			this.BtnSave.TabIndex = 8;
			this.BtnSave.Text = "Save";
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnClear
			// 
			this.BtnClear.Location = new System.Drawing.Point(80, 16);
			this.BtnClear.Name = "BtnClear";
			this.BtnClear.Size = new System.Drawing.Size(64, 24);
			this.BtnClear.TabIndex = 8;
			this.BtnClear.Text = "Clear";
			this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
			// 
			// CheckScroll
			// 
			this.CheckScroll.Checked = true;
			this.CheckScroll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckScroll.Location = new System.Drawing.Point(152, 16);
			this.CheckScroll.Name = "CheckScroll";
			this.CheckScroll.Size = new System.Drawing.Size(80, 16);
			this.CheckScroll.TabIndex = 8;
			this.CheckScroll.Text = "autoscroll";
			this.CheckScroll.CheckedChanged += new System.EventHandler(this.CheckScroll_CheckedChanged);
			// 
			// OutputView
			// 
			this.OutputView.AutoArrange = false;
			this.OutputView.BackColor = System.Drawing.Color.MediumAquamarine;
			this.OutputView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.Col1,
																						 this.Col2,
																						 this.Col3});
			this.OutputView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OutputView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OutputView.ForeColor = System.Drawing.Color.Black;
			this.OutputView.Location = new System.Drawing.Point(0, 0);
			this.OutputView.Name = "OutputView";
			this.OutputView.Size = new System.Drawing.Size(640, 200);
			this.OutputView.TabIndex = 7;
			this.OutputView.View = System.Windows.Forms.View.Details;
			// 
			// Col1
			// 
			this.Col1.Text = "#";
			this.Col1.Width = 30;
			// 
			// Col2
			// 
			this.Col2.Text = "Time";
			this.Col2.Width = 99;
			// 
			// Col3
			// 
			this.Col3.Text = "Message";
			this.Col3.Width = 506;
			// 
			// CheckTop
			// 
			this.CheckTop.Location = new System.Drawing.Point(240, 16);
			this.CheckTop.Name = "CheckTop";
			this.CheckTop.Size = new System.Drawing.Size(96, 16);
			this.CheckTop.TabIndex = 8;
			this.CheckTop.Text = "always on top";
			this.CheckTop.CheckedChanged += new System.EventHandler(this.CheckTop_CheckedChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.BtnSave);
			this.panel2.Controls.Add(this.BtnClear);
			this.panel2.Controls.Add(this.CheckScroll);
			this.panel2.Controls.Add(this.CheckTop);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 200);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(640, 48);
			this.panel2.TabIndex = 8;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
			// 
			// DebugConsoleWrapper
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 248);
			this.Controls.Add(this.OutputView);
			this.Controls.Add(this.panel2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(390, 160);
			this.Name = "DebugConsoleWrapper";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Debug Console";
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void CreateEventRow()
		{
			DateTime d=DateTime.Now;

			// create a ListView item/subitems : [event nb] - [time] - [empty string]
			string msg1 = (++EventCounter).ToString();
			string msg2 = d.ToLongTimeString();
			ListViewItem elem = new ListViewItem(msg1);
			elem.SubItems.Add(msg2);
			elem.SubItems.Add("");
			this.OutputView.Items.Add(elem);

			// we save the message item for incoming text updates
			CurrentMsgItem=elem.SubItems[2];
		}


		public void UpdateCurrentRow(bool CreateRowNextTime)
		{
			if (CurrentMsgItem==null) CreateEventRow();
			CurrentMsgItem.Text=Buffer.ToString();

			// if null, a new row will be created next time this function is called
			if (CreateRowNextTime==true) CurrentMsgItem=null;

			// this is the autoscroll, move to the last element available in the ListView
			if (this.CheckScroll.CheckState == CheckState.Checked)
			{
				this.OutputView.EnsureVisible(this.OutputView.Items.Count-1);
                Windows.Forms.Application.DoEvents();
            }
		}

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			this.SaveFileDlg.Filter="Text file (*.txt)|*.txt|All files (*.*)|*.*" ;
			this.SaveFileDlg.FileName="log.txt";
			this.SaveFileDlg.ShowDialog();

			FileInfo  fileInfo = new FileInfo(SaveFileDlg.FileName);

			// create a new textfile and export all lines
			StreamWriter s = fileInfo.CreateText();
			for (int i=0;i<this.OutputView.Items.Count;i++)
			{
				StringBuilder sb=new StringBuilder();
				sb.Append(this.OutputView.Items[i].SubItems[0].Text);
				sb.Append("\t");
				sb.Append(this.OutputView.Items[i].SubItems[1].Text);
				sb.Append("\t");
				sb.Append(this.OutputView.Items[i].SubItems[2].Text);
				s.WriteLine(sb.ToString());
			}

			s.Close();
		}

		private void BtnClear_Click(object sender, System.EventArgs e)
		{
			this.EventCounter=0;
			this.OutputView.Items.Clear();
			this.CurrentMsgItem=null;
			this.Buffer = new StringBuilder();
		}

		private void CheckTop_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.CheckTop.CheckState == CheckState.Checked) 
				this.TopMost = true;
			else
				this.TopMost = false;
		}

		private void CheckScroll_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.CheckScroll.CheckState == CheckState.Checked)
				this.OutputView.EnsureVisible(this.OutputView.Items.Count-1);
		}

		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

	}
	// DebugConsole Singleton
    #endif
	public class DebugConsole : TraceListener
	{

#if nrt
		public DebugConsoleWrapper DebugForm = new DebugConsoleWrapper();
#else	
        //public DebugConsoleWrapper DebugForm ;
#endif
        // if this parameter is set to true, a call to WriteLine will always create a new row
		// (if false, it may be appended to the current buffer created with some Write calls)
		public bool UseCrWl = true;

		public DebugConsole() 
		{
#if nrt
			DebugForm.Show();
#endif
		}

		public void Init(bool UseDebugOutput, bool UseCrForWriteLine)
		{
            #if nrt
			if (UseDebugOutput==true)
				Debug.Listeners.Add(this);
			else
				Trace.Listeners.Add(this);

			this.UseCrWl = UseCrForWriteLine;
#endif
		}

		override public void Write(string message) 
		{   
            #if nrt
			DebugForm.Buffer.Append(message);
			DebugForm.UpdateCurrentRow(false);
#else
            Console.Write(message);
#endif
		}

		override public void WriteLine(string message) 
		{
            #if nrt
            //return;
            if (this.UseCrWl==true) 
			{
				DebugForm.CreateEventRow();
				DebugForm.Buffer=new StringBuilder();
			}

			DebugForm.Buffer.Append(message); 
			DebugForm.UpdateCurrentRow(true);
			DebugForm.Buffer = new StringBuilder(); 
#else
            Console.WriteLine(message);
#endif
		}
	}

}
