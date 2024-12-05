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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CarRental
{
    public partial class addEmployee : Form
    {
        private db db;
        public int des1;
        string connect = db.connect;
        public addEmployee()
        {
            db = new db();
            InitializeComponent();
            db = new db();
        }
        private void addEmployee_Load(object sender, EventArgs e)
        {
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = "select name from `role`";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox1.Items.Add(Rooms.Rows[i]["name"]);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string password = GetHashPass(textBox4.Text.ToString());
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                string role = comboBox1.Text;
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

                string sqlQuery = $@"Insert Into employee(Role_id,firstName,lastName,phone,employeeLogin,employeePass) Values ('{des1}','{textBox1.Text}','{textBox2.Text}','{maskedTextBox1.Text}','{textBox3.Text}','{password}')";
                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = connect;
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    int res = cmd.ExecuteNonQuery();

                    if (res == 1)
                    {
                        MessageBox.Show("Пользователь добавлен");

                    }
                    else
                    {
                        MessageBox.Show("Пользователь не добавлен");
                    }
                    textBox2.Text = null;
                    comboBox1.Text = null;
                    textBox3.Text = null;
                    textBox1.Text = null;
                    textBox4.Text = null;
                    maskedTextBox1.Text = null;
                    DialogResult = DialogResult.OK;

                }
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
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz_-ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = CreatePassword(15);
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
