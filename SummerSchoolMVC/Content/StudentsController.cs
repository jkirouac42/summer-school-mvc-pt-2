﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SummerSchoolMVC.Models;

namespace SummerSchoolMVC.Content
{
    public class StudentsController : Controller
    {
        private SummerSchoolDBEntities db = new SummerSchoolDBEntities();

        // GET: Students
        public ActionResult Index()
        {
            ViewBag.TotalFees = TotalEnrollmentFees();
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,")] Student student)
        {
      

             int enrollmentCount = db.Students.Count();


            if (student.LastName.ToLower() == "potter")
            {
                student.EnrollmentFee = 100;
            }

            if (student.LastName.ToLower() == "longbottom")
            {
                student.EnrollmentFee = 0;
                if (enrollmentCount <= 10)
                {
                    student.EnrollmentFee = 200;
                }
            }
            if (student.LastName.ToLower() == "malfoy")
            {
                return View("Malfoy");
            }
            if (student.LastName.ToLower() == "riddle")
            {
                student.EnrollmentFee = 200;
                return View("Riddle");
            }
            if (student.LastName.ToLower() == "voldemort")
            {
                student.EnrollmentFee = 200;
                return View("Voldemort");
            }

            if (student.FirstName.First() == student.LastName.First())
            {
                student.EnrollmentFee = 180;
            }

            else {
                student.EnrollmentFee = 200;
            }

            





            if (ModelState.IsValid)
                {
                  db.Students.Add(student);
                 db.SaveChanges();
                 return RedirectToAction("Index");
                }

            return View(student);
        }

         public decimal TotalEnrollmentFees()
        { decimal totalFees;
            totalFees = (from student in db.Students
                         select student.EnrollmentFee).Sum();
            
            return totalFees;
            
        }

       
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
            
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,EnrollmentFee")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}
