using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CarRental
{
    public partial class adminForm : Form
    {
        private db db;
        private static string table = string.Empty;
        public adminForm(string labelLog)
        {
            db = new db();
            InitializeComponent();
            this.label1.Text = $"{labelLog}";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            loginForm loginForm = new loginForm();
            loginForm.Show();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void adminForm_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(92, 96, 255);
            button1.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
            button3.BackColor = Color.FromArgb(34, 36, 49);
            button3.ForeColor = Color.FromArgb(92, 96, 255);
            button2.BackColor = Color.FromArgb(34, 36, 49);
            button2.ForeColor = Color.FromArgb(92, 96, 255);
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По Марке");
            comboBox1.Items.Add("По Модели");
            comboBox1.Items.Add("По Году выпуска");
            comboBox1.Items.Add("По Гос.Номеру");
            comboBox1.Items.Add("По Статусу");
            comboBox1.Items.Add("По Цене");
            string query = "SELECT make as 'Марка', model as 'Модель', year as 'Год выпуска', license_plate as 'Гос.Номер', status as 'Статус', price 'Цена за сутки' FROM cars";
            table = "cars";
            db.MySqlReturnData(query, dataGridView1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(92, 96, 255);
            button2.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
            button3.BackColor = Color.FromArgb(34, 36, 49);
            button3.ForeColor = Color.FromArgb(92, 96, 255);
            button1.BackColor = Color.FromArgb(34, 36, 49);
            button1.ForeColor = Color.FromArgb(92, 96, 255);
            label2.Text = "Клиенты";
            textBox1.Text = "Поиск";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По Имени");
            comboBox1.Items.Add("По Фамилии");
            comboBox1.Items.Add("По Почте");
            comboBox1.Items.Add("По Телефону");
            comboBox1.Items.Add("По Вод.Удостоверению");
            string query = "SELECT first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', driver_license as 'Вод.Удостоверение', passport as 'Паспорт' FROM customers";
            table = "customers";
            db.MySqlReturnData(query, dataGridView1);
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(92, 96, 255);
            button1.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
            button3.BackColor = Color.FromArgb(34, 36, 49);
            button3.ForeColor = Color.FromArgb(92, 96, 255);
            button2.BackColor = Color.FromArgb(34, 36, 49);
            button2.ForeColor = Color.FromArgb(92, 96, 255);
            label2.Text = "Машины";
            textBox1.Text = "Поиск";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По Марке");
            comboBox1.Items.Add("По Модели");
            comboBox1.Items.Add("По Году выпуска");
            comboBox1.Items.Add("По Гос.Номеру");
            comboBox1.Items.Add("По Статусу");
            comboBox1.Items.Add("По Цене");
            string query = "SELECT make as 'Марка', model as 'Модель', year as 'Год выпуска', license_plate as 'Гос.Номер', status as 'Статус' , price 'Цена за сутки' FROM cars";
            table = "cars";
            db.MySqlReturnData(query, dataGridView1);
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(92, 96, 255);
            button3.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
            button2.BackColor = Color.FromArgb(34, 36, 49);
            button2.ForeColor = Color.FromArgb(92, 96, 255);
            button1.BackColor = Color.FromArgb(34, 36, 49);
            button1.ForeColor = Color.FromArgb(92, 96, 255);
            textBox1.Text = "Поиск";
            label2.Text = "Сотрудники";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По Имени");
            comboBox1.Items.Add("По Фамилии");
            comboBox1.Items.Add("По Телефону");
            comboBox1.Items.Add("По Логину");
            comboBox1.Items.Add("По Паролю");
            comboBox1.Items.Add("По Роле");
            string query = "SELECT employee.firstName as 'Имя', employee.lastName as 'Фамилия', employee.phone as 'Телефон', role.name as 'Роль', employee.employeeLogin as 'Логин', employee.employeePass as 'Пароль' FROM employee JOIN role ON employee.Role_id=role.Role_id";
            table = "employee";
            db.MySqlReturnData(query, dataGridView1);
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(92, 96, 255);
            button4.ForeColor = Color.FromArgb(34, 36, 49);
            button3.BackColor = Color.FromArgb(34, 36, 49);
            button3.ForeColor = Color.FromArgb(92, 96, 255);
            button2.BackColor = Color.FromArgb(34, 36, 49);
            button2.ForeColor = Color.FromArgb(92, 96, 255);
            button1.BackColor = Color.FromArgb(34, 36, 49);
            button1.ForeColor = Color.FromArgb(92, 96, 255);
            textBox1.Text = "Поиск";
            label2.Text = "Аренды";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По клиенту");
            comboBox1.Items.Add("По машине");
            comboBox1.Items.Add("По дате взятия");
            comboBox1.Items.Add("По менеджеру");
            comboBox1.Items.Add("По дате возврата");
            comboBox1.Items.Add("По сумме");
            string query = "Select make as 'Марка', model as 'Модель', first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', rental_date as 'Дата взятия', return_date as 'Дата возврата', total_amount as 'Сумма' FROM carrental.rentals inner join customers on rentals.customer_id = customers.customer_id inner join cars on cars.car_id = rentals.car_id; ";
            table = "rentals";
            db.MySqlReturnData(query, dataGridView1);
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (table == "cars")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Марка"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Модель"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Год выпуска"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Гос.Номер"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Статус"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Цена"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
            else if (table == "customers")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Имя"], System.ComponentModel.ListSortDirection.Descending);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Фамилия"], System.ComponentModel.ListSortDirection.Descending);
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Телефон"], System.ComponentModel.ListSortDirection.Descending);
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Вод.Удостоверение"], System.ComponentModel.ListSortDirection.Descending);
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Паспорт"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
            else if (table == "employee")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Имя"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Фамилия"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Телефон"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Логин"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Пароль"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
            else if (table == "rentals")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Логин"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Роль"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (table == "cars")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Марка"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Модель"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Год выпуска"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Гос.Номер"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Статус"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Цена"], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            else if (table == "customers")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Имя"], System.ComponentModel.ListSortDirection.Ascending);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Фамилия"], System.ComponentModel.ListSortDirection.Ascending);
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Телефон"], System.ComponentModel.ListSortDirection.Ascending);
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Вод.Удостоверение"], System.ComponentModel.ListSortDirection.Ascending);
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Паспорт"], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            else if (table == "employee")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Имя"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Фамилия"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Телефон"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Логин"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Пароль"], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            else if (table == "rentals")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Логин"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Роль"], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (table == "customers")
            {
                addCustomer addCustomer = new addCustomer();
                addCustomer.ShowDialog();
            }
            else if (table == "cars")
            {
                addCar addCar = new addCar();
                addCar.ShowDialog();
            }
            else if (table == "employee")
            {
                addEmployee addEmployee = new addEmployee();
                addEmployee.ShowDialog();
            }
            else if (table == "rentals")
            {
                MessageBox.Show("Нет прав доустпа!");
            }
            LoadData();
        }
        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(db.connect))
            {
                connection.Open();
                string query = "";
                if (table == "customers")
                {
                    query = "SELECT first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', driver_license as 'Вод.Удостоверение', passport as 'Паспорт' FROM customers";
                }
                else if (table == "cars")
                {
                    query = "SELECT make as 'Марка', model as 'Модель', year as 'Год выпуска', license_plate as 'Гос.Номер', status as 'Статус' , price 'Цена за сутки' FROM cars";
                }
                else
                {
                    query = "SELECT employee.firstName as 'Имя', employee.lastName as 'Фамилия', employee.phone as 'Телефон', role.name as 'Роль', employee.employeeLogin as 'Логин', employee.employeePass as 'Пароль' FROM employee JOIN role ON employee.Role_id=role.Role_id";
                }
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                bool isEmptyRow = true;
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        isEmptyRow = false;
                        break;
                    }
                }

                if (isEmptyRow)
                {
                    MessageBox.Show("Выбранная строка пуста и не может быть отредактирована.");
                    return;
                }
                if (table == "customers")
                {
                    editCustomer editCustomer = new editCustomer(selectedRow);
                    if (editCustomer.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
                else if (table == "cars")
                {
                    editCar editCar = new editCar(selectedRow);
                    if (editCar.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
                else if (table == "employee")
                {
                    editEmployee editEmployee = new editEmployee(selectedRow);
                    if (editEmployee.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования");
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            string query = "";
            if (table == "cars") { query = $"SELECT make as 'Марка', model as 'Модель', year as 'Год выпуска', license_plate as 'Гос.Номер', status as 'Статус', price 'Цена за сутки' FROM cars WHERE make LIKE '%{txt}%' OR model LIKE '%{txt}%' OR license_plate LIKE '%{txt}%' OR status LIKE '%{txt}%' OR year LIKE '%{txt}%' OR price LIKE '%{txt}%'"; }
            else if (table == "customers") { query = $"SELECT first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', driver_license as 'Вод.Удостоверение', passport as 'Паспорт' FROM customers WHERE first_name LIKE '%{txt}%' OR last_name LIKE '%{txt}%' OR phone LIKE '%{txt}%' OR driver_license LIKE '%{txt}%' OR passport LIKE '%{txt}%'"; }
            else if (table == "employee") { query = $"SELECT firstName as 'Имя', lastName as 'Фамилия', phone as 'Телефон', Role_id as 'Роль', employeeLogin as 'Логин', employeePass as 'Пароль' FROM employee WHERE firstName LIKE '%{txt}%' OR lastName LIKE '%{txt}%' OR phone LIKE '%{txt}%' OR employeeLogin LIKE '%{txt}%' OR employeePass LIKE '%{txt}%' OR Role_id LIKE '%{txt}%'"; }
            else if (table == "rentals") { query = $"SELECT customers.passport as 'Клиент', cars.license_plate as 'Машина', rentals.rental_date as 'Дата взятия', employee.employeeLogin as 'Менеджер',rentals.return_date as 'Дата возвращения', rentals.total_amount as 'Сумма' FROM rentals JOIN customers ON rentals.customer_id = customers.customer_id JOIN cars ON rentals.car_id = cars.car_id JOIN employee ON rentals.employee_id = employee.employee_id;"; }
            db.MySqlReturnData(query, dataGridView1);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void DeleteRowFromDatabase(DataGridViewRow row)
        {
            int id = Convert.ToInt32(row.Cells["Логин"].Value);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                bool isEmpty = true;
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        isEmpty = false;
                        break;
                    }
                }
                if (isEmpty)
                {
                    MessageBox.Show("Выбранная строка пуста и не может быть удалена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (table == "cars")
                {
                    if (IsDataUsedInRentals(selectedRow))
                    {
                        MessageBox.Show("Данные из выбранной строки используются в других таблицах и не могут быть удалены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                var result = MessageBox.Show("Вы уверены, что хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(selectedRow);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsDataUsedInRentals(DataGridViewRow row)
        {
            string carId = row.Cells["Модель"].Value?.ToString();
            if (carId != null)
            {
                using (MySqlConnection conn = new MySqlConnection(db.connect))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM carrental.rentals INNER JOIN cars ON cars.car_id = rentals.car_id WHERE cars.model = @carId;";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@carId", carId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при проверке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                }
            }
            return false;
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            sysAdminForm sysAdminForm = new sysAdminForm();
            sysAdminForm.ShowDialog();
        }
    }
}
