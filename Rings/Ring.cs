using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Rings
{
    public class Ring
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float RMin { get; set; }
        public float RMax { get; set; }
        public int SegmentCount { get; set; }
        int StreamLengthMax { get; set; }
        public float DeltaAngle { get; set; }
        public float Rotation { get; set; }
        float RotationSpeed { get; set; }
        public LineStream[] Streams { get; set; }
        public GraphicsPath element;
        public Pen Pen;
        public Color Brush;
        /// <param name="x">Координата X центра кольца</param>
        /// <param name="y">Координата Y центра кольца</param>
        /// <param name="rMin">Внутренний радиус</param>
        /// <param name="rMax">Внешний радиус</param>
        /// <param name="count">Количество сегментов</param>
        /// <param name="rotation">Поворот кольца</param>
        /// <param name="rotaionSpeed">Скорость вращения</param>
        /// <param name="maxSpeed">Максимальная скорость движения сигналов в кольце</param>
        /// <param name="lumSpeedMax">Максимальная скорость изменения интенсивности сигналов</param>
        /// <param name="c">Цвет заливки</param>
        /// <param name="fps"></param>
        public Ring(float x, float y, float rMin, float rMax, int count, float rotation, float rotaionSpeed, double maxSpeed, double lumSpeedMax, Color pen, Color fill, int fps)
        {
            X = x;
            Y = y;
            RMin = rMin;
            RMax = rMax;
            SegmentCount = count;
            Pen = new Pen(pen, 1F);
            Brush = fill;
            Rotation = rotation;
            RotationSpeed = rotaionSpeed / fps;
            StreamLengthMax = (int)Math.Sqrt(count);
            int streamCount = count / StreamLengthMax / 2;
            Streams = new LineStream[streamCount];
            for (int i = 0; i < streamCount;i++)
            {
                Streams[i] = new LineStream(
                    Program.rand.Next(StreamLengthMax / 2, StreamLengthMax),
                    Program.rand.NextD(0, count - 1), count,
                    Program.rand.NextD(maxSpeed / 4, maxSpeed) * Math.Pow(-1, Program.rand.Next(0, 2)) / fps,
                    Program.rand.NextD(lumSpeedMax / 2, lumSpeedMax) / fps, fill);
            }
            element = new GraphicsPath();
            DeltaAngle = 360F / count;
            element.AddArc(-rMax, -rMax, 2 * rMax, 2 * rMax, 0, DeltaAngle);
            element.AddArc(-rMin, -rMin, 2 * rMin, 2 * rMin, DeltaAngle, -DeltaAngle);
        }
        public void NextFrame()
        {
            Rotation += RotationSpeed;
            foreach(var s in Streams)
            {
                s.NextFrame();
            }
        }
    }
}
