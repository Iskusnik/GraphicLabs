namespace _2pointsNET4_8
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.numericUpDownThick = new System.Windows.Forms.NumericUpDown();
            this.pictureBoxColorPicker = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAxes = new System.Windows.Forms.CheckBox();
            this.buttonDeleteGrouping = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonDel = new System.Windows.Forms.Button();
            this.button2PointsLine = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkBoxOnLocal = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.hScrollBar1);
            this.splitContainerMain.Panel1.Controls.Add(this.vScrollBar1);
            this.splitContainerMain.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelButtons);
            this.splitContainerMain.Size = new System.Drawing.Size(800, 450);
            this.splitContainerMain.SplitterDistance = 550;
            this.splitContainerMain.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 433);
            this.hScrollBar1.Maximum = 3600;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(532, 17);
            this.hScrollBar1.TabIndex = 2;
            this.hScrollBar1.Value = 1800;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(532, 0);
            this.vScrollBar1.Maximum = 3600;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(18, 450);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Value = 1800;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(550, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.checkBoxOnLocal);
            this.panelButtons.Controls.Add(this.buttonClear);
            this.panelButtons.Controls.Add(this.numericUpDownThick);
            this.panelButtons.Controls.Add(this.pictureBoxColorPicker);
            this.panelButtons.Controls.Add(this.label1);
            this.panelButtons.Controls.Add(this.numericUpDownZ);
            this.panelButtons.Controls.Add(this.numericUpDownY);
            this.panelButtons.Controls.Add(this.numericUpDownX);
            this.panelButtons.Controls.Add(this.checkBoxAxes);
            this.panelButtons.Controls.Add(this.buttonDeleteGrouping);
            this.panelButtons.Controls.Add(this.richTextBox1);
            this.panelButtons.Controls.Add(this.textBox1);
            this.panelButtons.Controls.Add(this.buttonDel);
            this.panelButtons.Controls.Add(this.button2PointsLine);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(246, 450);
            this.panelButtons.TabIndex = 0;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(29, 176);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(195, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // numericUpDownThick
            // 
            this.numericUpDownThick.Location = new System.Drawing.Point(180, 92);
            this.numericUpDownThick.Name = "numericUpDownThick";
            this.numericUpDownThick.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownThick.TabIndex = 11;
            this.numericUpDownThick.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownThick.ValueChanged += new System.EventHandler(this.numericUpDownThick_ValueChanged);
            // 
            // pictureBoxColorPicker
            // 
            this.pictureBoxColorPicker.BackColor = System.Drawing.Color.Black;
            this.pictureBoxColorPicker.Location = new System.Drawing.Point(145, 89);
            this.pictureBoxColorPicker.Name = "pictureBoxColorPicker";
            this.pictureBoxColorPicker.Size = new System.Drawing.Size(29, 23);
            this.pictureBoxColorPicker.TabIndex = 10;
            this.pictureBoxColorPicker.TabStop = false;
            this.pictureBoxColorPicker.BackColorChanged += new System.EventHandler(this.pictureBoxColorPicker_BackColorChanged);
            this.pictureBoxColorPicker.Click += new System.EventHandler(this.pictureBoxColorPicker_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Точка начала координат";
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.Location = new System.Drawing.Point(122, 358);
            this.numericUpDownZ.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownZ.TabIndex = 8;
            this.numericUpDownZ.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(64, 358);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownY.TabIndex = 7;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(6, 358);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownX.TabIndex = 6;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // checkBoxAxes
            // 
            this.checkBoxAxes.AutoSize = true;
            this.checkBoxAxes.Location = new System.Drawing.Point(188, 361);
            this.checkBoxAxes.Name = "checkBoxAxes";
            this.checkBoxAxes.Size = new System.Drawing.Size(46, 17);
            this.checkBoxAxes.TabIndex = 5;
            this.checkBoxAxes.Text = "Оси";
            this.checkBoxAxes.UseVisualStyleBackColor = true;
            this.checkBoxAxes.CheckedChanged += new System.EventHandler(this.checkBoxAxes_CheckedChanged);
            // 
            // buttonDeleteGrouping
            // 
            this.buttonDeleteGrouping.Location = new System.Drawing.Point(29, 147);
            this.buttonDeleteGrouping.Name = "buttonDeleteGrouping";
            this.buttonDeleteGrouping.Size = new System.Drawing.Size(195, 23);
            this.buttonDeleteGrouping.TabIndex = 4;
            this.buttonDeleteGrouping.Text = "Разбить группу";
            this.buttonDeleteGrouping.UseVisualStyleBackColor = true;
            this.buttonDeleteGrouping.Click += new System.EventHandler(this.buttonDeleteGrouping_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 384);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(246, 66);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "Ctrl + click - выделение объектов в группу\nShift + Удалить - разбить группу";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(246, 83);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "";
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(29, 118);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(195, 23);
            this.buttonDel.TabIndex = 1;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // button2PointsLine
            // 
            this.button2PointsLine.Location = new System.Drawing.Point(29, 89);
            this.button2PointsLine.Name = "button2PointsLine";
            this.button2PointsLine.Size = new System.Drawing.Size(105, 23);
            this.button2PointsLine.TabIndex = 0;
            this.button2PointsLine.Text = "Линия";
            this.button2PointsLine.UseVisualStyleBackColor = true;
            this.button2PointsLine.Click += new System.EventHandler(this.button2PointsLine_Click);
            // 
            // checkBoxOnLocal
            // 
            this.checkBoxOnLocal.AutoSize = true;
            this.checkBoxOnLocal.Location = new System.Drawing.Point(6, 313);
            this.checkBoxOnLocal.Name = "checkBoxOnLocal";
            this.checkBoxOnLocal.Size = new System.Drawing.Size(242, 17);
            this.checkBoxOnLocal.TabIndex = 13;
            this.checkBoxOnLocal.Text = "Преобразования относительно локальной";
            this.checkBoxOnLocal.UseVisualStyleBackColor = true;
            this.checkBoxOnLocal.CheckedChanged += new System.EventHandler(this.checkBoxOnLocal_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainerMain);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "X";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button button2PointsLine;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonDeleteGrouping;
        private System.Windows.Forms.CheckBox checkBoxAxes;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxColorPicker;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown numericUpDownThick;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.CheckBox checkBoxOnLocal;
    }
}

