using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace AccessoriesShopSystem
{
    public partial class LoginForm : Form
    {
        Class1 c = new Class1();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("spLogin", c.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@u", txtuser.Text);
            cmd.Parameters.AddWithValue("@p", txtpass.Text);
            c.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                this.Hide();
                Class1.mainform();
            }
            else
            {
                MessageBox.Show("Incorrect UserName Or Password");
            }
            c.con.Close();
        }
    }
}
