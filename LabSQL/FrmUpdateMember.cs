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

namespace LabSQL
{
    public partial class FrmUpdateMember : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        string value;
        private ClubRegistrationQuery regquery;
        SqlCommand sqlComm;
        SqlDataReader sqlRead;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            string[] UpPrograms = new string[]
            {
                "BS Information Technology",
                "BS in Computer Engineering",
                "BS Computer Science",
                "Bachelor of Multimedia Arts",
                "BS in Tourism Management"
            };

            string[] Program = new string[5];
            for (int i = 0; i < Program.Length; i++)
            {
                UpcbProgram.Items.Add(UpPrograms[i].ToString());
            }

            string[] Gender = new string[]
            {
                "Female", "Male", "Rather not Specify"
            };

            for (int i = 0; i < 3; i++)
            {
                UpcbGender.Items.Add(Gender[i].ToString());
            }
            regquery = new ClubRegistrationQuery();
            using (SqlConnection sqlCon = new SqlConnection(regquery.connectionString))
            {
                sqlCon.Open();
                string query = "SELECT StudentID FROM ClubMembers";
                sqlComm = new SqlCommand(query, sqlCon);
                sqlRead = sqlComm.ExecuteReader();

                while (sqlRead.Read())
                {
                    UpcbStudID.Items.Add(sqlRead.GetValue(0));
                }
                sqlRead.Close();
                sqlCon.Close();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            regquery = new ClubRegistrationQuery();
            using (SqlConnection sqlConnection = new SqlConnection(regquery.connectionString))
            {
                sqlConnection.Open();
                string update = "UPDATE ClubMembers SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = @StudentId";
                sqlComm = new SqlCommand(update, sqlConnection);
                sqlComm.Parameters.AddWithValue("@FirstName", UptbFirstName.Text);
                sqlComm.Parameters.AddWithValue("@MiddleName", UptbMiddleName.Text);
                sqlComm.Parameters.AddWithValue("@LastName", UptbLastName.Text);
                sqlComm.Parameters.AddWithValue("@Age", int.Parse(UptbAge.Text));
                sqlComm.Parameters.AddWithValue("@Gender", UpcbGender.Text);
                sqlComm.Parameters.AddWithValue("@Program", UpcbProgram.Text);
                sqlComm.Parameters.AddWithValue("@StudentId", value);

                sqlComm.ExecuteNonQuery();
                sqlConnection.Close();
                this.Close();

                MessageBox.Show("Successfully Updated!\n(Kindly Refresh the List)", "SUCCESS!");
            }
        }

        private void UpcbStudID_SelectedIndexChanged(object sender, EventArgs e)
        {
            value = UpcbStudID.SelectedItem.ToString();
            regquery = new ClubRegistrationQuery();
            using (SqlConnection sqlConn = new SqlConnection(regquery.connectionString))
            {
                sqlConn.Open();
                string q = "SELECT * FROM ClubMembers WHERE StudentId = '" + value + "'";
                sqlComm = new SqlCommand(q, sqlConn);
                sqlRead = sqlComm.ExecuteReader();
                while (sqlRead.Read())
                {
                    UptbFirstName.Text = "" + sqlRead.GetValue(2);
                    UptbMiddleName.Text = "" + sqlRead.GetValue(3);
                    UptbLastName.Text = "" + sqlRead.GetValue(4);
                    UptbAge.Text = "" + sqlRead.GetValue(5).ToString();
                    UpcbGender.Text = "" + sqlRead.GetValue(6);
                    UpcbProgram.Text = "" + sqlRead.GetValue(7); 
                }
                sqlRead.Close();
                sqlConn.Close();
            }
        }
    }
}

