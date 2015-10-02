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
        public AddToLogFunctionDelegate addToLogDelegate;

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

        private void Callback(IAsyncResult result)
        {
            HttpListenerContext context = null;
            HttpListenerResponse response = null;
            HttpListener listener = (HttpListener)result.AsyncState;
            if (!listener.IsListening)
                return;
            context = listener.EndGetContext(result);
            AsyncProcessing(listener);
            mainViewForm.Invoke(addToLogDelegate, "Request recieved.");
            response = context.Response;

            string page = Directory.GetCurrentDirectory() + context.Request.Url.LocalPath;
            
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
