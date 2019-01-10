using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        [Display(Name = "Nazwa Przedmiotu")]
        public string Name { get; set; }
        [Display(Name = "Wartość")]
        public bool IsImportant { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
