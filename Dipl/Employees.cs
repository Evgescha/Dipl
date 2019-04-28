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
    public partial class Employees : Form
    {
        string command = "";
        int idEmpl = -1;
        public Employees()
        {
            InitializeComponent();
        }
        public Employees(int idEmpl)
        {
            this.idEmpl = idEmpl;            
            InitializeComponent();
            loadData();
        }
        private void loadData() {
            string[] info = DBase.DB.SelectOne("employees", idEmpl + "");
            textBox1.Text = info[1];
            textBox2.Text = info[2];
            textBox3.Text = info[3];
            listBox1.Text = info[4];
            textBox5.Text = info[5];
            textBox6.Text = info[6];
        }
        private void button2_Click(object sender, EventArgs e)
        {
            updateSave();
        }
        private void updateSave() {
            switch (idEmpl) {
                case -1:Add(); break;
                default: updateEmpl(); break;
            }
        }
        private void Add() {
            command = $"INSERT INTO employees ( surname, firstname, lastname, role, login, [password] ) VALUES('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{listBox1.Text}','{textBox5.Text}','{textBox6.Text}')";
            DBase.DB.Update(command, true);
        }
        private void updateEmpl() {
            command = $"UPDATE employees SET surname='{textBox1.Text}', firstname='{textBox2.Text}',lastname='{textBox3.Text}',role='{listBox1.Text}',login='{textBox5.Text}',[password]='{textBox6.Text}' WHERE id={idEmpl}";
            DBase.DB.Update(command, true);
        }
    }
}
