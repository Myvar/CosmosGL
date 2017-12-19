using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGL.System.TrueType;
using LibTessDotNet;
using Point = System.Drawing.Point;

namespace CGLFontConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }


        public List<Triangle> TrianglateGlyph(Glyph g)
        {
            var re = new List<Triangle>();


            var tess = new LibTessDotNet.Tess();


            var c = 0;
            var last = 0;


            var vecs = new List<Vec3>();

            for (var i = 0; i < g.Points.Count; i++)
            {
                var glyphPointA = g.Points[i];


                vecs.Add(new Vec3()
                {
                    X = glyphPointA["x"],
                    Y = glyphPointA["y"],
                    Z = 0
                });

                if (i == g.ContourEnds[c])
                {
                    c += 1;
                    var contour = new LibTessDotNet.ContourVertex[vecs.Count];

                    for (var index = 0; index < vecs.Count; index++)
                    {
                        contour[index].Position = vecs[index];
                        contour[index].Data = Color.Black;
                    }
                    tess.AddContour(contour, LibTessDotNet.ContourOrientation.Clockwise);

                    vecs.Clear();
                }
            }

            tess.Tessellate(LibTessDotNet.WindingRule.EvenOdd, LibTessDotNet.ElementType.Polygons, 3);


            Console.WriteLine("Output triangles:");
            int numTriangles = tess.ElementCount;
            for (int i = 0; i < numTriangles; i++)
            {
                var v0 = tess.Vertices[tess.Elements[i * 3]].Position;
                var v1 = tess.Vertices[tess.Elements[i * 3 + 1]].Position;
                var v2 = tess.Vertices[tess.Elements[i * 3 + 2]].Position;
               

                re.Add(new Triangle(new Point((int) v0.X, (int) v0.Y),
                    new Point((int) v1.X, (int) v1.Y),
                    new Point((int) v2.X, (int) v2.Y)));
            }

            return re;
        }

        public int DrawGlyph(int x, int y, float size, Glyph g, TrueTypeFont f)
        {
            var scale = size / f.UnitsPerEm;

            var gr = panel1.CreateGraphics();
            gr.SmoothingMode = SmoothingMode.AntiAlias;

            gr.TranslateTransform(x, y + ((f.YMax - f.YMin) * scale));
            gr.ScaleTransform(scale, -scale);

            var triangles = TrianglateGlyph(g);

            var debugColors = new [] { Brushes.Red, Brushes.Blue};

            var cgg = new CGLFGlyph();
            cgg.YMin = g.YMin;
            cgg.YMax = g.YMax;
            cgg.XMin = g.XMin;
            cgg.XMax = g.XMax;

            for (var i = 0; i < triangles.Count; i++)
            {
                var triangle = triangles[i];
                cgg.Triangles.Add(triangle);

                gr.FillPolygon(checkBox1.Checked ? debugColors[i % 2] : Brushes.Black, triangle.ToArray());
            }

            Cglf.AddGlyph(cgg);

            return (int) (g.XMax * scale);
        }

        public void DrawFontFile(string name)
        {
            var f = new TrueTypeFont(File.ReadAllBytes(name));

            Cglf.UnitsPerEm = f.UnitsPerEm;
            Cglf.YMax = f.YMax;
            Cglf.YMin = f.YMin;
            Cglf.XMin = f.XMin;
            Cglf.XMax = f.XMax;

            var x = 0;
            var y = 0;
            var c = 0;

            var size = Convert.ToDouble(textBox1.Text);

            panel1.Height = 70 * f.Lenght;

            for (int i = 7; i < f.Lenght; i++)
            {
                x += DrawGlyph(x, y, (float) size, f.ReadGlyph(i), f) + 5;


                if (c > (panel1.Width / (int)(f.XMax * (size / f.UnitsPerEm))) )
                {
                    c = 0;
                    x = 0;
                    y += (int)(f.YMax * (size / f.UnitsPerEm)) + 5;
                }
                c++;
            }

            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        public CGLF Cglf { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.CreateGraphics().Clear(Color.White);

            Cglf = new CGLF();

            var fl = new OpenFileDialog();
            if (fl.ShowDialog() == DialogResult.OK)
            {
                DrawFontFile(fl.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fl = new SaveFileDialog();
            if (fl.ShowDialog() == DialogResult.OK)
            {
                Cglf.WriteToFile(fl.FileName);
            }
        }
    }

    public class Triangle
    {
        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        public GraphicsPath ToPath()
        {
            var re = new GraphicsPath();

            re.AddPolygon(ToArray());

            return re;
        }
        
        public Point[] ToArray()
        {
            return new[]
            {
                A, B, C
            };
        }

        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
    }
}