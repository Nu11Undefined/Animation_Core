using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Rings
{
    public static class Ext
    {
        public static double NextD(this Random rand, double min, double max)
        {
            return min + (max - min) * rand.NextDouble();
        }
    }
}
