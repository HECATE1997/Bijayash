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
    public partial class DeveloperPage : Form
    {
        public UserSessionModel session { get; set; }
        public DeveloperPage()
        {
            InitializeComponent();
        }

        public DeveloperPage(UserSessionModel usm)
        {
            InitializeComponent();
            session = usm;
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void issueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevIssue di = new DevIssue(session);
            di.MdiParent = this;
            di.Show();
        }
    }
}
