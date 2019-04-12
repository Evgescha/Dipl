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
        float[] price;
        string command;
        public Contracts(int idCl, int idAut)
        {
            InitializeComponent();
            this.idAut = idAut;
            this.idCl = idCl;
        }

        private void Contracts_Load(object sender, EventArgs e)
        {
            loadClientInfo();
            loadAutoInfo();
        }
        private void loadClientInfo()
        {
            string[] arrCl = DBase.DB.SelectOne("clients",idCl+"");
            textBox6.Text = arrCl[1] + " " + arrCl[2] + " " + arrCl[3];
            textBox1.Text = arrCl[5];

            textBox3.Text = LogIn.logIn.FIO[0] + " "+LogIn.logIn.FIO[1] + " "+LogIn.logIn.FIO[2];

        }
        private void loadAutoInfo()
        {
            string[] arrAut = DBase.DB.SelectOne("cars", idAut+"");
            textBox2.Text = arrAut[1] + ": " + arrAut[2];
            textBox4.Text = arrAut[3];
            loadPriceInfo(arrAut[9]);
        }
        private void loadPriceInfo(string id)
        {
            string[] arrPrice = DBase.DB.SelectOne("prices", id);
            price = new float[arrPrice.Length];
            for (int i = 0; i < price.Length; i++) {
                price[i] = float.Parse(arrPrice[i]);
            }
            textBox5.Text += "Цена за 1-2 дня: "  + price[1] + " \n";
            textBox5.Text += "Цена за 3-5 дней: " + price[2] + " \n";
            textBox5.Text += "Цена за 6-30 дня: " + price[3] + " \n";
            textBox5.Text += "Цена за >30 дней: " + price[4] + " \n";

        }

        private void calculate() {
            int days = int.Parse(textBox7.Text);
            float allPrice = days;
            if (days <= 2) allPrice *= price[1];
            else if (days <= 5) allPrice *= price[2];
            else if (days <= 9) allPrice *= price[3];
            else allPrice *= price[4];
            float avanse = allPrice / 2;
            textBox8.Text = allPrice+"";
            textBox9.Text = avanse+"";
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
    }
   
}
