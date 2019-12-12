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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonDeleteGrouping = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonDel = new System.Windows.Forms.Button();
            this.button2PointsLine = new System.Windows.Forms.Button();
            this.checkBoxAxes = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelButtons.SuspendLayout();
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
            this.splitContainerMain.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelButtons);
            this.splitContainerMain.Size = new System.Drawing.Size(800, 450);
            this.splitContainerMain.SplitterDistance = 550;
            this.splitContainerMain.TabIndex = 0;
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
            this.textBox1.Location = new System.Drawing.Point(29, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(195, 56);
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
            this.button2PointsLine.Size = new System.Drawing.Size(195, 23);
            this.button2PointsLine.TabIndex = 0;
            this.button2PointsLine.Text = "Линия";
            this.button2PointsLine.UseVisualStyleBackColor = true;
            this.button2PointsLine.Click += new System.EventHandler(this.button2PointsLine_Click);
            // 
            // checkBoxAxes
            // 
            this.checkBoxAxes.AutoSize = true;
            this.checkBoxAxes.Location = new System.Drawing.Point(4, 361);
            this.checkBoxAxes.Name = "checkBoxAxes";
            this.checkBoxAxes.Size = new System.Drawing.Size(46, 17);
            this.checkBoxAxes.TabIndex = 5;
            this.checkBoxAxes.Text = "Оси";
            this.checkBoxAxes.UseVisualStyleBackColor = true;
            this.checkBoxAxes.CheckedChanged += new System.EventHandler(this.checkBoxAxes_CheckedChanged);
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
    }
}

