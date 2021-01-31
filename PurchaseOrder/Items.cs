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
            if (Convert.ToInt32(Config.UserInfo.Rows[0]["UserRights"]) == 2)
            {
                btnStockout.Enabled = false;
            }
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
            else if (e.KeyCode == Keys.F2)
            {
                if (Convert.ToInt32(Config.UserInfo.Rows[0]["UserRights"]) == 1)
                {
                    btnStockout_Click(sender, null);
                }
            }
        }

        private void RefreshTable()
        {
            DataTable dtable = new DataTable();
            if (txtSearch.Text=="")
            { 
                dtable = ItemsProcess.RetrieveData();
            }
            else
            {
                dtable = ItemsProcess.RetrieveData(txtSearch.Text.Trim());
            }

            txtCountTotal.Text = dtable.Rows.Count.ToString();

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
                    dataGridView1.Rows[x].Cells[6].Value = ">>";
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
            Form additem = new ItemsInfo();
            ItemsInfo.FormStatus = 1;
            additem.ShowDialog();
            RefreshTable();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                Form additem = new ItemsInfo();
                ItemsInfo.FormStatus = 2;
                ItemsInfo.ItemCode = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                additem.ShowDialog();
                RefreshTable();
            }
        }

        private void btnStockout_Click(object sender, EventArgs e)
        {
            Form form = new StockOutRange();
            form.ShowDialog();
        }
    }
}
