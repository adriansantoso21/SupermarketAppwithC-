
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SupermarketApp
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mkadr\Documents\SMARKETDB.mdf;Integrated Security=True;Connect Timeout=30");
        
        //Tombol add
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into CategoryTbl values(" + CatIdTb.Text + ", '" + CatNameTb.Text + "','" + CatDescTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("category Added Successfully");
                Con.Close();
                populate();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        //Tombol silang
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate() 
        {
            Con.Open();
            string query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }


        private void CatDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
            CatIdTb.Text = "123";
        }

        //button delete
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from CategoryTbl where CatId=" + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Edit delete
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update CategoryTbl set CatName='" + CatNameTb.Text + "', CatDesc='" + CatDescTb.Text + "' where CatId=" + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Successfully Updated");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //tombol sidebar products
        private void button2_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        //tombol sidebar seller
        private void button1_Click(object sender, EventArgs e)
        {
            FormSeller sell = new FormSeller();
            sell.Show();
            this.Hide();
        }

        //log out
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
