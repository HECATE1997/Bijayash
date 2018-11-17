using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBCErrorHander
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
        }
        

        private void issuesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminRegister ar = new AdminRegister();
            ar.MdiParent = this;
            ar.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 ss = new Form1();
            ss.Show();
        }
    }
}
