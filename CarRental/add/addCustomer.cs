using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class addCustomer : Form
    {
        private db db;
        string connect = db.connect;
        public addCustomer()
        {
            db = new db();
            InitializeComponent();
        }
        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectRus(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!db.CharCorrectRus(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && maskedTextBox3.Text != "")
            {
                MySqlConnection con = new MySqlConnection(connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand($@"Insert Into customers(first_name,last_name,phone,driver_license,passport)Values ('{textBox1.Text}','{textBox2.Text}','{maskedTextBox1.Text}','{maskedTextBox2.Text}','{maskedTextBox3.Text}')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Клиент добавлен");
                textBox1.Text = "";
                textBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                maskedTextBox3.Text = "";
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
                textBox2.Text = char.ToUpper(textBox2.Text[0]) + textBox2.Text.Substring(1);
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
    }
}
