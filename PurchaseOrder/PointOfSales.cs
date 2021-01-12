using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PointOfSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                btnSales_Click(sender, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
        private void RefreshTable()
        { 


        
        
        }

    }
}

