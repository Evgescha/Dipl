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
    public partial class EngineerPlace : Form
    {
        Cars car;
        string command = "";
        string selectCars = "SELECT id as [ИД], brand as [Марка],model as [Модель],years as [Выпуск],transmission as [Коробка],color as [Цвет],horsepower as [ЛС],engine_size as [Двигатель], alls as [Всего], free as [Свободных],id_price as [Цена]  FROM cars WHERE id>0 ";
        public EngineerPlace()
        {
            InitializeComponent();
        }
        //сброс

        private void button2_Click(object sender, EventArgs e)
        {
            reset();   
        }

        private void EngineerPlace_Load(object sender, EventArgs e)
        {
            reset();
        }
        //add
        private void button4_Click(object sender, EventArgs e)
        {
            add();
        }
        //update
        private void button3_Click(object sender, EventArgs e)
        {
            update();
        }
        //delete
        private void button5_Click(object sender, EventArgs e)
        {
            delete();
        }
        //найти
        private void button1_Click(object sender, EventArgs e)
        {
            find();
        }


        void add() {
            car = new Cars();
            car.Show();
        }
        void update() {
            int idAu = int.Parse(dgvAutos[0, dgvAutos.CurrentCell.RowIndex].Value.ToString());
            car = new Cars(idAu);
            car.Show();
        }
        void delete() {
            DialogResult dialogResult = MessageBox.Show("Удаление", "Действительно удалить?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    int idAu = int.Parse(dgvAutos[0, dgvAutos.CurrentCell.RowIndex].Value.ToString());
                    command = "DELETE FROM cars WHERE id =" + idAu;
                    DBase.DB.Update(command, true);
                }
                catch (Exception e) { MessageBox.Show("Ошибка удаления"); }
            }
        }
        void find() {
            string command = selectCars;

            string temp = "";
            if (textBox1.Text.Length > 0) temp += $" AND brand LIKE(\"%{textBox1.Text}%\") ";
            if (textBox2.Text.Length > 0) temp += $" AND model LIKE(\"%{textBox2.Text}%\") ";
            if (textBox3.Text.Length > 0) temp += $" AND years LIKE(\"%{textBox3.Text}%\") ";
            if (listBox1.SelectedItem.ToString() != "Любая") temp += $" AND transmission LIKE(\"%{listBox1.SelectedItem.ToString()}%\") ";
            Console.WriteLine(command + temp);
            DBase.DB.selectToGrid(command + temp, dgvAutos);
            dgvAutos.Columns[0].Visible = false;
            dgvAutos.Columns[10].Visible = false;

        }

        void reset() {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            listBox1.SetSelected(0, true);
            DBase.DB.selectToGrid(selectCars, dgvAutos);
            dgvAutos.Columns[0].Visible = false;
            dgvAutos.Columns[10].Visible = false;
        }

        private void EngineerPlace_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogIn.logIn.Show();
        }
    }
}
