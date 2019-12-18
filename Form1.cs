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

        float[][] Rxyz;

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

            GraphicObject.NullPoint = new PointF(pictureBox1.Size.Width/2, pictureBox1.Size.Height/2);

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
            numericUpDownX.Value = pictureBox1.Size.Width / 2;
            numericUpDownY.Value = pictureBox1.Size.Height / 2;
            numericUpDownZ.Value = 1;
            
            prevHScrollBar = 1800;
            prevVScrollBar = 1800;

            hScrollBar1.Value = 1800;
            vScrollBar1.Value = 1800;



            SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ);

            pictureBox1.Refresh();
        }

        float prevHScrollBar = 1800;
        float prevVScrollBar = 1800;
        

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
            GraphicPoint newNullPoint = new GraphicPoint(1, 1);
            /**/
            float X = GraphicObject.NullPoint.X;
            float Y = GraphicObject.NullPoint.Y;
            newNullPoint.point = new PointF(X, Y);
            newNullPoint.z = GraphicObject.NullZ;
            

            
            newNullPoint = ApplyMatrix(newNullPoint, matrix);

           
            GraphicObject.NullPoint.X = newNullPoint.point.X;
            GraphicObject.NullPoint.Y = newNullPoint.point.Y;
            GraphicObject.NullZ = newNullPoint.z;


            matrix[3][0] = GraphicObject.NullPoint.X;
            matrix[3][1] = GraphicObject.NullPoint.Y;
            matrix[3][2] = GraphicObject.NullZ;
            matrix[3][3] = 1;

            SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ, Rxyz);
            UpdateAllObjs();
            pictureBox1.Refresh();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
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
            GraphicPoint newNullPoint = new GraphicPoint(1, 1);
            /**/
            float X = GraphicObject.NullPoint.X;
            float Y = GraphicObject.NullPoint.Y;
            newNullPoint.point = new PointF(X, Y);
            newNullPoint.z = GraphicObject.NullZ;



            newNullPoint = ApplyMatrix(newNullPoint, matrix);

            

            GraphicObject.NullPoint.X = newNullPoint.point.X;
            GraphicObject.NullPoint.Y = newNullPoint.point.Y;
            GraphicObject.NullZ = newNullPoint.z;

            matrix[3][0] = GraphicObject.NullPoint.X;
            matrix[3][1] = GraphicObject.NullPoint.Y;
            matrix[3][2] = GraphicObject.NullZ;
            matrix[3][3] = 1;

            SetAxes(GraphicObject.NullPoint.X, GraphicObject.NullPoint.Y, GraphicObject.NullZ, Rxyz);
            UpdateAllObjs();
            pictureBox1.Refresh();
        }

        private void UpdateAllObjs()
        {
            foreach (GraphicObject obj in selectableGraphicObjects)
                obj.ApplyMatrix(Rxyz);
                
        }
    }
}
