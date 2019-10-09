using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2pointsNET4_8
{
    class MyPoint
    {
        public static Random rand = new Random();
        //Координаты точки
        public int x { get; set; }
        public int y{ get; set; }

        //Радиус - размер точки
        public int r { get; set; }

        public MyPoint(int maxX, int maxY)
        {
            x = rand.Next(0, maxX);
            y = rand.Next(0, maxY);
            r = 10;
        }
    }
}
