using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models.ViewModels.ManageViewModels
{
    public class ShowRecoveryCodesViewModel
    {

        [Display(Name = "Kody odzyskiwania")]
        public string[] RecoveryCodes { get; set; }
    }
}
