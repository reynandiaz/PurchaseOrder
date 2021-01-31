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
    public partial class PointOfSales : Form
    {
        public Action NotifyMainFormToOpenChildForm2;
        public PointOfSales()
        {
            InitializeComponent();
        }


        private void btnSales_Click(object sender, EventArgs e)
        {
            //MDI FORM OPEN CHILD
            //if (NotifyMainFormToOpenChildForm2 != null)
            //{
            //    NotifyMainFormToOpenChildForm2();
            //}

            Form sales = new Sales();
            sales.ShowDialog();
            RefreshTable();
            
        }

        private void PointOfSales_Load(object sender, EventArgs e)
        {
            RefreshTable();
            if (Convert.ToInt32(Config.UserInfo.Rows[0]["UserRights"]) == 2)
            {
                btnStockOut.Enabled = false;
            }
            else
            {
                btnStockOut.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PointOfSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSales_Click(sender, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (Convert.ToInt32(Config.UserInfo.Rows[0]["UserRights"]) == 1)
                { 
                    btnStockOut_Click(sender, null);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
        private void RefreshTable()
        {
            string datecode = DateTime.Now.ToString("yyyyMMdd");

            string query = "SELECT * FROM  " +
                           "transactionheader th " +
                           "INNER JOIN paymentmethod pm " +
                           "ON pm.PaymentMethodID = th.PaymentMethodID ";
            if (Convert.ToInt32(Config.UserInfo.Rows[0]["UserRights"]) == 2)
            {
                query = query + "WHERE TransactionCode LIKE '" + Config.PCCode + "-" + datecode + "-%' order by TransactionCode desc";
            }
            else
            {
                query = query + "WHERE TransactionCode LIKE '%-" + datecode + "-%' order by TransactionCode desc";
            }

            DataTable dtable = Config.RetreiveData(query);

            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            if (dtable.Rows.Count > 0)
            {
                dataGridView1.Rows.Add(dtable.Rows.Count);
                int x = 0;
                foreach (DataRow row in dtable.Rows)
                {
                    dataGridView1.Rows[x].Cells[0].Value = row["TransactionCode"].ToString();
                    dataGridView1.Rows[x].Cells[1].Value = Convert.ToDateTime(row["CreatedDate"]).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[x].Cells[2].Value = Convert.ToDateTime(row["CreatedDate"]).ToString("hh:mm tt");
                    dataGridView1.Rows[x].Cells[3].Value = row["PaymentMethod"].ToString();
                    dataGridView1.Rows[x].Cells[4].Value = row["TotalPrice"].ToString();
                    dataGridView1.Rows[x].Cells[5].Value = row["ReceivedPayment"].ToString();
                    dataGridView1.Rows[x].Cells[6].Value = row["UpdatedBy"].ToString();
                    dataGridView1.Rows[x].Cells[7].Value = ">>";
                    x++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                DataTable data =  Process.SalesProcess.RetrieveSalesData(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                dataGridView2.Rows.Clear();
                dataGridView2.AllowUserToAddRows = true;
                if (data.Rows.Count > 0)
                {
                    dataGridView2.Rows.Add(data.Rows.Count);
                    int x = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        dataGridView2.Rows[x].Cells[0].Value = row["ItemName"].ToString();
                        dataGridView2.Rows[x].Cells[1].Value = row["Quantity"].ToString();
                        dataGridView2.Rows[x].Cells[2].Value = row["Price"].ToString();
                        x++;
                    }
                    lblCode.Text = data.Rows[data.Rows.Count - 1][0].ToString();
                    lblReceived.Text = data.Rows[data.Rows.Count - 1]["ReceivedPayment"].ToString(); 
                    lblTotalPrice.Text = data.Rows[data.Rows.Count - 1]["TotalPrice"].ToString();
                }
                dataGridView2.AllowUserToAddRows = false;
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            if (lblCode.Text != "***")
            { 
                Reports.SalesReceipt.frmReceipt.TransactionCode = lblCode.Text.Trim();
                Form receipt = new Reports.SalesReceipt.frmReceipt();
                receipt.ShowDialog();
            }
        }

        private void btnStockOut_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Print Today's Stock out?", "System Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Reports.StockOut.frmStockOut.TransactionCode = SalesProcess.GenerateTransactionCode();
                Reports.StockOut.frmStockOut.DateFrom = DateTime.Now;
                Reports.StockOut.frmStockOut.DateTo = DateTime.Now;
                Reports.StockOut.frmStockOut.blTodayOnly = true;
                Form stockout = new Reports.StockOut.frmStockOut();
                stockout.ShowDialog();
            }
        }
    }
}

