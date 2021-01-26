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
    public partial class Main : Form
    {
        private PointOfSales pointofsales = new PointOfSales();
        private Sales sales;
        private Items items = new Items();
        private AddItems additems;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "System Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Config.UserInfo = null;
                foreach (Form frm in this.MdiChildren)
                {
                    frm.Dispose();
                    frm.Close();
                }
                this.Close();
            }
        }

        private void pointOfSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pointofsales.Dispose();
            pointofsales = new PointOfSales();
            pointofsales.MdiParent = this;
            pointofsales.NotifyMainFormToOpenChildForm2 += NotifyMainFormToOpenForm2;
            pointofsales.Show();
        }
        private void NotifyMainFormToOpenForm2()
        {
            if (sales != null) { sales.Dispose(); }
            sales = new Sales();
            sales.MdiParent = this;
            sales.Show();
        }
        private void NotifyMainFormToOpenChildFormAddItem()
        {
            if (additems != null) { additems.Dispose(); }
            additems = new AddItems();
            additems.MdiParent = this;
            additems.Show();
        }
        

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            items.Dispose();
            items = new Items();
            items.MdiParent = this;
            items.NotifyMainFormToOpenChildFormAddItem += NotifyMainFormToOpenChildFormAddItem;
            items.Show();
        }
    }
}
