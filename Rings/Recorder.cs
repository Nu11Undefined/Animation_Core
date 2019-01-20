using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Accord.Video.FFMPEG;

namespace Rings
{
    public class Recorder
    {
        VideoFileWriter vFW = new VideoFileWriter();
        Bitmap bmp;
        Bitmap bmpBack;
        Graphics g;
        int FPS { get; set; }
        int FrameCount { get; set; }
        int MarginFrameCount { get; set; }
        int Frame { get; set; }
        public Recorder(int w, int h, int fps, int frameCount, int marginFrame)
        {
            FPS = fps;
            FrameCount = frameCount;
            MarginFrameCount = marginFrame;
            bmp = new Bitmap(w, h);
            bmpBack = new Bitmap("2.jpg");
            g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
        public void Record(RingSystem rS)
        {
            rS.X = bmp.Width / 2;
            rS.Y = bmp.Height / 2;
            int alpha = 255;
            Color back = Color.FromArgb(0, 0, 5);
            vFW.Open("rings.mp4", bmp.Width, bmp.Height, FPS, VideoCodec.MPEG4, 10 * bmp.Width * bmp.Height);
            for(int i = 0;i<FrameCount + 2*MarginFrameCount;i++)
            {
                Console.WriteLine($"{100*(double)i/(FrameCount + 2*MarginFrameCount):f1}");
                //g.Clear(back);
                g.DrawImage(bmpBack, 0, 0, bmp.Width, bmp.Height);
                g.DrawRingSystem(rS);
                // затенение
                using (SolidBrush sB = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
                    g.FillRectangle(sB, 0, 0, bmp.Width, bmp.Height);
                vFW.WriteVideoFrame(bmp);
                rS.NextFrame();
                if (i < MarginFrameCount) alpha = (MarginFrameCount - i)*255/MarginFrameCount;
                if (i > FrameCount + MarginFrameCount) alpha = (i - MarginFrameCount - FrameCount)*255/MarginFrameCount;
            }
            vFW.Close();
        }
    }
}
