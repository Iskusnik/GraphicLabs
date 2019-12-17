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
        bool isRightClickLast = false;
        GraphicObject selectedObj = null;
        PointF selectedPoint;
        GraphicPoint rcmPoint = null;
        int key = -1;
        //-1 - ничего
        //1 - что-то
        int selectMode = -1;
        Color GlobalColor;
        //Координаты курсора
        //float hoverX, hoverY;

        public FormMain()
        {
            InitializeComponent();
        }

        private void button2PointsLine_Click(object sender, EventArgs e)
        {
            Line newLine = new Line(pictureBox1.Width, pictureBox1.Height);
            newLine.LocalPen = new Pen(GlobalColor, (float)numericUpDownThick.Value);
            selectableGraphicObjects.Add(newLine);
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
                if (!(selectedObj is null))
                    selectedObj.DrawObject(e.Graphics, specPen, solidBrushSpec);

                
            }

            if (checkBoxAxes.Checked)
            {
                float nullX = GraphicObject.NullPoint.X;
                float nullY = GraphicObject.NullPoint.Y;
                float nullZ = GraphicObject.NullZ;
                Line[] axes = GetAxes(nullX, nullY, nullZ);

                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0: specPen = new Pen(Color.LightBlue, 1); solidBrushSpec = new SolidBrush(Color.LightBlue); break;
                        case 1: specPen = new Pen(Color.PaleVioletRed, 1); solidBrushSpec = new SolidBrush(Color.PaleVioletRed); break;
                        case 2: specPen = new Pen(Color.LightGreen, 1); solidBrushSpec = new SolidBrush(Color.LightGreen); break;
                    }
                    axes[i].DrawObject(e.Graphics, specPen, solidBrushSpec);
                } 
            }
        }
        private Line[] GetAxes(float x, float y, float z)
        {
            Line[] lines = new Line[3];

            //TODO: перевычислить X и Y относительно матрицы изменений

            PointF nullPointX1 = new PointF(x + 10000, y);
            PointF nullPointX2 = new PointF(x - 10000, y);

            PointF nullPointY1 = new PointF(x, y + 10000);
            PointF nullPointY2 = new PointF(x, y - 10000);


            PointF nullPointZ = new PointF(x, y);
            lines[0] = new Line(nullPointX1, nullPointX2);
            lines[1] = new Line(nullPointY1, nullPointY2);
            lines[2] = new Line(nullPointZ, nullPointZ);


            return lines;
            //throw new NotImplementedException();
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
                if (e.Button == MouseButtons.Right &&
                    selectedObj != null &&
                    ((selectedObj as Line).A.isSelected || (selectedObj as Line).B.isSelected))
                {
                    GraphicPoint selectedPoint = (selectedObj as Line).A.isSelected ? (selectedObj as Line).A : (selectedObj as Line).B;
                    pictureBox1.ContextMenuStrip.Items[0].Text = "X: " + selectedPoint.X.ToString();
                    pictureBox1.ContextMenuStrip.Items[1].Text = "Y: " + selectedPoint.Y.ToString();
                    pictureBox1.ContextMenuStrip.Items[2].Text = "Z: " + selectedPoint.Z.ToString();
                    isMouseDown = false;
                    isRightClickLast = true;
                    rcmPoint = selectedPoint;
                }
                else
                {
                    pictureBox1.ContextMenuStrip.Visible = false;
                    isRightClickLast = false;
                    rcmPoint = null;
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
            {
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
                if (obj is GraphicGroup)
                    if ((obj as GraphicGroup).CheckSelection(hoverX, hoverY))
                        hoveredObj = obj;
            }
            if (hoveredObj != null)
                textBox1.Text = hoveredObj.GetInfo(pictureBox1.Size);
            else
                ;//textBox1.Text = "";



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

                        (selectedObj as Line).A.point = new PointF((selectedObj as Line).A.point.X + dx, (selectedObj as Line).A.point.Y + dy);
                        (selectedObj as Line).B.point = new PointF((selectedObj as Line).B.point.X + dx, (selectedObj as Line).B.point.Y + dy);

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

            //textBox1.Text += selectMode.ToString();
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

        //Сtrl = собрать группу
        //Shift = разбить группу
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

        private void buttonDeleteGrouping_Click(object sender, EventArgs e)
        {
            if (selectMode == -1 && selectedObj is GraphicGroup)
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            ContextMenuStrip cm = new ContextMenuStrip();
            cm.Items.Add("X");
            cm.Items.Add("Y");
            cm.Items.Add("Z");
            cm.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);
            cm.Opening += new CancelEventHandler(contexMenu_Opening);

            GraphicObject.NullPoint = new PointF(pictureBox1.Size.Width/2, pictureBox1.Size.Height/2);

            numericUpDownX.Value = pictureBox1.Size.Width / 2;
            numericUpDownY.Value = pictureBox1.Size.Height / 2;
            numericUpDownZ.Value = 1;
            GlobalColor = pictureBoxColorPicker.BackColor;
            // ...


            pictureBox1.ContextMenuStrip = cm;
        }



        void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            string[] info = item.Text.Split(' ');
            int result = ReturnNumericDialog.ShowDialog(info[0], info[1]);
            
            switch(info[0])
            {
                case "X:":
                    {
                        rcmPoint.X = result;

                        break;
                    }
                case "Y:":
                    {
                        rcmPoint.Y = result;
                        break;
                    }
                case "Z:":
                    {
                        rcmPoint.Z = result;
                        break;
                    }

            }
            Refresh();
        }

        void contexMenu_Opening(object sender, CancelEventArgs e)
        {
            if (isRightClickLast)
                e.Cancel = false;
            else
                e.Cancel = true;
            // your code here
        }

        private void checkBoxAxes_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            GraphicObject.NullPoint = new PointF((float)numericUpDownX.Value, pictureBox1.Height - (float)numericUpDownY.Value);
            GraphicObject.NullZ = (float) numericUpDownZ.Value;
            pictureBox1.Refresh();
        }

        private void pictureBoxColorPicker_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBoxColorPicker.BackColor = colorDialog1.Color;
        }

        private void pictureBoxColorPicker_BackColorChanged(object sender, EventArgs e)
        {
            GlobalColor = pictureBoxColorPicker.BackColor;

            if (selectedObj is Line)
                (selectedObj as Line).LocalPen = new Pen(GlobalColor, (float)numericUpDownThick.Value);
        }

        private void numericUpDownThick_ValueChanged(object sender, EventArgs e)
        {
            if (selectedObj is Line)
                (selectedObj as Line).LocalPen = new Pen(GlobalColor, (float)numericUpDownThick.Value);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            selectedObj = null;
            selectableGraphicObjects.Clear();
            pictureBox1.Refresh();
        }
    }
}
