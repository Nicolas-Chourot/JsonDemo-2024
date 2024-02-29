using MDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace JsonDemo.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Sigle"), Required(ErrorMessage = "Obligatoire")]
        public string Code { get; set; }

        [Display(Name = "Titre"), Required(ErrorMessage = "Obligatoire")]
        public string Title { get; set; }

        [JsonIgnore]
        public string Caption
        {
            get { return Code + " " + Title; }
        }
        [JsonIgnore]
        public List<Registration> Registrations { get { return DB.Registrations.ToList().Where(r => r.CourseId == Id).ToList(); } }

        [JsonIgnore]
        public List<Student> Students
        {
            get
            {
                var students = new List<Student>();
                foreach (var registration in Registrations.OrderBy(r => r.Student.LastName).ThenBy(r => r.Student.FirstName))
                {
                    students.Add(registration.Student);
                }
                return students;
            }
        }
        [JsonIgnore]
        public SelectList StudentsSelectList { get { return SelectListUtilities<Student>.Convert(Students.ToList(), "Caption"); } }

        public void DeleteAllRegistrations()
        {
            foreach (Registration registration in Registrations)
            {
                DB.Registrations.Delete(registration.Id);
            }
        }
        public void UpdateRegistrations(List<int> selectedStudentsId)
        {
            DeleteAllRegistrations();
            if (selectedStudentsId != null)
                foreach (int studentId in selectedStudentsId)
                {
                    DB.Registrations.Add(new Registration { StudentId = studentId, CourseId = Id });
                }
        }

    }
}