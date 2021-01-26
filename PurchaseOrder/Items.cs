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
    public partial class Items : Form
    {
        public Action NotifyMainFormToOpenChildFormAddItem;

        public Items()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void Items_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, null);
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnAdd_Click(sender, null);
            }
        }
        private void RefreshTable()
        {
            DataTable dtable = ItemsProcess.RetrieveData();

            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            if (dtable.Rows.Count > 0)
            {
                dataGridView1.Rows.Add(dtable.Rows.Count);
                int x = 0;
                foreach (DataRow row in dtable.Rows)
                {
                    dataGridView1.Rows[x].Cells[0].Value = row["ItemCode"].ToString();
                    dataGridView1.Rows[x].Cells[1].Value = row["ItemName"].ToString();
                    dataGridView1.Rows[x].Cells[2].Value = "P "+row["UnitPrice"].ToString();
                    dataGridView1.Rows[x].Cells[3].Value = row["MinStocks"].ToString();
                    dataGridView1.Rows[x].Cells[4].Value = row["CurrentStocks"].ToString();
                    dataGridView1.Rows[x].Cells[5].Value = row["MaxStocks"].ToString();
                    x++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (NotifyMainFormToOpenChildFormAddItem != null)
            //{
            //    NotifyMainFormToOpenChildFormAddItem();
            //}
            Form additem = new AddItems();
            additem.ShowDialog();
            RefreshTable();
        }
    }
}
