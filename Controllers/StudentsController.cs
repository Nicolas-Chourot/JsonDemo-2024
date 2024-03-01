﻿using JsonDemo.Models;
using MDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsonDemo.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult Index()
        {
            return View(DB.Students.ToList().OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }
        public ActionResult Details(int id)
        {
            return View(DB.Students.Get(id));
        }
        public ActionResult Create()
        {
            return View(new Student());
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                DB.Students.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public ActionResult Edit(int id)
        {
            Student student = DB.Students.Get(id);
            if (student != null)
            {
                return View(DB.Students.Get(id));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Student student, List<int> selectedCoursesId)
        {
            if (ModelState.IsValid)
            {
                DB.Students.Update(student, selectedCoursesId);
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public ActionResult Delete(int id)
        {
            DB.Students.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
