using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracaInz.Models
{
    public class Person
    {
        public int Id { get; set; }


        [Required, StringLength(60), Display(Name = "Imiona")]
        public string FirstMidName { get; set; }

        [Required, StringLength(30), Display(Name = "Naziwsko")]
        public string LastName { get; set; }

        [DataType(DataType.Date), Display(Name = "Data Urodzenia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Pesel { get; set; }

        [Display(Name = "Imię i Nazwisko")]
        public string FullName
        {
            get
            {
                return FirstMidName + " " + LastName;
            }
        }

        public int ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int? StudentID { get; set; }
        public virtual Student Student { get; set; }



        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
