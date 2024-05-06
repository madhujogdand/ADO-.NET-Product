using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Data;

namespace ADO.NET_Product
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnectionproduct"].ConnectionString;
            con = new SqlConnection(constr);

        }

        private DataSet GetAllProducts()
        {
            string qry = "select * from product";
            da= new SqlDataAdapter(qry,con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb=new SqlCommandBuilder(da);
            ds= new DataSet();
            da.Fill(ds, "product");
            return ds;
        }
        private void clearFields()
        { 
           txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
              ds=GetAllProducts();
                DataRow row = ds.Tables["product"].NewRow();
                row["name"]=txtName.Text;
                row["price"]=txtPrice.Text;
                ds.Tables["product"].Rows.Add(row);
                int result = da.Update(ds.Tables["product"]);
                if (result >= 1)
                {
                    MessageBox.Show("Record Inserted");
                    clearFields();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
              ds= GetAllProducts();
                DataRow row = ds.Tables["product"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtPrice.Text = row["price"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProducts();
                DataRow row = ds.Tables["product"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["price"]= txtPrice.Text;
                    int result = da.Update(ds.Tables["product"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record Updated");
                        clearFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProducts();
                DataRow row = ds.Tables["product"].Rows.Find(txtId.Text);
                if (row != null)
                {
                 row.Delete();
                    int result = da.Update(ds.Tables["product"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record Deleted");
                        clearFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
}

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds=GetAllProducts();
            dataGridView1.DataSource = ds.Tables["product"];
        }
    }
}
