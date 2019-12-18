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
        
        public Pen LocalPen;
        public Line(PointF A, PointF B)
        {
            this.A = new GraphicPoint(A.X, A.Y);
            this.B = new GraphicPoint(B.X, B.Y);
        }
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



            float k = (line.B.point.Y - line.A.point.Y) / (line.B.point.X - line.A.point.X);
            float b = (-line.A.point.X * (line.B.point.Y - line.A.point.Y) / (line.B.point.X - line.A.point.X)) + line.A.point.Y;
            

            //Координаты точек
            float Ax = this.A.point.X, Ay = this.A.point.Y;
            float Bx = this.B.point.X, By = this.B.point.Y;

            //Вычисляем параметры прямой
            float A = Ay - By;
            float B = Bx - Ax;
            float C = Ax * By - Ay * Bx;
            float d = (float) ((Math.Abs(A*clckX + B*clckY + C)) / Math.Sqrt(A*A + B*B));

            //Проверка клика по линии (или на расстоянии r от линии)
            if (clckX < Math.Max(line.A.point.X, line.B.point.X) + r && clckX > Math.Min(line.A.point.X, line.B.point.X) - r &&
                clckY < Math.Max(line.A.point.Y, line.B.point.Y) + r && clckY > Math.Min(line.A.point.Y, line.B.point.Y) - r &&
                d < r)//clckY - clckX * k - b < r && clckY - clckX * k - b > -r)
            {
                isSelected = true;
                return true;
            }
            //Проверка клика по точке
            if (line.A.CheckSelection(clckX, clckY))
            {
                isSelected = true;
                return true;
            }

            if (line.B.CheckSelection(clckX, clckY))
            {
                isSelected = true;
                return true;
            }

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
            {
                if (LocalPen != null)
                    pen = LocalPen;
                else
                    pen = new Pen(Color.Black, 3);
            }

            if (brush == null)
                brush = new SolidBrush(Color.Green);

            float r = GraphicPoint.pointR;
            graphics.DrawLine(pen, A.point, B.point);

            if (this.isSelected)
            {
                A.DrawObject(graphics, pen, brush);
                B.DrawObject(graphics, pen, brush);
            }
        }

        //TODO: понять, как получить реальную высоту объекта graphics
        public override string GetInfo(Size size)
        {
            //Параметры прямой
            float A = -1, B = -1, C = -1;

            //Координаты точек
            float Ax = this.A.X, Ay = this.A.Y;//size.Height - this.A.Y;
            float Bx = this.B.X, By = this.B.Y;// size.Height - this.B.Y;

            //Вычисляем параметры прямой
            A = Ay - By;
            B = Bx - Ax;
            C = Ax * By - Ay * Bx;
            //return "x:" + this.A.X.ToString() + " y: " + this.A.Y.ToString();
            string pointA = this.A.GetInfo(size);
            string pointB = this.B.GetInfo(size);

            return "A:" + A.ToString() + " B: " + B.ToString() + " C: " + C.ToString() + "\n" +
                    "Точка А: " + pointA + "\n" + "Точка B: " + pointB + "\n";
        }

        public override void MoveObject(float X, float Y)
        {
            A.MoveObject(X, Y);
            B.MoveObject(X, Y);
        }

        public override void ApplyMatrix(float[][] matrix)
        {
            this.A.ApplyMatrix(matrix);
            this.B.ApplyMatrix(matrix);
        }

        public override void ApplyMatrixLocal(float[][] matrix)
        {
            this.A.ApplyMatrixLocal(matrix);
            this.B.ApplyMatrixLocal(matrix);
        }
    }
}
