using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Data;

namespace ADO.NET_Product
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnectionproduct"].ConnectionString;
            con = new SqlConnection(constr);


        }

        private void clearFields()
        { 
           txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string qry = "insert into product values(@name,@price)";
                cmd = new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("name", txtName.Text);
                cmd.Parameters.AddWithValue("price", txtPrice.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record Inserted");
                    clearFields();                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from product where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("id", txtId.Text);
                con.Open();
                rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        txtName.Text = rd["name"].ToString();
                        txtPrice.Text = rd["price"].ToString();
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
            finally
            {
                con.Close();
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                string qry = "update product set name=@name, price=@price where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("name", txtName.Text);
                cmd.Parameters.AddWithValue("price", txtPrice.Text);
                cmd.Parameters.AddWithValue("id", txtId.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record Updated");
                    clearFields();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                string qry = "delete from product where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("id", txtId.Text);
               
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record Deleted");
                    clearFields();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from product";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rd= cmd.ExecuteReader();
            DataTable table= new DataTable();
            table.Load(rd);
            dataGridView1.DataSource = table;
        }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
