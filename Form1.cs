using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2pointsNET4_8
{
    public partial class FormMain : Form
    {
        List<Line> lines = new List<Line>(10);
        bool isMouseDown = false;
        Line selectedLine = new Line();
        PointF selectedPoint = new Point();
        //-1 - ничего
        //0 - линия
        //1 - точка А
        //2 - точка B
        int selectMode = -1;

        public FormMain()
        {
            
            InitializeComponent();
        }

        private void button2PointsLine_Click(object sender, EventArgs e)
        {
            lines.Add(new Line(pictureBox1.Width, pictureBox1.Height));
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen usualPen = new Pen(Color.Green, 3);
            SolidBrush solidBrush = new SolidBrush(Color.Green);


            Pen specPen = new Pen(Color.Orange, 3);
            SolidBrush solidBrushSpec = new SolidBrush(Color.Orange);
            foreach (var line in lines)
            {
                if (line != selectedLine)
                {
                    int r = Line.pointR;
                    e.Graphics.DrawLine(new Pen(Color.Black, 3), line.A, line.B);

                    //Ширина и высота задаются диаметром
                    //Координаты указывают в левый верхний угол прямоугольника, в котором находится круг
                    e.Graphics.DrawEllipse(usualPen, line.A.X - r, line.A.Y - r, 2 * r, 2 * r);
                    e.Graphics.FillEllipse(solidBrush, line.A.X - r, line.A.Y - r, 2 * r, 2 * r);

                    e.Graphics.DrawEllipse(usualPen, line.B.X - r, line.B.Y - r, 2 * r, 2 * r);
                    e.Graphics.FillEllipse(solidBrush, line.B.X - r, line.B.Y - r, 2 * r, 2 * r);
                }
                else
                {
                    int r = Line.pointR;
                    e.Graphics.DrawLine(new Pen(Color.OrangeRed, 3), line.A, line.B);

                    //Ширина и высота задаются диаметром
                    //Координаты указывают в левый верхний угол прямоугольника, в котором находится круг
                    e.Graphics.DrawEllipse(specPen, line.A.X - r, line.A.Y - r, 2 * r, 2 * r);
                    e.Graphics.FillEllipse(solidBrushSpec, line.A.X - r, line.A.Y - r, 2 * r, 2 * r);

                    e.Graphics.DrawEllipse(specPen, line.B.X - r, line.B.Y - r, 2 * r, 2 * r);
                    e.Graphics.FillEllipse(solidBrushSpec, line.B.X - r, line.B.Y - r, 2 * r, 2 * r);
                }
            }
        }

        //Сканирование рисунка на клик по объекту
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            selectMode = -1;
            float k = 0;
            float b = 0;

            //Доп длина клика по линии
            int r = 4;


            //Координаты клика
            float clckX = e.Location.X;
            float clckY = e.Location.Y;

            foreach (var line in lines)
            {
                k = (line.B.Y - line.A.Y) / (line.B.X - line.A.X);
                b = (-line.A.X * (line.B.Y - line.A.Y) / (line.B.X - line.A.X)) + line.A.Y;

                //Проверка клика по линии (или на расстоянии r от линии)
                if (clckX < Math.Max(line.A.X, line.B.X) && clckX > Math.Min(line.A.X, line.B.X) &&
                    clckY < Math.Max(line.A.Y, line.B.Y) && clckY > Math.Min(line.A.Y, line.B.Y) &&
                    clckY - clckX*k - b < r && clckY - clckX * k - b > -r)
                {
                    selectedLine = line;
                    //Сохраняем данные о начальном положении клика
                    selectedPoint = new PointF(clckX, clckY);
                    selectMode = 0;
                }

                //Проверка клика по точке
                if ((clckX - line.A.X) * (clckX - line.A.X) < Line.pointR * Line.pointR &&
                    (clckY - line.A.Y) * (clckY - line.A.Y) < Line.pointR * Line.pointR)
                {
                    selectedLine = line;
                    selectedPoint = line.A;
                    selectMode = 1;
                }

                if ((clckX - line.B.X) * (clckX - line.B.X) < Line.pointR * Line.pointR &&
                    (clckY - line.B.Y) * (clckY - line.B.Y) < Line.pointR * Line.pointR)
                {
                    selectedLine = line;
                    selectedPoint = line.B;
                    selectMode = 2;
                }
            }
        }

        //Очистка от данных после сканирования
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            selectMode = -1;
        }

        //Передвижение объекта
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true && selectMode != -1)
            {
                
                if (selectMode > 0)
                {
                    selectedPoint.X = e.Location.X;
                    selectedPoint.Y = e.Location.Y;

                    if (selectMode == 1)
                        selectedLine.A = selectedPoint;

                    if (selectMode == 2)
                        selectedLine.B = selectedPoint;
                }

                if (selectMode == 0)
                {
                    float dx = e.Location.X - selectedPoint.X;
                    float dy = e.Location.Y - selectedPoint.Y;

                    selectedLine.A = new PointF(selectedLine.A.X + dx, selectedLine.A.Y + dy);
                    selectedLine.B = new PointF(selectedLine.B.X + dx, selectedLine.B.Y + dy);

                    selectedPoint.X = e.Location.X;
                    selectedPoint.Y = e.Location.Y;
                }
                //проверка на приближение к границе
                /*
                if (rect.Right > pictureBox1.Width)
                {
                    rect.X = pictureBox1.Width - rect.Width;
                }
                if (rect.Top < 0)
                {
                    rect.Y = 0;
                }
                if (rect.Left < 0)
                {
                    rect.X = 0;
                }
                if (rect.Bottom > pictureBox1.Height)
                {
                    rect.Y = pictureBox1.Height - rect.Height;
                }*/
                Refresh();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            lines.Remove(selectedLine);
            selectedLine = null;
            Refresh();
        }
    }
}
