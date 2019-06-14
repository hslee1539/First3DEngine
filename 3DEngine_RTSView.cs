using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;


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
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form1";
            this.Text = "Form1";
			this.Load += new System.EventHandler( this.Form1_Load );
			this.Closed += new System.EventHandler ( this.Form1_Closed );
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.KeyUp += new KeyEventHandler ( this.Form1_KeyUp );
			this.MouseMove += new MouseEventHandler ( this.Form1_MouseMove );
            this.ResumeLayout(false);

        }
		
	}
	
    public partial class Form1 : Form
    {
//------------------------------Variables---------------------------------------------------Start
		public ThreadStart TS;
		public Thread T;
		public Object3D[] OB3D;
		public ObjectAngle[] OBAngle;
		public Object2D[] OB2D;
		public Camara Observer;
		public Graphics GG;
		public static int OBnum;
		public bool ProgramEXIT;
		public Point P;
		public int Modelnum;
		public Model3D[] Model;
		public double ScreenSize;
//-----------------------------Variables End------------------------------------------------END
//-----------------------------Structs and Class--------------------------------------------Start
		public struct Point3D
		{
			public double x;
			public double y;
			public double z;
			public double r;
			
			public void Set( double xx, double yy, double zz , double rr)
			{
				x = xx;
				y = yy;
				z = zz;
				r = rr;
			}
			public void Set ( double xx, double yy, double zz )
			{
				x = xx;
				y = yy;
				z = zz;
			}
			public void Plus ( Point3D p )
			{
				x += p.x;
				y += p.y;
				z += p.z;
			}
		}
		public struct Point2D
		{
			public double x;
			public double y;
			public double r;
			
			public void Set ( double xx, double yy, double rr)
			{
				x = xx;
				y = yy;
				r = rr;
			}
			public void Set ( double xx, double yy)
			{
				x = xx;
				y = yy;
			}
			public void Plus ( Point2D p )
			{
				x += p.x;
				y += p.y;
			}
		}
		//=====Main Start====
		public struct Object3D
		{
			public Point3D[] point;
			public Color C;
			public int Count;
			
			public void Create()
			{
				point = new Point3D[4];
				Count = 4;
			}
			public void Create( int n )
			{
				point = new Point3D[n];
				Count = 4;
			}
			
			public void Move ( Point3D movPoint )
			{
				for ( byte i = 0; i < Count; i++ )
				{
					point[i].Plus( movPoint );
				}
			}
		}
		public struct ObjectAngle
		{
			public Point2D[] point;
			public Color C;
			public int Count;
			
			public void Create()
			{
				point = new Point2D[4];
				Count = 4;
			}
			public void Create ( int n )
			{
				point = new Point2D[n];
				Count = n;
			}
			public void Move ( Point2D movPoint )
			{
				for ( byte i = 0; i < Count; i++ )
				{
					point[i].Plus( movPoint );
				}
			}
		}
		public struct Object2D
		{
			public Point[] point;
			public Color C;
			public int Count;
			public double d;
			
			public void Create ()
			{
				point = new Point[4];
				Count = 4;
			}
			public void Create ( int n )
			{
				point = new Point[ n ];
				Count = n;
			}
			public void GrahpicsStart ( Graphics G )
			{
				G.FillPolygon( new SolidBrush ( C ), point );
			}
		}
		public struct Camara 
		{
			public Point3D point;
			public Point2D angle;
			
			public void Create()
			{
				point = new Point3D();
				angle = new Point2D();
			}
		}
		public struct Model3D
		{
			public Object3D[] OB;
			private Object3D[] OB_;
			public Point2D angle;
			public Point3D start;
			public int[] Address;
			public int num;
			
			public void Create ( string FA, double x, double y, double z)
			{
				StreamReader SR = new StreamReader ( FA );

				num = (int)double.Parse ( (string)(SR.ReadLine()) );

				OB = new Object3D [ num ];
				OB_ = new Object3D [ num ];
				Address = new int[ num ];
				angle = new Point2D();
				start = new Point3D();
				start.Set( x,y,z);
				
				
				for ( int i = 0; i < num; i++)
				{
					OB[i].Create();
					OB_[i].Create();
				}
				for ( int i = 0; i < num; i++)
				{

					for ( int ii = 0; ii < 4; ii++)
					{
						OB_[i].point[ii].x = double.Parse ( (string)(SR.ReadLine()) );
						OB_[i].point[ii].y = double.Parse ( (string)(SR.ReadLine()) );
						OB_[i].point[ii].z = double.Parse ( (string)(SR.ReadLine()) );
						OB[i].point[ii].x = OB_[i].point[ii].x;
						OB[i].point[ii].y = OB_[i].point[ii].y;
						OB[i].point[ii].z = OB_[i].point[ii].z;
					}
					OB_[i].C = Color.FromArgb( (byte)double.Parse ( (string)(SR.ReadLine()) ), (byte)double.Parse ( (string)(SR.ReadLine()) ), (byte)double.Parse ( (string)(SR.ReadLine()) ));
					OB[i].C = OB_[i].C;
					Address[i] = OBnum;
					OBnum++;
				}
				SR.Close();
			}
			
			public void fRollingModel(Camara C)
			{
				for ( int i = 0; i < num; i++ )
				{
					for ( int ii = 0; ii < 4; ii ++ )
					{
						OB[i].point[ii] = fTransfrom( OB_[i].point[ii], angle - C.angle );
					}
				}
			}
		}
//-----------------------------Structs and Class End----------------------------------------END
//-----------------------------Methodes-----------------------------------------------------Start
		public Point3D fTransfrom ( Point3D point, Point2D Angle)
		{
			Point3D returnData = new Point3D();
			double x = point.x;
			double y = point.y;
			double z = point.z;
			
			double r = Math.Sqrt ( x*x + y*y + z*z );
			double tanAngle1 = Math.Atan2 ( z, Math.Sqrt( x*x + y*y ) ) + Angle.y;
			double tanAngle2 = Math.Atan2 ( y, x ) + Angle.x;
			double x2 = r * Math.Cos ( tanAngle1 ) * Math.Cos ( tanAngle2 );
			double y2 = r * Math.Cos ( tanAngle1 ) * Math.Sin ( tanAngle2 );
			double z2 = r * Math.Sin ( tanAngle1 );
			
			returnData.Set( x2 , y2 , z2 , r);
			return returnData;
		}
		public void ChangeModel()
		{
			
		}
		public Point2D f3DtoAngle ( Point3D m, Point3D c, Point2D Angle )
		{
			Point2D returnData = new Point2D();
			
			double x = m.x - c.x;
			double y = m.y - c.y;
			double z = m.z - c.z;
			double r = Math.Sqrt ( x*x + y*y + z*z );
			double tanAngle1 = Math.Atan2 ( z, Math.Sqrt( x*x + y*y ) ) + Angle.y;
			double tanAngle2 = Math.Atan2 ( y, x ) + Angle.x;
			double x2 = r * Math.Cos ( tanAngle1 ) * Math.Cos ( tanAngle2 );
			double y2 = r * Math.Cos ( tanAngle1 ) * Math.Sin ( tanAngle2 );
			double z2 = r * Math.Sin ( tanAngle1 );
			
			double r2 = Math.Sqrt( x2*x2 + y2*y2 + z2*z2 );
			double x3 = x2/r2;
			double y3 = y2/r2;
			double z3 = z2/r2;
			returnData.Set ( Math.Atan2 ( y3,x3) , Math.Atan2(z3, Math.Sqrt ( x3*x3 + y3*y3 ) ) );
			
			return returnData;
		}
		public Point fAngleto2D ( Point2D Angle )
		{
			double Size;
			double Root2 = Math.Sqrt(2);
			Size = 720 * 2 / ( Math.PI * ( 2 - Root2 ) );
			
			if ( Angle.x > Math.PI)
			{
				Angle.x -= 2 * Math.PI;
			}
			
			Angle.x = Angle.x * Math.Abs( Math.Cos( Angle.y ) );
			
			Angle.x = Angle.x + ( 16 - 8 * Root2 ) * Math.PI / 18;
			Angle.y = Angle.y + ( 2 - Root2) * Math.PI / 4;
			
			Angle.x = Angle.x * Size;
			Angle.y = Angle.y * Size;
			
			return new Point ( 1280 -(int)Angle.x, 720 -(int)Angle.y);
		}
		public Point2D f3Dto2D ( Point3D m, Point3D c, Point2D Angle )
		{
			Point2D returnData = new Point2D();
			
			double x = m.x - c.x;
			double y = m.y - c.y;
			double z = m.z - c.z;
			double r = Math.Sqrt ( x*x + y*y + z*z );
			double tanAngle1 = Math.Asin ( z / Math.Sqrt( x*x + y*y +z*z ) ) + Angle.y;
			double tanAngle2 = Math.Atan2 ( y, x ) + Angle.x;
			double x2 = r * Math.Cos ( tanAngle1 ) * Math.Cos ( tanAngle2 );
			double y2 = r * Math.Cos ( tanAngle1 ) * Math.Sin ( tanAngle2 );
			double z2 = r * Math.Sin ( tanAngle1 );
			
			double x3 = ScreenSize * x2 / Math.Abs(x2);
			double y3 = ScreenSize;
			if ( y > 0 && Math.Abs( x2 ) < y )
			{
				x3 = x2 * ScreenSize / y2;
				y3 = z2 * ScreenSize / y2;
			}
			
			returnData.r = r;
			returnData.Set ( x3, y3);
			
			return returnData;
		}
		public Point f2DtoScrnPoint ( Point2D P )
		{
			P.x += 640;
			P.y += 360;
			
			return new Point( (int)P.x, (int)P.y );
		}
		public void Model3DtoObject3D()
		{
			int OBindex = 0;
			for ( int i = 0; i < Modelnum ; i++ )
			{
				for ( int ii = 0; ii < Model[i].num; ii++)
				{
					for ( int iii= 0; iii < 4 ; iii++)
					{
						OB3D[OBindex].point[iii].x = Model[i].start.x + Model[i].OB[ii].point[iii].x;
						OB3D[OBindex].point[iii].y = Model[i].start.y + Model[i].OB[ii].point[iii].y;
						OB3D[OBindex].point[iii].z = Model[i].start.z + Model[i].OB[ii].point[iii].z;
					}
					OB3D[OBindex].C = Model[i].OB[ii].C;
					OBindex ++;
				}
			}
		}
		public void MAP_Load ()
		{
			ScreenSize = 640;
			Observer = new Camara();
			Observer.Create();
			Observer.point.Set(0,0,100);
			Observer.angle.Set(0, 6 );
			
			
			Modelnum = 2;
			OBnum = 0;
			Model = new Model3D[ Modelnum ];
			
			Model[0].Create( @"data01.gd", 50,50, 0);
			Model[1].Create( @"data01.gd", 25,25, 0); // Create() include OBnum++
			
			OB3D = new Object3D [OBnum];
			OBAngle = new ObjectAngle [OBnum];
			OB2D = new Object2D [OBnum];
			
			for ( int i = 0; i < OBnum; i++ )
			{
				OB3D[i].Create();
				OBAngle[i].Create( OB3D[i].Count );
				OB2D[i].Create ( OBAngle[i].Count );
			}
		}
		
//-----------------------------Methodes End-------------------------------------------------End
//=============================Grahpics Thread=================================================================
		public void ThreadingGraphics()
		{
			Thread.Sleep(5000);
			double Big = -1;
			
			while ( ProgramEXIT )
			{
				for ( int ii = 0; ii < OBnum; ii++)
				{
					for ( int i = 0; i < OB3D[ii].Count; i++)
					{
						OBAngle[ii].point[i] = f3Dto2D ( OB3D[ii].point[i], Observer.point, Observer.angle );
						
						OB2D[ii].point[i] = f2DtoScrnPoint ( OBAngle[ii].point[i] );
					}
					for ( int i = 0; i < OB3D[ii].Count; i++)
					{
						if( OBAngle[ii].point[i].r > Big )
						{
							Big = OBAngle[ii].point[i].r;
						}
					}
					OB2D[ii].d = Big;
					OB2D[ii].C = OB3D[ii].C;
					
				}
				GG.Clear(Color.FromArgb(255,255,255));
				for ( int ii = 0; ii < OBnum; ii++)
				{
					OB2D[ii].GrahpicsStart( GG );
				}

				Thread.Sleep(50);
				Observer.angle.x += 0.05;
				Observer.point.x -= 1;
				if ( Observer.point.x < -10)
				{
					Observer.point.x = 10;
				}
			}
			
		}

		
//============================G*T==============================================================================
//-----------------------------------------------------------------
        public Form1()
        {
			P = new Point(0xFFF,0xFFF);

			MAP_Load();
			
			ProgramEXIT = true;

            InitializeComponent();
        }
		
		private void Form1_Load ( object sender, EventArgs e)
		{
			//TS = new ThreadStart ( ThreadingGraphics );
			//T = new Thread ( TS );
			//GG = CreateGraphics();
			//T.Start();

		}
		
		private void Form1_Closed ( object sender, EventArgs e )
		{
			//T.Join();

		}
		
		private void Form1_Paint ( object sender, PaintEventArgs e )
		{
			GG = CreateGraphics();
			double Big = 0xffffff;
			double[] Sort = new double[OBnum];
			int[] SortName = new int [OBnum];
			double Sort_Big;
			int Sort_Num;
			
			Model3DtoObject3D();
			
		
			for ( int ii = 0; ii < OBnum; ii++)
			{	
				Big = 0xffffff;
				for ( int i = 0; i < OB3D[ii].Count; i++)
				{
					OBAngle[ii].point[i] = f3DtoAngle ( OB3D[ii].point[i], Observer.point, Observer.angle );
					
					OB2D[ii].point[i] = fAngleto2D ( OBAngle[ii].point[i] );
				}
				for ( int i = 0; i < OB3D[ii].Count; i++)
				{
					if( OBAngle[ii].point[i].r < Big )
					{
						Big = OBAngle[ii].point[i].r;
					}
				}
				OB2D[ii].d = Big;
				OB2D[ii].C = OB3D[ii].C;
				
			}
			for ( int i = 0; i < OBnum; i++)
			{
				Sort[i] = OB2D[i].d;
				SortName[i]= i;
			}
			for ( int i = 0; i < OBnum; i++)
			{
				Sort_Big = -1;
				Sort_Num = i;
				for ( int ii = i; ii < OBnum; ii++)
				{
					if ( Sort[ii] > Sort_Big )
					{
						Sort_Big = Sort[ii];
						Sort_Num = ii;
					}
				}
				Sort[Sort_Num] = Sort[i];
				Sort[i] = Sort_Big;
				SortName[Sort_Num] = SortName[i];
				SortName[i] = Sort_Num;
			}
			
			GG.Clear(Color.FromArgb(20,20,35));
			for ( int ii = 0; ii < OBnum; ii++)
			{
				OB2D[SortName[ii]].GrahpicsStart( GG );
				//Thread.Sleep(100);
			}
			
			
		}
		private void Form1_MouseMove ( object sender, MouseEventArgs e)
		{
			
		}
		private void Form1_KeyUp ( object sender, KeyEventArgs e )
		{
			switch ( (Int32)e.KeyCode )
			{
				case 37:
					Observer.point.y += 10;
					Form1_Paint(null,null);
					break;
				case 38:
					Observer.point.x += 10;
					Form1_Paint(null,null);
					break;
				case 39:

					Observer.point.y -= 10;
					Form1_Paint(null,null);
					break;
				case 40:
					Observer.point.x -= 10;
					Form1_Paint(null,null);
					break;
				case 98:
					Observer.point.z += 5;
					Form1_Paint(null,null);
					break;
				case 100:
					Observer.angle.x += 0.02;
					Form1_Paint(null,null);
					break;
				case 101:
					Observer.angle.y =0;
					Observer.angle.x =0;
					Form1_Paint(null,null);
					break;
				case 102:
					Observer.angle.x -= 0.02;
					Form1_Paint(null,null);
					break;
				case 104:
					Observer.point.z -= 5;
					Form1_Paint(null,null);
					break;
			}
		}
	}
		
		//---------------------------------------------------------

	
}