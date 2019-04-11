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
        private Clients clients;

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

            DBase.DB.selectToGrid("SELECT id, surname as [Фамилия], firstname as [Имя], lastname as [Отчество], passport as [Пасспорт], phone as [Телефон] FROM clients", dgvClients);
            dgvClients.Columns[0].Visible = false;
        }
        private void resetAuto() {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            listBox1.SetSelected(0, true);
            radioButton1.Checked = true;
            string command = "SELECT id as [ИД], brand as [Марка],model as [Модель],years as [Выпуск],transmission as [Коробка],color as [Цвет],horsepower as [ЛС],engine_size as [Двигатель],rental as [Арендован], id_price as [Цена] FROM cars";
            DBase.DB.selectToGrid(command, dgvAutos);
            dgvAutos.Columns[0].Visible = false;
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
            clients = new Clients();
            clients.Show();
        }
        //изменить клиента
        private void button6_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvClients.CurrentCell.RowIndex;
            int id = int.Parse(dgvClients[0, rowIndex].Value.ToString());
            clients = new Clients(id);
            clients.Show();
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
        // кнопка поиска авто
        private void button1_Click(object sender, EventArgs e)
        {
            string command = "SELECT id as [ИД], brand as [Марка],model as [Модель],years as [Выпуск],transmission as [Коробка],color as [Цвет],horsepower as [ЛС],engine_size as [Двигатель],rental as [Арендован], id_price as [Цена] FROM cars WHERE id>0 ";
           
            string temp = "";
            if (textBox4.Text.Length<1) temp += $" AND brand LIKE(\"%{textBox1.Text}%\") ";
            if (textBox5.Text.Length < 1) temp += $" AND model LIKE(\"%{textBox2.Text}%\") ";
            if (textBox6.Text.Length < 1) temp += $" AND years LIKE(\"%{textBox3.Text}%\") ";
            if (listBox1.SelectedItem.ToString() != "Любая") temp += $" AND transmission LIKE(\"%{listBox1.SelectedItem.ToString()}%\") ";
            Console.WriteLine(command + temp);
            DBase.DB.selectToGrid(command + temp, dgvAutos);
            dgvAutos.Columns[0].Visible = false;
            dgvAutos.Columns[9].Visible = false;
        }
    }
}
