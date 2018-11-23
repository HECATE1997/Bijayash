using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

        //Goes back to register page
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        //Goes to Issue View
        private void issueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevIssue di = new DevIssue(session);
            di.MdiParent = this;
            di.Show();
        }

        //Opens Git Repository
        private void gitRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();

            driver.Url = ("https://github.com/login");

            driver.FindElement(By.Id("login_field")).SendKeys("HECATE1997");
            driver.FindElement(By.Id("password")).SendKeys("Bijayash1997");
        }
    }
}
