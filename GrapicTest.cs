using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
//using System.Math;

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
			this.Load += new System.EventHandler ( this.Form1_Load );
			this.Closed += new System.EventHandler ( this.Form1_Close );
			this.KeyUp += new KeyEventHandler ( this.Form1_KeyUp );
            this.ResumeLayout(false);

        }
		
	}
	
    public partial class Form1 : Form
    {
		private ThreadStart TS = null;
		private Thread T = null;
		private bool ProgramExit = true;
		private int KeyChar;
		
		public struct XY
		{
			public double x;
			public double y;
			
			public XY ( double dx, double dy )
			{
				x = dx;
				y = dy;
			}
			public XY ( int nx, int ny )
			{
				x = nx;
				y = ny;
			}
		}
		
		public class Object_07
		{
			public bool Enable = false;
			public double x;
			public double y;
			public XY obXY[7];
			public Object_00()
			{
				x = 0.0;
				y = 0.0;
				obXY[0] = new XY(0,0);
				obXY[1] = new XY(30,0);
				obXY[2] = new XY(30,10);
				obXY[3] = new XY(10,10);
				obXY[4] = new XY(10,30);
				obXY[5] = new XY(0,30);
				obXY[6] = new XY(0,0);
			}
			public void Move ( int nx, int ny)
			{
				x = (double)nx + x;
				y = (double)ny + y;
			}
			public void Set ( int nx, int ny)
			{
				x = (double)nx;
				y = (double)ny;
				
			}
			private void Renew()
			{
				for ( int i = 0; i <= 7 ; i ++)
				{
					obXY[i].x += x;
					obXY[i].y += y;
				}
				x=0.0;
				y=0.0;
			}
		}
		
		public class Me
		{
			public double x;
			public double y;
			public bool Enable = false;
			public Me()
			{
				x = 200.0;
				y = 200.0;
			}
			public void XY ( int nx, int ny)
			{
				x = (double)nx;
				y = (double)ny;
			}
			public void XY ( double dx, double dy )
			{
				x= dx;
				y= dy;
			}
		}
		
        public Form1()
        {
            InitializeComponent();
        }
		private void Form1_Load ( object sender, EventArgs e )
		{

			TS = new ThreadStart ( Form1_Main );
			T = new Thread ( TS );
			T.Start( );
		}
		private void Form1_Close ( object sender, EventArgs e )
		{
			ProgramExit = false;
			T.Join();
		} 
		private void Form1_Main ( )
		{
			Graphics G = CreateGraphics();
			Me MyXY = new Me();
			Object_07 ob07 = new Object_07();
			
			ob07.Set(180,50);
			ob07.Enable = true;
			MyXY.Enable = true;
			
			while ( ProgramExit )
			{
				if ( KeyChar != 0)
				{
					if(KeyChar == 37 )
					{
						MyXY.x -= 10;
					}
					else if ( KeyChar == 38 )
					{
						MyXY.y -= 10;
					}
					else if ( KeyChar == 39 )
					{
						MyXY.x += 10;
					}
					else if ( KeyChar == 40 )
					{
						MyXY.y += 10;
					}
					else
					{
						MyXY.x +=1;
					}
					KeyChar = 0;
					Form1_Graphic ( G, MyXY, ob07 );
				}
			}
			G.Dispose();
		}
		
		private void Form1_Graphic( Graphics G, Me MyXY, Object_07 ob07 )
		{
			G.Clear ( Color.White );
			if ( MyXY.Enable == true )
			{
				if ( ob07.Enalbe == true )
				{
					for ( int i = 0; i <=7; i++)
					{
						ob07.obXY[i]
					}
				}
			}
		}
		
		private void Form1_KeyUp ( object sender, KeyEventArgs e )
		{
			KeyChar = (int)e.KeyCode;
		}
		
		private void Form1_Paint( object sender, PaintEventArgs e )
		{

			
			
			//e.Graphics.DrawLine ( Pens.Black, 0,0,100,100);
		}
	}
	
}