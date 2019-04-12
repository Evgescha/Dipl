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

        }
        private void loadAutoInfo()
        {
            string[] arrAut = DBase.DB.SelectOne("cars", idAut+"");
            textBox2.Text = arrAut[1] + ": " + arrAut[2];
            textBox4.Text = arrAut[3];
        }
    }
}
