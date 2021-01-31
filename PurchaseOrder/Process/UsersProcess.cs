using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PurchaseOrder.Process
{
    public class UsersProcess
    {
        public struct returnUsers
        {
            public string rtnUserCode;
            public bool rtnSuccess;
            public string rtnMessage;
        }

        public static DataTable GetUsersData() {
            DataTable rtnvalue = new DataTable();

            string query = "Select * from users";

            return rtnvalue= Config.RetreiveData(query); ;
        }

        public static returnUsers SaveUser(string UserCode,string UserName,string Password)
        {
            returnUsers rtnValue = new returnUsers();

            try
            {
                string strCheck = "Select count(UserCode) from Users where UserCode = '" + UserCode + "'";
                int cnt = Config.ExecuteIntScalar(strCheck);
                if (cnt == 0)
                {
                    string strInsert = "Insert into Users " +
                        "(UserCode,Username,password,userrights,createddate,updateddate,updatedby) " +
                        "values  " +
                        "('" + UserCode + "','" + UserName + "','" + Password + "',2,now(),now(),'" + Config.UserInfo.Rows[0]["UserCode"].ToString() + "')";
                    Config.ExecuteCmd(strInsert);
                    rtnValue.rtnSuccess = true;
                    rtnValue.rtnMessage = "Record Saved!";
                }
                else
                {
                    rtnValue.rtnMessage = "Already Registered!";
                    rtnValue.rtnSuccess = false;
                }
            }
            catch (Exception exc)
            {
                rtnValue.rtnMessage = exc.ToString();
                rtnValue.rtnSuccess = false;
            }
            return rtnValue;
        }

        public static returnUsers UpdateUser(string UserCode, string UserName, string Password)
        {
            returnUsers rtnValue = new returnUsers();
            try
            {
                string strUpdate = " UPDATE users "+
                                   "SET UserName = '"+ UserName +"'  "+
                                   "     , Password = '"+ Password +"'  "+
                                   " WHERE UserCode = '"+ UserCode +"'";
                Config.ExecuteCmd(strUpdate);
                rtnValue.rtnSuccess = true;
                rtnValue.rtnMessage = "Records Updated!";
            }
            catch (Exception exc)
            {
                rtnValue.rtnMessage = exc.ToString();
                rtnValue.rtnSuccess = false;
            }
            return rtnValue;
        }
        public static returnUsers UpdateStatus(string UserCode)
        {
            returnUsers rtnValue = new returnUsers();

            string checkExist = "Select count(UserCode) from Users where UserCode = '" + UserCode + "'";

            int cntExist = Config.ExecuteIntScalar(checkExist);
            if (cntExist > 0)
            {
                string chkActivated = "Select count(UserCode) from Users where UserCode = '" + UserCode + "' and DeletedDate is null";
                int cntActivated = Config.ExecuteIntScalar(chkActivated);
                if (cntActivated > 0)
                {
                    //update deleteddate now
                    string strUpdateDeletedDate = " UPDATE users "+
                                            "SET DeletedDate = now() "+
                                            "WHERE UserCode = '"+ UserCode +"'";
                    Config.ExecuteCmd(strUpdateDeletedDate);
                    rtnValue.rtnMessage = "User Deactivated!";
                }
                else
                {
                    string strUpdateDeletedDate = " UPDATE users " +
                        "SET DeletedDate = null " +
                        "WHERE UserCode = '" + UserCode + "'";
                    Config.ExecuteCmd(strUpdateDeletedDate);
                    rtnValue.rtnMessage = "User Activated!";
                }
                rtnValue.rtnSuccess = true ;
            }
            else
            {
                rtnValue.rtnSuccess = false;
            }
            return rtnValue;
        }

    }
}
