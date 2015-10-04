using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CRAPserver
{
    class HTTPServer
    {
        private HttpListener listener;
        private Data dataObject;
        private MainView mainViewForm;
        private AddToLogFunctionDelegate addToLogDelegate;
        /*
        private AddStateDelegate addStateDelegate;
        private GetStateTypeDelegate getStateTypeDelegate;
        private AddNodeDelegate addNodeDelegate;
        private GetIPAddressDelegate getIPAddressDelegate;
        private AddMessageToTableDelegate addMessageToTableDelegate;
        private GetMessageDelegate getMessageDelegate;*/

        public HTTPServer(MainView mv, AddToLogFunctionDelegate delATLF, Data dat)
            /*,
            AddStateDelegate delAS, GetStateTypeDelegate delGST, AddNodeDelegate delAN, GetIPAddressDelegate delGIA, AddMessageToTableDelegate delAMTT, GetMessageDelegate delGM)*/
        {
            // Creates new HTTP listener object
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:6372/");

            mainViewForm = mv;
            dataObject = dat;
            addToLogDelegate = delATLF;
            /*addStateDelegate = delAS;
            getStateTypeDelegate = delGST;
            addNodeDelegate = delAN;
            getIPAddressDelegate = delGIA;
            addMessageToTableDelegate = delAMTT;
            getMessageDelegate = delGM;*/
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

            string UriRequest = context.Request.Url.AbsoluteUri;
            string LocalPathRequest = context.Request.Url.LocalPath;
            HTTPGetArgumentParse ParsedURI = new HTTPGetArgumentParse(UriRequest);

            byte[] buffer = null;


            if (LocalPathRequest.Length == 7 && LocalPathRequest.Substring(0, 7).Equals("/a.crap"))
            {
                if (ParsedURI.getType() == 0)
                {
                    // Command recieved
                    dataObject.GetIPAddress(ParsedURI.getNodeID());
                    IPAddress ipAddress = 
                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                else if (ParsedURI.getType() == 1)
                {
                    // Update recieved

                }

                // Put the file in the buffer, and send to the client
                buffer = Encoding.UTF8.GetBytes("<html><body><h1>NodeID: " + ParsedURI.getNodeID().ToString() + "</h></body></html>");
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

