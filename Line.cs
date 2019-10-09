using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2pointsNET4_8
{
    class Line
    {
        public static int pointR = 5;

        public static Random rand = new Random();
        public PointF A { get; set; }
        public PointF B { get; set; }

        public Line(int maxX, int maxY)
        {
            int x = rand.Next(0, maxX);
            int y = rand.Next(0, maxY);
            A = new PointF(x, y);

            x = rand.Next(0, maxX);
            y = rand.Next(0, maxY);
            B = new PointF(x, y);
        }
        public Line()
        {

        }
    }
}
