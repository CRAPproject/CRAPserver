using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRAPserver
{
    class HTTPGetArgumentParse
    {
        string messageToBeParsed;

        public HTTPGetArgumentParse(string message)
        {
            messageToBeParsed = message;
        }

        public int getNodeID()
        {
            return Convert.ToInt32(getParameter("nodeid"));
        }

        // Command = 0
        // Update = 1
        public int getType()
        {
            if (getParameter("type").Equals("command")) return 0;
            else if (getParameter("type").Equals("update")) return 1;
            else return -1;
        }
        
        public string getParameter(string argument)
        {
            String returnString = "";
            try
            {
                var queryString = messageToBeParsed.Substring(messageToBeParsed.IndexOf('?')).Split('#')[0];
                var parsedString = HttpUtility.ParseQueryString(queryString);
                returnString = parsedString.Get(argument);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.ToString());
            }
            return returnString;
        }
    }
}
