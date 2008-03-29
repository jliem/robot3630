using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Robotics.Services.Sample.BlobTracker
{
    public partial class Display : Form
    {
        private BlobTrackerService bt;

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);

        private ColorBin bin;

        public Display(BlobTrackerService bt)
        {
            this.bt = bt;
            InitializeComponent();
        }

        private void Display_Load(object sender, EventArgs e)
        {
        }

        public void SetImage(Image img)
        {
            picBox.Image = img;
        }

        public void Write(String message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Write);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.textBox.Text += message + Environment.NewLine;
                this.textBox.SelectionStart = textBox.Text.Length - 1;
                textBox.ScrollToCaret();
            }
        }


        private void btnPause_Click(object sender, EventArgs e)
        {

            bt.toggleTimer();

            if (bt.timer.Enabled == true)
            {
                btnPause.Text = "Pause";
            }
            else
            {
                btnPause.Text = "Unpause";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSetBin_Click(object sender, EventArgs e)
        {
            if (bin != null)
            {
                // Remove existing bin
                bt.RemoveColorBin(bin);
            }

            bin = new ColorBin();
            bin.RedMin = int.Parse(rMin.Text);
            bin.RedMax = int.Parse(rMax.Text);
            bin.BlueMin = int.Parse(bMin.Text);
            bin.BlueMax = int.Parse(bMax.Text);
            bin.GreenMin = int.Parse(gMin.Text);
            bin.GreenMax = int.Parse(gMax.Text);

            bt.AddColorBin(bin);
        }
    }
}