﻿using MDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsonDemo.Models
{
public class StudentsRepository : Repository<Student>
{
    public bool Update(Student student, List<int> selectedCoursesId)
    {
        BeginTransaction();
        var result = base.Update(student);
        if (result) student.UpdateRegistrations(selectedCoursesId);
        EndTransaction();
        return result;
    }
    public override bool Delete(int Id)
    {
        Student student = DB.Students.Get(Id);
        if (student != null)
        {
            BeginTransaction();
            student.DeleteAllRegistrations();
            var result = base.Delete(Id);
            EndTransaction();
            return result;
        }
        return false;
    }
    [JsonIgnore]
    public SelectList ToSelectList
    {
        get
        {
            return SelectListUtilities<Student>.Convert(ToList(), "Caption");
        }
    }
}
}