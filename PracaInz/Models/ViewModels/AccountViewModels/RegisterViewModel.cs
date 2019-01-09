using System;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła są różne.")]
        public string ConfirmPassword { get; set; }


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
    }
}
