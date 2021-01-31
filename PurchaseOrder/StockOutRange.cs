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
    public partial class StockOutRange : Form
    {
        public StockOutRange()
        {
            InitializeComponent();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            Reports.StockOut.frmStockOut.TransactionCode = SalesProcess.GenerateTransactionCode();
            Reports.StockOut.frmStockOut.DateFrom =dtFrom.Value;
            Reports.StockOut.frmStockOut.DateTo = dtTo.Value;
            Reports.StockOut.frmStockOut.blTodayOnly = false;
            Form stockout = new Reports.StockOut.frmStockOut();
            stockout.ShowDialog();
            this.Close();
        }

        private void StockOutRange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnClose_Click(sender, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void StockOutRange_Load(object sender, EventArgs e)
        {

        }
    }
}
