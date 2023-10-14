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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace RentalApplication
{
    public partial class AdminHomeForm : Form
    {
        public AdminHomeForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AdminAddAppliancesForm adminAddAppliancesForm = new AdminAddAppliancesForm();
            adminAddAppliancesForm.Show();
            this.Hide();
        }

        private void AdminHomeForm_Load(object sender, EventArgs e)
        {
            this.displayData();
        }
        private void displayData()
        {
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Htet_Aung;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Appliances", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;

                // Add Edit Button Column
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Text = "Edit";
                editButtonColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(editButtonColumn);

                // Add Delete Button Column
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.Text = "Delete";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(deleteButtonColumn);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                this.Delete(id);
            }
            if(e.ColumnIndex == 5)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string type = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string eneryConsumption = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string weeklyCost = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                AdminEditAppliancesForm adminEditAppliancesForm = new AdminEditAppliancesForm(id,name,type,eneryConsumption,weeklyCost);
                adminEditAppliancesForm.Show();
                this.Hide();
            }
        }
        private void Delete(int id)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(result == DialogResult.OK)
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Htet_Aung;Integrated Security=True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Appliances WHERE Appliance_ID=@appliance_id", conn);
                    cmd.Parameters.AddWithValue("@appliance_id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Appliance deleted!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    this.displayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AdminHomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
