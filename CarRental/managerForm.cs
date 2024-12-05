using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace CarRental
{
    public partial class managerForm : Form
    {
        private db db;
        private static string table = string.Empty;
        private DataGridViewRow selectedRow;
        public managerForm(string labelLog)
        {
            db = new db();
            InitializeComponent();
            this.label1.Text = $"{labelLog}";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button1.BackColor = Color.FromArgb(92, 96, 255);
            button1.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = false;
            button2.BackColor = Color.FromArgb(92, 96, 255);
            button2.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
            button1.BackColor = Color.FromArgb(34, 36, 49);
            button1.ForeColor = Color.FromArgb(92, 96, 255);
            label2.Text = "Клиенты";
            textBox1.Text = "Поиск";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По Имени");
            comboBox1.Items.Add("По Фамилии");
            comboBox1.Items.Add("По Телефону");
            comboBox1.Items.Add("По Вод.Удостоверению");
            comboBox1.Items.Add("По Паспорту");
            string query = "SELECT first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', driver_license as 'Вод.Удостоверение', passport as 'Паспорт' FROM customers";
            table = "customers";
            db.MySqlReturnData(query, dataGridView1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
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
                }else if (table == "rentals")
                {
                    query = "Select make as 'Марка', model as 'Модель', first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', rental_date as 'Дата взятия', return_date as 'Дата возврата', total_amount as 'Сумма' FROM carrental.rentals inner join customers on rentals.customer_id = customers.customer_id inner join cars on cars.car_id = rentals.car_id; ";
                }
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button4.BackColor = Color.FromArgb(92, 96, 255);
            button4.ForeColor = Color.FromArgb(34, 36, 49);
            button2.BackColor = Color.FromArgb(34, 36, 49);
            button2.ForeColor = Color.FromArgb(92, 96, 255);
            button1.BackColor = Color.FromArgb(34, 36, 49);
            button1.ForeColor = Color.FromArgb(92, 96, 255);
            textBox1.Text = "Поиск";
            label2.Text = "Аренды";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("По имени");
            comboBox1.Items.Add("По фамилии");
            comboBox1.Items.Add("По телефону");
            comboBox1.Items.Add("По марке");
            comboBox1.Items.Add("По моделе"); ;
            comboBox1.Items.Add("По дате взятия");
            comboBox1.Items.Add("По дате возврата");
            comboBox1.Items.Add("По сумме");
            string query = "Select make as 'Марка', model as 'Модель', first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', rental_date as 'Дата взятия', return_date as 'Дата возврата', total_amount as 'Сумма' FROM carrental.rentals inner join customers on rentals.customer_id = customers.customer_id inner join cars on cars.car_id = rentals.car_id; ";
            table = "rentals";
            db.MySqlReturnData(query, dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            string query = "";
            if (table == "cars") { query = $"SELECT make as 'Марка', model as 'Модель', year as 'Год выпуска', license_plate as 'Гос.Номер', status as 'Статус', price 'Цена за сутки' FROM cars WHERE make LIKE '%{txt}%' OR model LIKE '%{txt}%' OR license_plate LIKE '%{txt}%' OR status LIKE '%{txt}%' OR year LIKE '%{txt}%' OR price LIKE '%{txt}%'"; }
            else if (table == "customers") { query = $"SELECT first_name as 'Имя', last_name as 'Фамилия', phone as 'Телефон', driver_license as 'Вод.Удостоверение', passport as 'Паспорт' FROM customers WHERE first_name LIKE '%{txt}%' OR last_name LIKE '%{txt}%' OR phone LIKE '%{txt}%' OR driver_license LIKE '%{txt}%' OR passport LIKE '%{txt}%'"; }
            else if (table == "rentals") { query = $"SELECT customers.passport as 'Клиент', cars.license_plate as 'Машина', rentals.rental_date as 'Дата взятия', employee.employeeLogin as 'Менеджер',rentals.return_date as 'Дата возвращения', rentals.total_amount as 'Сумма' FROM rentals JOIN customers ON rentals.customer_id = customers.customer_id JOIN cars ON rentals.car_id = cars.car_id JOIN employee ON rentals.employee_id = employee.employee_id;"; }
            db.MySqlReturnData(query, dataGridView1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (table == "customers")
            {
                addCustomer addCustomer = new addCustomer();
                addCustomer.ShowDialog();
            }
            else if (table == "rentals")
            {
                addRental addRental = new addRental(); 
                addRental.ShowDialog();
            }
            LoadData();
        }

        private void button8_Click(object sender, EventArgs e)
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
                else
                    {
                    DataRow selectedRows = ((DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row;
                    editRental editRental = new editRental(selectedRows);
                    if (editRental.ShowDialog() == DialogResult.OK)
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

        private void backBtn_Click(object sender, EventArgs e)
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void managerForm_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(92, 96, 255);
            button1.ForeColor = Color.FromArgb(34, 36, 49);
            button4.BackColor = Color.FromArgb(34, 36, 49);
            button4.ForeColor = Color.FromArgb(92, 96, 255);
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
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
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
            else if (table == "rentals")
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
                    dataGridView1.Sort(dataGridView1.Columns["Имя"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Фамилия"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Телефон"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Дата взятия"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Дата возврата"], System.ComponentModel.ListSortDirection.Ascending);
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Сумма"], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
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
            else if (table == "rentals")
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
                    dataGridView1.Sort(dataGridView1.Columns["Имя"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Фамилия"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Телефон"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Дата взятия"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 6)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Дата возврата"], System.ComponentModel.ListSortDirection.Descending);
                }
                if (comboBox1.SelectedIndex == 7)
                {
                    dataGridView1.Sort(dataGridView1.Columns["Сумма"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (selectedRow != null)
            {
                CreateWordReport(selectedRow);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку.");
            }
        }
        private void CreateWordReport(DataGridViewRow row)
        {
            string templatePath = Directory.GetCurrentDirectory() + @"\template\template.docx";
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Add(templatePath);

            try
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string bookmarkName = cell.OwningColumn.HeaderText.Replace(" ", "_");
                    if (doc.Bookmarks.Exists(bookmarkName))
                    {
                        doc.Bookmarks[bookmarkName].Range.Text = cell.Value.ToString();
                    }
                }

                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                doc = null;
                wordApp = null;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = dataGridView1.Rows[e.RowIndex];
            }
        }
    }
}
