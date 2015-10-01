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
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://127.0.0.1/");
            server.Prefixes.Add("http://localhost/");

            server.Start();

            Debug.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerResponse response = context.Response;

                string page = Directory.GetCurrentDirectory() + context.Request.Url.LocalPath;

                if (page == string.Empty)
                    page = "index.html";

                TextReader tr = new StreamReader(page);
                string msg = tr.ReadToEnd();


                byte[] buffer = Encoding.UTF8.GetBytes(msg);

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }
        }
    }
}
