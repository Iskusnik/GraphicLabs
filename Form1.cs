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
    //TODO: Добавить вывод информации о выбранном объекте
    public partial class FormMain : Form
    {
        List<GraphicObject> graphicObjects = new List<GraphicObject>(10);
        bool isMouseDown = false;
        GraphicObject selectedObj = null;
        PointF selectedPoint;
        //-1 - ничего
        //1 - что-то
        int selectMode = -1;

        //Координаты курсора
        float hoverX, hoverY;

        public FormMain()
        {
            
            InitializeComponent();
        }

        private void button2PointsLine_Click(object sender, EventArgs e)
        {
            graphicObjects.Add(new Line(pictureBox1.Width, pictureBox1.Height));
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen specPen = new Pen(Color.OrangeRed, 3);
            SolidBrush solidBrushSpec = new SolidBrush(Color.Orange);

            foreach (var obj in graphicObjects)
            {
                if (obj is Line)
                {
                    Line line = (Line)obj;
                    if (line != selectedObj)
                        line.DrawObject(e.Graphics, null, null);
                    else
                        line.DrawObject(e.Graphics, specPen, solidBrushSpec);
                }
            }
        }

        //Сканирование рисунка на клик по объекту
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            selectMode = -1;


            //Координаты клика
            float clckX = e.Location.X;
            float clckY = e.Location.Y;
            selectedPoint = new GraphicPoint(clckX, clckY);

            foreach (var obj in graphicObjects)
            {
                if (obj is Line)
                    if ((obj as Line).ChangeSelection(clckX, clckY))
                    {
                        selectedObj = obj;
                        selectMode = 1;
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
            float hoverX = e.Location.X;
            float hoverY = e.Location.Y;
            GraphicObject hoveredObj = null;
            foreach (var obj in graphicObjects)
                if (obj is Line)
                    if ((obj as Line).CheckSelection(hoverX, hoverY))
                    {
                        hoveredObj = obj;
                        if ((hoveredObj as Line).A.CheckSelection(hoverX, hoverY))
                            hoveredObj = (hoveredObj as Line).A;
                        else
                            if ((hoveredObj as Line).B.CheckSelection(hoverX, hoverY))
                            hoveredObj = (hoveredObj as Line).B;
                    }
            if (hoveredObj != null)
                textBox1.Text = hoveredObj.GetInfo(pictureBox1.Size);
            else
                textBox1.Text = "";



            if (isMouseDown == true && selectMode != -1)
            {
                if (selectedObj is Line)
                {
                    if ((selectedObj as Line).A.isSelected)
                    {
                        selectedPoint = new PointF(e.Location.X, e.Location.Y);
                        (selectedObj as Line).A.point = selectedPoint;
                    }
                    else
                    if ((selectedObj as Line).B.isSelected)
                    {
                        selectedPoint = new PointF(e.Location.X, e.Location.Y);
                        (selectedObj as Line).B.point = selectedPoint;
                    }
                    else
                    {
                        float dx = e.Location.X - selectedPoint.X;
                        float dy = e.Location.Y - selectedPoint.Y;

                        (selectedObj as Line).A.point = new PointF((selectedObj as Line).A.X + dx, (selectedObj as Line).A.Y + dy);
                        (selectedObj as Line).B.point = new PointF((selectedObj as Line).B.X + dx, (selectedObj as Line).B.Y + dy);

                        selectedPoint = new PointF(e.Location.X, e.Location.Y);
                    }
                }

                Refresh();
            }
            else
            {

            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            graphicObjects.Remove(selectedObj);
            selectedObj = null;
            Refresh();
        }

        
    }
}
