using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Class
    {
        public int ClassID { get; set; }

        [StringLength(5)]
        [Display(Name = "Klasa")]
        public string Name { get; set; }

        [Display(Name = "Rok")]
        public int Year { get; set; }

        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }

        [Display(Name = "Nazwa")]
        public string FullName
        {
            get
            {
                if (Year > 0)
                {
                    return Year + " " + Name;
                }
                else
                {
                    return "(nieprzypisany)";
                }
            }
        }
    }
}
