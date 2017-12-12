#define WINDOWS

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGL.System.Fonts;
using CosmosGL.System.TrueType;

namespace WinDebugFrm
{
    public partial class Form1 : Form
    {
        private TrueTypeFont font = new TrueTypeFont(Cairo.CairoRegular_ttf);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var g = this.CreateGraphics();
            font.DrawLineAction = (x, y, x1, y1, color) => g.DrawLine(new Pen(Color.Black, 5), x, y, x1, y1);
            g.Clear(Color.White);

           
            float scale = 0.50f;

            font.DrawGlyph(null, 37, scale, scale, 50, 50);

          
            g.Flush();
        }
    }
}
