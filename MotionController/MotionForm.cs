using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Robotics.CoroBot.MotionController
{
    public partial class MotionForm : Form
    {
        MotionControllerOperations _port;

        private double motorPower;

        private DateTime startTime;
        private DateTime endTime;

        public MotionForm(MotionControllerOperations port, double motorPower)
        {
            _port = port;
            this.motorPower = motorPower;

            InitializeComponent();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (txtDistControl.Text.Length > 0)
            {
                _port.Post(new Drive(new DriveRequest(double.Parse(txtDistControl.Text), motorPower)));
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (txtDistControl.Text.Length > 0)
            {
                _port.Post(new Drive(new DriveRequest(-1 * double.Parse(txtDistControl.Text), motorPower)));
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (txtDegreeControl.Text.Length > 0)
            {
                double radians = double.Parse(txtDegreeControl.Text) * Math.PI / 180;
                _port.Post(new Turn(new TurnRequest(radians, motorPower)));
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (txtDegreeControl.Text.Length > 0)
            {
                double radians = -1 * double.Parse(txtDegreeControl.Text) * Math.PI / 180;
                _port.Post(new Turn(new TurnRequest(radians, motorPower)));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _port.Post(new Stop());
        }

        private void btnDriveCalibrate_Click(object sender, EventArgs e)
        {
            if (btnDriveCalibrate.Text == "Begin Drive")
            {
                startTime = DateTime.Now;
                _port.Post(new BeginCalibrateDrive(new BeginCalibrateDriveRequest()));
                btnDriveCalibrate.Text = "End Drive";
            }
            else if (btnDriveCalibrate.Text == "End Drive")
            {
                _port.Post(new Stop());
                endTime = DateTime.Now;
                btnDriveCalibrate.Enabled = false;
                btnDriveSubmit.Enabled = true;
                txtDistCalibrate.Enabled = true;
            }
        }

        private void btnDriveSubmit_Click(object sender, EventArgs e)
        {
            if(txtDistCalibrate.Text.Length > 0)
            {
                _port.Post(new SetDriveCalibration(
                    new SetDriveCalibrationRequest(
                        double.Parse(txtDistCalibrate.Text),
                        (endTime-startTime))));
                btnDriveCalibrate.Enabled = true;
                btnDriveCalibrate.Text = "Begin Drive";
                btnDriveSubmit.Enabled = false;
                txtDistCalibrate.Enabled = false;
            }
        }

        private void btnTurnCalibrate_Click(object sender, EventArgs e)
        {
            if (btnTurnCalibrate.Text == "Begin Right")
            {
                _port.Post(new BeginCalibrateTurn(new BeginCalibrateTurnRequest()));
                btnTurnCalibrate.Text = "End Turn";
            }
            else if (btnTurnCalibrate.Text == "End Turn")
            {
                _port.Post(new Stop());
                btnTurnCalibrate.Enabled = false;
                btnTurnSubmit.Enabled = true;
                txtDegreeCalibrate.Enabled = true;
            }
        }

        private void btnCalibrateLeft_Click(object sender, EventArgs e)
        {
            if (btnCalibrateLeft.Text == "Begin Left")
            {
                _port.Post(new BeginCalibrateLeft(new BeginCalibrateLeftRequest()));
                btnCalibrateLeft.Text = "End Turn";
            }
            else if (btnCalibrateLeft.Text == "End Turn")
            {
                _port.Post(new Stop());
                btnCalibrateLeft.Enabled = false;
                btnLeftSubmit.Enabled = true;
                txtLeftCalibrate.Enabled = true;
            }
        }

        private void btnTurnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDegreeCalibrate.Text.Length > 0)
            {
                double radians = double.Parse(txtDegreeCalibrate.Text) * Math.PI / 180;
                _port.Post(new SetTurnCalibration(new SetTurnCalibrationRequest(radians)));
                btnTurnCalibrate.Enabled = true;
                btnTurnCalibrate.Text = "Begin Right";
                btnTurnSubmit.Enabled = false;
                txtDegreeCalibrate.Enabled = false;
            }
        }
        private void btnLeftSubmit_Click(object sender, EventArgs e)
        {
            if (txtLeftCalibrate.Text.Length > 0)
            {
                double radians = double.Parse(txtLeftCalibrate.Text) * Math.PI / 180;
                _port.Post(new SetLeftCalibration(new SetLeftCalibrationRequest(radians)));
                btnCalibrateLeft.Enabled = true;
                btnCalibrateLeft.Text = "Begin Left";
                btnLeftSubmit.Enabled = false;
                txtLeftCalibrate.Enabled = false;
            }

        }

        private void btnWaypointTest_Click(object sender, EventArgs e)
        {
            Vector2 prevWaypoint = new Vector2(double.Parse(txtCurrX.Text), double.Parse(txtCurrY.Text));
            double prevHeading = double.Parse(txtCurrHeading.Text) * Math.PI / 180;

            Vector2 point = new Vector2(double.Parse(txtDestX.Text), double.Parse(txtDestY.Text));
            LinkedList<Vector2> list = new LinkedList<Vector2>();
            list.AddLast(point);
            list.AddLast(new Vector2(21, 15));
            list.AddLast(new Vector2(21, 10));
            list.AddLast(new Vector2(21, 5));
            list.AddLast(new Vector2(21, 3));
            list.AddLast(new Vector2(15, 3));
            list.AddLast(new Vector2(10, 3));
            //list.AddLast(new Vector2(3, 2));

            _port.Post(new BeginWaypoint(new BeginWaypointRequest(prevWaypoint, prevHeading,
                list)));
            
        }

        private void btnManualCalibrate_Click(object sender, EventArgs e)
        {
            _port.Post(new SetManualCalibration(new SetManualCalibrationRequest(double.Parse(txtDistanceEncoder.Text),
                double.Parse(txtTurnEncoder.Text), double.Parse(txtLeftEncoder.Text))));
            MessageBox.Show("Manual calibration done");
        }

        private void MotionForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSetAsCurrent_Click(object sender, EventArgs e)
        {
            txtCurrX.Text = txtDestX.Text;
            txtCurrY.Text = txtDestY.Text;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Vector2 prevWaypoint = new Vector2(double.Parse(txtCurrX.Text), double.Parse(txtCurrY.Text));
            double prevHeading = double.Parse(txtCurrHeading.Text) * Math.PI / 180;

            Vector2 point = new Vector2(double.Parse(txtDestX.Text), double.Parse(txtDestY.Text));

            _port.Post(new BeginWaypoint(new BeginWaypointRequest(prevWaypoint, prevHeading,
                point)));
        }
    }
}