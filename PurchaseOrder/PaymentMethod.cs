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
    public partial class PaymentMethod : Form
    {
        public PaymentMethod()
        {
            InitializeComponent();
        }

        private void PaymentMethod_Load(object sender, EventArgs e)
        {
            GeneratePaymentMethod();
        }
        private void GeneratePaymentMethod()
        {
            DataTable data = PaymentMethodProcess.GetPaymentMethods();

            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            if (data.Rows.Count > 0)
            {
                dataGridView1.Rows.Add(data.Rows.Count);
                int x = 0;
                foreach (DataRow row in data.Rows)
                {
                    dataGridView1.Rows[x].Cells[0].Value = row["PaymentMethodID"].ToString();
                    dataGridView1.Rows[x].Cells[1].Value = row["PaymentMethod"].ToString();
                    x++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Sales.PaymentMethod = 0;

                //string strPaymentMethod = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                string strPaymentMethod = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

                Sales.PaymentMethod =Convert.ToInt32(strPaymentMethod);
                this.Close();
            }
        }
    }
}
