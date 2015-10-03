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
        public void addNode(string ID, string Type)
        {
            DataRow row = nodes.NewRow();
            row["ID"] = ID;
            row["Type"] = Type;
            nodes.Rows.Add(row);

        }
      
        public bool saveAll(string Folder)
        {
            ///<summary> Saves All tables into the Folder given </summary>
            bool success;
            try
            {
                DatabaseFileHander SaveHandler = new DatabaseFileHander();
                SaveHandler.saveNodeTable(Folder + "NodeData.csv", nodes);
                SaveHandler.saveStateTable(Folder + "StateData.csv", state);
                SaveHandler.saveTypeTable(Folder + "TypeData.csv", types);
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
            ///<summary> loads All tables into the Folder given </summary>
            DatabaseFileHander LoadHandler = new DatabaseFileHander();
            nodes = LoadHandler.LoadNodeTable(Folder + "NodeData.csv");
            state = LoadHandler.LoadStateTable(Folder + "StateData.csv");
            types = LoadHandler.LoadTypesTable(Folder + "TypeData.csv");
            return true;
        
        }
       
    }
}
