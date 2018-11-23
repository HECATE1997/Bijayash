using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TBCErrorHander
{
    public partial class AdminPage : Form
    {
        public UserSessionModel session { get; set; }

        public AdminPage()
        {
            InitializeComponent();
        }
        //session call
        public AdminPage(UserSessionModel usm)
        {
            InitializeComponent();
            session = usm;
        }

        //opens Issue page
        private void issuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminIssue ar = new AdminIssue(session);
            ar.MdiParent = this;
            ar.Show();
        }
        //Opens Create developer/admin  Page
        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminRegister ar = new AdminRegister(session);
            ar.MdiParent = this;
            ar.Show();
        }

        //Goes back to login pge
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 ss = new Form1();
            ss.Show();
        }

        //Opens Git Repo
        private void gitRepoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://github.com/");

            IWebDriver driver = new ChromeDriver();

            driver.Url = ("https://github.com/login");

            driver.FindElement(By.Id("login_field")).SendKeys("HECATE1997");
            driver.FindElement(By.Id("password")).SendKeys("Bijayash1997");
        }
    }
}
