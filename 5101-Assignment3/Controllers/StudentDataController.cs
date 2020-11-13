using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using _5101_Assignment3.Models;
using MySql.Data.MySqlClient;

namespace _5101_Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        //Create an instance of the database context class to access the school database
        private SchoolDBContext School = new SchoolDBContext();

        /// <summary>
        /// Returns a list of students from the database
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of students
        /// </returns>
        [HttpGet]
        public List<Student> ListStudents()
        {
            //Connect to the database and get a Result Set using a SQL Query
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            //SQL Query - Gets all the students from the "students" table
            cmd.CommandText = "SELECT * FROM students";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students
            List<Student> Students = new List<Student> { };

            //Read each row from the Result Set
            while (ResultSet.Read())
            {
                //Access each column's information and assign it to new variables
                uint StudentID = (uint)ResultSet["studentid"];
                string StudentFirstName = ResultSet["studentfname"].ToString();
                string StudentLastname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                //Split the enrollment date information to get rid of the time
                string[] SplitEnrollmentDate = ResultSet["enroldate"].ToString().Split(' ');
                string StudentEnrollmentDate = SplitEnrollmentDate[0];

                //Create an instance of a Student and assign the above variables to its fields
                Student NewStudent = new Student();
                NewStudent.StudentID = StudentID;
                NewStudent.FirstName = StudentFirstName;
                NewStudent.LastName = StudentLastname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollmentDate = StudentEnrollmentDate;

                //Add the current Student to the list of Students
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer once the loop ends
            Connection.Close();

            //Return the list of students
            return Students;
        }

        /// <summary>
        /// Returns a Student from the database given their unique identifier {id}
        /// </summary>
        /// <example>GET api/StudentData/FindStudent/{id}</example>
        /// <param name="id">The student's unique identifier in the "students" table</param>
        /// <returns>
        /// A student instance with the information from the student found using {id}
        /// </returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            //Create an instance of a Student
            Student NewStudent = new Student();

            //Connect to the database and get a Result Set using a SQL Query
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            //SQL QUERY - Gets a student from the "students" table using "studentid" as a matching condition with
            //the {id} given to this method
            cmd.CommandText = "SELECT * FROM students WHERE studentid = " + id;
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Read each row from the Result Set
            while (ResultSet.Read())
            {
                //Access each column's information and assign it to new variables
                uint StudentID = (uint)ResultSet["studentid"];
                string StudentFirstName = ResultSet["studentfname"].ToString();
                string StudentLastname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                //Split the enrollment date information to get rid of the time
                string[] SplitEnrollmentDate = ResultSet["enroldate"].ToString().Split(' ');
                string StudentEnrollmentDate = SplitEnrollmentDate[0];

                //Assign the above variables to the fields of NewStudent
                NewStudent.StudentID = StudentID;
                NewStudent.FirstName = StudentFirstName;
                NewStudent.LastName = StudentLastname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollmentDate = StudentEnrollmentDate;
            }

            //Close the connection between the MySQL Database and the WebServer once the loop ends
            Connection.Close();

            //Return the Student
            return NewStudent;
        }
    }
}
