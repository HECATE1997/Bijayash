using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TBCErrorHander
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connection = "Data Source=Desktop-d2molfd;Initial Catalog=BugTracker;User ID=sa;Password=Bijayash1997";

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connection);
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Person where UserName='" + textBox1.Text +"' and Password = '"+textBox2.Text+"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1")
            {
                BugTrackerEntities bte = new BugTrackerEntities();
                var item = bte.People.Where(a => a.UserName == textBox1.Text).SingleOrDefault();
                UserSessionModel session = new UserSessionModel();
                session.PersonId = item.PersonId;
                session.RoleId = item.RoleId;
                session.UserName = item.UserName;
                session.connectionstring = connection;

                if (item.RoleId == 01)
                {
                    this.Hide();
                    AdminPage ap = new AdminPage(session);
                    ap.Show();
                }
                else if (item.RoleId == 3)
                {
                    this.Hide();
                    ClientPage cp = new ClientPage(session);
                    cp.Show();
                }
                else
                {
                    this.Hide();
                    DeveloperPage dp = new DeveloperPage(session);
                    dp.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid UserName or Password");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register ss = new Register();
            this.Hide();
            ss.Show();
        }
    }
}
