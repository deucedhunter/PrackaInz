using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Display(Name = "Numer opiekuna")]
        public string GudrdianPhoneNumber { get; set; }


        public virtual Person Person { get; set; }


        public int? ClassID { get; set; }
        public virtual Class Class { get; set; }


        public IEnumerable<Presence> Presence { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
    }
}
