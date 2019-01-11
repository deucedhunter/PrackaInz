using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Grade
    {
        public int GradeID { get; set; }


        [Range(2.0, 5.0), RegularExpression(@"^\d+\.\d{0,1}$")]
        [Display(Name = "Ocena")]
        public decimal Value { get; set; }

        public int? StudentID { get; set; }
        public virtual Student Student { get; set; }


        public int? EmployeerID { get; set; }
        public virtual Employee Employeer { get; set; }


        public int? CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}
