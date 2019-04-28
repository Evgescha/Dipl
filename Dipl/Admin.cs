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
        string command = "",
            commandEmpl = "SELECT id as[ИД], surname as [Фамилия], firstname as [Имя], lastname as [Отчество], role as [Должность], login as [Логин], password as [Пароль] FROM employees ",
            commandPost = "SELECT distinct p.id as [ИД], idEng as [ИД Сотрудника],surname as [Фамилия], firstname as[Имя], idCar as [ИД Авто],model as[Модель авто], count as [Поставлено], dates as [Дата] FROM post p, employees e, cars c WHERE (p.idEng=e.id AND p.idCar=c.id)  ",
            commandMoney = "SELECT d.id as [ИД], idContract as [ИД Контракта], idEmpl as [ИД Сотрудника],surname as [Фамилия], firstname as[Имя],  paid as [Получено], datePaid as [Дата]  FROM datePaid d, employees e WHERE e.id = d.idEmpl ";
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            loadData();
        }
        private void loadData()
        {
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
            command = $" AND (surname LIKE ('%{textBox2.Text}%') OR firstname LIKE ('%{textBox2.Text}%') OR model LIKE ('%{textBox2.Text}%')) ";
            DBase.DB.selectToGrid(commandPost + command, dgvPost);
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
        }
        void findMoney() {
            
            DBase.DB.selectToGrid(commandPost + command, dgvPost);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            findMoney();
        }

        private void loadPostData()
        {
            DBase.DB.selectToGrid(commandPost, dgvPost);
        }
        private void loadMoneyData()
        {
            DBase.DB.selectToGrid(commandMoney, dgvMoney);
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogIn.logIn.Show();
        }
    }
}
