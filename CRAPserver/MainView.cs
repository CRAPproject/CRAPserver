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
    public delegate void AddToLogFunctionDelegate(string lineDel);

    public partial class MainView : Form
    {
        HTTPServer server;
        public AddToLogFunctionDelegate addToLogDelegate;

        public MainView()
        {
            addToLogDelegate = new AddToLogFunctionDelegate(addToLog);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new HTTPServer(this, addToLogDelegate);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
        }

        public void addToLog(string line)
        {
            textLog.Text += (line + "\r\n");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
        }

    }
}
