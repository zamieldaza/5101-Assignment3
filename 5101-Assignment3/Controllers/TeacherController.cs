using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5101_Assignment3.Models;

namespace _5101_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        public ActionResult List()
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers();
            return View(Teachers);
        }

        // GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);
            return View(NewTeacher);
        }
    }
}