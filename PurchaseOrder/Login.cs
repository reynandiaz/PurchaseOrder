using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PurchaseOrder.Process;
using System.Net;

namespace PurchaseOrder
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CheckConnection();
            CheckIP();
            //Config.PCCode="1";
        }
        private void CheckConnection()
        {
            try
            {
                Config.connection.Open();
                lblConnection.Text = "Connected";
            }
            //catch (Exception exc)
            catch
            {
                lblConnection.Text = "Failed";
                txtUser.Enabled = false;
                txtPassword.Enabled = false;
                btnLogin.Enabled = false;
                //MessageBox.Show(exc.ToString());
                MessageBox.Show("Connection Failed!");
            }
            finally
            {
                Config.connection.Close();
            }
        }
        private void CheckIP()
        {
            string hostName = Dns.GetHostName();
            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            LoginProcess.CheckPCIPAddress(hostName);
            lblPCCode.Text = Config.PCCode;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUser.Text != "" && txtPassword.Text!="")
            {
                bool rtnLogin = LoginProcess.CheckLogin(txtUser.Text, txtPassword.Text);
                if (rtnLogin==true)
                {
                    Form main = new Main();
                    main.ShowDialog();
                }
            }
            txtUser.Text = "";
            txtPassword.Text = "";
            txtUser.Focus();
        }
    }
}
