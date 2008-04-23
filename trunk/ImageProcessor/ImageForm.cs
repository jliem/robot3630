using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Robotics.CoroBot.ImageProcessor;
using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;

namespace Robotics.CoroBot.ImageProcessor
{
    public partial class ImageForm : Form
    {
        private ImageProcessorOperations _imagePort;

        public ImageForm(ImageProcessorOperations imagePort)
        {
            InitializeComponent();
            _imagePort = imagePort;
        }

        public void UpdateImage(Bitmap image)
        {
            picBox.Image = image;
        }

        public void UpdateText(string text)
        {
            txtResults.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _imagePort.Post(new Get());
        }
    }
}