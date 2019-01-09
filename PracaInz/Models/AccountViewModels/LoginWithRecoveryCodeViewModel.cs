using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Kod odzyskiwania")]
        public string RecoveryCode { get; set; }
    }
}
