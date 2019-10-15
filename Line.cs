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
            isSelected = false;



            float k = (line.B.Y - line.A.Y) / (line.B.X - line.A.X);
            float b = (-line.A.X * (line.B.Y - line.A.Y) / (line.B.X - line.A.X)) + line.A.Y;
            

            //Координаты точек
            float Ax = this.A.X, Ay = this.A.Y;
            float Bx = this.B.X, By = this.B.Y;

            //Вычисляем параметры прямой
            float A = Ay - By;
            float B = Bx - Ax;
            float C = Ax * By - Ay * Bx;
            float d = (float) ((Math.Abs(A*clckX + B*clckY + C)) / Math.Sqrt(A*A + B*B));

            //Проверка клика по линии (или на расстоянии r от линии)
            if (clckX < Math.Max(line.A.X, line.B.X) + r && clckX > Math.Min(line.A.X, line.B.X) - r &&
                clckY < Math.Max(line.A.Y, line.B.Y) + r && clckY > Math.Min(line.A.Y, line.B.Y) - r &&
                d < r)//clckY - clckX * k - b < r && clckY - clckX * k - b > -r)
                return true;

            //Проверка клика по точке
            if (line.A.CheckSelection(clckX, clckY))
                return true;

            if (line.B.CheckSelection(clckX, clckY))
                return true;

            return false;
        }

        public override bool ChangeSelection(float x, float y)
        {
            isSelected = CheckSelection(x, y);

            if (isSelected)
            {
                A.ChangeSelection(x, y);
                B.ChangeSelection(x, y);
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

        //TODO: понять, как получить реальную высоту объекта graphics
        public override string GetInfo(Size size)
        {
            //Параметры прямой
            float A = -1, B = -1, C = -1;

            //Координаты точек
            float Ax = this.A.X, Ay = size.Height - this.A.Y;
            float Bx = this.B.X, By = size.Height - this.B.Y;

            //Вычисляем параметры прямой
            A = Ay - By;
            B = Bx - Ax;
            C = Ax * By - Ay * Bx;
            //return "x:" + this.A.X.ToString() + " y: " + this.A.Y.ToString();
            return "A:" + A.ToString() + " B: " + B.ToString() + " C: " + C.ToString();
        }
    }
}
