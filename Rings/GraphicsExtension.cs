using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Rings
{
    public static class GraphicsExtension
    {
        public static void DrawRingSystem(this Graphics g, RingSystem s)
        {
            g.TranslateTransform(s.X, s.Y);
            foreach (var r in s.Rings)
            {
                g.DrawRing(r);
            }
            g.ResetTransform();
        }
        private static void DrawRing(this Graphics g, Ring r)
        {
            g.RotateTransform(r.Rotation);
            foreach(var s in r.Streams)
            {
                g.RotateTransform(r.DeltaAngle * (int)Math.Floor(s.Offset));
                for(int i = 0;i<s.Count;i++)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb((int)(255 * s.Intensity * s[i]), s.Brush)))
                        g.FillPath(sb,r.element);
                    g.RotateTransform(r.DeltaAngle);
                }
                g.RotateTransform(-r.DeltaAngle * (int)s.Offset - s.Count * r.DeltaAngle);
            }
            for (int i = 0; i < r.SegmentCount; i++)
            {
                g.DrawPath(r.Pen, r.element);
                g.RotateTransform(r.DeltaAngle);
            }
            g.RotateTransform(-r.Rotation);
        }
    }
}
