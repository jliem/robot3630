using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Robotics.CoroBot.Coordinator;
using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;

namespace Robotics.CoroBot.Coordinator
{
    public partial class CoordinatorForm : Form
    {
        CoordinatorOperations port;

        public CoordinatorForm(CoordinatorOperations port)
        {
            this.port = port;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            port.Post(new Get());
        }

        public void AddText(string text)
        {

        }
    }
}