using MySql.Data.MySqlClient;
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

        private void LoadComboBoxes()
        {
            MySqlConnection connection = new MySqlConnection(db.connect);
            connection.Open();
            MySqlDataAdapter customersAdapter = new MySqlDataAdapter("SELECT customer_id, phone FROM customers", connection);
            DataTable customersTable = new DataTable();
            customersAdapter.Fill(customersTable);
            comboBoxCustomers.DataSource = customersTable;
            comboBoxCustomers.DisplayMember = "phone";
            comboBoxCustomers.ValueMember = "customer_id";

            // Load cars
            MySqlDataAdapter carsAdapter = new MySqlDataAdapter("SELECT car_id, make, price FROM cars WHERE status='available'", connection);
            DataTable carsTable = new DataTable();
            carsAdapter.Fill(carsTable);
            comboBoxCars.DataSource = carsTable;
            comboBoxCars.DisplayMember = "make";
            comboBoxCars.ValueMember = "car_id";

            // Load employees
            MySqlDataAdapter employeesAdapter = new MySqlDataAdapter("SELECT employee_id, employeeLogin FROM employee WHERE Role_id=2", connection);
            DataTable employeesTable = new DataTable();
            employeesAdapter.Fill(employeesTable);
            comboBoxEmployees.DataSource = employeesTable;
            comboBoxEmployees.DisplayMember = "employeeLogin";
            comboBoxEmployees.ValueMember = "employee_id";
        }

        private void dateTimePickerReturnDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private void dateTimePickerRentalDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private void comboBoxCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            if (comboBoxCars.SelectedValue != null && dateTimePickerRentalDate.Value != null && dateTimePickerReturnDate.Value != null)
            {
                DataRowView selectedCar = (DataRowView)comboBoxCars.SelectedItem;
                decimal pricePerDay = Convert.ToDecimal(selectedCar["price"]);
                TimeSpan rentalDuration = dateTimePickerReturnDate.Value - dateTimePickerRentalDate.Value;
                int rentalDays = rentalDuration.Days;
                if (rentalDays < 1) rentalDays = 1; // Minimum 1 day rental
                decimal totalAmount = rentalDays * pricePerDay;
                textBoxTotalAmount.Text = totalAmount.ToString("F2");
            }
        }
        private void buttonAddRental_Click_1(object sender, EventArgs e)
        {
            if (comboBoxCustomers.SelectedValue != null && comboBoxCars.SelectedValue != null && comboBoxEmployees.SelectedValue != null)
            {
                int customerId = (int)comboBoxCustomers.SelectedValue;
                int carId = (int)comboBoxCars.SelectedValue;
                int employeeId = (int)comboBoxEmployees.SelectedValue;
                DateTime rentalDate = dateTimePickerRentalDate.Value;
                DateTime returnDate = dateTimePickerReturnDate.Value;
                decimal totalAmount = Convert.ToDecimal(textBoxTotalAmount.Text);

                string query = "INSERT INTO rentals (customer_id, car_id, employee_id, rental_date, return_date, total_amount) " +
                               "VALUES (@customer_id, @car_id, @employee_id, @rental_date, @return_date, @total_amount)";

                MySqlConnection connection = new MySqlConnection(db.connect);
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customer_id", customerId);
                    command.Parameters.AddWithValue("@car_id", carId);
                    command.Parameters.AddWithValue("@employee_id", employeeId);
                    command.Parameters.AddWithValue("@rental_date", rentalDate);
                    command.Parameters.AddWithValue("@return_date", returnDate);
                    command.Parameters.AddWithValue("@total_amount", totalAmount);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Rental added successfully!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

//        SELECT rental_id, rental_date, return_date, first_name, last_name, make, model FROM carrental.rentals

//inner join customers on rentals.customer_id = customers.customer_id

//inner join cars on cars.car_id = rentals.car_id

//where rental_id = 1;
    }
}

