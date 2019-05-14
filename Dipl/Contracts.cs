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
    public partial class Contracts : Form
    {
        int idAut;
        int idCl;
        int idContr = -1;
        int idEmpl = int.Parse(LogIn.logIn.FIO[0]);
        float[] price;
        string command;
        float all, curr, avans, have;
        string[] contracts;
        public Contracts(int idCl, int idAut)
        {
            this.idAut = idAut;
            this.idCl = idCl;
            InitializeComponent();
            
           // loadClientInfo();
           // loadAutoInfo();
        }
        public Contracts(int idContr)
        {
            this.idContr = idContr;
            InitializeComponent();
            contracts = DBase.DB.SelectOne("contracts", idContr + "");
            idCl = int.Parse(contracts[2]);
            idAut = int.Parse(contracts[3]);
            textBox7.Text = contracts[4];
            textBox7.Enabled = false;
            textBox8.Text = contracts[5];
            textBox9.Text = contracts[6];
            textBox11.Text = contracts[7];
        }

        public ContractsClients ContractsClients
        {
            get => default(ContractsClients);
            set
            {
            }
        }

        private void Contracts_Load(object sender, EventArgs e)
        {
            loadClientInfo();
            loadAutoInfo();

        }
        private void loadClientInfo()
        {
            string[] arrCl = DBase.DB.SelectOne("clients", idCl + "");
            textBox6.Text = arrCl[1] + " " + arrCl[2] + " " + arrCl[3];
            textBox1.Text = arrCl[5];

            textBox3.Text = LogIn.logIn.FIO[0] + " " + LogIn.logIn.FIO[1] + " " + LogIn.logIn.FIO[2];

        }
        private void loadAutoInfo()
        {
            string[] arrAut = DBase.DB.SelectOne("cars", idAut + "");
            textBox2.Text = arrAut[1] + ": " + arrAut[2];
            textBox4.Text = arrAut[3];
            loadPriceInfo(arrAut[10]);
        }
        private void loadPriceInfo(string id)
        {
            string[] arrPrice = DBase.DB.SelectOne("prices", id);
            price = new float[arrPrice.Length];
            for (int i = 0; i < price.Length; i++) {
                price[i] = float.Parse(arrPrice[i]);
            }
            textBox5.Text += "Цена за 1-2 дня: " + price[1] + " \n";
            textBox5.Text += "Цена за 3-5 дней: " + price[2] + " \n";
            textBox5.Text += "Цена за 6-30 дня: " + price[3] + " \n";
            textBox5.Text += "Цена за >30 дней: " + price[4] + " \n";

        }

        private void calculate() {
            try {
                int days = int.Parse(textBox7.Text);
                float allPrice = days;
                if (days <= 2) allPrice *= price[1];
                else if (days <= 5) allPrice *= price[2];
                else if (days <= 9) allPrice *= price[3];
                else allPrice *= price[4];
                float avanse = (int)(allPrice / 2);
                textBox8.Text = allPrice + "";
                textBox9.Text = avanse + "";
            }
            catch (Exception ex) {
                textBox8.Text = "0";
                textBox9.Text = "0";
            }
        }

        private void onlyNumberPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            calculate();
        }

        // кнопка сохранить
        private void button5_Click(object sender, EventArgs e)
        {
            save();
        }
        private void save()
        {
            if (validate()) {
                
                all = float.Parse(textBox8.Text);
                avans = float.Parse(textBox9.Text);
                curr = float.Parse(textBox10.Text);
                have = float.Parse(textBox11.Text);
                switch (idContr) {
                    case -1: newContract();
                        break;
                    default: oldContract();
                        break;
                }
            }
        }
        private void newContract()
        {
            if (curr < avans || curr > all)
            {
                MessageBox.Show("Внесено меньше аванса или больше общей суммы");
                return;
            }

            command = $"INSERT INTO contracts(Employees_id,Client_id, Car_id,days, All_price, advanse, paid, dateAdd) VALUES({idEmpl},{idCl},{idAut},\"{textBox7.Text}\", {all.ToString()}, {avans.ToString()}, {curr.ToString()}, Date())";
            //MessageBox.Show(command);
            if (DBase.DB.Update(command, true)) {
                string lastId = DBase.DB.getColumn("SELECT Max(id) AS idd FROM contracts", new string[] { "idd" })[0];
                command = $"INSERT INTO datePaid(idContract, idEmpl, paid, datePaid) VALUES({lastId},{idEmpl}, {curr.ToString()}, Date())";
                DBase.DB.Update(command, false);
                command = $"UPDATE cars SET free=free-1 WHERE id={idAut}";
                DBase.DB.Update(command, false);

                //ContractsClients.CC.resenInAnotherForm();
                Close(); }

        }
        private void oldContract()
        {
            if ((curr+have)>all)
            {
                MessageBox.Show("Слишком большая сумма. Максимальная для контракта - "+(all-have));
                return;
            }
            if ((curr + have) == all) {
                command = $"UPDATE cars SET free=free+1 WHERE id={idAut}";
                DBase.DB.Update(command, false);
            }
            command = $"UPDATE contracts SET paid=paid+{curr} WHERE id={idContr}";
            // MessageBox.Show(command);
            if (DBase.DB.Update(command, true)) {
                command = $"INSERT INTO datePaid(idContract, idEmpl, paid, datePaid) VALUES({idContr},{idEmpl}, {curr.ToString()}, Date())";
                DBase.DB.Update(command, false);
                ContractsClients.CC.resenInAnotherForm(); Close(); }

        }
        private bool validate() {
            if (textBox7.Text.Length < 1 ||
                textBox10.Text.Length < 1)
                return false;
            else
                return true;
        }
    }
   
}
