using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class editCar : Form
    {
        private db db;
        string connect = db.connect;
        private DataGridViewRow selectedRow;
        public editCar(DataGridViewRow row)
        {
            db = new db();
            selectedRow = row;
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            textBox1.Text = selectedRow.Cells["Марка"].Value.ToString();
            textBox2.Text = selectedRow.Cells["Модель"].Value.ToString();
            textBox3.Text = selectedRow.Cells["Год выпуска"].Value.ToString();
            maskedTextBox1.Text = selectedRow.Cells["Гос.Номер"].Value.ToString();
            comboBox1.SelectedItem = selectedRow.Cells["Статус"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Цена за сутки"].Value.ToString();
        }
        private void UpdateDatabase(string make, string model, string year, string license_plate, object status, string price)
        {

            using (MySqlConnection connection = new MySqlConnection(db.connect))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE cars SET make = @make, model = @model, year = @year, license_plate = @license_plate, status = @status, price = @price WHERE license_plate = @license_plate", connection);
                command.Parameters.AddWithValue("@make", make);
                command.Parameters.AddWithValue("@model", model);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@license_plate", license_plate);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@price", price);
                command.ExecuteNonQuery();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && maskedTextBox1.Text != "" && textBox4.Text != "")
            {
                UpdateDatabase(textBox1.Text, textBox2.Text, textBox3.Text, maskedTextBox1.Text, comboBox1.SelectedItem, textBox4.Text);
                DialogResult = DialogResult.OK;
                Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                maskedTextBox1.Text = "";
                textBox4.Text = "";
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
            if (!db.CharCorrectEng(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectEng(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectNum(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectNum(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
