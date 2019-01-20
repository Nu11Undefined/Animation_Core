using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace Rings
{
    public static class Program
    {
        static int fps = 30;
        static int w = 750, h = 500;
        public static readonly Random rand = new Random(1);
        static void Main()
        {
            Player play = new Player(w, h, 30);
            RingSystem rS = new RingSystem(
                play.Width / 2, play.Height / 2,
                play.Height/6, 
                play.Height/3,
                play.Height/60, 35, Color.LightBlue, Color.LightSeaGreen, fps);
            play.Play(rS);
            //Recorder rec = new Recorder(w, h, fps, fps * 35, 5*fps);
            //rec.Record(rS);
        }
    }
}
