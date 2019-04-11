﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dipl
{
    class DBase
    {
        public static DBase DB = new DBase();
        OleDbConnection connection = new OleDbConnection();

        //задать путь
        public DBase()
        {
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;Persist Security Info = False; ";
        }
        //проверка  входа квартирантов
        public string[] LogIn(string login, string password)
        {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM employees e WHERE e.login=\"{login}\" AND e.password=\"{password}\" ";
                
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                string[] responce = new string[4];
                while (reader.Read())
                {
                    count++;
                    responce[0] = reader[1].ToString();
                    responce[1] = reader[2].ToString();
                    responce[2] = reader[3].ToString();
                    responce[3] = reader[4].ToString();
                }
                if (count == 1) { MessageBox.Show("Вход выполнен успешно"); connection.Close(); return responce; }
                else { MessageBox.Show("Неверные данные. Повторите еще раз"); connection.Close(); return null; }

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
                return null;
            }
            
        }
        //обновить/удалить/добавить значение
        public void Update(string command, bool onMessage)
        {
            try
            {
                connection.Open();

                OleDbCommand add = new OleDbCommand();
                add.Connection = connection;
                add.CommandText = command;
                add.ExecuteNonQuery();
                if (onMessage) MessageBox.Show("успешно!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        //получить ячейку значения
        public string getColumn(string command, string column)
        {
            try
            {
                connection.Open();

                OleDbCommand mes = new OleDbCommand();
                mes.Connection = connection;
                mes.CommandText = command;
                OleDbDataReader reader = mes.ExecuteReader();
                string massageString = "";
                while (reader.Read())
                {
                    massageString = reader[column].ToString();
                }
                MessageBox.Show(massageString);
                connection.Close();
                return massageString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
                return "";
            }
        }
        //вывести данные в грид
        public void selectToGrid(string com, DataGridView dg)
        {
            try
            {
                connection.Open();
                OleDbCommand mes = new OleDbCommand();
                mes.Connection = connection;
                mes.CommandText = com;
                OleDbDataAdapter da = new OleDbDataAdapter(mes.CommandText, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dg.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
    }
}