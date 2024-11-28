using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    internal class db
    {
        public static string connect = @"server=localhost;port=3306;username=root;password=root;database=carRental";
        MySqlConnection connection = new MySqlConnection(connect); 

        public DataTable MySqlReturnData(string query,DataGridView grid)
        {
            try
            {
                using (MySqlConnection myCon = new MySqlConnection(connect))
                {
                    myCon.Open();
                    if (myCon.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Не удалось установить подключение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(query, myCon))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        grid.DataSource = dt;
                        return dt;

                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show($"Возникла ошибка при выполнении запроса : {e.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Произошла ошибка : {e.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        static public int CheckUserRole(string login, string password)
        {
            int role = -1;
            string hashPassword = string.Empty;
            MySqlConnection con = new MySqlConnection(connect);
            con.Open();

            MySqlCommand cmd = new MySqlCommand($"Select * From employee Where employeeLogin = '{login}'", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            hashPassword = GetHashPass(password);

            try
            {
                if (password == dt.Rows[0].ItemArray.GetValue(6).ToString())
                {
                    if (Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(1)) == 2)
                    {
                        role = 1;
                    }
                    if (Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(1)) == 1)
                    {
                        role = 2;
                    }
                }
            }
            catch (System.Exception) {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return role;
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


        public bool CharCorrectEng(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z');
        }
        public bool CharCorrectRus(char c)
        {
            return (c >= 'а' && c <= 'я') ||
                   (c >= 'А' && c <= 'Я');
        }
        public bool CharCorrectNum(char c)
        {
            return (c >= '0' && c <= '9');
        }

    }
}
