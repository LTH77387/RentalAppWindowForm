using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalApplication
{
    public partial class LoginForm : Form
    {
        int attempts = 0;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {            
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please fill all fields!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(attempts >= 3)
                {
                    MessageBox.Show("You have exceed the maximum number of login fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Htet_Aung;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Name=@name AND Password=@password", conn);
                        cmd.Parameters.AddWithValue("@name", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        conn.Close();
                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows[0]["Role"].ToString().Trim() == "admin")
                            {
                                AdminHomeForm adminHomeForm = new AdminHomeForm();
                                adminHomeForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                CustomerHomeForm customerHomeForm = new CustomerHomeForm();
                                customerHomeForm.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Login Fail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            attempts+=1;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
