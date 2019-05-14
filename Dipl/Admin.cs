using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dipl
{
    public partial class Admin : Form
    {
        Employees employees;
        Cars cars;
        string command = "",
            commandEmpl = "SELECT id as[ИД], surname as [Фамилия], firstname as [Имя], lastname as [Отчество], role as [Должность], login as [Логин], password as [Пароль] FROM employees ",
            commandPost = "SELECT distinct p.id as [ИД], idEng as [ИД Сотрудника],surname as [Фамилия], firstname as[Имя], idCar as [ИД Авто],model as[Модель авто], count as [Поставлено], dates as [Дата] FROM post p, employees e, cars c WHERE (p.idEng=e.id AND p.idCar=c.id)  ",
            commandMoney = "SELECT d.id as [ИД], d.idContract as [ИД Контракта], d.idEmpl as [ИД Сотрудника],e.surname as [Фамилия Сотр], e.firstname as[Имя Сотр],  d.paid as [Получено],c.firstname as [Имя клиента],c.lastname as[Отчество клиента],passport as[Пасспорт клиента], datePaid as [Дата]  FROM datePaid d, employees e,clients c, contracts cont WHERE (e.id = d.idEmpl AND d.idContract=cont.id AND cont.Client_id=c.id)  ";
        public Admin()
        {
            InitializeComponent();
        }

        public Employees Employees
        {
            get => default(Employees);
            set
            {
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            loadData();
        }
        private void loadData()
        {
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
            loadEmplData();
            loadPostData();
            loadMoneyData();
        }
        private void loadEmplData()
        {
            DBase.DB.selectToGrid(commandEmpl, dgvEmpl);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            employees = new Employees();
            employees.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvEmpl.CurrentCell.RowIndex;
            int id = int.Parse(dgvEmpl[0, rowIndex].Value.ToString());
            employees = new Employees(id);
            employees.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            deleteEmpl();
        }
        private void deleteEmpl()
        {
            DialogResult dialogResult = MessageBox.Show("Действительно удалить?", "Удаление", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(dgvEmpl[0, dgvEmpl.CurrentCell.RowIndex].Value.ToString());
                    command = "DELETE FROM employees WHERE id =" + id;
                    DBase.DB.Update(command, true);
                    resetEmpl();
                }
                catch (Exception e) { MessageBox.Show("Ошибка удаления"); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetEmpl();
        }
        void resetEmpl() {
            loadEmplData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            findEmpl();
        }
        private void findEmpl() {
            command = $" WHERE surname LIKE ('%{textBox6.Text}%') OR firstname LIKE ('%{textBox6.Text}%') OR lastname LIKE ('%{textBox6.Text}%') OR role LIKE ('%{textBox6.Text}%') ";
            DBase.DB.selectToGrid(commandEmpl + command, dgvEmpl);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            findPost();
        }
        void findPost() {
            command = commandPost +$" AND (surname LIKE ('%{textBox1.Text}%') OR firstname LIKE ('%{textBox1.Text}%') OR model LIKE ('%{textBox1.Text}%')) ";
            if (checkBox1.Checked) command += $" AND dates>#{dateTimePicker1.Text.Replace(".","-")}#";
            DBase.DB.selectToGrid(command, dgvPost);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            contrSotr();
        }
        void contrSotr() {
            try{ 
            command = commandMoney + $" AND d.idEmpl = {dgvMoney[2, dgvMoney.CurrentCell.RowIndex].Value.ToString()}";
            DBase.DB.selectToGrid(command, dgvMoney);
            dgvMoney.Columns[0].Visible = false;
            dgvMoney.Columns[1].Visible = false;
            dgvMoney.Columns[2].Visible = false;
            } catch (System.NullReferenceException ex) {
                MessageBox.Show("Не удалось просмотреть платежи, принятые сотрудником.\nНе выбрана запись.");
            }
        }
        void openEmployees()
        {
            try
            {
                int id = int.Parse(dgvPost[1, dgvPost.CurrentCell.RowIndex].Value.ToString());
                employees = new Employees(id, "");
                employees.Show();
            }
            catch (NullReferenceException ex) {
                MessageBox.Show("Не удалось просмотреть информацию о сотруднике.\nНе выбрана запись.");
            }
        }
        void openAuto()
        {
            try
            {
                int id = int.Parse(dgvPost[4, dgvPost.CurrentCell.RowIndex].Value.ToString());
                cars = new Cars(id, "");
                cars.Show();
            }
            catch (NullReferenceException ex) {
                MessageBox.Show("Не удалось просмотреть подробности об авто.\nНе выбрана запись");
            }
        }
        void findMoney() {
            command = commandMoney + $" AND (e.surname LIKE ('%{textBox2.Text}%') OR e.firstname LIKE ('%{textBox2.Text}%') OR c.firstname LIKE ('%{textBox2.Text}%') OR c.lastname LIKE ('%{textBox2.Text}%') OR passport LIKE ('%{textBox2.Text}%')) ";
            if (checkBox2.Checked) command += $" AND datePaid>#{dateTimePicker2.Text.Replace(".", "-")}#";
            DBase.DB.selectToGrid(command, dgvMoney);
            dgvMoney.Columns[0].Visible = false;
            dgvMoney.Columns[1].Visible = false;
            dgvMoney.Columns[2].Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            findMoney();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openEmployees();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openAuto();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadPostData();
            textBox1.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            findMoney();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            openAllContrClient();
        }
        void openAllContrClient() {
            try
            {
                command = commandMoney + $" AND cont.Client_id = (SELECT Client_id FROM contracts WHERE id={dgvMoney[1, dgvMoney.CurrentCell.RowIndex].Value.ToString()})";
                DBase.DB.selectToGrid(command, dgvMoney);
                dgvMoney.Columns[0].Visible = false;
                dgvMoney.Columns[1].Visible = false;
                dgvMoney.Columns[2].Visible = false;
            }
            catch (NullReferenceException ex) {
                MessageBox.Show("Не удалось просмотреть контракты клиента. \nНе выбрана запись");
            }
        }

        private void loadPostData()
        {
            DBase.DB.selectToGrid(commandPost, dgvPost);
        }
        private void loadMoneyData()
        {
            DBase.DB.selectToGrid(commandMoney, dgvMoney);
            dgvMoney.Columns[0].Visible = false;
            dgvMoney.Columns[1].Visible = false;
            dgvMoney.Columns[2].Visible = false;
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogIn.logIn.Show();
        }
    }
}
