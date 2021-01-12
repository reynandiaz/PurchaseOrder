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
    public partial class RegisterItem : Form
    {
        public static string ItemCode;

        public RegisterItem()
        {
            InitializeComponent();
        }

        private void RegisterItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            { this.Close(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             if(txtCode.Text!="" && txtItemName.Text!="" && txtUnitPrice.Text!="")
            {
                var rtnValue = ItemsProcess.AddNewItem(txtCode.Text.Trim(), txtItemName.Text.Trim(), txtUnitPrice.Text.Trim());
                if (rtnValue.isSuccess == true)
                {
                    this.Close();
                }
            }
        }

        private void RegisterItem_Load(object sender, EventArgs e)
        {
            txtCode.Text = ItemCode;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
