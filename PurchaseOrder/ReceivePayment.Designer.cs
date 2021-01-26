
namespace PurchaseOrder
{
    partial class ReceivePayment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.btnReceive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPayment
            // 
            this.txtPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayment.Location = new System.Drawing.Point(12, 12);
            this.txtPayment.Multiline = true;
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(267, 46);
            this.txtPayment.TabIndex = 0;
            this.txtPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPayment_KeyPress);
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(12, 64);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(267, 43);
            this.btnReceive.TabIndex = 1;
            this.btnReceive.Text = "Receive";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // ReceivePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 115);
            this.ControlBox = false;
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.txtPayment);
            this.Name = "ReceivePayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceivePayment";
            this.Load += new System.EventHandler(this.ReceivePayment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Button btnReceive;
    }
}