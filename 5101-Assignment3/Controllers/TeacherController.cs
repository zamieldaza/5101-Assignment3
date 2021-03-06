﻿using System;
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
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFName, string TeacherLName, string TeacherEmployeeNumber, string TeacherHireDate, decimal TeacherSalary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.FirstName = TeacherFName;
            NewTeacher.LastName = TeacherLName;
            NewTeacher.EmployeeNumber = TeacherEmployeeNumber;
            NewTeacher.HireDate = TeacherHireDate;
            NewTeacher.Salary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //GET: /Teacher/Update/{id}
        [HttpGet]
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher TeacherToUpdate = controller.FindTeacher(id);

            return View(TeacherToUpdate);
        }

        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFName, string TeacherLName, string TeacherEmployeeNumber, decimal TeacherSalary)
        {
            Teacher TeacherUpdate = new Teacher();
            TeacherUpdate.FirstName = TeacherFName;
            TeacherUpdate.LastName = TeacherLName;
            TeacherUpdate.EmployeeNumber = TeacherEmployeeNumber;
            TeacherUpdate.Salary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherUpdate);

            return RedirectToAction("Show/"+id);
        }
    }
}