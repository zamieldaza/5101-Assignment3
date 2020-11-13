using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5101_Assignment3.Models;

namespace _5101_Assignment3.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Class/List
        public ActionResult List()
        {
            ClassDataController Controller = new ClassDataController();
            IEnumerable<Class> Classes = Controller.ListClasses();
            return View(Classes);
        }

        // GET: /Class/Show/{id}
        public ActionResult Show(int id)
        {
            ClassDataController Controller = new ClassDataController();
            Class NewClass = Controller.FindClass(id);
            return View(NewClass);
        }
    }
}