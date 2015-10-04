using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            int returnValue = -1;
            if (getParameter("type").Equals("command"))
            {
                Console.WriteLine("Command recieved");
                returnValue = 0;
            }
            else if (getParameter("type").Equals("update"))
            {
                Console.WriteLine("Update recieved");
                returnValue = 1;
            }
            return returnValue;
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

        public string[] getParameterList()
        {
            var queryString = messageToBeParsed.Substring(messageToBeParsed.IndexOf('?')).Split('#')[0];
            return Regex.Split(queryString, "(%w+)=(%w+)&*");
        }
    }
}
