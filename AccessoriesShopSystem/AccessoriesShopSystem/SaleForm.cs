using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AccessoriesShopSystem
{
    public partial class SaleForm : Form
    {
        Class1 c = new Class1();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public SaleForm()
        {
            InitializeComponent();
        }

        private void btnMainForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            Class1.mainform();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            ddgSaleItems.Columns.Add("A", "Invoice-No.");
            ddgSaleItems.Columns.Add("B", "Customer-ID");
            ddgSaleItems.Columns.Add("C", "Customer-Name");
            ddgSaleItems.Columns.Add("E", "Item-Name");
            ddgSaleItems.Columns.Add("C", "Current-Quantity");
            ddgSaleItems.Columns.Add("F", "Item-Quantity");
            ddgSaleItems.Columns.Add("D", "Purchase-Price");
            ddgSaleItems.Columns.Add("A", "Sale-Price");
            ddgSaleItems.Columns.Add("B", "Amount");
            ddgSaleItems.Columns.Add("C", "Profit");
            ddgSaleItems.Columns.Add("D", "Pay-Type");
            ddgSaleItems.Columns.Add("B", "Total Amount");
            ddgSaleItems.Columns.Add("D", "Sale Date");
            ddgSaleItems.Columns[0].Width = 200;
            ddgSaleItems.Columns[1].Width = 50;
            ddgSaleItems.Columns[2].Width = 50;
            ddgSaleItems.Columns[3].Width = 50;
            this.Height = 510;
        }
    }
}
