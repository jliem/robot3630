namespace Microsoft.Robotics.Services.Sample.BlobTracker
{
    partial class Display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picBox = new System.Windows.Forms.PictureBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rMin = new System.Windows.Forms.TextBox();
            this.rMax = new System.Windows.Forms.TextBox();
            this.gMax = new System.Windows.Forms.TextBox();
            this.gMin = new System.Windows.Forms.TextBox();
            this.Green = new System.Windows.Forms.Label();
            this.bMax = new System.Windows.Forms.TextBox();
            this.bMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetBin = new System.Windows.Forms.Button();
            this.btnRobotMoved = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetInterval = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(1, 4);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(640, 480);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(1, 490);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(824, 218);
            this.textBox.TabIndex = 1;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(10, 87);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(113, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Red";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rMin
            // 
            this.rMin.Location = new System.Drawing.Point(59, 17);
            this.rMin.Name = "rMin";
            this.rMin.Size = new System.Drawing.Size(43, 20);
            this.rMin.TabIndex = 4;
            this.rMin.Text = "200";
            this.rMin.GotFocus += new System.EventHandler(this.text_GotFocus);
            // 
            // rMax
            // 
            this.rMax.Location = new System.Drawing.Point(108, 17);
            this.rMax.Name = "rMax";
            this.rMax.Size = new System.Drawing.Size(43, 20);
            this.rMax.TabIndex = 5;
            this.rMax.Text = "256";
            this.rMax.GotFocus += new System.EventHandler(this.text_GotFocus);
            // 
            // gMax
            // 
            this.gMax.Location = new System.Drawing.Point(108, 43);
            this.gMax.Name = "gMax";
            this.gMax.Size = new System.Drawing.Size(43, 20);
            this.gMax.TabIndex = 8;
            this.gMax.Text = "100";
            this.gMax.GotFocus += new System.EventHandler(this.text_GotFocus);
            // 
            // gMin
            // 
            this.gMin.Location = new System.Drawing.Point(59, 43);
            this.gMin.Name = "gMin";
            this.gMin.Size = new System.Drawing.Size(43, 20);
            this.gMin.TabIndex = 7;
            this.gMin.Text = "0";
            this.gMin.GotFocus += new System.EventHandler(this.text_GotFocus);
            // 
            // Green
            // 
            this.Green.AutoSize = true;
            this.Green.Location = new System.Drawing.Point(15, 46);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(36, 13);
            this.Green.TabIndex = 6;
            this.Green.Text = "Green";
            // 
            // bMax
            // 
            this.bMax.Location = new System.Drawing.Point(108, 69);
            this.bMax.Name = "bMax";
            this.bMax.Size = new System.Drawing.Size(43, 20);
            this.bMax.TabIndex = 11;
            this.bMax.Text = "100";
            this.bMax.GotFocus += new System.EventHandler(this.text_GotFocus);
            // 
            // bMin
            // 
            this.bMin.Location = new System.Drawing.Point(59, 69);
            this.bMin.Name = "bMin";
            this.bMin.Size = new System.Drawing.Size(43, 20);
            this.bMin.TabIndex = 10;
            this.bMin.Text = "0";
            this.bMin.GotFocus += new System.EventHandler(this.text_GotFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Blue";
            // 
            // btnSetBin
            // 
            this.btnSetBin.Location = new System.Drawing.Point(59, 114);
            this.btnSetBin.Name = "btnSetBin";
            this.btnSetBin.Size = new System.Drawing.Size(75, 23);
            this.btnSetBin.TabIndex = 12;
            this.btnSetBin.Text = "Set Bin";
            this.btnSetBin.UseVisualStyleBackColor = true;
            this.btnSetBin.Click += new System.EventHandler(this.btnSetBin_Click);
            // 
            // btnRobotMoved
            // 
            this.btnRobotMoved.Location = new System.Drawing.Point(661, 413);
            this.btnRobotMoved.Name = "btnRobotMoved";
            this.btnRobotMoved.Size = new System.Drawing.Size(147, 23);
            this.btnRobotMoved.TabIndex = 13;
            this.btnRobotMoved.Text = "Robot moved";
            this.btnRobotMoved.UseVisualStyleBackColor = true;
            this.btnRobotMoved.Click += new System.EventHandler(this.btnRobotMoved_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(661, 49);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(153, 35);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(661, 23);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(299, 20);
            this.txtFilePath.TabIndex = 15;
            this.txtFilePath.Text = "E:\\SVNCode\\3630\\Images\\test.jpg";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bMin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rMin);
            this.groupBox1.Controls.Add(this.rMax);
            this.groupBox1.Controls.Add(this.btnSetBin);
            this.groupBox1.Controls.Add(this.Green);
            this.groupBox1.Controls.Add(this.bMax);
            this.groupBox1.Controls.Add(this.gMin);
            this.groupBox1.Controls.Add(this.gMax);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(661, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 147);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bin Control";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSetInterval);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtInterval);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Location = new System.Drawing.Point(661, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 124);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer Control";
            // 
            // btnSetInterval
            // 
            this.btnSetInterval.Location = new System.Drawing.Point(10, 46);
            this.btnSetInterval.Name = "btnSetInterval";
            this.btnSetInterval.Size = new System.Drawing.Size(113, 23);
            this.btnSetInterval.TabIndex = 15;
            this.btnSetInterval.Text = "Set Interval";
            this.btnSetInterval.UseVisualStyleBackColor = true;
            this.btnSetInterval.Click += new System.EventHandler(this.btnSetInterval_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Interval (ms)";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(75, 20);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(61, 20);
            this.txtInterval.TabIndex = 14;
            this.txtInterval.Text = "3000";
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 748);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRobotMoved);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.picBox);
            this.Name = "Display";
            this.Text = "Display";
            this.Load += new System.EventHandler(this.Display_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rMin;
        private System.Windows.Forms.TextBox rMax;
        private System.Windows.Forms.TextBox gMax;
        private System.Windows.Forms.TextBox gMin;
        private System.Windows.Forms.Label Green;
        private System.Windows.Forms.TextBox bMax;
        private System.Windows.Forms.TextBox bMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetBin;
        private System.Windows.Forms.Button btnRobotMoved;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnSetInterval;
    }
}