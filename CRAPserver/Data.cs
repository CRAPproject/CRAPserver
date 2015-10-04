using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net;

namespace CRAPserver
{
    class Data
    {
        ~Data()
        {
            SQLiteConnection.Close();
        }
        string DatabaseFile = "CRAPData.sqlite";
        public SQLiteConnection SQLiteConnection;
        public Data()
        {
            //Check to see if database file excists
            if (File.Exists(DatabaseFile))
            {
                //create connection
                SQLiteConnection = new SQLiteConnection("Data Source=" + DatabaseFile + ";Version=3;");
                SQLiteConnection.Open();
            }
            else
            {
                //create new databse file
                SQLiteConnection.CreateFile(DatabaseFile);
                SQLiteConnection = new SQLiteConnection("Data Source=" + DatabaseFile + ";Version=3;");
                SQLiteConnection.Open();
                CreateTables(SQLiteConnection);
            }

        }
        private static void CreateTables(SQLiteConnection Connection)
        {
            //Create Nodes Table
            string NodesTable = "create table NodesTable (NodeID int, Type int, IP text, Name text)";
            //Create Type Table
            string TypesTable = "create table TypeTable (TypeID int, Discription text)";
            //create States
            string StatesTable = "create table StatesTable (StateID int, NodeID varchar(50), StateType1 varchar(255), StateType2 varchar(255),StateType3 varchar(255), State varchar(255), TimeStamp DateTime )";
            //create Messagestack
            string MesageTable = "CREATE TABLE `MessageTable` (`NodeID`	varchar(50),`ttl`	int,`State_1`	TEXT,`State_2`	TEXT,`State_3`	TEXT,`State_4`	TEXT,`State_5`	TEXT,`State_6`	TEXT,`State`	TEXT)";
            // create stateType
            string stateType = "create table StateType (StateID int, StateType text, min int, max int)";
            //excute tables
            SQLiteCommand NodeTableCommand = new SQLiteCommand(NodesTable, Connection);
            NodeTableCommand.ExecuteNonQuery();
            SQLiteCommand TypeTableCommand = new SQLiteCommand(TypesTable, Connection);
            TypeTableCommand.ExecuteNonQuery();
            SQLiteCommand StatesTableCommand = new SQLiteCommand(StatesTable, Connection);
            StatesTableCommand.ExecuteNonQuery();
            SQLiteCommand MessageStackCommand = new SQLiteCommand(MesageTable, Connection);
            MessageStackCommand.ExecuteNonQuery();
            SQLiteCommand stateTypeCommand = new SQLiteCommand(stateType, Connection);
            stateTypeCommand.ExecuteNonQuery();
            

        }

        public int AddState(int StateID, string StateType, int min,int max)
        {
            try
            {
                string Add = "insert into StateType (StateID, StateType, min, max) values (\"" + StateID.ToString() + " \", \"" + StateType.ToString() + " \", \"" + min.ToString() + "\", \"" + max.ToString() + "\")";
                Console.WriteLine(Add);
                SQLiteCommand command = new SQLiteCommand(Add, SQLiteConnection);
                command.ExecuteNonQuery();
                return 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

        }
        public string GetStateTypefromID(string StateID)
        {
            string Statetype = "select * from stateType where StateID = " + StateID;
            SQLiteCommand command = new SQLiteCommand(Statetype, SQLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader["StateType"].ToString();
        }
        public string GetStateTypefromType(string StateType)
        {
            string Statetype = "select * from stateType where StateID = " + StateType;
            SQLiteCommand command = new SQLiteCommand(Statetype, SQLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader["StateID"].ToString();
        }
        public int AddNode(int NodeID, int Type, string IP, string name)
        {
            try
            {
                string Add = "insert into NodesTable (NodeID, Type, IP, Name) values (" + NodeID.ToString() + "," + Type.ToString() + ", \"" + IP + "\", \"" + name + "\")";
                SQLiteCommand command = new SQLiteCommand(Add, SQLiteConnection);
                command.ExecuteNonQuery();
                return 1;
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public IPAddress getIPAddress(int NodeID)
        {
            string getIP = "select * from NodesTable where NodeID = " + NodeID.ToString();
            SQLiteCommand command = new SQLiteCommand(getIP, SQLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return IPAddress.Parse(reader["IP"].ToString());
        }
        public int AddMessageToTable(MessageObject stateobj)
        {
            try
            {
                string Add = "insert into MessageTable (NodeID, ttl, State_1, State_2, State_3, State_4, State_5, State_6, State) values (" + stateobj.nodeid + "," +stateobj.ttl+", \"" + stateobj.statetype1 + " \", \"" + stateobj.statetype2 + "\", \"" + stateobj.statetype3 + "\", \"" + stateobj.statetype4 + " \" , \"" + stateobj.statetype5 + " \", \"" + stateobj.statetype6 + " \", \"" + stateobj.state + " \")";
                Console.WriteLine(Add);
                SQLiteCommand command = new SQLiteCommand(Add, SQLiteConnection);
                command.ExecuteNonQuery();
                return 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

        }
        public MessageObject[] GetMessage(int NodeID)
        {
            // returns an array of messages
            // calcaute the amount of results to know array size
            string count = "select count(*) from MessageTable where NodeID = " + NodeID.ToString();
            SQLiteCommand command = new SQLiteCommand(count, SQLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            int arraysize = Int32.Parse(reader[0].ToString());
            
            //create object
            MessageObject[] returnobj = new MessageObject[arraysize];
            string getMessages = "select * from MessageTable where NodeID = " + NodeID.ToString();
            SQLiteCommand commandobj = new SQLiteCommand(getMessages, SQLiteConnection);
            SQLiteDataReader objReader = commandobj.ExecuteReader();
            int i=0;
            while (objReader.Read())
           {
               MessageObject read = new MessageObject();
               read.nodeid = Int32.Parse(objReader["NodeID"].ToString());
               read.ttl = Int32.Parse(objReader["ttl"].ToString());
               read.statetype1 = objReader["State_1"].ToString();
               read.statetype2 = objReader["State_2"].ToString();
               read.statetype3 = objReader["State_3"].ToString();
               read.statetype4 = objReader["State_4"].ToString();
               read.statetype5 = objReader["State_5"].ToString();
               read.statetype6 = objReader["State_6"].ToString();
               read.state = objReader["State"].ToString();
               returnobj[i] = read;
                i++;
           }
            return returnobj;

        }
    }
}
