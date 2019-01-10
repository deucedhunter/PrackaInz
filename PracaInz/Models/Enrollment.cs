using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        [StringLength(100), Display(Name = "Krótki opis")]
        public string ShortDescription { get; set; }

        [StringLength(500), Display(Name = "Długi opis")]
        public string LongDescription { get; set; }

        public int ClassID { get; set; }
        public Class Class { get; set; }


        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
