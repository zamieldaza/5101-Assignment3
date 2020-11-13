using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5101_Assignment3.Models;

namespace _5101_Assignment3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Student/List
        public ActionResult List()
        {
            StudentDataController Controller = new StudentDataController();
            IEnumerable<Student> Students = Controller.ListStudents();
            return View(Students);
        }

        // GET: /Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();
            Student NewStudent = Controller.FindStudent(id);
            return View(NewStudent);
        }
    }
}