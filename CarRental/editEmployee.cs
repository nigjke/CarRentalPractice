using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class editEmployee : Form
    {
        private DataGridViewRow selectedRow;
        public int des1;
        string connect = db.connect;
        private db db;
        public editEmployee(DataGridViewRow row)
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
            comboBox1.SelectedItem = selectedRow.Cells["Роль"].Value.ToString();
            textBox3.Text = selectedRow.Cells["Логин"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Пароль"].Value.ToString();
        }
        public static string GetHashPass(string password)
        {

            byte[] bytesPass = Encoding.UTF8.GetBytes(password);

            SHA256Managed hashstring = new SHA256Managed();

            byte[] hash = hashstring.ComputeHash(bytesPass);

            string hashPasswd = string.Empty;

            foreach (byte x in hash)
            {

                hashPasswd += String.Format("{0:x2}", x);
            }

            hashstring.Dispose();

            return hashPasswd;
        }
        private void UpdateDatabase(object role, string firstName, string lastName, string phone, string employeeLogin, string employeePass)
        {

                DataTable Rooms = new DataTable();
                using (MySqlConnection coon = new MySqlConnection(connect))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = coon;
                    cmd.CommandText = $@"select Role_id from `role` where name = '{role}'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(Rooms);
                }
                for (int i = 0; i < Rooms.Rows.Count; i++)
                {
                    des1 = Convert.ToInt32(Rooms.Rows[i]["Role_id"]);
                }

                using (MySqlConnection connection = new MySqlConnection(db.connect))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"UPDATE employee SET Role_id = {des1}, firstName = @firstName, lastName = @lastName, phone = @phone, employeeLogin = @employeeLogin, employeePass = @employeePass WHERE employeePass = @employeePass", connection);
                    command.Parameters.AddWithValue(Convert.ToString(des1), role);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@employeeLogin", employeeLogin);
                    command.Parameters.AddWithValue("@employeePass", employeePass);
                    command.ExecuteNonQuery();
                }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                UpdateDatabase(comboBox1.SelectedItem, textBox1.Text, textBox2.Text, maskedTextBox1.Text, textBox3.Text, textBox4.Text);
                DialogResult = DialogResult.OK;
                Close();
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectEng(e.KeyChar) && !char.IsControl(e.KeyChar))
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
