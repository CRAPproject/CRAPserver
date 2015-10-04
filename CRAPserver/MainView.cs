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

            //testFunction();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            server = new HTTPServer(this, addToLogDelegate, dataObject);

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

        private void testFunction()
        {
            Data data = new Data();
            data.AddNode(1, 1, "192.168.1.1", "LEDBlinky");
            //  data.AddState(0, "0", 0, 0);
            int ti = data.AddState(1, "LED", 0, 0);
            Console.WriteLine("addstate success = " + ti.ToString());
            MessageObject testobj = new MessageObject();
            testobj.nodeid = 1;
            testobj.statetype1 = "1";
            testobj.state = "on";
            data.AddMessageToTable(testobj);
            MessageObject[] obj = data.GetMessage(1);
            string t = obj[0].Json(data);
            Console.WriteLine(t);
        }
    }
}
