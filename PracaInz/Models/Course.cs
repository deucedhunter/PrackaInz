using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Display(Name = "Kurs")]
        public string FullName { get; set; }

        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public Enrollment Enrollment { get; set; }
        public IEnumerable<Presence> Presence { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
    }
}
