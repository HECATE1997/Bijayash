using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBCErrorHander
{
    public partial class ClientViewIssue : Form
    {
        public UserSessionModel session { get; set; }
        //string connection = "Data Source=Desktop-d2molfd;Initial Catalog=BugTracker;Persist Security Info=True;User ID=sa;Password=Bijayash1997";
        
        public ClientViewIssue()
        {
            InitializeComponent();
        }

        public ClientViewIssue(UserSessionModel usm)
        {
            InitializeComponent();
            session = usm;
            fillComboBox();
            
        }

        void fillComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sda = new SqlCommand("Select IssueId,Title from Issue where InsertedBy='" + session.UserName + "'", con);
                sda.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sc = new SqlDataAdapter(sda);
                sc.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Title"].ToString());
                }
                con.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(session.connectionstring);
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("Select * from Issue where Title ='" + comboBox1.Text + "'", con);
            sqlCmd.ExecuteNonQuery();
            SqlDataReader sdr;
            sdr = sqlCmd.ExecuteReader();
            while(sdr.Read())
            {
                string description = (string)sdr["Description"].ToString();
                richTextBox1.Text = description;

                string solution = (string)sdr["Solution"].ToString();
                richTextBox2.Text = solution;
                
                BugTrackerEntities bte = new BugTrackerEntities();
                var item = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                if(item.IssueStatusId==4)
                {
                    textBox1.Text = "Pending";
                }
                else
                {
                    textBox1.Text = "Closed";
                }
                byte[] arr = item.Image;
                MemoryStream ms = new MemoryStream(arr);
                pictureBox1.Image = Image.FromStream(ms);
            }
            con.Close();
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientViewIssue_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
