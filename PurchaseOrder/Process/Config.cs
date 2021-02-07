using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace PurchaseOrder.Process
{
    public class Config
    {
        //Test Settings
        public static MySqlConnection connection = new MySqlConnection("Server=localhost;Database=purchaseorder_db;Uid=root;password=admin;SslMode=none;");

        //Deployed Settings
        //public static MySqlConnection connection = new MySqlConnection("Server=localhost;Database=purchaseorder_db;Uid=root;password=;SslMode=none;");

        public static DataTable UserInfo;
        public static string PCCode;

        public static void ExecuteCmd(string query)
        {
            var command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        public static DataTable RetreiveData(string query)
        {
            DataTable dtable = new DataTable();
            var command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                dtable.Load(reader);
            }
            finally
            {
                connection.Close();
            }

            return dtable;
        }
        public static long ExecuteIntScalar(string query)
        {
            long intReturn = 0;

            var command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                var cnt = command.ExecuteScalar();
                intReturn = (cnt.ToString() == "" ? 0 : Convert.ToInt64(cnt));
                return intReturn;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
