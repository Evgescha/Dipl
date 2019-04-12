﻿using System;
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
    public partial class ContractsClients : Form
    {
        public static ContractsClients CC;
        public int idCl;
        string command;
        Contracts contracts;
        public ContractsClients(int idCl)
        {
            CC = this;
            this.idCl = idCl;
            command = "SELECT " +
                "id as [ИД], " +
                "days as [Дней], " +
                "All_price as [Общая сумма], " +
                "advanse as [Аванс], " +
                "paid as [Внесено] "+
                " FROM contracts WHERE Client_id=" + idCl + "  ";
            InitializeComponent();
        }
        public void resenInAnotherForm() {
            radioButton1.Checked = true;
            DBase.DB.selectToGrid(command, dataGridView1);
        }
        private void ContractsClients_Load(object sender, EventArgs e)
        {
            DBase.DB.selectToGrid(command, dataGridView1);
            string[] fio = DBase.DB.getColumn("SELECT surname, firstname, lastname, passport " +
                "FROM clients " +
                "WHERE id=" + idCl, new string[] { "surname", "firstname", "lastname", "passport" });
            textBox2.Text = fio[0] + " " + fio[1] + " " + fio[2];
            textBox1.Text = fio[3];
        }

      
        public void resetGrid() {
            string temp = "";
            for (int i = 0; i < 3; i++)
            {
                RadioButton rb = ((RadioButton)groupBox1.Controls[i]);
                if (rb.Checked == true)
                    switch (i)
                    {
                        case 2: temp = "AND paid<All_price "; break;
                        case 1: temp = "AND All_price=paid"; break;
                    }
            }
            DBase.DB.selectToGrid(command + temp, dataGridView1);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            resetGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int idContr = int.Parse(dataGridView1[0, rowIndex].Value.ToString());
                contracts = new Contracts(idContr);
                contracts.Show();
            }
            catch (Exception ex) { MessageBox.Show("Не выбран контракт"); }
        }
    }
}
