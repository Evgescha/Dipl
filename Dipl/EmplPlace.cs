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
    public partial class EmplPlace : Form
    {
        public EmplPlace()
        {
            InitializeComponent();
            reset();
        }

      
        // кнопка сброс авто
        private void button2_Click(object sender, EventArgs e)
        {
            resetAuto();
        }
        // кнопка сброс клиента
        private void button3_Click(object sender, EventArgs e)
        {
            resetClient();
        }

        private void EmplPlace_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogIn.logIn.Show();
        }


        private void resetClient() {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton4.Checked = true;

            DBase.DB.selectToGrid("SELECT surname as [Фамилия], firstname as [Имя], lastname as [Отчество], passport as [Пасспорт], phone as [Телефон] FROM clients", dgvClients);
        }
        private void resetAuto() {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            listBox1.SetSelected(0, true);
            radioButton1.Checked = true;

            DBase.DB.selectToGrid("SELECT * FROM cars", dgvAutos);
            dgvAutos.Columns[0].Visible = false;
            dgvAutos.Columns[1].HeaderText = "Марка";
            dgvAutos.Columns[2].HeaderText = "Модель";
            dgvAutos.Columns[3].HeaderText = "Выпуск";
            dgvAutos.Columns[4].HeaderText = "Коробка";
            dgvAutos.Columns[5].HeaderText = "Цвет";
            dgvAutos.Columns[6].HeaderText = "Л.С.";
            dgvAutos.Columns[7].HeaderText = "Двигатель";
            dgvAutos.Columns[8].HeaderText = "Арендован";
            dgvAutos.Columns[9].Visible = false;
        }
        private void reset() {
            resetAuto();
            resetClient();
        }
        //кнопка найти у клиента
        private void button4_Click(object sender, EventArgs e)
        {
            
            string command = "SELECT surname as [Фамилия], firstname as [Имя], lastname as [Отчество], passport as [Пасспорт], phone as [Телефон] FROM clients WHERE id>0 ";
            string temp = "";
            if (textBox4.Text !="") temp += $" AND passport LIKE(\"%{textBox4.Text}%\") ";
            if (textBox5.Text != "") temp += $" AND phone LIKE(\"%{textBox5.Text}%\") ";
            if (textBox6.Text != "") temp += $" AND surname LIKE(\"%{textBox6.Text}%\") ";
           Console.WriteLine(command + temp);
            DBase.DB.selectToGrid(command+temp, dgvClients);
        }
        // добавить клиента
        private void button5_Click(object sender, EventArgs e)
        {

        }
        //изменить клиента
        private void button6_Click(object sender, EventArgs e)
        {

        }
        // удалить клиента
        private void button7_Click(object sender, EventArgs e)
        {

        }
        //оформить клиента
        private void button8_Click(object sender, EventArgs e)
        {

        }
        // оплатить клиента
        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
