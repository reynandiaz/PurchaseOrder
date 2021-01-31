using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using PurchaseOrder.Process;


namespace PurchaseOrder.Reports.StockOut
{
    public partial class frmStockOut : Form
    {
        public static string TransactionCode;
        //blTodayOnly =true today only;
        public static bool blTodayOnly;
        public static DateTime DateFrom;
        public static DateTime DateTo;
        public frmStockOut()
        {
            InitializeComponent();
        }

        private void frmStockOut_Load(object sender, EventArgs e)
        {
            dtStockout lstItems = new dtStockout();
            
            if(blTodayOnly == true)
            {
                lstItems = GetData(DateFrom.ToString("yyyy-MM-dd"), DateFrom.ToString("yyyy-MM-dd"));
                ReportParameterCollection reportParams = new ReportParameterCollection();
                reportParams.Add(new ReportParameter("prDate", DateFrom.ToString("yyyy/MM/dd")));
                reportParams.Add(new ReportParameter("prDate2", DateFrom.ToString("yyyy/MM/dd")));
                this.reportViewer1.LocalReport.SetParameters(reportParams);
            }
            else
            {
                lstItems = GetData(DateFrom.ToString("yyyy-MM-dd"), DateTo.ToString("yyyy-MM-dd"));
                ReportParameterCollection reportParams = new ReportParameterCollection();
                reportParams.Add(new ReportParameter("prDate", DateFrom.ToString("yyyy/MM/dd")));
                reportParams.Add(new ReportParameter("prDate2", DateTo.ToString("yyyy/MM/dd")));
                this.reportViewer1.LocalReport.SetParameters(reportParams);
            }

            ReportDataSource datasource = new ReportDataSource("DataSet1", lstItems.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();

        }
        private dtStockout GetData(string datefrom, string dateto)
        {
            dtStockout ds = new dtStockout();

            string query = "SELECT td.ItemCode,s.itemname,sum(td.Quantity) TotalStockOut,s.CurrentStocks FROM   " +
                            "transactiondetails td " +
                            "INNER JOIN itemstocks s " +
                            "ON td.ItemCode = s.ItemCode " +
                            "WHERE td.TransactionCode like '" + TransactionCode.Split('-')[0].ToString() + "-%' " +
                            "AND DATE_FORMAT(createddate,'%Y-%m-%d') between '" + datefrom + "' and '" + dateto + "' "+
                            "GROUP BY s.ItemCode ";


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
    }
}
