using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multimedia;

namespace Rings
{
    public class Player
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int FPS { get; set; }
        Timer timer;
        RenderWindow win;
        public Player(int w, int h, int fps)
        {
            Width = w;
            Height = h;
            FPS = fps;
            timer = new Timer() { Period = 1000 / fps };
            win = new RenderWindow(w, h);
            win.FormClosed += (s, e) => timer.Dispose();
            win.KeyUp += (s, e) =>
            {
                switch(e.KeyCode)
                {
                    case System.Windows.Forms.Keys.Space:
                        if (timer.IsRunning) timer.Stop();
                        else timer.Start();
                        break;
                    case System.Windows.Forms.Keys.Escape:
                        win.Close();
                        break;
                }
            };
        }
        public void Play(RingSystem rS)
        {
            timer.Tick += (s, e) => win.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
            {
                rS.NextFrame();
                win.DrawRingSystem(rS);
            }));
            timer.Start();
            win.ShowDialog();
        }
    }
}
