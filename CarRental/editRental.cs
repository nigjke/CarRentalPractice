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
    public partial class editRental : Form
    {
        private db db;
        private DataRow dataRow;
        string connect = db.connect;
        private TextBox txtMake;
        private TextBox txtModel;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPhone;
        private DateTimePicker dtpRentalDate;
        private DateTimePicker dtpReturnDate;
        private TextBox txtTotalAmount;
        private Button btnUpdate;
        string connectionString = db.connect;
        public editRental(DataRow row)
        {
            db = new db();
            this.dataRow = row;
            InitializeComponent();
            LoadComboBoxes();
            dateTimePickerRentalDate.MinDate = DateTime.Now;
            dateTimePickerRentalDate.MaxDate = DateTime.Now.AddYears(1);
            dateTimePickerReturnDate.MinDate = DateTime.Now.AddDays(1);
            dateTimePickerReturnDate.MaxDate = DateTime.Now.AddYears(2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxModel.Text != "" && comboBoxCustomer.Text != "" && comboBoxMake.Text != "" && dateTimePickerRentalDate.Text != "" && dateTimePickerReturnDate.Text != "" && textBoxTotalAmount.Text != "")
            {
                string connectionString = db.connect;
                string query = "UPDATE rentals SET customer_id = @customer_id, car_id = (SELECT car_id FROM cars WHERE make = @make AND model = @model), rental_date = @rental_date, return_date = @return_date, total_amount = @total_amount WHERE car_id = @old_car_id AND customer_id = @old_customer_id AND rental_date = @old_rental_date AND return_date = @old_return_date";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@make", comboBoxMake.Text);
                    command.Parameters.AddWithValue("@model", dataRow["Модель"].ToString());
                    command.Parameters.AddWithValue("@customer_id", comboBoxCustomer.SelectedValue);
                    command.Parameters.AddWithValue("@rental_date", dateTimePickerRentalDate.Value);
                    command.Parameters.AddWithValue("@return_date", dateTimePickerReturnDate.Value);
                    command.Parameters.AddWithValue("@total_amount", decimal.Parse(textBoxTotalAmount.Text, NumberStyles.Currency));

                    // Old values for the WHERE clause
                    command.Parameters.AddWithValue("@old_car_id", dataRow["car_id"]);
                    command.Parameters.AddWithValue("@old_customer_id", dataRow["customer_id"]);
                    command.Parameters.AddWithValue("@old_rental_date", Convert.ToDateTime(dataRow["Дата_взятия"]));
                    command.Parameters.AddWithValue("@old_return_date", Convert.ToDateTime(dataRow["Дата_возврата"]));


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
        private void CalculateTotalAmount()
        {
            if (comboBoxModel.SelectedItem == null || dateTimePickerRentalDate.Value == null || dateTimePickerReturnDate.Value == null)
            {
                return;
            }
            DateTime rentalDate = dateTimePickerRentalDate.Value;
            DateTime returnDate = dateTimePickerReturnDate.Value;
            int days = (returnDate - rentalDate).Days;
            button1.Enabled = true;
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
            comboBoxMake.Text = dataRow["Марка"].ToString();
            comboBoxModel.Text = dataRow["Модель"].ToString();
            comboBoxCustomer.Text = dataRow["Фамилия"].ToString();
            textPhone.Text = dataRow["Телефон"].ToString();
            dateTimePickerRentalDate.Value = Convert.ToDateTime(dataRow["Дата взятия"]);
            dateTimePickerReturnDate.Value = Convert.ToDateTime(dataRow["Дата возврата"]);
            textBoxTotalAmount.Text = dataRow["Сумма"].ToString();

        }
        private void comboBoxMake_SelectedIndexChanged(object sender, EventArgs e)
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
            textBoxTotalAmount.Text = "0";
        }

        private void dateTimePickerReturnDate_ValueChanged(object sender, EventArgs e)
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

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }
    }
}
