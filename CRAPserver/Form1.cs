using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRAPserver
{
    public partial class Form1 : Form
    {
        HTTPServer server;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new HTTPServer();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
            //textLog.Text.
        }
    }
}
