using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void sendBtn_Click(object sender, EventArgs e)
        {
            String loginUser = loginField.Text;
            String pwdUser = pwdField.Text;
            if (loginUser == "" || pwdUser == "") { 
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loginField.Text = "Login";
                pwdField.Text = "Password";
                pwdField.PasswordChar = default;
                pictureBox3.BackgroundImage = Properties.Resources.padlock;
                panel1.BackColor = Color.White;
                pwdField.ForeColor = Color.White;
            }
            else
            {
                int role = db.CheckUserRole(loginUser, pwdUser);
                if (role == 2)
                {
                    adminForm a = new adminForm(loginUser);
                    a.Show();
                    this.Hide();
                }
                else if (role == 1)
                {
                    managerForm a = new managerForm(loginUser);
                    a.Show();
                    this.Hide();
                }
                else
                {
                    loginField.Text = "Login";
                    pwdField.Text = "Password";
                    pwdField.PasswordChar = default;
                    pictureBox3.BackgroundImage = Properties.Resources.padlock;
                    panel1.BackColor = Color.White;
                    pwdField.ForeColor = Color.White;
                }
            }

        }
        private void loginField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private bool CharCorrectLogin(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z');
        }

        private void pwdField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void loginField_Click(object sender, EventArgs e)
        {
            loginField.Text = "";
            pictureBox2.BackgroundImage = Properties.Resources.avatar1;
            panel.BackColor = Color.FromArgb(92, 96, 255);
            loginField.ForeColor = Color.FromArgb(92, 96, 255);

            pictureBox3.BackgroundImage = Properties.Resources.padlock;
            panel1.BackColor = Color.White;
            pwdField.ForeColor = Color.White;
        }

        private void pwdField_Click(object sender, EventArgs e)
        {
            pwdField.Text = "";
            pwdField.PasswordChar = '*';
            pictureBox2.BackgroundImage = Properties.Resources.avatar;
            panel.BackColor = Color.White;
            loginField.ForeColor = Color.White;

            pictureBox3.BackgroundImage = Properties.Resources.padlock1;
            panel1.BackColor = Color.FromArgb(92, 96, 255);
            pwdField.ForeColor = Color.FromArgb(92, 96, 255);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.show;
            pwdField.PasswordChar = default;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.hide;
            pwdField.PasswordChar = '*';
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            pwdField.PasswordChar = default;
        }
    }
}
