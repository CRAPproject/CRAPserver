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
            table.Columns.Add("Temperature", typeof(int));
            return table;
        }
    }
}
