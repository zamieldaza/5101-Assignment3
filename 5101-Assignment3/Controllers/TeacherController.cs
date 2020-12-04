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

        // GET: /Teacher/List?SearchKey={SearchKey}
        public ActionResult List(string SearchKey=null)
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewAuthor = controller.FindTeacher(id);
            return View(NewAuthor);
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
    }
}