using System;

using System.Windows.Forms;

namespace Dipl
{
    public partial class Clients : Form
    {
        int id=-1;
        string[] client;
        public Clients()
        {
            InitializeComponent();
            button5.Text = "Добавить";
        }
        public Clients(int id)
        {
            this.id = id;
            InitializeComponent();
            initInfo();
            button5.Text = "Изменить";
        }

        private void initInfo() {
            client = DBase.DB.SelectOneClient(id.ToString());
            textBox1.Text = client[2];
            textBox2.Text = client[3];
            textBox3.Text = client[4];
            textBox4.Text = client[7];
            textBox5.Text = client[5];
            textBox6.Text = client[1];
            textBox7.Text = client[6];
        }
        // кнопка очисить
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void Clients_Load(object sender, EventArgs e)
        {

        }
        // кнопка изменить
        private void button5_Click(object sender, EventArgs e)
        {
            if (!validate()) { MessageBox.Show(""); return; }
            string command="";
            if (id != -1)
                command = $"UPDATE clients SET surname = \"{textBox6.Text}\", firstname = \"{textBox1.Text}\", lastname = \"{textBox2.Text}\", phone = \"{textBox3.Text}\", passport = \"{textBox5.Text}\", placeResidence = \"{textBox7.Text.Replace("\"", "'")}\", Job = \"{textBox4.Text.Replace("\"", "'")}\" WHERE id = {client[0]}";
            else
                command = $"INSERT INTO clients(surname, firstname, lastname, phone, passport, placeResidence, Job) VALUES( \"{textBox6.Text}\",  \"{textBox1.Text}\",  \"{textBox2.Text}\",  \"{textBox3.Text}\",  \"{textBox5.Text}\", \"{textBox7.Text.Replace("\"", "'")}\", \"{textBox4.Text.Replace("\"", "'")}\")";
            MessageBox.Show(command);
            DBase.DB.Update(command,true);
        }
        private bool validate() {
            if (
                textBox1.Text.Length < 1 ||
                textBox2.Text.Length < 1 ||
                textBox3.Text.Length < 1 ||
                textBox4.Text.Length < 1 ||
                textBox5.Text.Length < 1 ||
                textBox6.Text.Length < 1 ||
                textBox7.Text.Length < 1)
                return false;
            else return true;
        }
    }
}
