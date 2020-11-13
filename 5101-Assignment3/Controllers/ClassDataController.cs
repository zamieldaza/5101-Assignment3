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
    public class ClassDataController : ApiController
    {
        //Create an instance of the database context class to access the school database
        private SchoolDBContext School = new SchoolDBContext();

        /// <summary>
        /// Returns a list of classes from the database
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of classes
        /// </returns>
        [HttpGet]
        public List<Class> ListClasses()
        {
            //Connect to the database and get a Result Set using a SQL Query
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            //SQL Query - Gets all the classes from the "classes" table
            cmd.CommandText = "SELECT * FROM classes";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            List<Class> Classes = new List<Class> { };

            //Read each row from the Result Set
            while (ResultSet.Read())
            {
                //Access each column's information and assign it to new variables
                int ClassID = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                long TeacherID = (long)ResultSet["teacherid"];
                //Split the start and finish dates information to get rid of the time
                string[] SplitStartDate = ResultSet["startdate"].ToString().Split(' ');
                string ClassStartDate = SplitStartDate[0];
                string[] SplitFinishDate = ResultSet["finishdate"].ToString().Split(' ');
                string ClassFinishDate = SplitFinishDate[0];

                //Create an instance of a Class and assign the above variables to its fields
                Class NewClass = new Class();
                NewClass.ClassID = ClassID;
                NewClass.Code = ClassCode;
                NewClass.Name = ClassName;
                NewClass.StartDate = ClassStartDate;
                NewClass.FinishDate = ClassFinishDate;
                NewClass.TeacherID = TeacherID;

                //Add the current Class to the list of Classes
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer once the loop ends
            Connection.Close();

            //Return the list of classes
            return Classes;
        }

        /// <summary>
        /// Returns a class from the database given its unique identifier {id}
        /// </summary>
        /// <example>GET api/ClassData/FindClass/{id}</example>
        /// <param name="id">The class' unique identifier in the "classes" table</param>
        /// <returns>
        /// A class instance with the information from the class found using {id}
        /// </returns>
        [HttpGet]
        public Class FindClass(int id)
        {
            //Create an instance of a Class
            Class NewClass = new Class();

            //Connect to the database and get a Result Set using a SQL Query
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            //SQL QUERY - Gets a class from the "classes" table using "classid" as a matching condition with
            //the {id} given to this method
            cmd.CommandText = "SELECT * FROM classes WHERE classid = " + id;
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Read each row from the Result Set
            while (ResultSet.Read())
            {
                //Access each column's information and assign it to new variables
                int ClassID = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                long TeacherID = (long)ResultSet["teacherid"];
                //Split the start and finish dates information to get rid of the time
                string[] SplitStartDate = ResultSet["startdate"].ToString().Split(' ');
                string ClassStartDate = SplitStartDate[0];
                string[] SplitFinishDate = ResultSet["finishdate"].ToString().Split(' ');
                string ClassFinishDate = SplitFinishDate[0];

                //Assign the above variables to the fields of NewClass
                NewClass.ClassID = ClassID;
                NewClass.Code = ClassCode;
                NewClass.Name = ClassName;
                NewClass.StartDate = ClassStartDate;
                NewClass.FinishDate = ClassFinishDate;
                NewClass.TeacherID = TeacherID;
            }

            //Close the connection between the MySQL Database and the WebServer once the loop ends
            Connection.Close();

            //Return the Class
            return NewClass;
        }
    }
}
