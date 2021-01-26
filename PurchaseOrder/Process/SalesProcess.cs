using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace PurchaseOrder.Process
{
    public class SalesProcess
    {
        public struct returnScannedItem
        {
            public string rtnTransactionCode;
            public int rtnQty;
            public double rtnUnitPrice;
            public double rtnTotalPrice;
            public bool rtnSuccess;
            public string rtnMessage;
        }
        public static string GenerateTransactionCode()
        {
            string strCode="";
            string datecode = DateTime.Now.ToString("yyyyMMdd");

            string countmax = "SELECT count(TransactionCode) AS MaxTransCode FROM " +
                               "transactiondetails WHERE TransactionCode LIKE '" + Config.PCCode + "-" + datecode + "-%' ";
            int cntmax = Config.ExecuteIntScalar(countmax);

            string strgetmax = "SELECT max(TransactionCode) AS MaxTransCode FROM " +
                               "transactiondetails WHERE TransactionCode LIKE '"+ Config.PCCode +"-"+ datecode + "-%' ";
            DataTable datamax = Config.RetreiveData(strgetmax);
            if (cntmax==0)
            {
                strCode = Config.PCCode + "-" + datecode + "-0001";
            }
            else
            {
                string getSeq = (Convert.ToInt32(datamax.Rows[0][0].ToString().Split('-')[2])+1).ToString("0000");
                strCode = Config.PCCode + "-" + datecode + "-" + getSeq;
            }
            return strCode;
        }

        public static int ItemExist(string ItemCode)
        {
            int rtnExist = 0;
            string query = "Select count(ItemCode) from items where ItemCode = '" + ItemCode.Trim() + "'";
            return rtnExist = Config.ExecuteIntScalar(query);
        }

        public static returnScannedItem ScanItems(string TransactionCode, string ItemCode)
        {
            returnScannedItem rtnValue = new returnScannedItem();

            string strCheckScanned = "Select count(ItemCode) from transactiondetails " +
                "where TransactionCode = '" + TransactionCode + "' and ItemCode='" + ItemCode + "'";

            int cntCheck = Config.ExecuteIntScalar(strCheckScanned);
            if (cntCheck == 0)
            {
                string itemInfo = "Select * from items where ItemCode = '"+ ItemCode + "'";
                DataTable dtItemInfo = Config.RetreiveData(itemInfo);

                string strGetMaxSeq = "Select max(Seq) from transactiondetails "+
                    "where TransactionCode = '" + TransactionCode + "'";
                int intMaxSeq = Config.ExecuteIntScalar(strGetMaxSeq)+1;

                string strInsertDetails = "Insert into transactiondetails values (" +
                    "'" + TransactionCode + "'," +
                    intMaxSeq + "," +
                    "'" + dtItemInfo.Rows[0]["ItemCode"].ToString() + "'," +
                    "1," +
                    Convert.ToDouble(dtItemInfo.Rows[0]["UnitPrice"]) + "," +
                    "now()," +
                    "null," +
                    "now()," +
                    "'"+Config.UserInfo.Rows[0]["UserCode"].ToString() + "')";
                Config.ExecuteCmd(strInsertDetails);

                string UpdateStocks = "UPDATE stocks "+
                                      "SET CurrentStocks = CurrentStocks-1 " +
                                      "WHERE ItemCode = '"+ ItemCode +"'";
                Config.ExecuteCmd(UpdateStocks);

                rtnValue.rtnSuccess = true;
                rtnValue.rtnTotalPrice = Convert.ToDouble(dtItemInfo.Rows[0]["UnitPrice"]);
                rtnValue.rtnUnitPrice = Convert.ToDouble(dtItemInfo.Rows[0]["UnitPrice"]);
                rtnValue.rtnQty = 1;
            }
            else
            { 
                string strDetails = "SELECT * FROM  "+
                                    "transactiondetails td "+
                                    "INNER JOIN items i "+
                                    "ON i.ItemCode = td.ItemCode "+
                                    "WHERE td.TransactionCode = '"+ TransactionCode +"'"+
                                    "AND i.ItemCode = '"+ ItemCode +"'";
                DataTable dtDetails = Config.RetreiveData(strDetails);

                string strUpdateDetails = "Update transactiondetails " +
                    "set Quantity = " + (Convert.ToInt32(dtDetails.Rows[0]["Quantity"]) + 1) + ", " +
                    "Price = " + (Convert.ToDouble(dtDetails.Rows[0]["UnitPrice"]) * (Convert.ToInt32(dtDetails.Rows[0]["Quantity"]) + 1)) + " " +
                    " where TransactionCode = '" + TransactionCode + "' and ItemCode = '" + ItemCode + "'";
                Config.ExecuteCmd(strUpdateDetails);

                string UpdateStocks = "UPDATE stocks " +
                      "SET CurrentStocks = CurrentStocks-1 " +
                      "WHERE ItemCode = '" + ItemCode + "'";
                Config.ExecuteCmd(UpdateStocks);

                rtnValue.rtnSuccess = true;
                rtnValue.rtnTotalPrice = (Convert.ToDouble(dtDetails.Rows[0]["UnitPrice"]) * (Convert.ToInt32(dtDetails.Rows[0]["Quantity"]) + 1));
                rtnValue.rtnUnitPrice = (Convert.ToInt32(dtDetails.Rows[0]["Quantity"]) + 1);
                rtnValue.rtnQty = (Convert.ToInt32(dtDetails.Rows[0]["Quantity"]) + 1);
            }

            return rtnValue;
        }

        public static DataTable RetrieveSalesData(string TransactionCode)
        {
            DataTable rtnValue = new DataTable();

            string getData= "Select * from transactiondetails t " +
                "inner join Items i " +
                "on i.ItemCode = t.ItemCode " +
                "left JOIN transactionheader th "+
                "ON th.TransactionCode = t.TransactionCode "+
                "where t.TransactionCode='" + TransactionCode + "' order by t.createddate";
            
            return rtnValue = Config.RetreiveData(getData);
        }

        public static returnScannedItem InsertHeader(string TransactionCode, int TransactionType, 
            double TotalPrice, int PaymentMethod, string Comment,double ReceivedPayment)
        {
            returnScannedItem rtnValue = new returnScannedItem();
            string query = "INSERT INTO transactionheader (TransactionCode, TransactionType,ReceivedPayment, TotalPrice, PaymentMethodID, Comments, CreatedDate, DeletedDate, UpdatedDate, UpdatedBy) " +
                           "VALUES('"+TransactionCode+"', "+ TransactionType + "," + ReceivedPayment + " ," + TotalPrice+", "+ PaymentMethod +", '"+ Comment +"', now(), null, now(), '"+ Config.UserInfo.Rows[0]["UpdatedBy"] + "')";
            try
            {
                Config.ExecuteCmd(query);
                rtnValue.rtnSuccess = true;
            }
            catch(Exception exc)
            {
                rtnValue.rtnMessage = exc.ToString();
                rtnValue.rtnSuccess = false;
            }
            return rtnValue;
        }

        public static void CancelOrder(string TransactionCode)
        {
            string query = "DELETE FROM transactiondetails " +
                           "WHERE TransactionCode = '"+ TransactionCode +"'";
            Config.ExecuteCmd(query);
        }
        public static void DeleteItem(string TransactionCode,int Seq)
        {
            string query = "DELETE FROM transactiondetails " +
                           "WHERE TransactionCode = '" + TransactionCode + "' and Seq = " + Seq;

            Config.ExecuteCmd(query);
        }
    }
}
