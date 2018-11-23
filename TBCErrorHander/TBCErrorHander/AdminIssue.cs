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
    public partial class AdminIssue : Form
    {
        public UserSessionModel session { get; set; }
        //string connection = "Data Source=Desktop-d2molfd;Initial Catalog=BugTracker;Persist Security Info=True;User ID=sa;Password=Bijayash1997";

        public AdminIssue()
        {
            InitializeComponent();
        }

        public AdminIssue(UserSessionModel usm)
        {
            InitializeComponent();
            session = usm;
            fillComboBox();
            fillDevComboBox();
            fillIssueStatusComboBox();
        }

        //Fills Combo box with list of Issues

        void fillComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sda = new SqlCommand("Select Title from Issue", con);
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

        //Fills Combo box with list of developers
        void fillDevComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sda = new SqlCommand("Select UserName from Person where RoleId=2", con);
                sda.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sc = new SqlDataAdapter(sda);
                sc.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["UserName"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Fills combo box with Issue Status
        void fillIssueStatusComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(session.connectionstring);
                con.Open();
                SqlCommand sda = new SqlCommand("select ListItemName from ListItem where ListItemCategoryId=2", con);
                sda.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sc = new SqlDataAdapter(sda);
                sc.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox3.Items.Add(dr["ListItemName"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
        //On change of combo box, datas are inserted in Admin Issue
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
                string status = "";
                BugTrackerEntities bte = new BugTrackerEntities();
                var item = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                if(item.IssueStatusId==7)
                {
                    status = "On Progress";
                }
                else if(item.IssueStatusId==4)
                {
                    status = "Pending";
                }
                else if(item.IssueStatusId==10)
                {
                    status = "Closed";
                }
                else
                {
                    status = "Re Opened";
                }
                comboBox3.Text = status;
                comboBox2.Text = item.SolvedBy;
                byte[] arr = item.Image;
                MemoryStream ms = new MemoryStream(arr);
                pictureBox1.Image = Image.FromStream(ms);
            }
            con.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //Admin is able to view issue and edit description and issue status

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BugTrackerEntities bte = new BugTrackerEntities();
                var data = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                data.Description = richTextBox1.Text;
                data.SolvedBy = comboBox2.Text;
                if (comboBox3.Text == "Pending")
                {
                    data.IssueStatusId = 4;
                }
                else if (comboBox3.Text == "OnProgress")
                {
                    data.IssueStatusId = 7;
                }
                else if (comboBox3.Text == "Closed")
                {
                    data.IssueStatusId = 10;                }
                else
                {
                    data.IssueStatusId = 11;
                }
                bte.Entry(data).State = EntityState.Modified;
                bte.SaveChanges();
                MessageBox.Show("Saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
