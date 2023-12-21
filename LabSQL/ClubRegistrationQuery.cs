﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace LabSQL
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlDataReader;

        public DataTable dataTable;
        public BindingSource bindingSource;

        public String connectionString;

        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age = 0;

        public ClubRegistrationQuery()
        {
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ECHO\\source\\repos\\LabSQL\\LabSQL\\ClubDB.mdf;Integrated Security=True";

            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }
        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";

            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }
        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
        public bool UpdateStudentInfo(string studentID, string updatedFirstName, string updatedLastName, string updatedMiddleName, int updatedAge, string updatedGender, string updatedProgram)
        {
            sqlConnect.Open();
            string query = "UPDATE ClubMembers SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = @StudentId";
            SqlCommand cmd = new SqlCommand(query, sqlConnect);
            cmd.Parameters.AddWithValue("@FirstName", updatedFirstName);
            cmd.Parameters.AddWithValue("@LastName", updatedLastName);
            cmd.Parameters.AddWithValue("@MiddleName", updatedMiddleName);
            cmd.Parameters.AddWithValue("@Age", updatedAge);
            cmd.Parameters.AddWithValue("@Gender", updatedGender);
            cmd.Parameters.AddWithValue("@Program", updatedProgram);
            cmd.Parameters.AddWithValue("@StudentId", studentID);
            cmd.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Student Info is successfully updated!");
            return true;
        }
    }
}
