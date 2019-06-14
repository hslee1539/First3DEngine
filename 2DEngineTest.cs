using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;


namespace WindowsProgram1
{
	static class Program
	{
		[STAThread]
		static void Main ()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run ( new Form1 () );
		}
	}
	
	partial class Form1
	{
		private System.ComponentModel.IContainer components = null;
		protected override void Dispose ( bool disposing )
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
        }
		
		private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 302);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Closed += new System.EventHandler ( this.Form1_Closed );
            this.ResumeLayout(false);

        }
		
	}
	//-------------------------------------
	public class HS3D
	{
		public struct 
	}
	//--------------------------
    public partial class Form1 : Form
    {
		private ThreadStart TS = null;
		private Thread T = null;
		
        public Form1()
        {
			TS = new ThreadStart ( ThreadingGraphics );
			T = new Thread ( TS );
			T.Start ( );

            InitializeComponent();
        }
		
		public void ThreadingGraphics ( )
		{
			
		}
		
		private void Form1_Load ( object sender, EventArgs e )
		{

			T.Start();
			
		}
		private void Form1_Closed ( object sender, EventArgs e )
		{
			T.Join();
		}
		

		
		private void Form1_Paint( object sender, PaintEventArgs e )
		{
			
			//e.Graphics.DrawLine ( Pens.Black, 0,0,100,100);
		}
	}
	
}