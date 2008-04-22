namespace Robotics.CoroBot.MotionController
{
    partial class MotionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotionForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.txtDegreeControl = new System.Windows.Forms.TextBox();
            this.txtDistControl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTurnSubmit = new System.Windows.Forms.Button();
            this.btnDriveSubmit = new System.Windows.Forms.Button();
            this.txtDegreeCalibrate = new System.Windows.Forms.TextBox();
            this.txtDistCalibrate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTurnCalibrate = new System.Windows.Forms.Button();
            this.btnDriveCalibrate = new System.Windows.Forms.Button();
            this.btnWaypointTest = new System.Windows.Forms.Button();
            this.btnManualCalibrate = new System.Windows.Forms.Button();
            this.txtTurnEncoder = new System.Windows.Forms.TextBox();
            this.txtDistanceEncoder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnBackward);
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this.txtDegreeControl);
            this.groupBox1.Controls.Add(this.txtDistControl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive Controls";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(156, 79);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(196, 50);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 7;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(114, 50);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnLeft.TabIndex = 6;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Location = new System.Drawing.Point(196, 15);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(75, 23);
            this.btnBackward.TabIndex = 5;
            this.btnBackward.Text = "Backward";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(114, 15);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 4;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // txtDegreeControl
            // 
            this.txtDegreeControl.Location = new System.Drawing.Point(61, 52);
            this.txtDegreeControl.Name = "txtDegreeControl";
            this.txtDegreeControl.Size = new System.Drawing.Size(44, 20);
            this.txtDegreeControl.TabIndex = 3;
            // 
            // txtDistControl
            // 
            this.txtDistControl.Location = new System.Drawing.Point(60, 17);
            this.txtDistControl.Name = "txtDistControl";
            this.txtDistControl.Size = new System.Drawing.Size(44, 20);
            this.txtDistControl.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Degrees";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Distance:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTurnSubmit);
            this.groupBox2.Controls.Add(this.btnDriveSubmit);
            this.groupBox2.Controls.Add(this.txtDegreeCalibrate);
            this.groupBox2.Controls.Add(this.txtDistCalibrate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnTurnCalibrate);
            this.groupBox2.Controls.Add(this.btnDriveCalibrate);
            this.groupBox2.Location = new System.Drawing.Point(12, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calibration";
            // 
            // btnTurnSubmit
            // 
            this.btnTurnSubmit.Enabled = false;
            this.btnTurnSubmit.Location = new System.Drawing.Point(191, 48);
            this.btnTurnSubmit.Name = "btnTurnSubmit";
            this.btnTurnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnTurnSubmit.TabIndex = 14;
            this.btnTurnSubmit.Text = "Submit Turn";
            this.btnTurnSubmit.UseVisualStyleBackColor = true;
            this.btnTurnSubmit.Click += new System.EventHandler(this.btnTurnSubmit_Click);
            // 
            // btnDriveSubmit
            // 
            this.btnDriveSubmit.Enabled = false;
            this.btnDriveSubmit.Location = new System.Drawing.Point(191, 17);
            this.btnDriveSubmit.Name = "btnDriveSubmit";
            this.btnDriveSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnDriveSubmit.TabIndex = 13;
            this.btnDriveSubmit.Text = "Submit Drive";
            this.btnDriveSubmit.UseVisualStyleBackColor = true;
            this.btnDriveSubmit.Click += new System.EventHandler(this.btnDriveSubmit_Click);
            // 
            // txtDegreeCalibrate
            // 
            this.txtDegreeCalibrate.Enabled = false;
            this.txtDegreeCalibrate.Location = new System.Drawing.Point(142, 50);
            this.txtDegreeCalibrate.Name = "txtDegreeCalibrate";
            this.txtDegreeCalibrate.Size = new System.Drawing.Size(44, 20);
            this.txtDegreeCalibrate.TabIndex = 12;
            // 
            // txtDistCalibrate
            // 
            this.txtDistCalibrate.Enabled = false;
            this.txtDistCalibrate.Location = new System.Drawing.Point(141, 20);
            this.txtDistCalibrate.Name = "txtDistCalibrate";
            this.txtDistCalibrate.Size = new System.Drawing.Size(44, 20);
            this.txtDistCalibrate.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Degrees";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Distance:";
            // 
            // btnTurnCalibrate
            // 
            this.btnTurnCalibrate.Location = new System.Drawing.Point(6, 48);
            this.btnTurnCalibrate.Name = "btnTurnCalibrate";
            this.btnTurnCalibrate.Size = new System.Drawing.Size(75, 23);
            this.btnTurnCalibrate.TabIndex = 8;
            this.btnTurnCalibrate.Text = "Begin Turn";
            this.btnTurnCalibrate.UseVisualStyleBackColor = true;
            this.btnTurnCalibrate.Click += new System.EventHandler(this.btnTurnCalibrate_Click);
            // 
            // btnDriveCalibrate
            // 
            this.btnDriveCalibrate.Location = new System.Drawing.Point(6, 19);
            this.btnDriveCalibrate.Name = "btnDriveCalibrate";
            this.btnDriveCalibrate.Size = new System.Drawing.Size(75, 23);
            this.btnDriveCalibrate.TabIndex = 7;
            this.btnDriveCalibrate.Text = "Begin Drive";
            this.btnDriveCalibrate.UseVisualStyleBackColor = true;
            this.btnDriveCalibrate.Click += new System.EventHandler(this.btnDriveCalibrate_Click);
            // 
            // btnWaypointTest
            // 
            this.btnWaypointTest.Location = new System.Drawing.Point(18, 229);
            this.btnWaypointTest.Name = "btnWaypointTest";
            this.btnWaypointTest.Size = new System.Drawing.Size(183, 23);
            this.btnWaypointTest.TabIndex = 2;
            this.btnWaypointTest.Text = "Begin Waypoint Test";
            this.btnWaypointTest.UseVisualStyleBackColor = true;
            this.btnWaypointTest.Click += new System.EventHandler(this.btnWaypointTest_Click);
            // 
            // btnManualCalibrate
            // 
            this.btnManualCalibrate.Location = new System.Drawing.Point(168, 273);
            this.btnManualCalibrate.Name = "btnManualCalibrate";
            this.btnManualCalibrate.Size = new System.Drawing.Size(183, 23);
            this.btnManualCalibrate.TabIndex = 3;
            this.btnManualCalibrate.Text = "Manual calibration";
            this.btnManualCalibrate.UseVisualStyleBackColor = true;
            this.btnManualCalibrate.Click += new System.EventHandler(this.btnManualCalibrate_Click);
            // 
            // txtTurnEncoder
            // 
            this.txtTurnEncoder.Location = new System.Drawing.Point(117, 299);
            this.txtTurnEncoder.Name = "txtTurnEncoder";
            this.txtTurnEncoder.Size = new System.Drawing.Size(44, 20);
            this.txtTurnEncoder.TabIndex = 18;
            this.txtTurnEncoder.Text = "2200";
            // 
            // txtDistanceEncoder
            // 
            this.txtDistanceEncoder.Location = new System.Drawing.Point(116, 269);
            this.txtDistanceEncoder.Name = "txtDistanceEncoder";
            this.txtDistanceEncoder.Size = new System.Drawing.Size(44, 20);
            this.txtDistanceEncoder.TabIndex = 17;
            this.txtDistanceEncoder.Text = "2114";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "360 deg. encoder:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "1 m encoder:";
            // 
            // MotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 427);
            this.Controls.Add(this.txtTurnEncoder);
            this.Controls.Add(this.txtDistanceEncoder);
            this.Controls.Add(this.btnManualCalibrate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnWaypointTest);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MotionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Motion Controller";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.TextBox txtDegreeControl;
        private System.Windows.Forms.TextBox txtDistControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnTurnSubmit;
        private System.Windows.Forms.Button btnDriveSubmit;
        private System.Windows.Forms.TextBox txtDegreeCalibrate;
        private System.Windows.Forms.TextBox txtDistCalibrate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTurnCalibrate;
        private System.Windows.Forms.Button btnDriveCalibrate;
        private System.Windows.Forms.Button btnWaypointTest;
        private System.Windows.Forms.Button btnManualCalibrate;
        private System.Windows.Forms.TextBox txtTurnEncoder;
        private System.Windows.Forms.TextBox txtDistanceEncoder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}