using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PurchaseOrder.Process;
using Microsoft.Reporting.WinForms;


namespace PurchaseOrder.Reports.SalesReceipt
{
    public partial class frmReceipt : Form
    {
        public static string TransactionCode;
        public frmReceipt()
        {
            InitializeComponent();
        }

        private void frmReceipt_Load(object sender, EventArgs e)
        {
            dtReceipt lstItems = new dtReceipt();
            lstItems = GetData();

            ReportDataSource datasource = new ReportDataSource("DataSet1", lstItems.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
        }
        private dtReceipt GetData() {

            dtReceipt ds = new dtReceipt();

            string query = "SELECT TransactionCode,Seq,ItemName,Quantity,Price FROM transactiondetails td " +
                           "INNER JOIN items i " +
                           "ON i.ItemCode = td.ItemCode " +
                           "WHERE TransactionCode = '"+TransactionCode+ "' order by Seq asc";

            var command = new MySqlCommand(query, Config.connection);

            try
            {
                Config.connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                da.Fill(ds, "DataTable1");
                return ds;
            }
            finally
            {
                Config.connection.Close();
            }
        }

        private void frmReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
