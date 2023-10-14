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
    public partial class CustomerHomeForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Htet_Aung;Integrated Security=True");
        public CustomerHomeForm()
        {
            InitializeComponent();
        }

        private void CustomerHomeForm_Load(object sender, EventArgs e)
        {
            this.displayData();
        }
        private void displayData()
        {
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
                DataGridViewButtonColumn addToCartButton = new DataGridViewButtonColumn();
                addToCartButton.Text = "Add to cart";
                addToCartButton.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(addToCartButton);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Lower to Higher")
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Appliances ORDER BY Energy_Consumption ASC", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();
                dataGridView1.DataSource = dt;

            }
            if (comboBox1.Text == "Higher to Lower")
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Appliances ORDER BY Energy_Consumption DESC", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();
                dataGridView1.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Appliances WHERE Type LIKE @type", conn);
                cmd.Parameters.AddWithValue("@type", "%" + txtSearch.Text + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            decimal totalCost = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                
                if (row.Cells[4].Value != null)
                {
                    decimal weeklyCost;
                    if (decimal.TryParse(row.Cells[4].Value.ToString(), out weeklyCost))
                    {
                        totalCost += weeklyCost;
                    }
                }
            }
            txtTotalPrice.Text = totalCost.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string type = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string energyConsumption = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string weeklyCost = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                // Create a new row for dataGridView2
                DataGridViewRow newRow = new DataGridViewRow();

                // Populate the newRow with the extracted values
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = id });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = name });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = type });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = energyConsumption });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = weeklyCost });

                // Add the newRow to dataGridView2
                dataGridView2.Rows.Add(newRow);
            }
        }
    }
}
