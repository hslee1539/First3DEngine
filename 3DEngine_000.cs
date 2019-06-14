using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
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
			this.Load += new System.EventHandler( this.Form1_Load );
			this.Closed += new System.EventHandler ( this.Form1_Closed );
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }
		
	}
	
    public partial class Form1 : Form
    {
		public ThreadStart TS;
		public Thread T;
		public Object4 OB;
		public Object4 OB_C;
		public Object4_2D OB_2D;
		public Camara Observer;
		
		
		public struct Point3D
		{
			public double x;
			public double y;
			public double z;
			public double r;
			
			public void Set ( double xx, double yy, double zz)
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
			public Point point()
			{
				return new PointF((float)x,(float)y);
			}
			
		}
		
		public struct Object4
		{
			Point3D[] point;
			Color C;

			public void Create()
			{
				point = new Point3D[4]
			}
			
			public void Move ( Point3D p )
			{
				for ( int i = 0; i < 4 ; i++)
				{
					point[i].Set (
						point.x + p.x,
						point.y + p.y,
						point.z + p.z
					);
				}
			}
			public void BasicSet ( )
			{
				point[0].Set ( 50, 50,50);
				point[1].Set ( 100, 50, 50);
				point[2].Set ( 100, 100, 50);
				point[3].Set ( 50, 100, 50);
				C = new Color.FromArgb(255,255,255);
			}
		}
		public struct Object4_2D
		{
			Point2D[] point;
			Color C;

			public void Create()
			{
				point = new Point2D[4];
			}
			public PointF[] ArrayPoint ( )
			{
				PointF[] p = new PointF[4];
				for ( int i = 0; i < 4; i++)
				{
					p[i] = point[i].point;
				}
				return p;
			}
		}
		public struct Camara
		{
			Point3D point;
			PointF Angle;
			
			public void Create()
			{
				point = new Point3D();
				Angle = new PointF ( 0f,0f);
			}
			
		}
		
		public Point2D f3DtoAngle ( Point3D m, Point3D c)
		{
			Point2D returnPoint2D = new Point2D();
			double x;
			double y;
			double z;
			double r;
			double rr;
			rr = (m.x - c.x)*(m.x - c.x ) + (m.y - c.y)*(m.y - c.y);
			r = Math.Sqrt ( rr + ( m.z - c.z )*( m.z - c.z ) );
			
			
			x = m.x - (r + 1.0)*c.x;
			y = m.y - (r + 1.0)*c.y;
			z = m.z - (r + 1.0)*c.z;
			
			returnPoint2D.Set ( 
				y/x,
				z/ Math.Sqrt(rr)
			);
			returnPoint2D.r = r;
			return returnPoint2D;
		}
		public Point2D f3DtoAngle2 ( Point3D m, Point3D c )
		{
			Point2D returnPoint2D = new Point2D();
			
			returnPoint2D.Set(
				Math.Atan(m.y-c.y , m.x - c.x),
				Math.Atan(m.z - c.z, Math.Sqrt( (m.x - c.x)*(m.x - c.x ) + (m.y - c.y)*(m.y - c.y) ) )
			);
			returnPoint2D.r = Math.Sqrt((m.x - c.x)*(m.x - c.x ) + (m.y - c.y)*(m.y - c.y) + ( m.z - c.z )*( m.z - c.z ));
			return returnPoint2D;
		}
		
		public Point3D fTransFormXYZ ( Point3D m, Point3D c, PointF angle)
		{
			Point3D returnPoint3D = new Point3D();
			
			double r;
			double rr;
			double x;
			double y;
			double z;
			
			double returnAtan;
			double returnAtan2;
			
			x = m.x - c.x;
			y = m.y - c.y;
			z = m.z - c.z;
			
			r = Math.Sqrt ( x*x + y*y );
			rr = Math.Sqrt ( x*x + y*y + z*z );
			
			returnAtan = Math.Atan2(z,r);
			returnAtan2 = Math.Atan2 ( y,z );
			
			returnPoint3D.Set(
				rr * Math.Cos( returnAtan + angle.y ) * Math.Cos ( returnAtan2 + Angle.x ),
				rr * Math.Cos( returnAtan + angle.y ) * Math.Sin ( returnAtan2 + angle.x ),
				rr * Math.Sin( returnAtan + angle.y )
			);
			returnPoint3D.r = rr;
			return returnPoint3D;
		}


		
		public int f3DGIRI ( Point3D Point1, Point3D Point2 )
		{
			return MATH.Sqrt(
				(Point1.x - Point2.x)*(Point1.x - Point2.x) +
				(Point1.y - Point2.y)*(Point1.y - Point2.Y) +
				(Point1.z - Point2.z)*(Point1.z - Point2.z)	
			);
		}
		
		
		
		public void ThreadingGraphics()
		{
			Graphics G = CreateGrahpics();
			while ( true )
			{
				for ( int i = 0; i < 4; i++)
				{
					OB_C[i] = fTransFormXYZ ( OB.point[i], Observer.point , Observer.Angle );
					OB_2D[i]= f3DtoAngle2 ( OB_C.Point[i], Observer.point );
				}
				OB_2D.C = OB.C;
				G.FillPolygon(new SolidBrush( OB_2D.C ), OB_2D.ArrayPoint() );
			}
		}

		

//-----------------------------------------------------------------
        public Form1()
        {
			OB = new Object4();
			OB_C = new Object4();
			OB_2D = new Object4_2D();
			
			OB.Create();
			OB_C.Create();
			OB_2D.Create();
			
			OB.BasicSet();
			
			Observer = new Camara();
			Observer.Create();
			
			Observer.point.Set(25,25,200);

            InitializeComponent();
        }
		
		private void Form1_Load ( object sender, EventArgs e)
		{
			TS = new ThreadStart ( ThreadingGraphics );
			T = new Thread ( TS );
			T.Start();

		}
		
		private void Form1_Closed ( object sender, EventArgs e )
		{
			T.Join();

		}
	}
		
		//---------------------------------------------------------

	
}