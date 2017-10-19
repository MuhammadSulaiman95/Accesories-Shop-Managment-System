using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccessoriesShopSystem
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        public void hd()
        {
            this.Hide();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            hd();
            Class1.saleform();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            hd();
            Class1.customerform();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            hd();
            Class1.purform();
        }

        private void brnReturn_Click(object sender, EventArgs e)
        {
            hd();
            Class1.saleform();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            hd();
            Class1.paymentform();
        }


    }
}
