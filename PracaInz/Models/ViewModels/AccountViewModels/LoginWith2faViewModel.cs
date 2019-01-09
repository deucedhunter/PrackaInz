using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models.ViewModels.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Kod potwierdzający")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Zapamiętaj to urządzenie")]
        public bool RememberMachine { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}
