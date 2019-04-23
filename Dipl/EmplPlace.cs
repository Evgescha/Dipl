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
        private Contracts contracts;
        private ContractsClients contractsClients;

        public EmplPlace()
        {
            InitializeComponent();
            //reset();
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
            string command = "SELECT id as [ИД], brand as [Марка],model as [Модель],years as [Выпуск],transmission as [Коробка],color as [Цвет],horsepower as [ЛС],engine_size as [Двигатель], alls as [Всего], free as [Свободных], id_price as [Цена] FROM cars";
            DBase.DB.selectToGrid(command, dgvAutos);
            dgvAutos.Columns[0].Visible = false;
            dgvAutos.Columns[10].Visible = false;
        }
        private void reset() {
            resetAuto();
            resetClient();
        }
        //кнопка найти у клиента
        private void button4_Click(object sender, EventArgs e)
        {

            findClient();
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
            int rowIndex = dgvClients.CurrentCell.RowIndex;
            int id = int.Parse(dgvClients[0, rowIndex].Value.ToString());
            string command = $"DELETE FROM clients WHERE id={id}";
            DBase.DB.Update(command, true);
            resetClient();
        }
        //оформить клиента
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndexCl = dgvClients.CurrentCell.RowIndex;
                int idCl = int.Parse(dgvClients[0, rowIndexCl].Value.ToString());

                int rowIndexAu = dgvAutos.CurrentCell.RowIndex;
                int idAu = int.Parse(dgvAutos[0, rowIndexAu].Value.ToString());


                if (bool.Parse(dgvAutos[8, rowIndexAu].Value.ToString())) { MessageBox.Show("Данная машина занята, выберите другую"); return; }
                contracts = new Contracts(idCl, idAu);
                contracts.Show();
            } catch (Exception ex) { MessageBox.Show("Не выбран пользователь или авто.");
                //MessageBox.Show(ex.ToString());
            }
        }
        // контракты клиента
        private void button9_Click(object sender, EventArgs e)
        {
            int rowIndexCl = dgvClients.CurrentCell.RowIndex;
            int idCl = int.Parse(dgvClients[0, rowIndexCl].Value.ToString());
            contractsClients = new ContractsClients(idCl);
            contractsClients.Show();
        }
        // кнопка поиска авто
        private void button1_Click(object sender, EventArgs e)
        {
            findAuto("");
        }
        
        private void findAuto(string str) {
            string command = "SELECT id as [ИД], brand as [Марка],model as [Модель],years as [Выпуск],transmission as [Коробка],color as [Цвет],horsepower as [ЛС],engine_size as [Двигатель], alls as [Всего], free as [Свободных],id_price as [Цена]  FROM cars WHERE id>0 ";

            string temp = "";
            if (textBox4.Text.Length > 0) temp += $" AND brand LIKE(\"%{textBox1.Text}%\") ";
            if (textBox5.Text.Length > 0) temp += $" AND model LIKE(\"%{textBox2.Text}%\") ";
            if (textBox6.Text.Length > 0) temp += $" AND years LIKE(\"%{textBox3.Text}%\") ";
            if (listBox1.SelectedItem.ToString() != "Любая") temp += $" AND transmission LIKE(\"%{listBox1.SelectedItem.ToString()}%\") ";
            Console.WriteLine(command + temp+str);
            DBase.DB.selectToGrid(command + temp+str, dgvAutos);
            dgvAutos.Columns[0].Visible = false;
            dgvAutos.Columns[10].Visible = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            string temp = "";
            if (radioButton3.Checked) temp = "AND free>0 ";
            if (radioButton2.Checked) temp = "AND alls-free>0 ";
            findAuto(temp);
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            findClient();

        }
        private void findClient() {
            string command = "SELECT DISTINCT id, surname as [Фамилия], firstname as [Имя], lastname as [Отчество], passport as [Пасспорт], phone as [Телефон] FROM clients WHERE id>0 ";
            string temp = "";
            if (textBox4.Text != "") temp += $" AND passport LIKE(\"%{textBox4.Text}%\") ";
            if (textBox5.Text != "") temp += $" AND phone LIKE(\"%{textBox5.Text}%\") ";
            if (textBox6.Text != "") temp += $" AND surname LIKE(\"%{textBox6.Text}%\") ";
            if (radioButton5.Checked) temp = "AND id IN (SELECT Client_id from contracts where paid<All_price) ";
            if (radioButton6.Checked) temp = "AND id NOT  IN (SELECT Client_id from contracts where paid<All_price) ";
            Console.WriteLine(command + temp);
            DBase.DB.selectToGrid(command + temp, dgvClients);
            dgvClients.Columns[0].Visible = false;
        }

        private void EmplPlace_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int rowIndexAu = dgvAutos.CurrentCell.RowIndex;
            int idAu = int.Parse(dgvAutos[0, rowIndexAu].Value.ToString());
            
            string[] price =DBase.DB.SelectOne("prices", idAu + "");
            string msg = "Цены на данное авто:"   + "\n" +
                        "1-2 дня   - " + price[1] + "\n"+
                        "3-5 дней  - " + price[2] + "\n" +
                        "6-29 дней - " + price[3] + "\n" +
                        "30 и выше - " + price[4] + "\n" ;
            MessageBox.Show(msg);
        }
    }
}
