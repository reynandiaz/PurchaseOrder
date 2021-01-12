using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    Config.ExecuteCmd(insertquery);

                    rtnValue.isSuccess = true;
                    rtnValue.rtnItemCode = ItemCode;
                }
            }
            catch 
            { rtnValue.isSuccess = false; }

            return rtnValue;
        }
    }
}
