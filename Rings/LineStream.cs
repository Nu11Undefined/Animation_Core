using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Rings
{
    /// <summary>
    /// Линейный поток информации
    /// </summary>
    public class LineStream
    {
        public int Count { get; set; }
        public double Offset { get; set; }
        public double MaxOffset { get; set; }
        double MovingSpeed { get; set; }
        /// <summary>
        /// Интенсивность проявления сигнала [0;1]
        /// Определяет непрозрачность отображения
        /// </summary>
        public double Intensity { get; set; }
        public double IntensitySpeed { get; set; }
        double[] Values { get; set; }
        public double this[int index]
        {
            get
            {
                return Values[index];
            }
            set
            {
                Values[index] = value;
            }
        }
        public Color Brush { get; set; }
        /// <param name="count">Количество элементов</param>
        /// <param name="offset">Смещение сигнала в проводнике</param>
        /// <param name="maxOffset">Длина проводника</param>
        /// <param name="speed">Скорость перемещения</param>
        /// <param name="intSpeed">Скорость изменения яркости</param>
        /// <param name="c">Цвет сигнала</param>
        public LineStream(int count, double offset, double maxOffset, double speed, double intSpeed, Color c)
        {
            Count = count;
            Values = new double[count];
            Refresh();
            MovingSpeed = speed;
            IntensitySpeed = intSpeed;
            Offset = offset;
            MaxOffset = maxOffset;
            Brush = c;
        }
        /// <summary>
        /// Переместить сигнал
        /// Изменить интенсивность
        /// </summary>
        public void NextFrame()
        {
            // перемещение
            Offset += MovingSpeed;
            if (Offset < 0) Offset += MaxOffset;
            if (Offset > MaxOffset) Offset -= MaxOffset;
            // яркость
            Intensity += IntensitySpeed;
            if (Intensity >= 1) InvertIntensity();
            if (Intensity <= 0) Refresh();
        }
        /// <summary>
        /// Обновить сигнал
        /// </summary>
        private void Refresh()
        {
            InvertIntensity();
            for (int i = 0; i < Count; i++)
            {
                Values[i] = Program.rand.NextDouble();
            }
        }
        private void InvertIntensity() => IntensitySpeed *= -1;
    }
}
