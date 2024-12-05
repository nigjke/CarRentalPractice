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
        private System.Windows.Forms.Timer rentalCheckTimer;
        public addRental()
        {
            db = new db();
            InitializeComponent();
            LoadComboBoxes();
            dateTimePickerReturnDate.MinDate = DateTime.Now.AddDays(1);
            dateTimePickerReturnDate.MaxDate = DateTime.Now.AddYears(2);

            rentalCheckTimer = new System.Windows.Forms.Timer();
            rentalCheckTimer.Interval = 60000;
            rentalCheckTimer.Tick += new EventHandler(CheckRentals);
            rentalCheckTimer.Start();
        }
        string connectionString = db.connect;
        private void LoadComboBoxes()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string queryMake = "SELECT DISTINCT make FROM cars WHERE status = 'Свободна'";
                MySqlDataAdapter adapterMake = new MySqlDataAdapter(queryMake, connection);
                DataTable dataTableMake = new DataTable();
                adapterMake.Fill(dataTableMake);
                comboBoxMake.DataSource = dataTableMake;
                comboBoxMake.DisplayMember = "make";

                string queryCustomers = "SELECT customer_id, last_name, phone FROM customers";
                MySqlDataAdapter adapterCustomers = new MySqlDataAdapter(queryCustomers, connection);
                DataTable dataTableCustomers = new DataTable();
                adapterCustomers.Fill(dataTableCustomers);
                comboBoxCustomer.DataSource = dataTableCustomers;
                comboBoxCustomer.DisplayMember = "last_name";
                comboBoxCustomer.ValueMember = "customer_id";
            }
        }
        private void CheckRentals(object sender, EventArgs e)
        {
            string connectionString = db.connect;
            string query = "UPDATE cars SET status = 'Свободна' WHERE car_id IN (SELECT car_id FROM rentals WHERE return_date < NOW() AND status = 'Занята')";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
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
            string queryModel = "SELECT model FROM cars WHERE make = @make AND status = 'Свободна'";

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

                    string updateStatusQuery = "UPDATE cars SET status = 'Занята' WHERE car_id = @car_id";
                    MySqlCommand updateStatusCommand = new MySqlCommand(updateStatusQuery, connection);
                    updateStatusCommand.Parameters.AddWithValue("@car_id", GetCarId(comboBoxMake.Text, comboBoxModel.Text));
                    updateStatusCommand.ExecuteNonQuery();
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
   

