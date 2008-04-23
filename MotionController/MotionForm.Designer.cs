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
            this.btnLeftSubmit = new System.Windows.Forms.Button();
            this.txtLeftCalibrate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCalibrateLeft = new System.Windows.Forms.Button();
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
            this.txtCurrX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnSetAsCurrent = new System.Windows.Forms.Button();
            this.txtDestY = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDestX = new System.Windows.Forms.TextBox();
            this.txtCurrHeading = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCurrY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox2.Controls.Add(this.btnLeftSubmit);
            this.groupBox2.Controls.Add(this.txtLeftCalibrate);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnCalibrateLeft);
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
            this.groupBox2.Size = new System.Drawing.Size(278, 111);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calibration";
            // 
            // btnLeftSubmit
            // 
            this.btnLeftSubmit.Enabled = false;
            this.btnLeftSubmit.Location = new System.Drawing.Point(191, 77);
            this.btnLeftSubmit.Name = "btnLeftSubmit";
            this.btnLeftSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnLeftSubmit.TabIndex = 18;
            this.btnLeftSubmit.Text = "Submit Left";
            this.btnLeftSubmit.UseVisualStyleBackColor = true;
            this.btnLeftSubmit.Click += new System.EventHandler(this.btnLeftSubmit_Click);
            // 
            // txtLeftCalibrate
            // 
            this.txtLeftCalibrate.Enabled = false;
            this.txtLeftCalibrate.Location = new System.Drawing.Point(142, 79);
            this.txtLeftCalibrate.Name = "txtLeftCalibrate";
            this.txtLeftCalibrate.Size = new System.Drawing.Size(44, 20);
            this.txtLeftCalibrate.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(87, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Degrees";
            // 
            // btnCalibrateLeft
            // 
            this.btnCalibrateLeft.Location = new System.Drawing.Point(6, 77);
            this.btnCalibrateLeft.Name = "btnCalibrateLeft";
            this.btnCalibrateLeft.Size = new System.Drawing.Size(75, 23);
            this.btnCalibrateLeft.TabIndex = 15;
            this.btnCalibrateLeft.Text = "Begin Left";
            this.btnCalibrateLeft.UseVisualStyleBackColor = true;
            this.btnCalibrateLeft.Click += new System.EventHandler(this.btnCalibrateLeft_Click);
            // 
            // btnTurnSubmit
            // 
            this.btnTurnSubmit.Enabled = false;
            this.btnTurnSubmit.Location = new System.Drawing.Point(191, 48);
            this.btnTurnSubmit.Name = "btnTurnSubmit";
            this.btnTurnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnTurnSubmit.TabIndex = 14;
            this.btnTurnSubmit.Text = "Submit Right";
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
            this.btnTurnCalibrate.Text = "Begin Right";
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
            this.btnWaypointTest.Location = new System.Drawing.Point(478, 190);
            this.btnWaypointTest.Name = "btnWaypointTest";
            this.btnWaypointTest.Size = new System.Drawing.Size(183, 23);
            this.btnWaypointTest.TabIndex = 2;
            this.btnWaypointTest.Text = "Begin Waypoint Test";
            this.btnWaypointTest.UseVisualStyleBackColor = true;
            this.btnWaypointTest.Click += new System.EventHandler(this.btnWaypointTest_Click);
            // 
            // btnManualCalibrate
            // 
            this.btnManualCalibrate.Location = new System.Drawing.Point(480, 158);
            this.btnManualCalibrate.Name = "btnManualCalibrate";
            this.btnManualCalibrate.Size = new System.Drawing.Size(183, 23);
            this.btnManualCalibrate.TabIndex = 3;
            this.btnManualCalibrate.Text = "Manual calibration";
            this.btnManualCalibrate.UseVisualStyleBackColor = true;
            this.btnManualCalibrate.Click += new System.EventHandler(this.btnManualCalibrate_Click);
            // 
            // txtTurnEncoder
            // 
            this.txtTurnEncoder.Location = new System.Drawing.Point(429, 184);
            this.txtTurnEncoder.Name = "txtTurnEncoder";
            this.txtTurnEncoder.Size = new System.Drawing.Size(44, 20);
            this.txtTurnEncoder.TabIndex = 18;
            this.txtTurnEncoder.Text = "2200";
            // 
            // txtDistanceEncoder
            // 
            this.txtDistanceEncoder.Location = new System.Drawing.Point(428, 154);
            this.txtDistanceEncoder.Name = "txtDistanceEncoder";
            this.txtDistanceEncoder.Size = new System.Drawing.Size(44, 20);
            this.txtDistanceEncoder.TabIndex = 17;
            this.txtDistanceEncoder.Text = "2114";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(329, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "360 Right";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "1 m encoder:";
            // 
            // txtCurrX
            // 
            this.txtCurrX.Location = new System.Drawing.Point(51, 34);
            this.txtCurrX.Name = "txtCurrX";
            this.txtCurrX.Size = new System.Drawing.Size(44, 20);
            this.txtCurrX.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Current:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "X";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGo);
            this.groupBox3.Controls.Add(this.btnSetAsCurrent);
            this.groupBox3.Controls.Add(this.txtDestY);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtDestX);
            this.groupBox3.Controls.Add(this.txtCurrHeading);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtCurrY);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtCurrX);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(327, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(288, 121);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Coordinate Control";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(51, 89);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 20;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnSetAsCurrent
            // 
            this.btnSetAsCurrent.Location = new System.Drawing.Point(184, 58);
            this.btnSetAsCurrent.Name = "btnSetAsCurrent";
            this.btnSetAsCurrent.Size = new System.Drawing.Size(98, 23);
            this.btnSetAsCurrent.TabIndex = 20;
            this.btnSetAsCurrent.Text = "Set as Current";
            this.btnSetAsCurrent.UseVisualStyleBackColor = true;
            this.btnSetAsCurrent.Click += new System.EventHandler(this.btnSetAsCurrent_Click);
            // 
            // txtDestY
            // 
            this.txtDestY.Location = new System.Drawing.Point(101, 60);
            this.txtDestY.Name = "txtDestY";
            this.txtDestY.Size = new System.Drawing.Size(44, 20);
            this.txtDestY.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Dest:";
            // 
            // txtDestX
            // 
            this.txtDestX.Location = new System.Drawing.Point(51, 60);
            this.txtDestX.Name = "txtDestX";
            this.txtDestX.Size = new System.Drawing.Size(44, 20);
            this.txtDestX.TabIndex = 17;
            // 
            // txtCurrHeading
            // 
            this.txtCurrHeading.Location = new System.Drawing.Point(151, 34);
            this.txtCurrHeading.Name = "txtCurrHeading";
            this.txtCurrHeading.Size = new System.Drawing.Size(44, 20);
            this.txtCurrHeading.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(148, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Heading";
            // 
            // txtCurrY
            // 
            this.txtCurrY.Location = new System.Drawing.Point(101, 34);
            this.txtCurrY.Name = "txtCurrY";
            this.txtCurrY.Size = new System.Drawing.Size(44, 20);
            this.txtCurrY.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(111, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Y";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(429, 212);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 20);
            this.textBox1.TabIndex = 21;
            this.textBox1.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(329, 215);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "360 Left";
            // 
            // MotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 281);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox3);
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
            this.Load += new System.EventHandler(this.MotionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.TextBox txtCurrX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCurrHeading;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCurrY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSetAsCurrent;
        private System.Windows.Forms.TextBox txtDestY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDestX;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnLeftSubmit;
        private System.Windows.Forms.TextBox txtLeftCalibrate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCalibrateLeft;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label12;
    }
}