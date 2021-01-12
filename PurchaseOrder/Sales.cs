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
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print and Save!");
        }

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode.ToString() == "F5")
            //{
            //    btnSave_Click(sender, null);
            //}
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
            txtTransactionCode.Text=SalesProcess.GenerateTransactionCode();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n' && txtBarcode.Text != "")
            {
                if (SalesProcess.ItemExist(txtBarcode.Text) == 0)
                {
                    RegisterItem.ItemCode = "";
                    RegisterItem.ItemCode = txtBarcode.Text;
                    Form registeritem = new RegisterItem();
                    registeritem.ShowDialog();
                }
                var rtnValue = SalesProcess.ScanItems(txtTransactionCode.Text.Trim(), txtBarcode.Text.Trim());
                
                
                RefreshTable();
                txtBarcode.Text = ""; 
                txtQty.Text = "";
                txtBarcode.Focus();
                
            }
        }

        private void RefreshTable()
        {
            DataTable dtable = new DataTable();
            dtable = SalesProcess.RetrieveSalesData(txtTransactionCode.Text);

            txtTotalPrice.Text = "";
            double totalPrice = 0;

            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            if (dtable.Rows.Count > 0)
            {
                dataGridView1.Rows.Add(dtable.Rows.Count);
                int x = 0;
                foreach (DataRow row in dtable.Rows)
                {
                    dataGridView1.Rows[x].Cells[0].Value = row["ItemName"].ToString();
                    dataGridView1.Rows[x].Cells[1].Value = row["Quantity"].ToString();
                    dataGridView1.Rows[x].Cells[2].Value = row["UnitPrice"].ToString();
                    dataGridView1.Rows[x].Cells[3].Value = row["Price"].ToString();

                    totalPrice = totalPrice + Convert.ToDouble(row["Price"]);

                    dataGridView1.Rows[x].Cells[4].Value = "X";
                    x++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
            txtTotalPrice.Text = totalPrice.ToString();
        }
    }
}
