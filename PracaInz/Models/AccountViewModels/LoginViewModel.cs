using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamietać?")]
        public bool RememberMe { get; set; }
    }
}
