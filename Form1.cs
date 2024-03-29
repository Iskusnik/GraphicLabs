﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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
        GraphicObject A = null;
        GraphicObject B = null;


        float[][] Rxyz;
        float[][] RxyzLocal;
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
            if (morph)

                for (int j = 0; j < pointsA.Count; j+=2)
                {
                    GraphicPoint Ap = new GraphicPoint(1, 1);
                    GraphicPoint Bp = new GraphicPoint(1, 1);

                    PointF pA = new PointF((pointsA[j].point.X * curT / T + pointsB[j].point.X * (T - curT) / T), (pointsA[j].point.Y * curT / T + pointsB[j].point.Y * (T - curT) / T));
                    PointF pB = new PointF((pointsA[j + 1].point.X * curT / T + pointsB[j + 1].point.X * (T - curT) / T), (pointsA[j + 1].point.Y * curT / T + pointsB[j + 1].point.Y * (T - curT) / T));

                    Ap.point = pA;
                    Ap.z = (pointsA[j].z * curT / T + pointsB[j].z * (T - curT) / T);

                    Bp.point = pB;
                    Bp.z = (pointsA[j + 1].z * curT / T + pointsB[j + 1].z * (T - curT) / T);

                    Line newLine = new Line(0, 0);
                    newLine.A = Ap;
                    newLine.B = Bp;
                    newLine.DrawObject(e.Graphics, null, null);
                }

            if (checkBoxAxes.Checked)
            {
                float nullX = GraphicObject.NullPoint.X;
                float nullY = GraphicObject.NullPoint.Y;
                float nullZ = GraphicObject.NullZ;
                Line[] axes = AxisLines;

                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0: specPen = new Pen(Color.LightBlue, 3); solidBrushSpec = new SolidBrush(Color.LightBlue); break;
                        case 1: specPen = new Pen(Color.PaleVioletRed, 3); solidBrushSpec = new SolidBrush(Color.PaleVioletRed); break;
                        case 2: specPen = new Pen(Color.LightGreen, 3); solidBrushSpec = new SolidBrush(Color.LightGreen); break;
                    }
                    axes[i].DrawObject(e.Graphics, specPen, solidBrushSpec);
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

            if (mhb != 0 && selectedObj != null && selectedObj is GraphicObject)
            {
                Point loc = e.Location;
                switch (mhb)
                {
                    case 1:
                        if (selectedObj is Line)
                        {
                            Line median = new Line(loc, new PointF(
                                ((selectedObj as Line).A.point.X + (selectedObj as Line).B.point.X) / 2,
                                ((selectedObj as Line).A.point.Y + (selectedObj as Line).B.point.Y) / 2));
                            median.A.z = ((selectedObj as Line).A.z + (selectedObj as Line).B.z) / 2;

                            selectableGraphicObjects.Add(median);
                        }
                        break;

                    case 2:
                        if (selectedObj is Line)
                        {
                            //Через x1, y1
                            //A(y-y1)-B(x-x1)=0

                            //Координаты точек
                            float Ax = (selectedObj as Line).A.point.X, Ay = (selectedObj as Line).A.point.Y;
                            float Bx = (selectedObj as Line).B.point.X, By = (selectedObj as Line).B.point.Y;

                            //Вычисляем параметры прямой
                            float A = By - Ay;
                            float B = Ax - Bx;
                            float C = -Ay * B - Ax * A;
                            float d = (float)((Math.Abs(A * clckX + B * clckY + C)) / Math.Sqrt(A * A + B * B));

                            float Aheight = B;
                            float Bheight = -A;
                            float Cheight = -Aheight * loc.X - Bheight * loc.Y;

                            Line height = new Line(loc, GetIntersection(A, B, C, Aheight, Bheight, Cheight));

                            selectableGraphicObjects.Add(height);
                        }
                        break;

                    case 3:
                        if (selectedObj is GraphicGroup)
                        {
                            Line Aline = (Line)(selectedObj as GraphicGroup).objectsGroup[0];
                            Line Bline = (Line)(selectedObj as GraphicGroup).objectsGroup[1];

                            float AlineX = Aline.A.point.X - Aline.B.point.X;
                            float AlineY = Aline.A.point.Y - Aline.B.point.Y;
                            float Alen = (float)Math.Sqrt(Math.Pow(AlineX, 2) + Math.Pow(AlineY, 2));

                            float BlineX = Bline.A.point.X - Bline.B.point.X;
                            float BlineY = Bline.A.point.Y - Bline.B.point.Y;
                            float Blen = (float)Math.Sqrt(Math.Pow(BlineX, 2) + Math.Pow(BlineY, 2));

                            PointF bissectrPoint = new PointF(AlineX*100/Alen + BlineX * 100 / Blen + Aline.A.point.X, AlineY * 100 / Alen + BlineY * 100 / Blen + Aline.A.point.Y);
                            Line bissectr = new Line(new PointF(Aline.A.point.X, Aline.A.point.Y), bissectrPoint);

                            selectableGraphicObjects.Add(bissectr);
                            
                            /*
                            
                            float Alen = (float)Math.Sqrt(Math.Pow(A.A.point.X - A.B.point.X, 2) + Math.Pow(A.A.point.Y - A.B.point.Y, 2));
                            float Blen = (float)Math.Sqrt(Math.Pow(B.A.point.X - B.B.point.X, 2) + Math.Pow(B.A.point.Y - B.B.point.Y, 2));
                            float newX = A.A.point.X + (Alen/Blen)*
                            */
                        }
                        break;
                }
                mhb = 0;
            }

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

            if (selectMode == 2)
            {
                bool checker = false;
                foreach (var obj in selectableGraphicObjects)
                    if (obj.CheckSelection(clckX, clckY))
                    {
                        checker = true;
                        if (A == null)
                            A = obj;
                        else
                            B = obj;
                    }

                if (!checker)
                    { 
                        A = null;
                        B = null;
                    }
            }
        }
        public static PointF GetIntersection(float A1, float B1, float C1, float A2, float B2, float C2)
        {
            var X = B1 * C2 - B2 * C1;
            var Y = A2 * C1 - A1 * C2;

            var Z = A1 * B2 - A2 * B1;


            return new PointF(X / Z, Y / Z);
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
                        textBox1.Text = "";
                        if ((hoveredObj as Line).A.CheckSelection(hoverX, hoverY))
                        {
                            hoveredObj = (hoveredObj as Line).A;
                            textBox1.Text = "A:";
                        }
                        else
                            if ((hoveredObj as Line).B.CheckSelection(hoverX, hoverY))
                        {
                            hoveredObj = (hoveredObj as Line).B;
                            textBox1.Text = "B:";
                        }
                    }
                if (obj is GraphicGroup)
                    if ((obj as GraphicGroup).CheckSelection(hoverX, hoverY))
                    {
                        hoveredObj = obj;
                        textBox1.Text = "";
                    }
            }
            if (hoveredObj != null)
                textBox1.Text += hoveredObj.GetInfo(pictureBox1.Size);
            else
                ;//textBox1.Text = "";


            float dx = e.Location.X - selectedPoint.X;
            float dy = e.Location.Y - selectedPoint.Y;

            if (isMouseDown == true && selectMode == -1)
            {
                if (selectedObj is Line)
                {
                    if ((selectedObj as Line).A.isSelected)
                    {
                        selectedPoint = new PointF(e.Location.X, e.Location.Y);
                        //(selectedObj as Line).A.point = selectedPoint;

                        (selectedObj as Line).A.MoveObject(dx, dy);
                    }
                    else
                    if ((selectedObj as Line).B.isSelected)
                    {
                        selectedPoint = new PointF(e.Location.X, e.Location.Y);
                        //(selectedObj as Line).B.point = selectedPoint;

                        (selectedObj as Line).B.MoveObject(dx, dy);
                    }
                    else
                    {
                        

                        //(selectedObj as Line).A.point = new PointF((selectedObj as Line).A.point.X + dx, (selectedObj as Line).A.point.Y + dy);
                        //(selectedObj as Line).B.point = new PointF((selectedObj as Line).B.point.X + dx, (selectedObj as Line).B.point.Y + dy);
                        (selectedObj as Line).MoveObject(dx, dy);
                        selectedPoint = new PointF(e.Location.X, e.Location.Y);
                    }
                }
                if (selectedObj is GraphicGroup)
                {
                    
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
            //richTextBox1.Text = "";
            //richTextBox1.Text += "Sin: " + (double)Math.Sin(2 * Math.PI * ((float)vScrollBar1.Value - prevVScrollBar) / 3600) + "\n";
            //richTextBox1.Text += "Cos: " + (double)Math.Cos(2 * Math.PI * ((float)vScrollBar1.Value - prevVScrollBar) / 3600) + "\n";
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

        bool debugVal = false;
        private void buttonDeleteGrouping_Click(object sender, EventArgs e)
        {
            /*
            if (debugVal)
            {
                hScrollBar1.Value = 180;
                vScrollBar1.Value = 180;
                debugVal = false;
            }
            else
            {
                hScrollBar1.Value = 225;
                vScrollBar1.Value = 225;
                debugVal = true;
            }*/

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

            Rxyz = new float[][]
            {
                new float[] {1, 0, 0, 0 },
                new float[] {0, 1, 0, 0 },
                new float[] {0, 0, 1, 0 },
                new float[] {0, 0, 0, 1 },
            };
            RxyzLocal = new float[][]
            {
                new float[] {1, 0, 0, 0 },
                new float[] {0, 1, 0, 0 },
                new float[] {0, 0, 1, 0 },
                new float[] {0, 0, 0, 1 },
            };
            GraphicObject.NullPoint = new PointF(pictureBox1.Size.Width / 2, pictureBox1.Size.Height / 2);

            numericUpDownX.Value = pictureBox1.Size.Width / 2;
            numericUpDownY.Value = pictureBox1.Size.Height / 2;
            numericUpDownZ.Value = 0;
            GlobalColor = pictureBoxColorPicker.BackColor;
            // ...



            SetAxes(pictureBox1.Size.Width / 2, pictureBox1.Size.Height / 2, 0);



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
        }

        private void checkBoxAxes_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            GraphicObject.NullPoint = new PointF((float)numericUpDownX.Value, (float)numericUpDownY.Value);
            GraphicObject.NullZ = (float)numericUpDownZ.Value;

            SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ, Rxyz);

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

            GraphicObject.NullPoint = new PointF((float)numericUpDownX.Value, (float)numericUpDownY.Value);
            GraphicObject.NullZ = 1;
            Rxyz = new float[][]
            {
                new float[] {1, 0, 0, 0 },
                new float[] {0, 1, 0, 0 },
                new float[] {0, 0, 1, 0 },
                new float[] {0, 0, 0, 1 },
            };
            RxyzLocal = new float[][]
            {
                new float[] {1, 0, 0, 0 },
                new float[] {0, 1, 0, 0 },
                new float[] {0, 0, 1, 0 },
                new float[] {0, 0, 0, 1 },
            };
            numericUpDownX.Value = pictureBox1.Size.Width / 2;
            numericUpDownY.Value = pictureBox1.Size.Height / 2;
            numericUpDownZ.Value = 1;
            
            prevHScrollBar = 1800;
            prevVScrollBar = 1800;
            prevHScrollBarLocal = 1800;
            prevHScrollBarLocal = 1800;

            hScrollBar1.Value = 1800;
            vScrollBar1.Value = 1800;



            SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ);

            pictureBox1.Refresh();
        }

        float prevHScrollBar = 1800;
        float prevVScrollBar = 1800;
        float prevHScrollBarLocal = 1800;
        float prevVScrollBarLocal = 1800;

        Line[] AxisLines = new Line[3];
        private Line[] SetAxes(float x, float y, float z, float[][] matrix)
        {

            /**/
            GraphicPoint gpX1 = new GraphicPoint(1, 1);
            gpX1.point = new PointF(x + 100000, y);
            gpX1.z = z;
            gpX1 = ApplyMatrix(gpX1, matrix);
            

            GraphicPoint gpX2 = new GraphicPoint(1, 1);
            gpX2.point = new PointF(x - 100000, y);
            gpX2.z = z;
            gpX2 = ApplyMatrix(gpX2, matrix);
            

            GraphicPoint gpY1 = new GraphicPoint(1, 1);
            gpY1.point = new PointF(x, y + 100000);
            gpY1.z = z;
            gpY1 = ApplyMatrix(gpY1, matrix);
            

            GraphicPoint gpY2 = new GraphicPoint(1, 1);
            gpY2.point = new PointF(x, y - 100000);
            gpY2.z = z;
            gpY2 = ApplyMatrix(gpY2, matrix);
            

            GraphicPoint gpZ1 = new GraphicPoint(1, 1);
            gpZ1.point = new PointF(x, y);
            gpZ1.z = z + 100000;
            gpZ1 = ApplyMatrix(gpZ1, matrix);

            GraphicPoint gpZ2 = new GraphicPoint(1, 1);
            gpZ2.point = new PointF(x, y);
            gpZ2.z = z - 100000;
            gpZ2 = ApplyMatrix(gpZ2, matrix);

            /*GraphicPoint gpX1 = ApplyMatrix(AxisLines[0].A, matrix);
            GraphicPoint gpX2 = ApplyMatrix(AxisLines[0].B, matrix);
            GraphicPoint gpY1 = ApplyMatrix(AxisLines[1].A, matrix);
            GraphicPoint gpY2 = ApplyMatrix(AxisLines[1].B, matrix);
            GraphicPoint gpZ1 = ApplyMatrix(AxisLines[2].A, matrix);
            GraphicPoint gpZ2 = ApplyMatrix(AxisLines[2].B, matrix);*/

            AxisLines[0] = new Line(gpX1, gpX2);
            AxisLines[1] = new Line(gpY1, gpY2);
            AxisLines[2] = new Line(gpZ1, gpZ2);

            //TODO: перевычислить X и Y относительно матрицы изменений
            //for (int i = 0; i < 3; i++)
            //{
            //lines[i].A = ApplyMatrix(lines[i].A,);
            //lines[i].B = ApplyMatrix(lines[i].A,);
            //}

            return AxisLines;
            //throw new NotImplementedException();
        }
        private Line[] SetAxes(float x, float y, float z)
        {


            GraphicPoint gpX1 = new GraphicPoint(1, 1);
            gpX1.point = new PointF(x + 100000, y);
            gpX1.z = z;

            GraphicPoint gpX2 = new GraphicPoint(1, 1);
            gpX2.point = new PointF(x - 100000, y);
            gpX2.z = z;

            GraphicPoint gpY1 = new GraphicPoint(1, 1);
            gpY1.point = new PointF(x, y + 100000);
            gpY1.z = z;

            GraphicPoint gpY2 = new GraphicPoint(1, 1);
            gpY2.point = new PointF(x, y - 100000);
            gpY2.z = z;

            GraphicPoint gpZ1 = new GraphicPoint(1, 1);
            gpZ1.point = new PointF(x, y);
            gpZ1.z = z - 100000;

            GraphicPoint gpZ2 = new GraphicPoint(1, 1);
            gpZ2.point = new PointF(x, y);
            gpZ2.z = z + 100000;


            PointF nullPointZ1 = new PointF(x, y);
            PointF nullPointZ2 = new PointF(x, y);

            AxisLines[0] = new Line(gpX1, gpX2);
            AxisLines[1] = new Line(gpY1, gpY2);
            AxisLines[2] = new Line(gpZ1, gpZ2);

            //TODO: перевычислить X и Y относительно матрицы изменений
            //for (int i = 0; i < 3; i++)
            //{
            //lines[i].A = ApplyMatrix(lines[i].A,);
            //lines[i].B = ApplyMatrix(lines[i].A,);
            //}

            return AxisLines;
            //throw new NotImplementedException();
        }
        private GraphicPoint ApplyMatrix(GraphicPoint point, float[][] matrix)
        {
            GraphicPoint changedPoint = new GraphicPoint(1, 1);
            float[] pointData = new float[] { point.point.X, point.point.Y, point.z, 1 };//{ point.X, point.Y, point.Z, 1 };//
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
            return changedPoint;
        }
        private GraphicPoint ApplyMatrixRelatedToLocal(GraphicPoint point, float[][] matrix)
        {
            GraphicPoint changedPoint = new GraphicPoint(1, 1);
            float[] pointData = new float[] { point.X, point.Y, point.Z, 1 };//{ point.X, point.Y, point.Z, 1 };//
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
            return changedPoint;
        }

        private void UpdateMatrix(float[][] matrix)
        {
            float[][] oldRxyz = new float[][]
            {
                new float[] {1, 0, 0, 0 },
                new float[] {0, 1, 0, 0 },
                new float[] {0, 0, 1, 0 },
                new float[] {0, 0, 0, 1 },
            };
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    oldRxyz[i][j] = Rxyz[i][j];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Rxyz[i][j] = ApplyRow(oldRxyz[i], GetColumn(matrix, j));
        }
        private void UpdateMatrixLocal(float[][] matrix)
        {
            float[][] oldRxyz = new float[][]
            {
                new float[] {1, 0, 0, 0 },
                new float[] {0, 1, 0, 0 },
                new float[] {0, 0, 1, 0 },
                new float[] {0, 0, 0, 1 },
            };
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    oldRxyz[i][j] = RxyzLocal[i][j];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    RxyzLocal[i][j] = ApplyRow(oldRxyz[i], GetColumn(matrix, j));
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

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (!checkBoxOnLocal.Checked)
            {
                float sinA = (float)Math.Sin(2 * Math.PI * ((float)vScrollBar1.Value - prevVScrollBar) / 3600);
                float cosA = (float)Math.Cos(2 * Math.PI * ((float)vScrollBar1.Value - prevVScrollBar) / 3600);

                prevVScrollBar = vScrollBar1.Value;

                float[][] matrix = new float[][]
                {
                new float[]{1, 0, 0, 0 },
                new float[]{0, cosA, sinA, 0 },
                new float[]{0, -sinA, cosA, 0 },
                new float[]{0, 0, 0, 1 },
                };
                UpdateMatrix(matrix);
                /*GraphicPoint newNullPoint = new GraphicPoint(1, 1);
                
                float X = GraphicObject.NullPoint.X;
                float Y = GraphicObject.NullPoint.Y;
                newNullPoint.point = new PointF(X, Y);
                newNullPoint.z = GraphicObject.NullZ;



                newNullPoint = ApplyMatrix(newNullPoint, Rxyz);


                GraphicObject.NullPoint.X = newNullPoint.point.X;
                GraphicObject.NullPoint.Y = newNullPoint.point.Y;
                GraphicObject.NullZ = newNullPoint.z;


                matrix[3][0] = GraphicObject.NullPoint.X;
                matrix[3][1] = GraphicObject.NullPoint.Y;
                matrix[3][2] = GraphicObject.NullZ;
                matrix[3][3] = 1;*/

                SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ, Rxyz);
                UpdateAllObjs();
                pictureBox1.Refresh();
            }
            else
            {
                float sinA = (float)Math.Sin(2 * Math.PI * ((float)vScrollBar1.Value - prevVScrollBarLocal) / 3600);
                float cosA = (float)Math.Cos(2 * Math.PI * ((float)vScrollBar1.Value - prevVScrollBarLocal) / 3600);

                prevVScrollBarLocal = vScrollBar1.Value;

                float[][] matrix = new float[][]
                {
                new float[]{1, 0, 0, 0 },
                new float[]{0, cosA, sinA, 0 },
                new float[]{0, -sinA, cosA, 0 },
                new float[]{0, 0, 0, 1 },
                };
                UpdateMatrixLocal(matrix);
                UpdateAllObjs();
                pictureBox1.Refresh();
            }
        }
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {

            if (!checkBoxOnLocal.Checked)
            {
                float sinA = (float)Math.Sin(2 * Math.PI * ((float)hScrollBar1.Value - prevHScrollBar) / 3600);
                float cosA = (float)Math.Cos(2 * Math.PI * ((float)hScrollBar1.Value - prevHScrollBar) / 3600);

                prevHScrollBar = hScrollBar1.Value;

                float[][] matrix = new float[][]
                {
                new float[]{cosA, 0, -sinA,  0 },
                new float[]{0,    1,  0,     0 },
                new float[]{sinA, 0,  cosA,  0 },
                new float[]{0,    0,  0,     1 },
                };
                UpdateMatrix(matrix);
                /*GraphicPoint newNullPoint = new GraphicPoint(1, 1);
                
                float X = GraphicObject.NullPoint.X;
                float Y = GraphicObject.NullPoint.Y;
                newNullPoint.point = new PointF(X, Y);
                newNullPoint.z = GraphicObject.NullZ;



                newNullPoint = ApplyMatrix(newNullPoint, Rxyz);



                GraphicObject.NullPoint.X = newNullPoint.point.X;
                GraphicObject.NullPoint.Y = newNullPoint.point.Y;
                GraphicObject.NullZ = newNullPoint.z;

                matrix[3][0] = GraphicObject.NullPoint.X;
                matrix[3][1] = GraphicObject.NullPoint.Y;
                matrix[3][2] = GraphicObject.NullZ;
                matrix[3][3] = 1;*/

                SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ, Rxyz);
                UpdateAllObjs();
                pictureBox1.Refresh();
            }
            else
            {
                float sinA = (float)Math.Sin(2 * Math.PI * ((float)hScrollBar1.Value - prevHScrollBarLocal) / 3600);
                float cosA = (float)Math.Cos(2 * Math.PI * ((float)hScrollBar1.Value - prevHScrollBarLocal) / 3600);

                prevHScrollBarLocal = hScrollBar1.Value;

                float[][] matrix = new float[][]
                {
                new float[]{cosA, 0, -sinA,  0 },
                new float[]{0,    1,  0,     0 },
                new float[]{sinA, 0,  cosA,  0 },
                new float[]{0,    0,  0,     1 },
                };
                UpdateMatrixLocal(matrix);
                UpdateAllObjs();
                pictureBox1.Refresh();
            }
        }

        private void UpdateAllObjs()
        {
            foreach (GraphicObject obj in selectableGraphicObjects)
            {
                obj.ApplyMatrix(Rxyz);
                obj.ApplyMatrixLocal(RxyzLocal);
            }   
        }

        private void checkBoxOnLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnLocal.Checked)
            {
                hScrollBar1.Value = (int)Math.Round(prevHScrollBarLocal);

                vScrollBar1.Value = (int)Math.Round(prevVScrollBarLocal);
            }
            else
            {
                hScrollBar1.Value = (int)Math.Round(prevHScrollBar);

                vScrollBar1.Value = (int)Math.Round(prevVScrollBar);
            }

        }

        private void buttonApplyMatrix_Click(object sender, EventArgs e)
        {
            float[][] matrix = new float[][]
                {
                new float[]{(float)numericUpDown01.Value, (float)numericUpDown02.Value, (float)numericUpDown03.Value,  (float)numericUpDown04.Value },
                new float[]{(float)numericUpDown11.Value, (float)numericUpDown12.Value, (float)numericUpDown13.Value,  (float)numericUpDown14.Value },
                new float[]{(float)numericUpDown21.Value, (float)numericUpDown22.Value, (float)numericUpDown23.Value,  (float)numericUpDown24.Value },
                new float[]{(float)numericUpDown31.Value, (float)numericUpDown32.Value, (float)numericUpDown33.Value,  (float)numericUpDown34.Value },
                };

            if (checkBoxOnLocal.Checked)
                UpdateMatrixLocal(matrix);
            else
                UpdateMatrix(matrix);
            UpdateAllObjs();
            pictureBox1.Refresh();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            SaveFileDialog saveDialog = new SaveFileDialog();

            GraphicPoint point = new GraphicPoint(0, 0);
            point.X = 0;
            point.Y = 0;
            point.Z = 0;
            //selectableGraphicObjects
            List<object> allInfo = new List<object>();
            allInfo.Add(Rxyz);
            allInfo.Add(RxyzLocal);
            allInfo.Add(point);
            allInfo.Add(selectableGraphicObjects);

            saveDialog.DefaultExt = "dat";
            saveDialog.ShowDialog();
           
            
            using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, allInfo);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.DefaultExt = "dat";
            openDialog.ShowDialog();


            List<object> allInfo = new List<object>();

            if (!openDialog.FileName.Equals(""))
            {
                using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open))
                {
                    allInfo = (List<object>)formatter.Deserialize(fs);
                }

                Rxyz = (float[][])allInfo[0];
                RxyzLocal = (float[][])allInfo[1];
                GraphicObject.NullPoint = ((GraphicPoint)allInfo[2]).point;
                GraphicObject.NullZ = ((GraphicPoint)allInfo[2]).z;
                selectableGraphicObjects = ((List<GraphicObject>)allInfo[3]);
            }
        }

        bool morph = false;
        List<GraphicPoint> pointsA;
        List<GraphicPoint> pointsB;
        int T = 1000;
        int curT = 0;
        private void buttonMorph_Click(object sender, EventArgs e)
        {
            if (A != null && B != null)
            {


                pointsA = GetPoints(A);
                pointsB = GetPoints(B);

                int temp = 0;
                while (pointsA.Count < pointsB.Count)
                {
                    pointsA.Add(pointsA[temp]);
                    temp++;
                    pointsA.Add(pointsA[temp]);
                    temp++;
                }

                temp = 0;
                while (pointsA.Count > pointsB.Count)
                {
                    pointsB.Add(pointsB[temp]);
                    temp++;
                    pointsB.Add(pointsB[temp]);
                    temp++;
                }

                morph = true;
                for (int i = 0; i < T; i++)
                {
                    curT = i;
                    Thread.Sleep(2);
                    pictureBox1.Refresh();
                }

                morph = false;
            }
        }

        private List<GraphicPoint> GetPoints(GraphicObject obj)
        {
            List<GraphicPoint> graphicPoints = new List<GraphicPoint>();

            if (obj is Line)
            {
                graphicPoints.Add((obj as Line).A);
                graphicPoints.Add((obj as Line).B);
            }
            if (obj is GraphicGroup)
                foreach (GraphicObject item in (obj as GraphicGroup).objectsGroup)
                    graphicPoints.AddRange(GetPoints(item));

            return graphicPoints;
        }


        int mhb = 0;
        private void buttonMedian_Click(object sender, EventArgs e)
        {
            mhb = 1;
        }

        

        private void buttonHeight_Click(object sender, EventArgs e)
        {
            mhb = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mhb = 3;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
