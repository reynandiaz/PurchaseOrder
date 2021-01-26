using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PurchaseOrder.Process
{
    public class ItemsProcess
    {
        public struct returnValue
        {
            public bool isSuccess;
            public string rtnItemCode;
        }

        public static returnValue AddNewItem(string ItemCode, string ItemName, string UnitPrice)
        {
            returnValue rtnValue = new returnValue();
            try
            {
                string checkQuery = "Select count(itemCode) from items where itemcode = '" + ItemCode + "'";

                int cntCheck = Config.ExecuteIntScalar(checkQuery);

                if (cntCheck == 0)
                {
                    string insertquery = "Insert into items (ItemCode,ItemName,UnitPrice,CreatedDate,UpdatedDate,UpdatedBy) " +
                                         "values ('" + ItemCode + "','" + ItemName + "','" + UnitPrice + "',now(),now(),'" + Config.UserInfo.Rows[0]["UserCode"] + "')";

                    string insertstocks = "INSERT INTO stocks (ItemCode, MinStocks, CurrentStocks, MaxStocks, CreatedDate, DeletedDate, UpdatedDate, UpdatedBy) " +
                                          "VALUES('"+ ItemCode + "', 0, 0, 0, now(), null, now(), '" + Config.UserInfo.Rows[0]["UserCode"] + "')";

                    Config.ExecuteCmd(insertquery);
                    Config.ExecuteCmd(insertstocks);

                    rtnValue.isSuccess = true;
                    rtnValue.rtnItemCode = ItemCode;
                }
            }
            catch 
            { 
                rtnValue.isSuccess = false; 
            }

            return rtnValue;
        }
        public static returnValue AddNewItem(string ItemCode, string ItemName, 
            string UnitPrice,string MinStocks,string Curstocks,string MaxStocks)
        {
            returnValue rtnValue = new returnValue();
            try
            {
                string checkQuery = "Select count(itemCode) from items where itemcode = '" + ItemCode + "'";

                int cntCheck = Config.ExecuteIntScalar(checkQuery);

                if (cntCheck == 0)
                {
                    string insertquery = "Insert into items (ItemCode,ItemName,UnitPrice,CreatedDate,UpdatedDate,UpdatedBy) " +
                                         "values ('" + ItemCode + "','" + ItemName + "','" + UnitPrice + "',now(),now(),'" + Config.UserInfo.Rows[0]["UserCode"] + "')";

                    string insertstocks = "INSERT INTO stocks (ItemCode, MinStocks, CurrentStocks, MaxStocks, CreatedDate, DeletedDate, UpdatedDate, UpdatedBy) " +
                                          "VALUES('" + ItemCode + "', '"+ MinStocks + "','" + Curstocks + "', '" + MaxStocks + "', now(), null, now(), '" + Config.UserInfo.Rows[0]["UserCode"] + "')";

                    Config.ExecuteCmd(insertquery);
                    Config.ExecuteCmd(insertstocks);

                    rtnValue.isSuccess = true;
                    rtnValue.rtnItemCode = ItemCode;
                }
            }
            catch
            {
                rtnValue.isSuccess = false;
            }

            return rtnValue;
        }
        public static DataTable RetrieveData()
        {
            DataTable rtnValue = new DataTable();

            string query = "SELECT * FROM  " +
                           "itemstocks";
            rtnValue = Config.RetreiveData(query);

            return rtnValue;
        }
        public static string GenerateBarcode()
        {
            string query = "Select max(ItemCode)+1 as maxcode from Items";
            return Config.ExecuteIntScalar(query).ToString(); ;
        }

    }
}
