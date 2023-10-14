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
    public partial class AdminEditAppliancesForm : Form
    {
        public int id;
        public string name, type, energyConsumption, weeklyCost;
        public AdminEditAppliancesForm(int id, string name, string type, string energyConsumption, string weeklyCost)
        {
            InitializeComponent();
            this.id = id;
            this.name = name;
            this.type = type;
            this.energyConsumption = energyConsumption;
            this.weeklyCost = weeklyCost;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "" || txtType.Text == "" || txtEnergyConsumption.Text == "" || txtWeeklyCost.Text == "")
            {
                MessageBox.Show("Please fill all fields!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Htet_Aung;Integrated Security=True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Appliances SET Name=@name, Type=@type, Energy_Consumption=@energy_consumption, Weekly_Cost=@weekly_cost WHERE Appliance_ID=@appliance_id", conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@type", txtType.Text);
                    cmd.Parameters.AddWithValue("@energy_consumption", txtEnergyConsumption.Text);
                    cmd.Parameters.AddWithValue("@weekly_cost", txtWeeklyCost.Text);
                    cmd.Parameters.AddWithValue("@appliance_id", this.id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    DialogResult result = MessageBox.Show("Appliance Updated!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        AdminHomeForm adminHomeForm = new AdminHomeForm();
                        adminHomeForm.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminHomeForm adminHomeForm = new AdminHomeForm();
            adminHomeForm.Show();
            this.Hide();
        }

        private void AdminEditAppliancesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void AdminEditAppliancesForm_Load(object sender, EventArgs e)
        {
            txtName.Text = this.name;
            txtType.Text = this.type;
            txtEnergyConsumption.Text = this.energyConsumption;
            txtWeeklyCost.Text = this.weeklyCost;
        }
    }
}
