using System;
using System.Collections;
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
                reader.Read(); count++;
                string[] responce = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    responce[i] = reader[i].ToString();
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
        
        public string[] SelectOne(string tablName, string id) {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM {tablName} WHERE id={id}";

                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                reader.Read(); count++; 
                string[] responce = new string[reader.FieldCount];
                for(int i=0; i<reader.FieldCount; i++)
                {
                    responce[i] = reader[i].ToString();
                } 
                
                if (count == 1) {connection.Close(); return responce; }
                else {connection.Close(); return null; }

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("FAIL" + ex);
                return null;
            }
        }
        //обновить/удалить/добавить значение
        public bool Update(string command, bool onMessage)
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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
                return false;
            }
        }
        //получить ячейку значения
        public string[] getColumn(string command, string[] column)
        {
            try
            {
                connection.Open();

                OleDbCommand mes = new OleDbCommand();
                mes.Connection = connection;
                mes.CommandText = command;
                OleDbDataReader reader = mes.ExecuteReader();
                reader.Read();
                string[] massageString = new string[column.Length];
                for(int i=0; i<reader.FieldCount; i++)
                {
                    massageString[i] = reader[column[i]].ToString();
                }
               
                connection.Close();
                return massageString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
                return null;
            }
        }

        //get
        public ArrayList getColumnAllRows(string command, string[] column)
        {
            try
            {
                connection.Open();

                OleDbCommand mes = new OleDbCommand();
                mes.Connection = connection;
                mes.CommandText = command;
                OleDbDataReader reader = mes.ExecuteReader();
                ArrayList array = new ArrayList();
                while (reader.Read()) {
                    for (int i = 0; i < column.Length; i++)
                    {
                        array.Add(reader[column[i]].ToString());
                    }
                }

              
                connection.Close();
                return array;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
                return null;
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
