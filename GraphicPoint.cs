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

        private PointF pointBeforeChanges;
        private float zBeforeChanges;

        public PointF point { get; set; }
        public float X 
        { 
            get { return point.X - GraphicObject.NullPoint.X; }
            set
            {
                float temp = value + GraphicObject.NullPoint.X;
                point = new PointF(temp, point.Y);
                pointBeforeChanges = point;
            }
        }
        public float Y 
        { 
            get { return point.Y - GraphicObject.NullPoint.Y; }//{ return -point.Y + GraphicObject.NullPoint.Y; }
            set
            {
                float temp = value + GraphicObject.NullPoint.Y;//float temp = -value + GraphicObject.NullPoint.Y;
                point = new PointF(point.X, temp);
                pointBeforeChanges = point;
            }
        }

        public float z;
        public float Z { get { return z - GraphicObject.NullZ; } set { z = value; zBeforeChanges = value; } }

        public GraphicPoint (float x, float y)
        {
            point = new PointF(x, y);
            Z = 0;

            zBeforeChanges = 0;
            pointBeforeChanges = point;
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
            return "X:" + X.ToString() + " Y: " + Y.ToString() + " Z: " + (Z).ToString();
        }

        //depricated
        public string GetInfo()
        {
            return "X:" + X.ToString();// + " Y: " + (size.Height - Y).ToString() + " Z: " + (Z).ToString();
        }

        public override void MoveObject(float X, float Y)
        {
            this.X = this.X + X;
            this.Y = this.Y + Y;
        }


        public override void ApplyMatrix(float[][] matrix)
        {
            GraphicPoint point = this;
            GraphicPoint changedPoint = new GraphicPoint(1, 1);
            float[] pointData = new float[] { point.pointBeforeChanges.X, point.pointBeforeChanges.Y, point.zBeforeChanges, 1 };//{ point.X, point.Y, point.Z, 1 };//
            float pqrs = 0;
            float tempX = 0, tempY = 0;

            for (int i = 0; i < 4; i++)
                switch (i)
                {
                    case 0: { tempX = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                    case 1: { tempY = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                    case 2: { changedPoint.z = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                    case 3: { pqrs = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                }
            changedPoint.point = new PointF(tempX, tempY);
            //changedPoint.X = tempX;
            //changedPoint.Y = tempY;
            for (int i = 0; i < 3; i++)
                switch (i)
                {
                    case 0: { tempX = changedPoint.point.X / pqrs; break; }
                    case 1: { tempY = changedPoint.point.Y / pqrs; break; }
                    case 2: { changedPoint.z = changedPoint.z / pqrs; break; }
                }
            changedPoint.point = new PointF(tempX, tempY);
            //changedPoint.X = tempX;
            //changedPoint.Y = tempY;
            
            this.point = changedPoint.point;
            this.z = changedPoint.z;
        }

        public override void ApplyMatrixLocal(float[][] matrix)
        {
            
            GraphicPoint changedPoint = new GraphicPoint(1, 1);
            float[] pointData = new float[] { this.X, this.Y, this.Z, 1 };//{ point.X, point.Y, point.Z, 1 };//
            float pqrs = 0;

            for (int i = 0; i < 4; i++)
                switch (i)
                {
                    case 0: { changedPoint.X = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                    case 1: { changedPoint.Y = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                    case 2: { changedPoint.Z = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                    case 3: { pqrs = ApplyRow(pointData, GetColumn(matrix, i)); break; }
                }
            //changedPoint.X = tempX;
            //changedPoint.Y = tempY;
            for (int i = 0; i < 3; i++)
                switch (i)
                {
                    case 0: { changedPoint.X = changedPoint.point.X / pqrs; break; }
                    case 1: { changedPoint.Y = changedPoint.point.Y / pqrs; break; }
                    case 2: { changedPoint.Z = changedPoint.Z / pqrs; break; }
                }
            //changedPoint.X = tempX;
            //changedPoint.Y = tempY;
            changedPoint.point = new PointF(changedPoint.point.X - GraphicObject.NullPoint.X, changedPoint.point.Y - GraphicObject.NullPoint.Y);
            changedPoint.z -= NullZ;
            this.point = changedPoint.point;
            this.z = changedPoint.z;
            //this.X = changedPoint.X;
            //this.Y = changedPoint.Y;
            //this.Z = changedPoint.Z;
        }


        private float ApplyRow(float[] pointData, float[] column)
        {
            float res = 0;

            for (int i = 0; i < 4; i++)
                res += pointData[i] * column[i];

            return res;
        }
        private float[] GetColumn(float[][] matrix, int column)
        {
            int N = matrix.Length;
            float[] resCol = new float[N];

            for (int i = 0; i < N; i++)
                resCol[i] = matrix[i][column];

            return resCol;
        }
    }
}
