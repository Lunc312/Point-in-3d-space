using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace kg_1
{
    public partial class Form1 : Form
    {
        float a, b, c, x, y, z;
        Point3d[] Point3d_mas;
        Point[] Point2d_mas;
        Point[] PointComplex_mas;
        float height, width;
        int length = 10;
        public static void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
        public struct Point3d
        {
            public float x, y, z;
            public Point3d(float x,float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetDoubleBuffered(panel1);
            SetDoubleBuffered(panel2);
            Point3d_mas = new Point3d[14];
            Point2d_mas = new Point[14];
            PointComplex_mas = new Point[12];
            input();
            Spatial();
            Complex();
        }
        private void input()
        {
            this.x = trackBar1.Value;
            this.y = trackBar2.Value;
            this.z = trackBar3.Value;
            this.a = trackBar4.Value;
            this.b = trackBar5.Value;
            this.c = trackBar6.Value;
            Point3d_mas[0] = new Point3d(0, 0, 0); //O
            Point3d_mas[1] = new Point3d(-length, 0, 0); //x1
            Point3d_mas[2] = new Point3d(0, -length, 0); //y1
            Point3d_mas[3] = new Point3d(0, 0, -length); //z1
            Point3d_mas[4] = new Point3d(length, 0, 0); //x2
            Point3d_mas[5] = new Point3d(0, length, 0); //y2
            Point3d_mas[6] = new Point3d(0, 0, length); //z2
            Point3d_mas[7] = new Point3d(x, y, z); //T
            Point3d_mas[8] = new Point3d(x, 0, 0); //tx
            Point3d_mas[9] = new Point3d(0, y, 0); //ty
            Point3d_mas[10] = new Point3d(0, 0, z); //tz
            Point3d_mas[11] = new Point3d(x, y, 0); //t1
            Point3d_mas[12] = new Point3d(x, 0, z); //t2
            Point3d_mas[13] = new Point3d(0, y, z); //t3
        }
        private void Spatial()
        {
            height = panel1.Height;
            width = panel1.Width;
            for (int i = 0; i < 14; i++)
            {
                Point2d_mas[i] = Projection2D(Point3d_mas[i]);
            }
        }

        private void Complex() {
            height = panel2.Height;
            width = panel2.Width;
            PointComplex_mas[0] = ProjectionComplex(0, 0); // O
            PointComplex_mas[1] = ProjectionComplex(-length, 0); // ox
            PointComplex_mas[2] = ProjectionComplex(0, length); // oz
            PointComplex_mas[3] = ProjectionComplex(0, -length); // oy1
            PointComplex_mas[4] = ProjectionComplex(length, 0); // oy3
            PointComplex_mas[5] = ProjectionComplex(-x, 0); // Tx
            PointComplex_mas[6] = ProjectionComplex(0, -y); // Ty1
            PointComplex_mas[7] = ProjectionComplex(y, 0); // Ty3
            PointComplex_mas[8] = ProjectionComplex(0, z); // Tz
            PointComplex_mas[9] = ProjectionComplex(-x, -y); // T1
            PointComplex_mas[10] = ProjectionComplex(-x, z); // T2
            PointComplex_mas[11] = ProjectionComplex(y, z); // T3
        }

        private void PointDraw(Point p, string s, Graphics g,Control c, Pen pen)
        {
            float r = 2;
            Brush b = Brushes.Black;
            if (pen.Color == Color.Yellow)
                b = Brushes.Yellow;
            if (pen.Color == Color.Green)
                b = Brushes.Green;
            if (pen.Color == Color.Blue)
                b = Brushes.Blue;
            g.FillEllipse(b, new RectangleF((int)(p.X - r), (int)(p.Y - r), r * 2, r* 2));
            g.DrawEllipse(pen, p.X-r , p.Y-r , 2*r,2* r);
            g.DrawString(s, c.Font, Brushes.Black, p.X , p.Y +1);
        }
        private void AxisDraw(Point p1, Point p2, string s, Graphics g, Control c)
        {
            Pen pen = new Pen(Brushes.Black);
            g.DrawLine(pen, p1.X,p1.Y, c.Width/2,c.Height/2);
            pen.Width++;
            g.DrawLine(pen, p2.X, p2.Y, c.Width / 2, c.Height / 2);
            g.DrawString(s, c.Font, Brushes.Black, p2.X, p2.Y + 1);
        }

        private void SpatialDraw(Graphics g, Control c)
        {
            Pen pen = new Pen(Brushes.Black);
            AxisDraw(Point2d_mas[1], Point2d_mas[4], "X",g, c);
            AxisDraw(Point2d_mas[2], Point2d_mas[5], "Y", g, c);
            AxisDraw(Point2d_mas[3], Point2d_mas[6], "Z",g,c);
            g.DrawLine(pen, Point2d_mas[11], Point2d_mas[7]);
            g.DrawLine(pen,Point2d_mas[12], Point2d_mas[7]);
            g.DrawLine(pen,Point2d_mas[13], Point2d_mas[7]);
            g.DrawLine(pen,Point2d_mas[11], Point2d_mas[8]);
            g.DrawLine(pen, Point2d_mas[11], Point2d_mas[9]);
            g.DrawLine(pen, Point2d_mas[12], Point2d_mas[8]);
            g.DrawLine(pen, Point2d_mas[12], Point2d_mas[10]);
            g.DrawLine(pen, Point2d_mas[13], Point2d_mas[9]);
            g.DrawLine(pen, Point2d_mas[13], Point2d_mas[10]);
            g.DrawLine(pen, Point2d_mas[0], Point2d_mas[8]);
            g.DrawLine(pen, Point2d_mas[0], Point2d_mas[9]);
            g.DrawLine(pen, Point2d_mas[0], Point2d_mas[10]);
            PointDraw(Point2d_mas[0], "O",g,c,pen);
            PointDraw(Point2d_mas[7], "T", g, c, pen);
            PointDraw(Point2d_mas[8], "Tx", g, c, pen);
            PointDraw(Point2d_mas[9], "Ty", g, c, pen);
            PointDraw(Point2d_mas[10], "Tz", g, c, pen);
            pen.Color = Color.Blue;
            PointDraw(Point2d_mas[11], "T1", g, c, pen);
            pen.Color = Color.Green;
            PointDraw(Point2d_mas[12], "T2", g, c, pen);
            pen.Color = Color.Yellow;
            PointDraw(Point2d_mas[13], "T3", g, c, pen);
        }
        private void ComplexDraw(Graphics g, Control c)
        {
            Pen pen = new Pen(Brushes.Black);
            AxisDraw(PointComplex_mas[0], PointComplex_mas[1], "X(-Y3)", g, c);
            AxisDraw(PointComplex_mas[0], PointComplex_mas[2], "Z(-Y1)", g, c);
            AxisDraw(PointComplex_mas[0], PointComplex_mas[3], "Y1(-Z)", g, c);
            AxisDraw(PointComplex_mas[0], PointComplex_mas[4], "Y3(-X)", g, c);
            g.DrawLine(pen, PointComplex_mas[9], PointComplex_mas[6]);
            g.DrawLine(pen, PointComplex_mas[9], PointComplex_mas[5]);
            g.DrawLine(pen, PointComplex_mas[10], PointComplex_mas[5]);
            g.DrawLine(pen, PointComplex_mas[10], PointComplex_mas[8]);
            g.DrawLine(pen, PointComplex_mas[11], PointComplex_mas[8]);
            g.DrawLine(pen, PointComplex_mas[11], PointComplex_mas[7]);
            float r;
            if (y > 0)
            {
                r = PointComplex_mas[11].X - PointComplex_mas[0].X;
                g.DrawArc(pen, PointComplex_mas[0].X - r, PointComplex_mas[0].X - r, 2 * (PointComplex_mas[11].X - PointComplex_mas[0].X), 2 * (PointComplex_mas[11].X - PointComplex_mas[0].X), 0, 90);
            }
            if (y == 0) { }
            if (y < 0) {
                r = PointComplex_mas[0].X - PointComplex_mas[11].X;
                g.DrawArc(pen, PointComplex_mas[0].X - r, PointComplex_mas[0].X - r, 2 * (PointComplex_mas[0].X - PointComplex_mas[11].X), 2 * (PointComplex_mas[0].X - PointComplex_mas[11].X), 180, 90);
            }
            PointDraw(PointComplex_mas[5], "Tx",g,c,pen);
            PointDraw(PointComplex_mas[6], "Ty1", g, c, pen);
            PointDraw(PointComplex_mas[7], "Ty2", g, c, pen);
            PointDraw(PointComplex_mas[8], "Tz", g, c, pen);
            pen.Color = Color.Blue;
            PointDraw(PointComplex_mas[9], "T1", g, c, pen);
            pen.Color = Color.Green;
            PointDraw(PointComplex_mas[10], "T2", g, c, pen);
            pen.Color = Color.Yellow;
            PointDraw(PointComplex_mas[11], "T3", g, c, pen);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SpatialDraw(e.Graphics, panel1);
        }
        
       
        private Point Projection2D(Point3d point) {
          return new Point((int)(width/2- 10*point.x *Math.Cos(a* Math.PI / 180) - 10 * point.y* Math.Cos(b * Math.PI / 180)-10*point.z*Math.Cos(c * Math.PI / 180)), (int)(height / 2 + 10 *point.z* Math.Sin(c * Math.PI / 180) + 10* point.y* Math.Sin(b * Math.PI / 180) + 10 * point.x *Math.Sin(a * Math.PI / 180)));
        }

        private Point ProjectionComplex(float x, float y)
        {
            return new Point((int)(width / 2 + 10*x),(int)( height / 2 - 10*y));
        }
        
        private void trackBarScroll(object sender, EventArgs e)
        {
            input();
            string name = (sender as TrackBar).Name;
            switch (name)
            {
                case "trackBar1":
                    label14.Text = trackBar1.Value.ToString();
                    break;
                case "trackBar2":
                    label22.Text = trackBar2.Value.ToString();
                    break;
                case "trackBar3":
                    label23.Text = trackBar3.Value.ToString();
                    break;
                case "trackBar4":
                    label33.Text = trackBar4.Value.ToString();
                    break;
                case "trackBar5":
                    label34.Text = trackBar5.Value.ToString();
                    break;
                case "trackBar6":
                    label35.Text = trackBar6.Value.ToString();
                    break;
            }
            label39.Text = "T(" + trackBar1.Value + "," + trackBar2.Value + "," + trackBar3.Value + ")";
            Spatial();
            Complex();
            panel1.Invalidate();
            panel2.Invalidate();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ComplexDraw(e.Graphics, panel2);
        }
    }
}
