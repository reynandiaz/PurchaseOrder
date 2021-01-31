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

namespace PurchaseOrder
{
    public partial class Users : Form
    {
        private bool blUpdateInfo;
        public Users()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Users_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnSave_Click(sender, null);
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            RefreshTable();
            blUpdateInfo = false;
        }

        private void RefreshTable()
        {
            DataTable dtable = UsersProcess.GetUsersData();

            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            if (dtable.Rows.Count > 0)
            {
                dataGridView1.Rows.Add(dtable.Rows.Count);
                int x = 0;
                foreach (DataRow row in dtable.Rows)
                {
                    dataGridView1.Rows[x].Cells[0].Value = row["UserCode"].ToString();

                    if (row["DeletedDate"].ToString() != "")
                    {
                        dataGridView1.Rows[x].Cells[0].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[x].Cells[0].Style.BackColor = Color.White;
                    }
                    dataGridView1.Rows[x].Cells[1].Value = row["UserName"].ToString();
                    dataGridView1.Rows[x].Cells[2].Value = row["Password"].ToString();
                    dataGridView1.Rows[x].Cells[3].Value = Convert.ToInt32(row["UserRights"]) == 1 ? "Admin" : "User";
                    dataGridView1.Rows[x].Cells[4].Value = ">>";
                    x++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserCode.Text != "" && txtUser.Text != "" && txtPassword.Text != "")
            {
                var rtnValue = new UsersProcess.returnUsers();
                if (blUpdateInfo == false)
                {
                    rtnValue = UsersProcess.SaveUser(txtUserCode.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    rtnValue = UsersProcess.UpdateUser(txtUserCode.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim()); 
                }
                if (rtnValue.rtnSuccess == true)
                {
                    RefreshTable();
                    txtUserCode.Text = "";
                    txtUser.Text = "";
                    txtPassword.Text = "";
                    MessageBox.Show(rtnValue.rtnMessage) ;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtUserCode.Text = "";
            txtUserCode.ReadOnly = false;
            txtUser.Text = "";
            txtPassword.Text = "";
            blUpdateInfo = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnClose_Click(sender, null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                txtUserCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtUserCode.ReadOnly = true;
                txtUser.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPassword.Text= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                blUpdateInfo = true;
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (txtUserCode.Text != "")
            {
                var rtnValue = UsersProcess.UpdateStatus(txtUserCode.Text);
                if (rtnValue.rtnSuccess == true)
                {
                    RefreshTable();
                    MessageBox.Show(rtnValue.rtnMessage);
                    btnNew_Click(sender, null);
                }
            }
        }
    }
}
