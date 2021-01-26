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
    public partial class ReceivePayment : Form
    {
        public ReceivePayment()
        {
            InitializeComponent();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            if(txtPayment.Text!="")
            {
                Sales.ReceivedPayment = Convert.ToDouble(txtPayment.Text);
                this.Close();
            }
        }

        private void ReceivePayment_Load(object sender, EventArgs e)
        {

        }

        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n' && txtPayment.Text.Trim() != "")
            {
                btnReceive.Focus();
            }
        }
    }
}
