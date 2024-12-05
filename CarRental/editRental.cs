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
        private DataGridViewRow selectedRow;
        public editRental(DataGridViewRow row)
        {
            db = new db();
            selectedRow = row;
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            comboBoxMake.Text = selectedRow.Cells["Марка"].ToString();
            comboBoxModel.Text = selectedRow.Cells["Модель"].ToString();
            comboBoxCustomer.Text = selectedRow.Cells["Фамилия"].ToString();
            textPhone.Text = selectedRow.Cells["Телефон"].ToString();
            dateTimePickerRentalDate.Value = Convert.ToDateTime(selectedRow.Cells["Дата взятия"]);
            dateTimePickerReturnDate.Value = Convert.ToDateTime(selectedRow.Cells["Дата возврата"]);
            textBoxTotalAmount.Text = selectedRow.Cells["Сумма"].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE rentals SET customer_id = (SELECT customer_id FROM customers WHERE first_name = @first_name AND last_name = @last_name AND phone = @phone), car_id = (SELECT car_id FROM cars WHERE make = @make AND model = @model), rental_date = @rental_date, return_date = @return_date, total_amount = @total_amount WHERE rental_id = @rental_id";

            using (MySqlConnection connection = new MySqlConnection(connect))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@make", txtMake.Text);
                command.Parameters.AddWithValue("@model", txtModel.Text);
                command.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                command.Parameters.AddWithValue("@last_name", txtLastName.Text);
                command.Parameters.AddWithValue("@phone", txtPhone.Text);
                command.Parameters.AddWithValue("@rental_date", dtpRentalDate.Value);
                command.Parameters.AddWithValue("@return_date", dtpReturnDate.Value);
                command.Parameters.AddWithValue("@total_amount", txtTotalAmount.Text);
                command.Parameters.AddWithValue("@rental_id", selectedRow.Cells["rental_id"]);

                connection.Open();
                command.ExecuteNonQuery();

                connection.Open();
                command.ExecuteNonQuery();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
