using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2pointsNET4_8
{
    abstract class GraphicObject
    {
        public static PointF NullPoint = new PointF(0, 0);

        public static float NullZ = 0;
        public bool isSelected { get; set; }

        abstract public bool CheckSelection(float x, float y);

        abstract public bool ChangeSelection(float x, float y);
        abstract public void DrawObject(Graphics graphics, Pen pen, Brush brush);
        abstract public string GetInfo(Size size);
        abstract public void MoveObject(float X, float Y);
        abstract public void ApplyMatrix(float[][] matrix);
        abstract public void ApplyMatrixLocal(float[][] matrix);
    }
}