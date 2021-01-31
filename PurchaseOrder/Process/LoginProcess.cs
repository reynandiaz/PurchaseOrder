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
                           "WHERE UserName = '"+ Username +"' AND password = '"+ Password +"' and deleteddate is null" ;
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

        public static void CheckPCIPAddress(string hostname)
        {
            string countquery = "Select * from userpc where HostName = '" + hostname + "'";

            DataTable dtable = Config.RetreiveData(countquery);
            if (dtable.Rows.Count == 0)
            {
                string maxPCCode = "SELECT * FROM generatepccode";
                int intMax = Config.ExecuteIntScalar(maxPCCode);
                Config.PCCode = intMax.ToString();

                string InsertCode = "Insert into userpc values("+ intMax + ",'" + hostname + "',now(),null,now())";
                Config.ExecuteCmd(InsertCode);
            }
            else 
            {
                Config.PCCode = dtable.Rows[0][0].ToString();
            }

        }
    }
}
