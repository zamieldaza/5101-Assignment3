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
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFirstName = ResultSet["teacherfname"].ToString();
                string TeacherLastname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                string TeacherHireDate = ResultSet["hiredate"].ToString();
                string TeacherSalary = ResultSet["salary"].ToString();

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherID = TeacherID;
                NewTeacher.FirstName = TeacherFirstName;
                NewTeacher.LastName = TeacherLastname;
                NewTeacher.EmployeeNumber = TeacherEmployeeNumber;
                NewTeacher.HireDate = TeacherHireDate;
                NewTeacher.Salary = TeacherSalary;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            //Return the final list of author names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFirstName = ResultSet["teacherfname"].ToString();
                string TeacherLastname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                string TeacherHireDate = ResultSet["hiredate"].ToString();
                string TeacherSalary = ResultSet["salary"].ToString();

                NewTeacher.TeacherID = TeacherID;
                NewTeacher.FirstName = TeacherFirstName;
                NewTeacher.LastName = TeacherLastname;
                NewTeacher.EmployeeNumber = TeacherEmployeeNumber;
                NewTeacher.HireDate = TeacherHireDate;
                NewTeacher.Salary = TeacherSalary;
            }

            //Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            return NewTeacher;
        }
    }
}
