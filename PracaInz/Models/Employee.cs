using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [DataType(DataType.Date), Display(Name = "Data Zatrudnienia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Nauczyciel?")]
        public bool isTeacher { get; set; }

        public virtual Person Person { get; set; }


        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Presence> Presence { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
    }
}
