using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRAPserver
{
    public class HTTPServer
    {
        private HttpListener listener;
        private MainView mainViewForm;
        private AddToLogFunctionDelegate addToLogDelegate;

        public HTTPServer(MainView mv, AddToLogFunctionDelegate del)
        {
            // Creates new HTTP listener object
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");

            mainViewForm = mv;
            addToLogDelegate = del;
        }

        public void Start()
        {
            listener.Start();
            AsyncProcessing(listener);
            mainViewForm.Invoke(addToLogDelegate, "Server started.");
        }

        public void Stop()
        {
            listener.Stop();
            mainViewForm.Invoke(addToLogDelegate, "Server stopped.");
        }

        private void AsyncProcessing(HttpListener listener)
        {
            if (listener == null)
                return;
            listener.BeginGetContext(new AsyncCallback(Callback), listener);
        }

        // Called when a URL is requested
        private void Callback(IAsyncResult result)
        {
            HttpListenerContext context = null;
            HttpListenerResponse response = null;
            HttpListener listener = (HttpListener)result.AsyncState;

            if (!listener.IsListening)
                return;

            context = listener.EndGetContext(result);
            response = context.Response;

            AsyncProcessing(listener);

            // Extract request and serve up an error page if not json
            string jsonRequest = context.Request.Url.LocalPath;
            byte[] buffer = null;

            if (jsonRequest.Length == 7 && jsonRequest.Substring(0, 7).Equals("/a.crap"))
            {
                string page = Directory.GetCurrentDirectory() + "\\index2.html";
                // Read file
                TextReader tr = new StreamReader(page);
                string msg = tr.ReadToEnd();

                // Put the file in the buffer, and send to the client
                buffer = Encoding.UTF8.GetBytes(msg);
                mainViewForm.Invoke(addToLogDelegate, "Legitimate request made.");
            }
            else
            {
                string page = Directory.GetCurrentDirectory() + "\\accessdenied.html";
                // Read file
                TextReader tr = new StreamReader(page);
                string msg = tr.ReadToEnd();

                // Put the file in the buffer
                buffer = Encoding.UTF8.GetBytes(msg);
                mainViewForm.Invoke(addToLogDelegate, "Access denied page served.");
            }

            // Send the buffer to the client
            response.ContentLength64 = buffer.Length;
            Stream st = response.OutputStream;
            st.Write(buffer, 0, buffer.Length);

            context.Response.Close();
        }
    }

}

