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
        public static int PaymentMethod;
        private int TransactionType;
        public static double ReceivedPayment;

        public static bool newItemAdded;

        public Sales()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PaymentMethod = 0;
                ReceivedPayment = 0.0;

                Form method = new PaymentMethod();
                method.ShowDialog();

                Form payment = new ReceivePayment();
                payment.ShowDialog();

                var rtnValue = SalesProcess.InsertHeader(txtTransactionCode.Text.Trim(), TransactionType, Convert.ToDouble(txtTotalPrice.Text),
                    PaymentMethod, "", ReceivedPayment);
                if (rtnValue.rtnSuccess == true)
                {
                    Reports.SalesReceipt.frmReceipt.TransactionCode = txtTransactionCode.Text.Trim();
                    Form receipt = new Reports.SalesReceipt.frmReceipt();
                    receipt.ShowDialog();

                    this.Close();
                }
            }

        }

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F5")
            {
                btnSave_Click(sender, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, null);
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
            txtTransactionCode.Text=SalesProcess.GenerateTransactionCode();
            TransactionType = 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Cancel order?", "System Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SalesProcess.CancelOrder(txtTransactionCode.Text);
                this.Close();
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n' && txtBarcode.Text.Trim() != "")
            {
                newItemAdded = false;   
                if (SalesProcess.ItemExist(txtBarcode.Text) == 0)
                {
                    RegisterItem.ItemCode = "";
                    RegisterItem.ItemCode = txtBarcode.Text;
                    Form registeritem = new RegisterItem();
                    registeritem.ShowDialog();
                }
                if (newItemAdded == false)
                { 
                    var rtnValue = SalesProcess.ScanItems(txtTransactionCode.Text.Trim(), txtBarcode.Text.Trim());
                }
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
                    dataGridView1.Rows[x].Cells[0].Value = row["Seq"].ToString();
                    dataGridView1.Rows[x].Cells[1].Value = row["ItemName"].ToString();
                    dataGridView1.Rows[x].Cells[2].Value = row["Quantity"].ToString();
                    dataGridView1.Rows[x].Cells[3].Value = row["UnitPrice"].ToString();
                    dataGridView1.Rows[x].Cells[4].Value = row["Price"].ToString();

                    totalPrice = totalPrice + Convert.ToDouble(row["Price"]);

                    dataGridView1.Rows[x].Cells[5].Value = "X";
                    x++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
            txtTotalPrice.Text = totalPrice.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                SalesProcess.DeleteItem(txtTransactionCode.Text,Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
                RefreshTable();
            }
        }
    }
}
