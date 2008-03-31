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

        public MotionForm(MotionControllerOperations port)
        {
            _port = port;
            InitializeComponent();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (txtDistControl.Text.Length > 0)
            {
                _port.Post(new Drive(new DriveRequest(double.Parse(txtDistControl.Text), .4)));
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (txtDistControl.Text.Length > 0)
            {
                _port.Post(new Drive(new DriveRequest(-1 * double.Parse(txtDistControl.Text), .4)));
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (txtDegreeControl.Text.Length > 0)
            {
                double radians = double.Parse(txtDegreeControl.Text) * Math.PI / 180;
                _port.Post(new Turn(new TurnRequest(radians, .4)));
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (txtDegreeControl.Text.Length > 0)
            {
                double radians = -1 * double.Parse(txtDegreeControl.Text) * Math.PI / 180;
                _port.Post(new Turn(new TurnRequest(radians, .4)));
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
                _port.Post(new BeginCalibrateDrive(new BeginCalibrateDriveRequest()));
                btnDriveCalibrate.Text = "End Drive";
            }
            else if (btnDriveCalibrate.Text == "End Drive")
            {
                _port.Post(new Stop());
                btnDriveCalibrate.Enabled = false;
                btnDriveSubmit.Enabled = true;
                txtDistCalibrate.Enabled = true;
            }
        }

        private void btnDriveSubmit_Click(object sender, EventArgs e)
        {
            if(txtDistCalibrate.Text.Length > 0)
            {
                _port.Post(new SetDriveCalibration(new SetDriveCalibrationRequest(double.Parse(txtDistCalibrate.Text))));
                btnDriveCalibrate.Enabled = true;
                btnDriveCalibrate.Text = "Begin Drive";
                btnDriveSubmit.Enabled = false;
                txtDistCalibrate.Enabled = false;
            }
        }

        private void btnTurnCalibrate_Click(object sender, EventArgs e)
        {
            if (btnTurnCalibrate.Text == "Begin Turn")
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

        private void btnTurnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDegreeCalibrate.Text.Length > 0)
            {
                double radians = double.Parse(txtDegreeCalibrate.Text) * Math.PI / 180;
                _port.Post(new SetTurnCalibration(new SetTurnCalibrationRequest(radians)));
                btnTurnCalibrate.Enabled = true;
                btnTurnCalibrate.Text = "Begin Turn";
                btnTurnSubmit.Enabled = false;
                txtDegreeCalibrate.Enabled = false;
            }
        }
    }
}