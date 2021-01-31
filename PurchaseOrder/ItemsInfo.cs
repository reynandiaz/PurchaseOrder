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
    public partial class ItemsInfo : Form
    {
        //FormStatus
        //1=add
        //2=modify
        public static int FormStatus;

        //Barcode
        public static string ItemCode;

        public ItemsInfo()
        {
            InitializeComponent();
        }



        private void AddItems_Load(object sender, EventArgs e)
        {
            if(FormStatus==2)
            {
                txtBarcode.Text = ItemCode;
                txtBarcode.ReadOnly = true;
                btnSave.Text = "Update";
                GenerateDetails();
            }
        }

        private void AddItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuto.Checked)
            {
                txtBarcode.Text= Process.ItemsProcess.GenerateBarcode();
                txtBarcode.ReadOnly = true;
            }
            else
            {
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text!="" && txtName.Text!="" && txtMin.Text!="" && txtCurrent.Text!="" && txtMax.Text!="")
            {
                if (FormStatus == 1)
                {
                    DialogResult dialogResult = MessageBox.Show("Save Item?", "System Message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var rtnValue = ItemsProcess.AddNewItem(txtBarcode.Text, txtName.Text, txtPrice.Text, txtMin.Text, txtCurrent.Text, txtMax.Text);
                        if (rtnValue.isSuccess == true)
                        {
                            MessageBox.Show("Item Added!");
                            this.Close();
                        }
                    }
                }
                else if (FormStatus == 2)
                {
                    DialogResult dialogResult = MessageBox.Show("Update Item?", "System Message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var rtnValue = ItemsProcess.UpdateItemDetails(txtBarcode.Text, txtName.Text, txtPrice.Text, txtMin.Text, txtCurrent.Text, txtMax.Text);
                        if (rtnValue.isSuccess == true)
                        {
                            MessageBox.Show("Item Details Updated!");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void GenerateDetails()
        {
            DataTable itemdata = ItemsProcess.ItemDetails(txtBarcode.Text.Trim());

            txtName.Text = itemdata.Rows[0]["ItemName"].ToString();
            txtMin.Text = itemdata.Rows[0]["MinStocks"].ToString();
            txtCurrent.Text = itemdata.Rows[0]["CurrentStocks"].ToString();
            txtMax.Text = itemdata.Rows[0]["MaxStocks"].ToString();
            txtPrice.Text = itemdata.Rows[0]["UnitPrice"].ToString();
        }
    }
}
