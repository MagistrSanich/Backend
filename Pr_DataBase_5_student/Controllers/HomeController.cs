using Pr_DataBase_5_student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Pr_DataBase_5_student.Controllers
{
    public class HomeController : Controller
    {
        StudentsContext db = new StudentsContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Details(int id =0)
        {
            Student student = db.Students.Find(id);
            if (student == null) return HttpNotFound();
            return View(student);
        }
        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            Student student = db.Students.Find(id);
            if (student == null) return HttpNotFound();
            ViewBag.Courses = db.Courses.ToList();
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student, int[] SelectedCourses)
        {
            Student newStudent = db.Students.Find(student.Id);
            newStudent.Name = student.Name;
            newStudent.Surname = student.Surname;

            newStudent.Courses.Clear();
            if(SelectedCourses !=null)
            {
                foreach(var c in db.Courses.Where(co=>SelectedCourses.Contains(co.Id)))
                {
                    newStudent.Courses.Add(c);
                }
                 
            }
            db.Entry(newStudent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}