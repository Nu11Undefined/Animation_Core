using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Rings
{
    public class RingSystem
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float RMax { get; set; }
        public Ring[] Rings { get; set; }
        public RingSystem(float x, float y, float rMin, float rMax, int count, int segmentCountMin, Color pen, Color fill, int fps)
        {
            X = x;
            Y = y;
            RMax = rMax;
            Rings = new Ring[count];
            float dR = (rMax - rMin) / count, r = rMin;
            for(int i = 0;i<count;i++, r+= dR)
            {
                Rings[i] = new Ring(
                    x, y,
                    r, r + dR,
                    (int)(segmentCountMin * (r+dR) / (rMin + dR)), (float)Program.rand.NextD(0, 2 * Math.PI),
                    (float)(Program.rand.NextD(3, 5) * Math.Pow(-1, Program.rand.Next(0, 2)))/(i/3+1),
                    0.5, 0.1, pen, fill, fps);
            }
        }
        public void NextFrame()
        {
            foreach(var r in Rings)
            {
                r.NextFrame();
            }
        }
    }
}
