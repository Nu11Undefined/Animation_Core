using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rings
{
    public class RenderWindow : Form
    {
        BufferedGraphicsContext ctx;
        BufferedGraphics buf;
        Graphics g;
        Bitmap bmp;
        Point MouseDownPoint { get; set; } = new Point(-1, -1);
        public RenderWindow(int w, int h)
        {
            Width = w;
            Height = h;
            FormBorderStyle = FormBorderStyle.None;
            ctx = BufferedGraphicsManager.Current;
            g = CreateGraphics();
            buf = ctx.Allocate(g, ClientRectangle);
            buf.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bmp = new Bitmap("2.jpg");
            // возможность перемещения окна
            MouseDown += (s, e) => MouseDownPoint = new Point(e.X, e.Y);
            MouseUp += (s, e) => MouseDownPoint = new Point(-1, -1);
            MouseMove += (s, e) =>
            {
                if (MouseDownPoint.X == -1) return;
                else Location = new Point(
                    Location.X + e.X - MouseDownPoint.X, 
                    Location.Y + e.Y - MouseDownPoint.Y);
            };
        }
        public void DrawRingSystem(RingSystem s)
        {
            buf.Graphics.DrawImage(bmp, 0, 0, ClientSize.Width, ClientSize.Height);
            buf.Graphics.DrawRingSystem(s);
            buf.Render();
        }
    }
}
