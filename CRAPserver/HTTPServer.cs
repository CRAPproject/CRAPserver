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

        public HTTPServer()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
        }

        public void Start()
        {
            listener.Start();
            AsyncProcessing(listener);
        }

        public void Stop()
        {
            listener.Stop();
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
            /* handle request */

            response = context.Response;

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
