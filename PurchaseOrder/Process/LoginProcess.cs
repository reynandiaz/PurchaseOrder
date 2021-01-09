using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PurchaseOrder.Process
{
    public class LoginProcess
    {
        public static bool CheckLogin(string Username,string Password)
        {
            bool rtn=false;
            string query = "SELECT* FROM users "+
                           "WHERE UserName = '"+ Username +"' AND password = '"+ Password +"'" ;
            DataTable userinfo = Config.RetreiveData(query);
            if (userinfo.Rows.Count != 0)
            {
                Config.UserInfo = userinfo;
                rtn = true;
            }
            else
            {
                Config.UserInfo = null;
            }
            return rtn;
        }
    }
}
