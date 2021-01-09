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
            if (NotifyMainFormToOpenChildForm2 != null)
            {
                NotifyMainFormToOpenChildForm2();
            }
        }

        private void PointOfSales_Load(object sender, EventArgs e)
        {

        }
    }
}

