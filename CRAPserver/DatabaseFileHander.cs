using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPserver
{
    class DatabaseFileHander
    {
        ///<summary> Contatins separate methods for saving and loading all datatypes </summary>
        /*public string[] LoadTypeData(string location)
        {

        }*/
        
        public bool saveNodeTable(string location, DataTable NodeTable)
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
        public bool saveTypeTable(string location, DataTable NodeTable)
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
        public bool saveStateTable(string location, DataTable NodeTable)
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
        public DataTable LoadNodeTable(string location)
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

        public DataTable LoadTypesTable(string location)
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

        public DataTable LoadStateTable(string location)
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
    }
}
