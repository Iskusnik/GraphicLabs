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
        //List<GraphicObject> allGraphicObjects = new List<GraphicObject>(10);
        List<GraphicObject> selectableGraphicObjects = new List<GraphicObject>(10);
        bool isMouseDown = false;
        GraphicObject selectedObj = null;
        PointF selectedPoint;
        int key = -1;
        //-1 - ничего
        //1 - что-то
        int selectMode = -1;

        //Координаты курсора
        //float hoverX, hoverY;

        public FormMain()
        {
            
            InitializeComponent();
        }

        private void button2PointsLine_Click(object sender, EventArgs e)
        {
            selectableGraphicObjects.Add(new Line(pictureBox1.Width, pictureBox1.Height));
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen specPen = new Pen(Color.OrangeRed, 3);
            SolidBrush solidBrushSpec = new SolidBrush(Color.Orange);

            foreach (var obj in selectableGraphicObjects)
            {
                if (obj is Line)
                {
                    Line line = (Line)obj;
                    if (line != selectedObj)
                        line.DrawObject(e.Graphics, null, null);
                    else
                        line.DrawObject(e.Graphics, specPen, solidBrushSpec);
                }
                if (obj is GraphicGroup)
                {
                    if (obj != selectedObj)
                        obj.DrawObject(e.Graphics, null, null);
                    else
                        obj.DrawObject(e.Graphics, specPen, solidBrushSpec);
                }
            }
        }

        //Сканирование рисунка на клик по объекту
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            //selectMode = -1;


            //Координаты клика
            float clckX = e.Location.X;
            float clckY = e.Location.Y;

            if (selectMode != 1)
            {
                
                selectedPoint = new GraphicPoint(clckX, clckY);
                selectedObj = null;

                foreach (var obj in selectableGraphicObjects)
                {

                    if (obj.ChangeSelection(clckX, clckY))
                        if (selectMode == -1)
                            selectedObj = obj;
                        //selectMode = 1;
                            

                    /*if (obj is Line)
                        if ((obj as Line).ChangeSelection(clckX, clckY))
                        {
                            selectedObj = obj;
                            selectMode = 1;
                        }

                 key = -1;
                        if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
                        {
                            if (key == (int)System.Windows.Input.Key.LeftCtrl)
                                ;

                            key = (int)System.Windows.Input.Key.LeftCtrl;
                        };
                    else
                    }
                 */
                }
            }
            if (selectMode == 1 && selectedObj is GraphicGroup)
                foreach (var obj in selectableGraphicObjects)
                    if (obj.CheckSelection(clckX, clckY))
                        (selectedObj as GraphicGroup).Add(obj);
            
        }

        //Очистка от данных после сканирования
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            //selectMode = -1;
        }

        //Передвижение объекта
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            float hoverX = e.Location.X;
            float hoverY = e.Location.Y;
            GraphicObject hoveredObj = null;
            foreach (var obj in selectableGraphicObjects)
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



            if (isMouseDown == true && selectMode == -1)
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
                if (selectedObj is GraphicGroup)
                {
                    float dx = e.Location.X - selectedPoint.X;
                    float dy = e.Location.Y - selectedPoint.Y;
                    selectedObj.MoveObject(dx, dy);
                    selectedPoint = new PointF(e.Location.X, e.Location.Y);
                }
                Refresh();
            }
            else
            {
                Refresh();
            }
            Refresh();

            textBox1.Text += selectMode.ToString();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (selectMode == -1)
            {
                selectableGraphicObjects.Remove(selectedObj);
                selectedObj = null;
                Refresh();
            }
            else if (selectMode == 2 && selectedObj is GraphicGroup)
            {
                foreach (var obj in (selectedObj as GraphicGroup).objectsGroup)
                    if (obj != null)
                        selectableGraphicObjects.Add(obj);

                selectableGraphicObjects.Remove(selectedObj);
                selectedObj = null;
                selectMode = -1;
                Refresh();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.ControlKey)
                if (selectMode != 1)
                {
                    selectMode = 1;
                    selectedObj = new GraphicGroup();
                    //textBox1.Text += "ctrl pressed";
                }

            if (e.KeyCode == Keys.ShiftKey)
                if (selectMode != 2)
                    selectMode = 2;
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                if (selectMode == 1)
                {
                    selectMode = -1;

                    if (selectedObj != null)
                    {
                        selectableGraphicObjects.Add(selectedObj);

                        foreach (var obj in (selectedObj as GraphicGroup).objectsGroup)
                            selectableGraphicObjects.Remove(obj);
                    }
                    //textBox1.Text += "ctrl unpressed";
                }

            if (e.KeyCode == Keys.ShiftKey)
                if (selectMode == 2)
                    selectMode = -1;
        }
    }
}
