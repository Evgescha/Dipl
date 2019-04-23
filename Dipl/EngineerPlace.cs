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
        public EngineerPlace()
        {
            InitializeComponent();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            listBox1.SetSelected(0, true);
        }

        private void EngineerPlace_Load(object sender, EventArgs e)
        {

        }
        //add
        private void button4_Click(object sender, EventArgs e)
        {

        }
        //update
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //delete
        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
