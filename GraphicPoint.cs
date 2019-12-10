using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2pointsNET4_8
{
    class GraphicPoint : GraphicObject
    {
        public static int pointR = 5;
        public PointF point { get; set; }
        public float X { get { return point.X - GraphicObject.NullPoint.X; } }
        public float Y { get { return -point.Y + GraphicObject.NullPoint.Y; } }

        private float z;
        public float Z { get { return z - GraphicObject.NullZ; } set { z = value; } }

        public GraphicPoint (float x, float y)
        {
            point = new PointF(x, y);
            Z = 1;
        }

        public static implicit operator PointF(GraphicPoint p)
        {
            return p.point;
        }

        public override bool CheckSelection(float clckX, float clckY)
        {
            if ((clckX - this.point.X) * (clckX - this.point.X) < GraphicPoint.pointR * GraphicPoint.pointR &&
                (clckY - this.point.Y) * (clckY - this.point.Y) < GraphicPoint.pointR * GraphicPoint.pointR)
                return true;
            else
                return false;

            
        }

        public override bool ChangeSelection(float x, float y)
        {
            isSelected = CheckSelection(x, y);
            return isSelected;
        }

        public override void DrawObject(Graphics graphics, Pen pen, Brush brush)
        {
            if (pen == null)
                pen = new Pen(Color.Black, 3);

            if (brush == null)
                brush = new SolidBrush(Color.Green);

            float r = GraphicPoint.pointR;
            

            //Ширина и высота задаются диаметром
            //Координаты указывают в левый верхний угол прямоугольника, в котором находится круг
            graphics.DrawEllipse(pen, point.X - r, point.Y - r, 2 * r, 2 * r);
            graphics.FillEllipse(brush, point.X - r, point.Y - r, 2 * r, 2 * r);

        }

        public override string GetInfo(Size size)
        {
            return "X:" + X.ToString() + " Y: " + (Y).ToString() + " Z: " + (Z).ToString();
        }

        //depricated
        public string GetInfo()
        {
            return "X:" + X.ToString();// + " Y: " + (size.Height - Y).ToString() + " Z: " + (Z).ToString();
        }

        public override void MoveObject(float X, float Y)
        {
            float x = point.X + X;
            float y = point.Y + Y;
            point = new PointF(x, y);
        }
    }
}
