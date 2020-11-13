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
    public class TeacherDataController : ApiController
    {
        //Create an instance of the database context class to access the school database
        private SchoolDBContext School = new SchoolDBContext();

        /// <summary>
        /// Returns a list of teachers from the database
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers
        /// </returns>
        [HttpGet]
        public List<Teacher> ListTeachers()
        {
            //Connect to the database and get a Result Set using a SQL Query
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            //SQL Query - Gets all the teachers from the "teachers" table
            cmd.CommandText = "SELECT * FROM teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Read each row from the Result Set
            while (ResultSet.Read())
            {
                //Access each column's information and assign it to new variables
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFirstName = ResultSet["teacherfname"].ToString();
                string TeacherLastname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                //Split the hire date information to get rid of the time
                string[] SplitHireDate = ResultSet["hiredate"].ToString().Split(' ');
                string TeacherHireDate = SplitHireDate[0];
                string TeacherSalary = ResultSet["salary"].ToString();

                //Create an instance of a Teacher and assign the above variables to its fields
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherID = TeacherID;
                NewTeacher.FirstName = TeacherFirstName;
                NewTeacher.LastName = TeacherLastname;
                NewTeacher.EmployeeNumber = TeacherEmployeeNumber;
                NewTeacher.HireDate = TeacherHireDate;
                NewTeacher.Salary = TeacherSalary;

                //Add the current Teacher to the list of Teachers
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer once the loop ends
            Connection.Close();

            //Return the list of teachers
            return Teachers;
        }

        /// <summary>
        /// Returns a teacher from the database given their unique identifier {id}
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher/10</example>
        /// <param name="id">The teacher's unique identifier in the "teachers" table</param>
        /// <returns>
        /// A teacher instance with the informations from the teacher found using {id}
        /// </returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            //Create an instance of a Teacher
            Teacher NewTeacher = new Teacher();

            //Connect to the database and get a Result Set using a SQL Query
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            //SQL QUERY - Gets a teacher from the "teachers" table using "teacherid" as a matching condition with
            //the {id} given to this method
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = " + id;
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Read the row from the Result Set
            while (ResultSet.Read())
            {
                //Access each column's information and assign it to new variables
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFirstName = ResultSet["teacherfname"].ToString();
                string TeacherLastname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                //Split the hire date information to get rid of the time
                string[] SplitHireDate = ResultSet["hiredate"].ToString().Split(' ');
                string TeacherHireDate = SplitHireDate[0];
                string TeacherSalary = ResultSet["salary"].ToString();

                //Assign the above variables to the fields of NewTeacher
                NewTeacher.TeacherID = TeacherID;
                NewTeacher.FirstName = TeacherFirstName;
                NewTeacher.LastName = TeacherLastname;
                NewTeacher.EmployeeNumber = TeacherEmployeeNumber;
                NewTeacher.HireDate = TeacherHireDate;
                NewTeacher.Salary = TeacherSalary;
            }

            //Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            //Return the Teacher
            return NewTeacher;
        }
    }
}
