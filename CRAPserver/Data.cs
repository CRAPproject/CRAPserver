using System;using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPserver
{
    class Data
    {
        public DataTable nodes;
        public DataTable types;
        public DataTable state;

        public Data()
        {
            nodes = newNodeTable();
            types = newTypesTable();
            state = newStateTable();
        }

        private DataTable newNodeTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Type", typeof(int));
            return table;
        }

        private DataTable newTypesTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Temperature", typeof(Boolean));
            return table;
        }

        private DataTable newStateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("NodeID", typeof(int));
            table.Columns.Add("Temperature", typeof(string));
            return table;
        }

        public string GetStateData(int Node, string Coloum)
        {
            string value;
            DataRow[] Row;
            Row = state.Select("NodeID = " + Node);
           DataRow temp = Row[0];
           value = temp[Coloum].ToString();
            return value;
        }
        public void SetStateData(int Node, string Coloum, string value)
    {
        DataRow[] Row;
        Row = state.Select("NodeID = " + Node);
        DataRow temp = Row[0];
        temp[Coloum] = value;
    }

        private bool saveNodeTable(string location, DataTable NodeTable)
        {
            // saves Node table to XML 
            bool success;
            try
            {
                NodeTable.WriteXml(location);
                success = true;
            }
            catch
            {
                Console.WriteLine("Unable to save Node Table");
                success = false;
            }
            return success;
        }
        private bool saveTypeTable(string location, DataTable NodeTable)
        {
            // saves Node table to XML 
            bool success;
            try
            {
                NodeTable.WriteXml(location);
                success = true;
            }
            catch
            {
                Console.WriteLine("Unable to save Type Table");
                success = false;
            }
            return success;
        }
        private bool saveStateTable(string location, DataTable NodeTable)
        {
            // saves State table to XML 
            bool success;
            try
            {
                NodeTable.WriteXml(location);
                success = true;
            }
            catch
            {
                Console.WriteLine("Unable to save State Table");
                success = false;
            }
            return success;
        }
        private DataTable LoadNodeTable(string location)
        {
            DataTable TempNodeTable = new DataTable();

            try
            {
                TempNodeTable.ReadXml(location);
            }
            catch
            {
                Console.WriteLine("Restore of Node table fail");
            }
            return TempNodeTable;
        }

        private DataTable LoadTypesTable(string location)
        {
            DataTable TempTypesTable = new DataTable();

            try
            {
                TempTypesTable.ReadXml(location);
            }
            catch
            {
                Console.WriteLine("Restore of Node table fail");
            }
            return TempTypesTable;
        }

        private DataTable LoadStateTable(string location)
        {
            DataTable TempStateTable = new DataTable();

            try
            {
                TempStateTable.ReadXml(location);
            }
            catch
            {
                Console.WriteLine("Restore of Node table fail");
            }
            return TempStateTable;
        }
        public bool saveAll(string Folder)
        {
            ///<summary> Saves All tables into the Folder given </summary>
            bool success;
            try
            {
                saveNodeTable(Folder + "NodeData.csv", nodes);
                saveStateTable(Folder + "StateData.csv", state);
                saveTypeTable(Folder + "TypeData.csv", types);
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        public bool LoadAll(string Folder)
        {
            nodes = LoadNodeTable(Folder + "NodeData.csv");
            state = LoadStateTable(Folder + "StateData.csv");
            types = LoadTypesTable(Folder + "TypeData.csv");
            return true;
        
        }
       
    }
}
