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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CarRental
{
    public partial class editCustomer : Form
    {
        private db db;
        string connect = db.connect;
        private DataGridViewRow selectedRow;
        public editCustomer(DataGridViewRow row)
        {
            db = new db();
            selectedRow = row;
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            textBox1.Text = selectedRow.Cells["Имя"].Value.ToString();
            textBox2.Text = selectedRow.Cells["Фамилия"].Value.ToString();
            maskedTextBox1.Text = selectedRow.Cells["Телефон"].Value.ToString();
            maskedTextBox2.Text = selectedRow.Cells["Вод.Удостоверение"].Value.ToString();
            maskedTextBox3.Text = selectedRow.Cells["Паспорт"].Value.ToString();
        }
        private void editCustomer_Load(object sender, EventArgs e)
        {
        }
        private void UpdateDatabase(string firstName, string lastName, string phone, string driverLicense, string passport)
        {

            using (MySqlConnection connection = new MySqlConnection(db.connect))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE customers SET first_name = @firstName, last_name = @lastName, phone = @phone, driver_license = @driverLicense WHERE passport = @passport", connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@driverLicense", driverLicense);
                command.Parameters.AddWithValue("@passport", passport);
                command.ExecuteNonQuery();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && maskedTextBox3.Text != "")
            {
                UpdateDatabase(textBox1.Text, textBox2.Text, maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text);
                DialogResult = DialogResult.OK;
                Close();
                textBox1.Text = "";
                textBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                maskedTextBox3.Text = "";
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectRus(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectRus(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
                textBox2.Text = char.ToUpper(textBox2.Text[0]) + textBox2.Text.Substring(1);
        }
    }
}
