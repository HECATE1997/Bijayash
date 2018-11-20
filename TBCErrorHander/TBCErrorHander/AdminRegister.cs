using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBCErrorHander
{
    public partial class AdminRegister : Form
    {
        int Role = 0;
        //string connection = "Data Source=Desktop-d2molfd;Initial Catalog=BugTracker;User ID=sa;Password=Bijayash1997";
        public UserSessionModel session { get; set; }
        public AdminRegister(UserSessionModel session)
        {
            InitializeComponent();
            session = session;
        }

        private void AdminRegister_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string returnMessage= "";
            if (textBox1.Text==""||textBox2.Text==""||textBox3.Text==""||textBox4.Text=="")
            {
                MessageBox.Show("One or More Fields Are Empty");
            }
            else if(textBox2.Text!=textBox3.Text)
            {
                MessageBox.Show("Password do not match");
            }
            else
            {
                if(checkBox1.Checked)
                {
                    Role = 1;
                }
                else
                {
                    Role = 2;
                }
                
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sqlcmd = new SqlCommand("SpPersonIns", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("@UserName", textBox1.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@PassWord", textBox2.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Mobile", Convert.ToInt64(textBox4.Text.Trim()));
                sqlcmd.Parameters.AddWithValue("@roleId", Role);
                sqlcmd.Parameters.AddWithValue("@ReturnMessage", returnMessage);
                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Saved");
                con.Close();
                Clear();
            }
        }

        void Clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            //AdminPage ap = new AdminPage();
            //ap.Show();
        }
    }
}
