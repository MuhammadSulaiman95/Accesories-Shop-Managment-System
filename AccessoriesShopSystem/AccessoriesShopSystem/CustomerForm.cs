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
    public partial class CustomerForm : Form
    {
        Class1 c = new Class1();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public CustomerForm()
        {
            InitializeComponent();
        }

        public void showdata()
        {
            cmd = new SqlCommand("spCustSelect", c.con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            CustomerView.DataSource = dt;
        }

        public void showdatav()
        {
            cmd = new SqlCommand("spVendorSelect", c.con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            ddgVendor.DataSource = dt;
        }

        public void autoidC()
        {
            c.con.Open();
            cmd = new SqlCommand("Select max(Cid)+1 from tblCustomer", c.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string id;
                id = dr[0].ToString();
                if (id.Equals(""))
                {
                    lblCustId.Text = "1";
                }
                else
                {
                    lblCustId.Text = id;
                }
            }
            c.con.Close();
        }

        public void autoidV()
        {
            c.con.Open();
            cmd = new SqlCommand("Select max(Vid)+1 from tblVendor", c.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string id;
                id = dr[0].ToString();
                if (id.Equals(""))
                {
                    lblVid.Text = "1";
                }
                else
                {
                    lblVid.Text = id;
                }
            }
            c.con.Close();
        }

        private void SetBtn()
        {
            btnCustAdd.Enabled = (txtCustName.Text != "" || txtCustName.Visible == false) && (txtCustPhone.Text != "" || txtCustPhone.Visible == false);
            btnCClear.Enabled = (txtCustName.Text != "" || txtCustName.Visible == false) && (txtCustPhone.Text != "" || txtCustPhone.Visible == false);
            btnCUpdate.Enabled = (txtCustName.Text != "" || txtCustName.Visible == false) && (txtCustPhone.Text != "" || txtCustPhone.Visible == false);
            btnCDelete.Enabled = (txtCustName.Text != "" || txtCustName.Visible == false) && (txtCustPhone.Text != "" || txtCustPhone.Visible == false);
            btnVAdd.Enabled = (txtVName.Text != "" || txtVName.Visible == false) && (txtVMob.Text != "" || txtVMob.Visible == false);
            btnVClear.Enabled = (txtVName.Text != "" || txtVName.Visible == false) && (txtVMob.Text != "" || txtVMob.Visible == false);
            btnVUpdate.Enabled = (txtVName.Text != "" || txtVName.Visible == false) && (txtVMob.Text != "" || txtVMob.Visible == false);
            btnVDelete.Enabled = (txtVName.Text != "" || txtVName.Visible == false) && (txtVMob.Text != "" || txtVMob.Visible == false);
        }

        public void clear1()
        {
            lblCustId.Text = "";
            txtCustName.Clear();
            txtCustAdress.Clear();
            txtCustPhone.Clear();
            txtCSearchName.Text = "";
            txtCSearchid.Text="";
        }

        public void clear2()
        {
            lblVid.Text = "";
            txtVName.Clear();
            txtVAdres.Clear();
            txtVMob.Clear();
            txtVSearchid.Text = "";
            txtVSearchName.Text = "";
        }



        private void btnMainForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            Class1.mainform();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            //btnCustAdd.Enabled = false;
            //btnCUpdate.Enabled = false;
            //btnCDelete.Enabled = false;
            //btnCClear.Enabled = false;
            //btnVAdd.Enabled = false;
            //btnVUpdate.Enabled = false;
            //btnVDelete.Enabled = false;
            //btnVClear.Enabled = false;
            autoidC();
            autoidV();
            showdata();
            showdatav();
        }

        private void btnCustAdd_Click(object sender, EventArgs e)
        {
            c.con.Open();
            try
            {
                cmd = new SqlCommand("spCustInsert", c.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", lblCustId.Text.ToString());
                cmd.Parameters.AddWithValue("@cn", txtCustName.Text);
                cmd.Parameters.AddWithValue("@cad", txtCustAdress.Text);
                cmd.Parameters.AddWithValue("@cph", txtCustPhone.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The data is inserted or saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Failed");
            }
            finally
            {
                c.con.Close();
            }
        }

        private void btnCUpdate_Click(object sender, EventArgs e)
        {
            c.con.Open();
            try
            {
                cmd = new SqlCommand("spCustUpdate", c.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", lblCustId.Text.ToString());
                cmd.Parameters.AddWithValue("@cn", txtCustName.Text);
                cmd.Parameters.AddWithValue("@cad", txtCustAdress.Text);
                cmd.Parameters.AddWithValue("@cph", txtCustPhone.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The data is inserted or saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Failed");
            }
            finally
            {
                c.con.Close();
            }
        }

        private void btnCDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("spCustDelete", c.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cid", lblCustId.Text.ToString());
            c.con.Open();
            cmd.ExecuteNonQuery();
            c.con.Close();
            MessageBox.Show("The data is Deleted");
            showdata();
        }

        private void btnCustSearch_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("FindCustomer", c.con);
            cmd.Parameters.AddWithValue("@Cid", txtCSearchid.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            c.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblCustId.Text = dr[0].ToString();
                txtCustName.Text = dr[1].ToString();
                txtCustAdress.Text = dr[2].ToString();
                txtCustPhone.Text = dr[3].ToString();
            }
            c.con.Close();
        }

        private void txtCSearchName_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("CName like '%" + txtCSearchName.Text + "%' ");
            CustomerView.DataSource = dv.ToTable();
        }

        private void CustomerView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblCustId.Text = CustomerView.CurrentRow.Cells["Cid"].Value.ToString();
            txtCustName.Text = CustomerView.CurrentRow.Cells["CName"].Value.ToString();
            txtCustAdress.Text = CustomerView.CurrentRow.Cells["CAddress"].Value.ToString();
            txtCustPhone.Text = CustomerView.CurrentRow.Cells["CPhone"].Value.ToString();
        }

        private void btnVAdd_Click(object sender, EventArgs e)
        {
            c.con.Open();
            try
            {
                cmd = new SqlCommand("spVendorInsert", c.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vid", lblVid.Text.ToString());
                cmd.Parameters.AddWithValue("@vn", txtVName.Text);
                cmd.Parameters.AddWithValue("@vad", txtVAdres.Text);
                cmd.Parameters.AddWithValue("@vph", txtVMob.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The data is inserted or saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Failed");
            }
            finally
            {
                c.con.Close();
            }
        }

        private void btnVUpdate_Click(object sender, EventArgs e)
        {
            c.con.Open();
            try
            {
                cmd = new SqlCommand("spVendorUpdate", c.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vid", lblVid.Text.ToString());
                cmd.Parameters.AddWithValue("@vn", txtVName.Text);
                cmd.Parameters.AddWithValue("@vad", txtVAdres.Text);
                cmd.Parameters.AddWithValue("@vph", txtVMob.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The data is inserted or saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Failed");
            }
            finally
            {
                c.con.Close();
            }
        }

        private void btnVDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("spVendorDelete", c.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vid", lblVid.Text.ToString());
            c.con.Open();
            cmd.ExecuteNonQuery();
            c.con.Close();
            MessageBox.Show("The data is Deleted");
            showdatav();
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("FindVendor", c.con);
            cmd.Parameters.AddWithValue("@vid", txtVSearchid.Text.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            c.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblVid.Text = dr[0].ToString();
                txtVName.Text = dr[1].ToString();
                txtVAdres.Text = dr[2].ToString();
                txtVMob.Text = dr[3].ToString();
            }
            c.con.Close();
        }

        private void txtVSearchName_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("VName like '%" + txtVSearchName.Text + "%' ");
            ddgVendor.DataSource = dv.ToTable();
        }

        private void btnCClear_Click(object sender, EventArgs e)
        {
            clear1();
        }

        private void btnVClear_Click(object sender, EventArgs e)
        {
            clear2();
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtCustPhone_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtVName_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtVMob_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }




    }
}
