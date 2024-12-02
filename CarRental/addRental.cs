using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class addRental : Form
    {
        private db db;
        public addRental()
        {
            db = new db();
            InitializeComponent();
            LoadComboBoxes();
        }
        string connectionString = db.connect;
        private void LoadComboBoxes()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string queryMake = "SELECT DISTINCT make FROM cars";
                MySqlDataAdapter adapterMake = new MySqlDataAdapter(queryMake, connection);
                DataTable dataTableMake = new DataTable();
                adapterMake.Fill(dataTableMake);
                comboBoxMake.DataSource = dataTableMake;
                comboBoxMake.DisplayMember = "make";

                string queryCustomers = "SELECT customer_id, last_name,phone FROM customers";
                MySqlDataAdapter adapterCustomers = new MySqlDataAdapter(queryCustomers, connection);
                DataTable dataTableCustomers = new DataTable();
                adapterCustomers.Fill(dataTableCustomers);
                comboBoxCustomer.DataSource = dataTableCustomers;
                comboBoxCustomer.DisplayMember = "last_name";
                comboBoxCustomer.ValueMember = "customer_id";

            }
        }
        private void CalculateTotalAmount()
        {
            if (comboBoxModel.SelectedItem == null || dateTimePickerRentalDate.Value == null || dateTimePickerReturnDate.Value == null)
            {
                return;
            }
            DateTime rentalDate = dateTimePickerRentalDate.Value;
            DateTime returnDate = dateTimePickerReturnDate.Value;
            int days = (returnDate - rentalDate).Days;

            if (days < 1)
            {
                MessageBox.Show("Количество суток не может быть меньше 1 дня.");
                return;
            }

            string selectedModel = comboBoxModel.Text;
            string connectionString = db.connect;
            string queryPrice = "SELECT price FROM cars WHERE model = @model";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryPrice, connection);
                command.Parameters.AddWithValue("@model", selectedModel);
                connection.Open();
                object result = command.ExecuteScalar();
                decimal pricePerDay;

                if (result != null && result != DBNull.Value)
                {
                    pricePerDay = Convert.ToDecimal(result);
                }
                else
                {
                    pricePerDay = 0; 
                }
                decimal totalAmount = pricePerDay * days;
                textBoxTotalAmount.Text = totalAmount.ToString("C");
            }
        }
        private int GetCarId(string make, string model)
        {
            string connectionString = db.connect;
            string query = "SELECT car_id FROM cars WHERE make = @make AND model = @model";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@make", make);
                command.Parameters.AddWithValue("@model", model);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
        private void comboBoxMake_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedMake = comboBoxMake.Text;
            string queryModel = "SELECT model FROM cars WHERE make = @make";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryModel, connection);
                command.Parameters.AddWithValue("@make", selectedMake);
                MySqlDataAdapter adapterModel = new MySqlDataAdapter(command);
                DataTable dataTableModel = new DataTable();
                adapterModel.Fill(dataTableModel);
                comboBoxModel.DataSource = dataTableModel;
                comboBoxModel.DisplayMember = "model";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxModel.Text != "" && comboBoxCustomer.Text != "" && comboBoxMake.Text != "" && dateTimePickerRentalDate.Text != "" && dateTimePickerReturnDate.Text != "" && textBoxTotalAmount.Text != "")
            {
                string connectionString = db.connect;
                string query = "INSERT INTO rentals (customer_id, car_id, rental_date, return_date, total_amount) VALUES (@customer_id, @car_id, @rental_date, @return_date, @total_amount)";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@customer_id", comboBoxCustomer.SelectedValue);
                    command.Parameters.AddWithValue("@car_id", GetCarId(comboBoxMake.Text, comboBoxModel.Text));
                    command.Parameters.AddWithValue("@rental_date", dateTimePickerRentalDate.Value);
                    command.Parameters.AddWithValue("@return_date", dateTimePickerReturnDate.Value);
                    command.Parameters.AddWithValue("@total_amount", decimal.Parse(textBoxTotalAmount.Text, NumberStyles.Currency));

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePickerReturnDate_ValueChanged_1(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private void addRental_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCustomer.SelectedIndex != -1)
            {
                DataRowView selectedRow = comboBoxCustomer.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    textPhone.Text = selectedRow["phone"].ToString();
                }
            }
        }
    }
}
   

