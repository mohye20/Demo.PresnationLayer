using System.ComponentModel.DataAnnotations;

namespace Demo.PresnationLayer.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm new Password is Required")]
        [Compare("NewPassword",ErrorMessage ="Password Dosen't match")]
        public string ConfirmPassword { get; set; }
    }
}