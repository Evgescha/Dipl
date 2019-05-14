using System;
using System.Collections;
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
    public partial class Cars : Form
    {
        string command = "";
        int idAut = -1;
        public Cars()
        {
            InitializeComponent();
        }

        public Cars(int idAut) {
            this.idAut = idAut;
            InitializeComponent();
        }
        public Cars(int idAut, string str)
        {
            this.idAut = idAut;
            InitializeComponent();
            comboBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            listBox1.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;


            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            button2.Visible = false;
            button1.Visible = false;
        }
        private void Cars_Load(object sender, EventArgs e)
        {
            ArrayList brands = DBase.DB.getColumnAllRows("SELECT brand FROM brands", new string[] { "brand" });
            for (int i = 0; i < brands.Count; i++) { 
            comboBox1.Items.Add(brands[i]); }
            if (idAut != -1) loadInfo();
        }
        private void loadInfo() {
            string[] infoAuth = DBase.DB.SelectOne("cars", idAut + "");
            string[] infoPrice = DBase.DB.SelectOne("prices", idAut + "");


            comboBox1.Text = infoAuth[1];
            textBox2.Text = infoAuth[2];
            textBox3.Text = infoAuth[3];
            listBox1.Text = infoAuth[4];
            textBox4.Text = infoAuth[5];
            textBox5.Text = infoAuth[6];
            textBox6.Text = infoAuth[7];
            textBox7.Text = infoAuth[8];


            textBox8.Text = infoPrice[1];
            textBox9.Text = infoPrice[2];
            textBox10.Text = infoPrice[3];
            textBox11.Text = infoPrice[4];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!validate()) { MessageBox.Show("Не все поля заполнены"); return; }
            if (idAut == -1) addNew(); else saveCurr();
            if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                command = $"INSERT INTO brands(brand) VALUES(\"{comboBox1.Text}\")";
                DBase.DB.Update(command, true);
            }
        }
        private void addNew() {
            command = $"INSERT INTO prices(one_two_day,three_five_days, six_twenty_nine_days, more_thirty) VALUES({textBox8.Text},{textBox9.Text},{textBox10.Text},{textBox11.Text})";
            DBase.DB.Update(command, false);
            string lastId = DBase.DB.getColumn("SELECT Max(id) AS idd FROM prices", new string[] { "idd" })[0];
            command = $"INSERT INTO cars(brand,model, years, transmission, color, horsepower, engine_size, alls,free, id_price) " +
                $"VALUES(\"{comboBox1.Text}\", \"{textBox2.Text}\", {textBox3.Text}, \"{listBox1.Text}\",\"{textBox4.Text}\", {textBox5.Text}, {textBox6.Text}, {textBox7.Text}, {textBox7.Text},{lastId} )";
            DBase.DB.Update(command, false);
            lastId = DBase.DB.getColumn("SELECT Max(id) AS idd FROM cars", new string[] { "idd" })[0];
            command = "INSERT INTO post ( idEng, idCar, [count], dates ) VALUES(" + LogIn.logIn.FIO[0]+", "+lastId+", "+textBox7.Text+", Date())";
            //MessageBox.Show(command);
            DBase.DB.Update(command, true);
        }
        private void saveCurr() {
            command = $"UPDATE prices SET one_two_day={textBox8.Text},three_five_days={textBox9.Text}, six_twenty_nine_days={textBox10.Text}, more_thirty={textBox11.Text} WHERE id ={idAut}";
            DBase.DB.Update(command, false);
            command = $"UPDATE cars SET brand=\"{comboBox1.Text}\", model=\"{textBox2.Text}\",  years={textBox3.Text},  transmission=\"{listBox1.Text}\",  " +
                $"color=\"{textBox4.Text}\",  horsepower={textBox5.Text},  engine_size={textBox6.Text},  alls={textBox7.Text}, free={textBox7.Text} WHERE id={idAut}";
            DBase.DB.Update(command, false);
            command = $"UPDATE post SET [count]={textBox7.Text} WHERE idEng={LogIn.logIn.FIO[0]} AND idCar={idAut}";
            DBase.DB.Update(command, true);
        }

        private bool validate() {
            if (comboBox1.Text.Length < 1 ||
                textBox2.Text.Length < 1 ||
                textBox3.Text.Length < 1 ||
                listBox1.Text.Length < 1 ||
                textBox4.Text.Length < 1 ||
                textBox5.Text.Length < 1 ||
                textBox6.Text.Length < 1 ||
                textBox7.Text.Length < 1 ||
                textBox8.Text.Length < 1 ||
                textBox9.Text.Length < 1 ||
                textBox10.Text.Length < 1 ||
                textBox11.Text.Length < 1) return false;
            else
                return true;
        }
        private void onlyNumberPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearText();
        }
        private void clearText() {
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
        }
    }
}
