using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace AccessoriesShopSystem
{
    class Class1
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public DataTable dt;
        public Class1()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        }

        public static void mainform()
        {
            MainForm cf = new MainForm();
            cf.Show();
        }

        public static void customerform()
        {
            CustomerForm cf = new CustomerForm();
            cf.Show();
        }

        public static void saleform()
        {
            SaleForm sf = new SaleForm();
            sf.Show();
        }

        public static void purform()
        {
            PurchaseForm sf = new PurchaseForm();
            sf.Show();
        }

        public static void paymentform()
        {
            PaymentForm sf = new PaymentForm();
            sf.Show();
        }

        public DataTable showdata(string q)
        {
            da = new SqlDataAdapter(q, con);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public object getData(string q)
        {
            object o = showdata(q);
            return o;
        }

        public void IUD()
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
