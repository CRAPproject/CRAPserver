using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CRAPserver
{
    class MessageObject
    {
        public int nodeid;
        public string statetype1;
        public string statetype2;
        public string statetype3;
        public string statetype4;
        public string statetype5;
        public string statetype6;
        public string state;
        public int ttl;
        
        public MessageObject()
        {
            statetype1 = "";
            statetype2 = "";
            statetype3 = "";
            statetype4 = "";
            statetype5 = "";
            statetype6 = "";

        }

        public string Json(Data data)
        {
            //Returns a Json
            jsonInfomation jsonout = new jsonInfomation();
            jsonout.nodeid = nodeid;
            //state 1 lookup
            try { jsonout.statetype1 = data.GetStateType(statetype1); }
            catch { }
            try { jsonout.statetype2 = data.GetStateType(statetype2); }
            catch { }
            try { jsonout.statetype3 = data.GetStateType(statetype3); }
            catch { }
            try {jsonout.statetype4 = data.GetStateType(statetype4);}
            catch { }
           try { jsonout.statetype5 = data.GetStateType(statetype5);}
           catch { }
           try { jsonout.statetype6 = data.GetStateType(statetype6); }
           catch { }
            jsonout.state = state;

            string output = JsonConvert.SerializeObject(jsonout, Formatting.Indented);
            return output;

        }
    }
}
