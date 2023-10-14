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
    public partial class AdminAddAppliancesForm : Form
    {
        public AdminAddAppliancesForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "" || txtType.Text == "" || txtEnergyConsumption.Text == "" || txtWeeklyCost.Text == "")
            {
                MessageBox.Show("Please fill all fields!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Htet_Aung;Integrated Security=True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Appliances (Name,Type,Energy_Consumption,Weekly_Cost) VALUES (@name,@type,@energy_consumption,@weekly_cost)", conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@type", txtType.Text);
                    cmd.Parameters.AddWithValue("@energy_consumption", txtEnergyConsumption.Text);
                    cmd.Parameters.AddWithValue("@weekly_cost", txtWeeklyCost.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    DialogResult result = MessageBox.Show("Appliances created!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        txtName.Text = string.Empty;
                        txtType.Text = string.Empty;
                        txtEnergyConsumption.Text = string.Empty;
                        txtWeeklyCost.Text = string.Empty;
                        AdminHomeForm adminHomeForm = new AdminHomeForm();
                        adminHomeForm.Show();
                        this.Hide();
                    }

                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AdminAddAppliancesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminHomeForm adminHomeForm = new AdminHomeForm();
            adminHomeForm.Show();
            this.Hide();
        }
    }
}
