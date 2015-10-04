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
        Data dataObject = new Data();
        public AddToLogFunctionDelegate addToLogDelegate;

        public MainView()
        {
            addToLogDelegate = new AddToLogFunctionDelegate(addToLog);
            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            server = new HTTPServer(this, addToLogDelegate);
            Data testData = new Data();
           int t= testData.AddNode(12345678, 87654321, "192.168.1.1", "Dave the CRAPnode");
           string test = testData.getIPAddress(12345678);
           Console.WriteLine("Ip Address =" + test);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
