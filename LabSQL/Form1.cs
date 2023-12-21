using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace LabSQL
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        public FrmClubRegistration()
        {
            InitializeComponent();
        }
        private long StudentID;

        public void RefreshListOfMembers()
        {
            ClubRegistrationQuery ClubRegQ = new ClubRegistrationQuery();
            ClubRegQ.DisplayList();
            dgvListClubMem.DataSource = ClubRegQ.bindingSource;
        }
        
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmUpdateMember frmUpdateMember = new FrmUpdateMember();
            frmUpdateMember.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClubRegistrationQuery crq = new ClubRegistrationQuery();
            RefreshListOfMembers();
            string[] Programs = new string[]
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
                cbProgram.Items.Add(Programs[i].ToString());
            }

            string[] Gender = new string[]
            {
                "Female", "Male", "Rather not Specify"
            };

            for(int i = 0; i < 3; i++)
            {
                cbGender.Items.Add(Gender[i].ToString());
            }
        }

        private int Transac;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfMembers();
        }
        private long StudentId;

        private int RegistrationID()
        {
            Transac = 0;
            Transac++;
            return Transac;
        }
        private void btnRegis_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();
            FirstName = tbFirstName.Text;
            MiddleName = tbMiddleName.Text;
            LastName = tbLastName.Text;
            Gender = cbGender.SelectedItem.ToString();
            Program = cbProgram.Text;

            Age = int.Parse(tbAge.Text);
            StudentId = long.Parse(tbStudID.Text);
            clubRegistrationQuery = new ClubRegistrationQuery();
            clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);

            RefreshListOfMembers();
            Clear();
        }
        public void Clear()
        {
            tbStudID.Clear();
            tbLastName.Clear();
            tbFirstName.Clear();
            tbMiddleName.Clear();
            tbAge.Clear();
            cbGender.SelectedIndex = -1;
            cbProgram.SelectedIndex = -1;
        }
    }

}
