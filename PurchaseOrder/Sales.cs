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
        private DataTable scanneditems;
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
            if (e.KeyCode.ToString() == "F1")
            {
                btnSave_Click(sender, null);
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
            scanneditems = new DataTable();
        }

        private void Sales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Close();
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                //scanneditems = SalesProcess.ScanBarcode(scanneditems,txtBarcode.Text);
            }
        }
        private void RefreshTable(DataTable data)
        { 
            foreach(DataRow row in data.Rows)
            { }
        }
    }
}
