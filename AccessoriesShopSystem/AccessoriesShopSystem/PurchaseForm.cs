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
    public partial class PurchaseForm : Form
    {
        Class1 c = new Class1();
        SqlCommand cmd;
        SqlDataReader dr;
        SqlTransaction tra;
        DataTable dt,xTable,UpTable;
        SqlDataAdapter da;
        string A, B, C, D, E, F, G, H;
        public PurchaseForm()
        {
            InitializeComponent();
            xTable = new DataTable();
            ddgPurItems.Columns.Add("A", "Invoice No");
            ddgPurItems.Columns.Add("B", "Item Name");
            ddgPurItems.Columns.Add("C", "Item Quantity");
            ddgPurItems.Columns.Add("D", "Purchase Price");
            ddgPurItems.Columns.Add("E", "Total Amount");
            ddgPurItems.Columns.Add("F", "Purchase Date");
            ddgPurItems.Columns.Add("G", "Vendor Name");
            ddgPurItems.Columns[0].Width = 100;
            ddgPurItems.Columns[1].Width = 200;
            ddgPurItems.Columns[2].Width = 200;
            ddgPurItems.Columns[3].Width = 100;
            ddgPurItems.Columns[4].Width = 200;
            ddgPurItems.Columns[5].Width = 100;
            ddgPurItems.Columns[6].Width = 200;
            this.Height = 510;
            UpTable = new DataTable();
            ddgUpPur.Columns.Add("A", "Invoice No");
            ddgUpPur.Columns.Add("B", "Session ID");
            ddgUpPur.Columns.Add("C", "Item Name");
            ddgUpPur.Columns.Add("D", "Item Quantity");
            ddgUpPur.Columns.Add("E", "Purchase Price");
            ddgUpPur.Columns.Add("F", "Total Amount");
            ddgUpPur.Columns.Add("G", "Purchase Date");
            ddgUpPur.Columns.Add("H", "Vendor Name");
            ddgUpPur.Columns[0].Width = 100;
            ddgUpPur.Columns[1].Width = 100;
            ddgUpPur.Columns[2].Width = 200;
            ddgUpPur.Columns[3].Width = 100;
            ddgUpPur.Columns[4].Width = 100;
            ddgUpPur.Columns[5].Width = 100;
            ddgUpPur.Columns[6].Width = 100;
            ddgUpPur.Columns[7].Width = 200;
            this.Height = 510;
        }

        public void showdata()
        {
            string q = "spStockSelect";
            StockGrid.DataSource = c.getData(q);

        }
        public void showdata1()
        {
            da = new SqlDataAdapter("spPurSelect", c.con);
            dt = new DataTable();
            da.Fill(dt);
            ddgInv.DataSource = dt;
        }

        public void autoidI()
        {
            c.con.Open();
            cmd = new SqlCommand("Select max(InvNo)+1 from tblPurchase", c.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string id;
                id = dr[0].ToString();
                if (id.Equals(""))
                {
                    txtInv.Text = "1";
                }
                else
                {
                    txtInv.Text = id;
                }
            }
            c.con.Close();
        }

        public void autoComp()
        {
            AutoCompleteStringCollection cl = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cl1 = new AutoCompleteStringCollection();
            c.con.Open();
            cmd = new SqlCommand("spVendorSelect", c.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cl.Add(dr[1].ToString());
                cl1.Add(dr[1].ToString());
            }
            txtVName.AutoCompleteCustomSource = cl;
            txtUpCustSearch.AutoCompleteCustomSource = cl1;
            c.con.Close();
        }

        public void autoComp1()
        {
            AutoCompleteStringCollection cl2 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cl3 = new AutoCompleteStringCollection();
            c.con.Open();
            cmd = new SqlCommand("spPurSelect", c.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cl2.Add(dr[0].ToString());
                cl3.Add(dr[0].ToString());
            }
            txtUpInvSearch.AutoCompleteCustomSource = cl2;
            txtInvSearch.AutoCompleteCustomSource = cl3;
            c.con.Close();
        }

        public void autoComp2()
        {
            AutoCompleteStringCollection cl4 = new AutoCompleteStringCollection();
            c.con.Open();
            cmd = new SqlCommand("spStockSelect", c.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cl4.Add(dr[1].ToString());
            }
            txtSearchItemName.AutoCompleteCustomSource = cl4;
            c.con.Close();
        }

        public void clear()
        {
            txtItemName.Text = "";
            txtNewQty.Text = "";
            txtPurPrice.Text = "";
            txtTotalAmt.Text = "";
            txtGTotal.Text = "";
            txtVName.Text = "";
            purDate.ResetText();
            txtRAmt.Text = "";
            txtRBal.Text = "";
            autoidI();
            txtSearchItemName.Text = "";
            txtSearchPurItem.Text = "";
            ddgPurItems.Rows.Clear();
            xTable.Clear();
        }

        public void clear1()
        {
            txtUpIName.Text = "";
            txtUpNQty.Text = "";
            txtUpPPrice.Text = "";
            txtUpTAmt.Text = "";
            txtUpGTotal.Text = "";
            txtUpVName.Text = "";
            txtSId.Text = "";
            txtUpInv.Text = "";
            txtUpVName.Text = "";
            upPurDate.ResetText();
            txtUpRAmt.Text = "";
            txtUpRBal.Text = "";
            txtInvSearch.Text = "";
            txtUpInvSearch.Text = "";
            txtUpCustSearch.Text = "";
            txtUpSearchPurItem.Text = "";
            UpTable.Clear();
            ddgUpPur.Rows.Clear();
        }

        public void itm()
        {
            int itot=0;
            for(int i=0; i<ddgPurItems.Rows.Count; i++)
            {
                itot = itot + int.Parse(ddgPurItems.Rows[i].Cells[2].Value.ToString());
            }
            lblTotalItem.Text = itot.ToString();
        }

        public void GTotal()
        {
            decimal itot = 0;
            for (int i = 0; i<ddgPurItems.Rows.Count; i++)
            {
                itot = itot + decimal.Parse(ddgPurItems.Rows[i].Cells[4].Value.ToString());
            }
            txtGTotal.Text = itot.ToString();
        }

        public void Upitm()
        {
            int itot = 0;
            for (int i = 0; i < ddgUpPur.Rows.Count; i++)
            {
                itot = itot + int.Parse(ddgUpPur.Rows[i].Cells[3].Value.ToString());
            }
            lblUpTotItems.Text = itot.ToString();
        }

        public void UpGTot()
        {
            decimal itot = 0;
            for (int i = 0; i < ddgUpPur.Rows.Count; i++)
            {
                itot = itot + decimal.Parse(ddgUpPur.Rows[i].Cells[5].Value.ToString());
            }
            txtUpGTotal.Text = itot.ToString();
        }

        private void SetBtn()
        {
            btnAdd.Enabled = (txtItemName.Text != "" || txtItemName.Visible == false) && (txtNewQty.Text != "" || txtNewQty.Visible == false) && (txtPurPrice.Text != "" || txtPurPrice.Visible == false) && (txtVName.Text != "" || txtVName.Visible == false) && (txtGTotal.Text != "" || txtGTotal.Visible == false);
            btnUpdate.Enabled = (txtUpIName.Text != "" || txtUpIName.Visible == false) && (txtUpNQty.Text != "" || txtUpNQty.Visible == false) && (txtPurPrice.Text != "" || txtPurPrice.Visible == false) && (txtUpVName.Text != "" || txtUpVName.Visible == false) && (txtUpGTotal.Text != "" || txtUpGTotal.Visible == false);//&& (txtTotQty.Text != "" || txtTotQty.Visible == false);
            btnInvSearch.Enabled = (txtInvSearch.Text != "" || txtInvSearch.Visible == false) && (txtUpInvSearch.Text != "" || txtUpInvSearch.Visible == false);
        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            showdata();
            showdata1();
            autoidI();
            autoComp();
            autoComp1();
            autoComp2();
            btnInvSearch.Enabled = false;
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            //UpGTot();
            //Upitm();
        }

        private void StockGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemName.Text = StockGrid.CurrentRow.Cells["ItemName"].Value.ToString();
            txtPurPrice.Text = StockGrid.CurrentRow.Cells["PurPrice"].Value.ToString();
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            A = txtInv.Text.ToString();
            B = txtItemName.Text;
            C = txtNewQty.Text.ToString();
            D = txtPurPrice.Text.ToString();
            E = txtTotalAmt.Text.ToString();
            F = purDate.Value.ToString();
            G = txtVName.Text;
            ddgPurItems.Rows.Add(A, B, C, D, E, F, G);
            xTable.Clear();
            itm();
            GTotal();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            ddgPurItems.Rows.RemoveAt(ddgPurItems.CurrentRow.Index);
            itm();
            GTotal();
        }

        private void btnDeleteAllItem_Click(object sender, EventArgs e)
        {
            ddgPurItems.Rows.Clear();
            xTable.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            purDate.Format = DateTimePickerFormat.Custom;
            purDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            c.con.Open();
            tra = c.con.BeginTransaction();
            try
            {
                SqlCommand cmd1 = new SqlCommand("spPurP", c.con, tra);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@inv", ddgPurItems.Rows[0].Cells[0].Value.ToString());
                cmd1.Parameters.AddWithValue("@totitems", lblTotalItem.Text.ToString());
                cmd1.Parameters.AddWithValue("@gtot", txtGTotal.Text.ToString());
                cmd1.Parameters.AddWithValue("@pamt", txtRAmt.Text.ToString());
                cmd1.Parameters.AddWithValue("@datein", ddgPurItems.Rows[0].Cells[5].Value.ToString());
                cmd1.Parameters.AddWithValue("@vname", ddgPurItems.Rows[0].Cells[6].Value);
                cmd1.ExecuteNonQuery();
                foreach (DataGridViewRow row in ddgPurItems.Rows)
                {
                    cmd = new SqlCommand("spPurInsert", c.con, tra);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inv", row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@itemname", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@qty", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@purprice", row.Cells[3].Value);
                    cmd.Parameters.AddWithValue("@tamt", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@datein", row.Cells[5].Value);
                    cmd.Parameters.AddWithValue("@vname", row.Cells[6].Value);
                    cmd.ExecuteNonQuery();
                }
                tra.Commit();
                c.con.Close();
                MessageBox.Show("Record Inserted");
                clear();
                showdata();
            }
            catch (Exception ex)
            {
                tra.Rollback();
                MessageBox.Show(ex.Message + " Failed");
            }
            showdata();
        }

        private void btnMainForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            Class1.mainform();
        }

        private void btnInvSearch_Click(object sender, EventArgs e)
        {
            txtUpInvSearch.ReadOnly = true;
            txtInvSearch.Visible = false;
            string q = "select * from tblPurchase where InvNo='" + txtInvSearch.Text.ToString() + "'";
            ddgInv.DataSource = c.getData(q);
            int n = 0;
            foreach (DataGridViewRow row in ddgInv.Rows)
            {
                if (ddgInv.Rows.Count != n + 1)
                {
                    ddgUpPur.Rows.Add();
                    ddgUpPur.Rows[n].Cells[0].Value = row.Cells[0].Value.ToString();
                    ddgUpPur.Rows[n].Cells[1].Value = row.Cells[1].Value.ToString();
                    ddgUpPur.Rows[n].Cells[2].Value = row.Cells[3].Value.ToString();
                    ddgUpPur.Rows[n].Cells[3].Value = row.Cells[4].Value.ToString();
                    ddgUpPur.Rows[n].Cells[4].Value = row.Cells[5].Value.ToString();
                    ddgUpPur.Rows[n].Cells[5].Value = row.Cells[8].Value.ToString();
                    ddgUpPur.Rows[n].Cells[6].Value = row.Cells[9].Value.ToString();
                    ddgUpPur.Rows[n].Cells[7].Value = row.Cells[7].Value.ToString();
                }
                n += 1;
            }
            c.con.Open();
            SqlCommand cmd1 = new SqlCommand("FindPurInv", c.con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@inv", txtInvSearch.Text.ToString());
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                txtUpGTotal.Text = (dr[1].ToString());
                txtUpRAmt.Text = (dr[2].ToString());
                txtUpRBal.Text = (dr[3].ToString());
            }
            c.con.Close();
            btnInvSearch.Enabled = false;
        }

        private void btnUpUpgrade_Click(object sender, EventArgs e)
        {
            A = txtUpInv.Text.ToString();
            B = txtSId.Text;
            C = txtUpIName.Text;
            D = txtUpNQty.Text.ToString();
            E = txtUpPPrice.Text.ToString();
            F = txtUpTAmt.Text.ToString();
            G = upPurDate.Value.ToString();
            H = txtUpVName.Text;
            ddgUpPur.Rows.Add(A, B, C, D, E, F, G, H);
            UpTable.Clear();
            UpGTot();
            Upitm();
        }

        private void BtnUpDel_Click(object sender, EventArgs e)
        {
            ddgUpPur.Rows.RemoveAt(ddgUpPur.CurrentRow.Index);
            UpGTot();
            Upitm();
        }

        private void btnUpDelAll_Click(object sender, EventArgs e)
        {
            ddgUpPur.Rows.Clear();
        }

        private void ddgInv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUpInv.Text = ddgInv.CurrentRow.Cells["InvNo"].Value.ToString();
            txtUpIName.Text = ddgInv.CurrentRow.Cells["ItemName"].Value.ToString();
            txtUpPPrice.Text = ddgInv.CurrentRow.Cells["PurPrice"].Value.ToString();
            txtUpNQty.Text = ddgInv.CurrentRow.Cells["Qty"].Value.ToString();
            upPurDate.Text = ddgInv.CurrentRow.Cells["DateIn"].Value.ToString();
            txtUpVName.Text = ddgInv.CurrentRow.Cells["VName"].Value.ToString();
            txtSId.Text = ddgInv.CurrentRow.Cells["Sid"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            purDate.Format = DateTimePickerFormat.Custom;
            purDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            c.con.Open();
            SqlTransaction tra = c.con.BeginTransaction();
            try
            {
                SqlCommand cmd1 = new SqlCommand("spPurP", c.con, tra);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@inv", ddgUpPur.Rows[0].Cells[0].Value);
                cmd1.Parameters.AddWithValue("@totitems", lblUpTotItems.Text.ToString());
                cmd1.Parameters.AddWithValue("@gtot", txtUpGTotal.Text.ToString());
                cmd1.Parameters.AddWithValue("@pamt", txtUpRAmt.Text.ToString());
                cmd1.Parameters.AddWithValue("@datein", ddgUpPur.Rows[0].Cells[6].Value.ToString());
                cmd1.Parameters.AddWithValue("@vname", ddgUpPur.Rows[0].Cells[7].Value);
                cmd1.ExecuteNonQuery();
                foreach (DataGridViewRow row in ddgUpPur.Rows)
                {
                    cmd = new SqlCommand("spPurUpdate", c.con,tra);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inv", row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@sid", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@itemname", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@qty", row.Cells[3].Value);
                    cmd.Parameters.AddWithValue("@purprice", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@tamt", row.Cells[5].Value);
                    cmd.Parameters.AddWithValue("@datein", row.Cells[6].Value);
                    cmd.Parameters.AddWithValue("@vname", row.Cells[7].Value);
                    cmd.ExecuteNonQuery();
                }
                tra.Commit();
                MessageBox.Show("Record Updated");
                clear1();
            }
            catch (Exception ex)
            {
                tra.Rollback();
                MessageBox.Show(ex.Message + "Failed");
            }
            finally
            {
                c.con.Close();
            }
            showdata();
        }

        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            DataView dv = c.dt.DefaultView;
            dv.RowFilter = string.Format("ItemName like '%" + txtSearchItemName.Text + "%' ");
            StockGrid.DataSource = dv.ToTable();
        }

        private void txtUpInvSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("Convert([InvNo], System.String) like '%{0}%'", txtUpInvSearch.Text);
            ddgInv.DataSource = dv.ToTable();
        }

        private void textUpCustSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("VName like '%" + txtUpCustSearch.Text + "%' ");
            ddgInv.DataSource = dv.ToTable();
        }

        //private void txtSearchPurItem_TextChanged(object sender, EventArgs e)
        //{
        //    DataView dv = c.dt.DefaultView;
        //    dv.RowFilter = string.Format("ItemName like '%" + txtSearchPurItem.Text + "%' ");
        //    ddgPurItems.DataSource = dv.ToTable();
        //}

        private void txtUpSearchPurItem_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("ItemName like '%" + txtUpSearchPurItem.Text + "%' ");
            ddgInv.DataSource = dv.ToTable();
        }

        private void txtPurPrice_Leave(object sender, EventArgs e)
        {
            int s;
            decimal p, g;
            if (int.TryParse(txtNewQty.Text, out s) && decimal.TryParse(txtPurPrice.Text, out p))
            {
                s = int.Parse(txtNewQty.Text);
                p = decimal.Parse(txtPurPrice.Text);
                g = s * p;
                txtTotalAmt.Text = g.ToString();
            }
            else
            {
                MessageBox.Show("Incorrect Value");
                txtPurPrice.Clear();
                txtNewQty.Text = "";
            }
        }

        private void txtUpPPrice_Leave(object sender, EventArgs e)
        {
            int s;
            decimal p, g;
            if (int.TryParse(txtUpNQty.Text, out s) && decimal.TryParse(txtUpPPrice.Text, out p))
            {
                s = int.Parse(txtUpNQty.Text);
                p = decimal.Parse(txtUpPPrice.Text);
                g = s * p;
                txtUpTAmt.Text = g.ToString();
            }
            else
            {
                MessageBox.Show("Incorrect Value");
                txtUpPPrice.Clear();
            }
        }


        private void txtRAmt_Leave(object sender, EventArgs e)
        {
            decimal s, p, g;
            if (decimal.TryParse(txtGTotal.Text, out s) && decimal.TryParse(txtRAmt.Text, out p))
            {
                s = decimal.Parse(txtGTotal.Text);
                p = decimal.Parse(txtRAmt.Text);
                g = s - p;
                txtRBal.Text = g.ToString();
            }
            else
            {
                MessageBox.Show("Incorrect Value");
                txtRAmt.Clear();
            }
        }

        private void txtUpRAmt_Leave(object sender, EventArgs e)
        {
            decimal s, p, g;
            if (decimal.TryParse(txtUpGTotal.Text, out s) && decimal.TryParse(txtUpRAmt.Text, out p))
            {
                s = decimal.Parse(txtUpGTotal.Text);
                p = decimal.Parse(txtUpRAmt.Text);
                g = s - p;
                txtUpRBal.Text = g.ToString();
            }
            else
            {
                MessageBox.Show("Incorrect Value");
                txtUpRAmt.Clear();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnUpClear_Click(object sender, EventArgs e)
        {
            clear1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showdata();
            showdata1();
            ddgUpPur.Rows.Clear();
            btnInvSearch.Enabled = true;
            txtUpInvSearch.ReadOnly = false;
            txtInvSearch.Visible = true;
            txtInvSearch.Clear();
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtNewQty_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtPurPrice_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtVName_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtNetAmt_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtUpIName_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtUpNQty_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtTotQty_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtUpPPrice_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtUpVName_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtUpNAmt_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

        private void txtVName_Leave(object sender, EventArgs e)
        {
            c.con.Open();
            cmd = new SqlCommand("select RBal from tblVendor where VName='" + txtVName.Text + "'", c.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                txtCBal.Text = dr[0].ToString();
            }
            dr.Close();
            cmd.ExecuteNonQuery();
            c.con.Close();
        }

        private void txtInvSearch_TextChanged(object sender, EventArgs e)
        {
            SetBtn();
        }

    }
}
