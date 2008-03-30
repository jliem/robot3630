using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

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
            this.WindowState = FormWindowState.Maximized;
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
                this.textBox.AppendText(message + Environment.NewLine);

                this.textBox.SelectionStart = textBox.Text.Length;
                textBox.ScrollToCaret();
            }
        }


        private void btnPause_Click(object sender, EventArgs e)
        {

            if (bt.timer.Enabled == true)
            {
                Write("*** PAUSED ***");
                btnPause.Text = "Unpause";
                bt.SetTimerEnabled(false);
            }
            else
            {
                Write("*** UNPAUSED ***");
                btnPause.Text = "Pause";
                bt.SetTimerEnabled(true);
         
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

            txtBin.Text = bin.RedMin + "-" + bin.RedMax + ", " + bin.GreenMin + "-" +
                bin.GreenMax + ", " + bin.BlueMin + "-" + bin.BlueMax;
        }

        private void btnRobotMoved_Click(object sender, EventArgs e)
        {
            Write("***** Robot has moved *****");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String filePath = txtFilePath.Text;

            if (File.Exists(filePath))
            {
                Write("Could not write to " + filePath + " because there was already a file there");
            }
            else
            {
                Bitmap img = (Bitmap)(picBox.Image);
                img.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);

                Write("Saved image to " + filePath);
            }
        }

        private void btnSetInterval_Click(object sender, EventArgs e)
        {
            bt.timer.Interval = int.Parse(txtInterval.Text);
            Write("Interval set to " + bt.timer.Interval);
        }

        private void text_GotFocus(object sender, EventArgs e)
        {
            // C# sucks, apparently this doesn't work b/c mouse_down gets called and
            // the text is immediately deselected
            TextBox box = (TextBox)sender;
            box.SelectAll();
        }
    }
}