using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2pointsNET4_8
{
    class Line: GraphicObject
    {
       

        public static Random rand = new Random();
        public GraphicPoint A { get; set; }
        public GraphicPoint B { get; set; }




        public Line(int maxX, int maxY)
        {
            int x = rand.Next(0, maxX);
            int y = rand.Next(0, maxY);
            A = new GraphicPoint(x, y);

            x = rand.Next(0, maxX);
            y = rand.Next(0, maxY);
            B = new GraphicPoint(x, y);
        }
        public Line()
        {

        }

        //TODO: Проверить вертикальную и горизонтальную линии. 
        //TODO: Провести перпендикуляр к линии.
        public override bool CheckSelection(float clckX, float clckY)
        {
            Line line = this;
            int r = 5;
            float k = (line.B.Y - line.A.Y) / (line.B.X - line.A.X);
            float b = (-line.A.X * (line.B.Y - line.A.Y) / (line.B.X - line.A.X)) + line.A.Y;
            isSelected = false;

            //Проверка клика по линии (или на расстоянии r от линии)
            if (clckX < Math.Max(line.A.X, line.B.X) && clckX > Math.Min(line.A.X, line.B.X) &&
                clckY < Math.Max(line.A.Y, line.B.Y) && clckY > Math.Min(line.A.Y, line.B.Y) &&
                clckY - clckX * k - b < r && clckY - clckX * k - b > -r)
            {
                line.isSelected = true;
                line.A.isSelected = false;
                line.B.isSelected = false;
            }

            //Проверка клика по точке
            if (line.A.CheckSelection(clckX, clckY))
            {
                line.isSelected = true;
                line.A.isSelected = true;
                line.B.isSelected = false;
            }

            if (line.B.CheckSelection(clckX, clckY))
            {
                line.isSelected = true;
                line.A.isSelected = false;
                line.B.isSelected = true;
            }

            return isSelected;
        }

        public override void DrawObject(Graphics graphics, Pen pen, Brush brush)
        {
            if (pen == null)
                pen = new Pen(Color.Black, 3);

            if (brush == null)
                brush = new SolidBrush(Color.Green);

            float r = GraphicPoint.pointR;
            graphics.DrawLine(pen, A, B);

            A.DrawObject(graphics, pen, brush);

            B.DrawObject(graphics, pen, brush);
        }
    }
}
