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
    public partial class Register : Form
    {
        public UserSessionModel session { get; set; }

        public Register(UserSessionModel usm)
        {
            InitializeComponent();
            session = usm;
        }

        //string connection = "Data Source=Desktop-d2molfd;Initial Catalog=BugTracker;User ID=sa;Password=Bijayash1997";
        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("One or More Fields Are Empty");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Password do not match");
            }
            else
            {
                int Role = 3;
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sqlcmd = new SqlCommand("SpPersonIns", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                string returnMessage = "";
                sqlcmd.Parameters.AddWithValue("@UserName", textBox1.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@PassWord", textBox2.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Mobile", textBox4.Text.Trim());
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
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}
