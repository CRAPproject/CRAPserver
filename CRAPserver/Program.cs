using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;


namespace CRAPserver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

         
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
            
            Data testData = new Data();
            testData.AddNode(12345678, 87654321, "192.168.1.1", "Dave the CRAPnode");
        }
    }
}
