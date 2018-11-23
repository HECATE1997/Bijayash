using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBCErrorHander
{
    public partial class DevIssue : Form
    {
        public UserSessionModel session { get; set; }
        //string connection = "Data Source=Desktop-d2molfd;Initial Catalog=BugTracker;Persist Security Info=True;User ID=sa;Password=Bijayash1997";

        public DevIssue()
        {
            InitializeComponent();
        }

        //Initiates page
        public DevIssue(UserSessionModel usm)
        {
            InitializeComponent();
            session = usm;
            fillComboBox();
        }

        //fills combo box with list of issues
        void fillComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sda = new SqlCommand("Select IssueId,Title from Issue where SolvedBy='" + session.UserName + "'AND IssueStatusId <> 10", con);
                sda.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sc = new SqlDataAdapter(sda);
                sc.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Title"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DevIssue_Load(object sender, EventArgs e)
        {

        }

        //Change data when combo box is changed
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(session.connectionstring);
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("Select * from Issue where Title ='" + comboBox1.Text + "'", con);
            sqlCmd.ExecuteNonQuery();
            SqlDataReader sdr;
            sdr = sqlCmd.ExecuteReader();
            while (sdr.Read())
            {
                string description = (string)sdr["Description"].ToString();
                richTextBox1.Text = description;

                string solution = (string)sdr["Solution"].ToString();
                richTextBox2.Text = solution;

                BugTrackerEntities bte = new BugTrackerEntities();
                var item = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                byte[] arr = item.Image;
                MemoryStream ms = new MemoryStream(arr);
                pictureBox1.Image = Image.FromStream(ms);
            }
            con.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Edits Issues
        private void button1_Click(object sender, EventArgs e)
        {
            try{ 
                BugTrackerEntities bte = new BugTrackerEntities();
                var data = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                data.Solution = richTextBox2.Text;
                data.IssueStatusId = 7;
                bte.Entry(data).State = EntityState.Modified;
                bte.SaveChanges();
                MessageBox.Show("Saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
