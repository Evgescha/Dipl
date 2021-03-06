﻿using System;
using System.Windows.Forms;

namespace Dipl
{
    public partial class LogIn : Form
    {
        public string[] FIO = new string[4];
        EmplPlace emplPlace;
        EngineerPlace engineerPlace;
        Admin admin;
        public static LogIn logIn;

        public LogIn()
        {
            InitializeComponent();
            logIn = this;
        }

        public EmplPlace EmplPlace
        {
            get => default(EmplPlace);
            set
            {
            }
        }

        public Admin Admin
        {
            get => default(Admin);
            set
            {
            }
        }

        public EngineerPlace EngineerPlace
        {
            get => default(EngineerPlace);
            set
            {
            }
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                FIO=DBase.DB.LogIn(textBox1.Text.Trim(), textBox2.Text.Trim());
                if (FIO != null)
                {
                    switch (FIO[4])
                    {
                        case "Employee":
                            emplPlace = new EmplPlace();
                            emplPlace.Show();
                            break;
                        case "Engineer":
                            engineerPlace = new EngineerPlace();
                            engineerPlace.Show();
                            break;
                        case "Admin":
                            admin = new Admin();
                            admin.Show();
                            break;
                    }
                    this.Hide();
                }
            }
            else { MessageBox.Show("Не все поля заполнены."); }
        }

        //ввод только цифр и их стирание
        private void onlyNumberPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        //если нажат Enter
        private void EnterDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1_Click(sender, e); }
        }

        private bool validate() {
            if (textBox1.TextLength < 1 || textBox2.TextLength < 1) return false; else return true;

        }
    }
}
