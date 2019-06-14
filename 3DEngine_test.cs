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
			this.Load = new System.EventHandler( this.Form1_Load );
			this.Closed = new System.EventHandler ( this.Form1_Closed );
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }
		
	}
	
    public partial class Form1 : Form
    {
		public ThreadStart TS;
		public Thread T;
		public int Max;
		public int ThreadMessage;
		public double size_ = 10; // Program size
		
		public int SortIndex = 24;
		public int[24] nSortData;
		public int Max = 6;
		
		public rMAP[6] arrayMAP;
		public rMAP[6] arrayMAP3D;
		public rMAP2D[6] arrayMAP2D;
		
		public Observer Me;
		
		public void SetMAP()
		{ //육각면체 
			arrayMAP[0].Point_[0].Set(0,0,0);
			arrayMAP[0].Point_[1].Set(4,0,0);
			arrayMAP[0].Point_[2].Set(4,4,0);
			arrayMAP[0].Point_[3].Set(0,4,0);
			arrayMAP[0].Start.Set(50,50,50);
			arrayMAP[0].C = Color.FromArgb ( 255,255,255);
			
			arrayMAP[1].Point_[0].Set(0,0,0);
			arrayMAP[1].Point_[1].Set(4,0,0);
			arrayMAP[1].Point_[2].Set(4,4,0);
			arrayMAP[1].Point_[3].Set(0,4,0);
			arrayMAP[1].Start.Set(50,50,54);
			arrayMAP[1].C = Color.FromArgb ( 50,50,50);
			
			arrayMAP[2].Point_[0].Set(0,0,0);
			arrayMAP[2].Point_[1].Set(4,0,0);
			arrayMAP[2].Point_[2].Set(4,0,4);
			arrayMAP[2].Point_[3].Set(0,0,4);
			arrayMAP[2].Start.Set(50,50,50);
			arrayMAP[2].C = Color.FromArgb ( 150,150,150);
			
			arrayMAP[3].Point_[0].Set(0,0,0);
			arrayMAP[3].Point_[1].Set(4,0,0);
			arrayMAP[3].Point_[2].Set(4,0,4);
			arrayMAP[3].Point_[3].Set(0,0,4);
			arrayMAP[3].Start.Set(50,54,50);
			arrayMAP[3].C = Color.FromArgb ( 150,150,150);
			
			arrayMAP[4].Point_[0].Set(0,0,0);
			arrayMAP[4].Point_[1].Set(0,4,0);
			arrayMAP[4].Point_[2].Set(0,4,4);
			arrayMAP[4].Point_[3].Set(0,0,4);
			arrayMAP[4].Start.Set(50,50,50);
			arrayMAP[4].C = Color.FromArgb ( 150,150,150);
			
			arrayMAP[5].Point_[0].Set(0,0,0);
			arrayMAP[5].Point_[1].Set(0,4,0);
			arrayMAP[5].Point_[2].Set(0,4,4);
			arrayMAP[5].Point_[3].Set(0,0,4);
			arrayMAP[5].Start.Set(54,50,50);
			arrayMAP[5].C = Color.FromArgb ( 150,150,150);
			
			
		}
		
		public struct Point3D
		{
			public double x;
			public double y;
			public double z;
			public double r;
			public void Set( double xx, double yy, double zz)
			{
				x = xx;
				y = yy;
				z = zz;
			}
		}
		public struct Point2D
		{
			public double x;
			public double y;
			public double r;
			public void Set ( double xx, double yy)
			{
				x = xx;
				y = yy;
			}
		}
		public struct Observer
		{
			public Point3D Point_;
			public Point2D angle;
			public void Set()
			{
				Point_.Set(52,100,52);
				angle.Set(0,0);
			}
		}
		public struct rMAP
		{
			public Point3D[4] Point_;
			public Color C;
			public Point3D start;
		}
		public struct rMAP2D
		{
			public Point2D[4] Point_;
			public Color C;
		}
		
		public int f3DGIRI ( Point3D Point1, Point3D Point2 )
		{
			return MATH.Sqrt(
				(Point1.x - Point2.x)*(Point1.x - Point2.x) +
				(Point1.y - Point2.y)*(Point1.y - Point2.Y) +
				(Point1.z - Point2.z)*(Point1.z - Point2.z)	
			);
		}
		public rMAP f3Dto2D ( Point3D Camara, rMAP MAP)// r;
		{
			rMAP returnMAP;
			for ( int i = 0; i< 4; i++)
			{
				returnMAP.Point_[i].r = f3DGIRI( Camara, MAP.Point_[i] );
				returnMAP.Point_[i].x = MATH.Abs(Camara.x - MAP.Point_[i].x - MAP.start.x) / returnMAP.Point_[i].r;
				returnMAP.Point_[i].y = MATH.Abs(Camara.y - MAP.Point_[i].y - MAP.start.y) / returnMAP.Point_[i].r;
				returnMAP.Point_[i].z = MATH.Abs(Camara.z - MAP.Point_[i].z - MAP.start.z) / returnMAP.Point_[i].r;
			}
			returnMAP.C = MAP.C;
			return returnMAP;
		}
		
		public rMAP2D fXYangle ( Point3D Camara, rMAP MAP)// this (rMAP)MAP.Point_ must exist in Camara cicle
		{
			rMAP2D return2DMAP;
			for ( int i = 0; i < 4; i++)
			{
				return2DMAP.Point_[i].x = MATH.Atan((MAP.Point_[i].y - Camara.y) / (MAP.Point_[i].x - Camara.x));
				return2DMAP.Point_[i].y = MATH.Atan((MAP.Point_[i].z - Camara.z) 
				return2DMAP.Point_[i].r = MAP.Point_[i].r
				/ MATH.Sqrt( (MAP.Point_[i].x - Camara.x)*(MAP.Point_[i].x - Camara.x) + (MAP.Point_[i].y - Camara.y)*(MAP.Point_[i].y - Camara.y) ) );
			}
			return2DMAP.C = MAP.C;
			return return2DMAP;
		}
//-----------------------------------------------------------------
        public Form1()
        {
			
			SetMAP();
			Me.Set();
			
            InitializeComponent();
        }
		
		private Form1_Load ( object sender, EventArgs e)
		{
			TS = new ThreadStart ( ThreadingGraphics(0) );
			ThreadMessage = 1;
			for ( int n = 1; n< 36; n++)
			{
				TS += ThreadStart ( ThreadingGraphics ( n ) );
			}
			T = new Thread ( TS );
			T.Start();
		}
		
		private Form1_Closed ( object sender, EventArgs e )
		{
			T.Join();
			ThreadMessege = -1024;
		}
		
		
		//---------------------------------------------------------
		public void ThreadingGraphics ( int n )
		{
			for (;ThreadMessage > 0;) // Becase it needs to EXIT Threading
			{

				if ( ThreadMessage <38) // For Stopping Thread...
				{\
					ThreadMessage ++;
					// Here is Threading
					for ( int i = n; i < Max; i += 36)
					{
						arrayMAP3D[i] = f3Dto2D ( Me.Point_ , arrayMAP[i] );
						arrayMAP2D[i] = fXYangle ( Me.Point_, arrayMAP3D[i]);
						
					}
					
					ThreadMessage ++;
				}
				if ( ThreadMessage == 73)
				{
					int big;
					int small;
					double data;
					int iii=-1;
					ThreadMessage = 100;
					
					for ( int i = 0; i < Max; i++)
					{
						big = 0;
						small = 0;
						for ( int ii = 0; ii < 3 ; ii++)
						{
							iii++
							if( arrayMAP2D[i].Point_[ii].r < arrayMAP2D[i].Point_[ii + 1].r )
							{
								nSortData[iii] = 
							}
							
						}
					}
					
					ThreadMessage = 1;
				}
			}
		}
	}
	
}