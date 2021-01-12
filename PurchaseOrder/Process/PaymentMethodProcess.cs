using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PurchaseOrder.Process
{
    public class PaymentMethodProcess
    {
        public static DataTable GetPaymentMethods()
        {
            DataTable rtnValue = new DataTable();

            string query = "select * from paymentmethod where deletedDate is null";

            return rtnValue = Config.RetreiveData(query);
        }
    }
}
