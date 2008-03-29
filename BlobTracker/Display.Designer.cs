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
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
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
            this.textBox.Size = new System.Drawing.Size(824, 246);
            this.textBox.TabIndex = 1;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(661, 80);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(153, 36);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(658, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Red";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rMin
            // 
            this.rMin.Location = new System.Drawing.Point(702, 166);
            this.rMin.Name = "rMin";
            this.rMin.Size = new System.Drawing.Size(43, 20);
            this.rMin.TabIndex = 4;
            this.rMin.Text = "200";
            // 
            // rMax
            // 
            this.rMax.Location = new System.Drawing.Point(751, 166);
            this.rMax.Name = "rMax";
            this.rMax.Size = new System.Drawing.Size(43, 20);
            this.rMax.TabIndex = 5;
            this.rMax.Text = "256";
            // 
            // gMax
            // 
            this.gMax.Location = new System.Drawing.Point(751, 192);
            this.gMax.Name = "gMax";
            this.gMax.Size = new System.Drawing.Size(43, 20);
            this.gMax.TabIndex = 8;
            this.gMax.Text = "100";
            // 
            // gMin
            // 
            this.gMin.Location = new System.Drawing.Point(702, 192);
            this.gMin.Name = "gMin";
            this.gMin.Size = new System.Drawing.Size(43, 20);
            this.gMin.TabIndex = 7;
            this.gMin.Text = "0";
            // 
            // Green
            // 
            this.Green.AutoSize = true;
            this.Green.Location = new System.Drawing.Point(658, 195);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(36, 13);
            this.Green.TabIndex = 6;
            this.Green.Text = "Green";
            // 
            // bMax
            // 
            this.bMax.Location = new System.Drawing.Point(751, 218);
            this.bMax.Name = "bMax";
            this.bMax.Size = new System.Drawing.Size(43, 20);
            this.bMax.TabIndex = 11;
            this.bMax.Text = "100";
            // 
            // bMin
            // 
            this.bMin.Location = new System.Drawing.Point(702, 218);
            this.bMin.Name = "bMin";
            this.bMin.Size = new System.Drawing.Size(43, 20);
            this.bMin.TabIndex = 10;
            this.bMin.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(658, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Blue";
            // 
            // btnSetBin
            // 
            this.btnSetBin.Location = new System.Drawing.Point(702, 263);
            this.btnSetBin.Name = "btnSetBin";
            this.btnSetBin.Size = new System.Drawing.Size(75, 23);
            this.btnSetBin.TabIndex = 12;
            this.btnSetBin.Text = "Set Bin";
            this.btnSetBin.UseVisualStyleBackColor = true;
            this.btnSetBin.Click += new System.EventHandler(this.btnSetBin_Click);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 748);
            this.Controls.Add(this.btnSetBin);
            this.Controls.Add(this.bMax);
            this.Controls.Add(this.bMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gMax);
            this.Controls.Add(this.gMin);
            this.Controls.Add(this.Green);
            this.Controls.Add(this.rMax);
            this.Controls.Add(this.rMin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.picBox);
            this.Name = "Display";
            this.Text = "Display";
            this.Load += new System.EventHandler(this.Display_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
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
    }
}