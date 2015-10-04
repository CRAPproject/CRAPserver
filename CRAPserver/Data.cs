using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CRAPserver
{
    class Data
    {
        ~Data()
        {
            SQLiteConnection.Close();
        }
        string DatabaseFile = "CRAPData.sqlite";
        SQLiteConnection SQLiteConnection;
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
        private static void CreateTables( SQLiteConnection Connection)
        {
            //Create Nodes Table
            string NodesTable = "create table NodesTable (NodeID int, Type int, IP text, Name text)";
            //Create Type Table
            string TypesTable = "create table TypeTable (TypeID int, Discription text)";
            //create States
            string StatesTable = "create table StatesTable (StateID int, NodeID varchar(50), StateType1 varchar(255), StateType2 varchar(255),StateType3 varchar(255), State varchar(255), TimeStamp DateTime )";
            //create Messagestack
            string MesageStack = "create table MessageStack (NodeID varchar(50), message varchar(255), ttl int)";
            //excute tables
            SQLiteCommand NodeTableCommand = new SQLiteCommand(NodesTable, Connection);
            NodeTableCommand.ExecuteNonQuery();
            SQLiteCommand TypeTableCommand = new SQLiteCommand(TypesTable, Connection);
            TypeTableCommand.ExecuteNonQuery();
            SQLiteCommand StatesTableCommand = new SQLiteCommand(StatesTable, Connection);
            StatesTableCommand.ExecuteNonQuery();
            SQLiteCommand MessageStackCommand = new SQLiteCommand(MesageStack, Connection);
            MessageStackCommand.ExecuteNonQuery();

        }
        public int AddNode(int NodeID, int Type, string IP, string name)
        {
            try
            {
            string Add = "insert into NodesTable (NodeID, Type, IP, Name) values (" +NodeID.ToString() +"," +Type.ToString() +","+IP +","+name+")";
            SQLiteCommand command = new SQLiteCommand(Add, SQLiteConnection);
            command.ExecuteNonQuery();
            return 1;
            }
            catch
            {
            return 0;
            }
        }
        public string getIPAddress(int NodeID)
        {
            string getIP = "select * from NodeTable where NodeID = " + NodeID.ToString();
            SQLiteCommand command = new SQLiteCommand(getIP, SQLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader["IP"].ToString();
        }
    }
}
