using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace RED.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(100, ErrorMessage = "Потребителското име трябва да бъде поне 5 символа.", MinimumLength = 5)]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, ErrorMessage = "Паролата трябва да бъде поне 6 символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(100, ErrorMessage = "Потребителското име трябва да бъде поне 5 символа.", MinimumLength = 5)]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, ErrorMessage = "Паролата трябва да бъде поне 6 символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Потвърждаването на паролата е задължително.")]
        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        [Compare("Password", ErrorMessage = "Паролата и потвърждението и не съвпадат!")]
        public string ConfirmPassword { get; set; }
    }
}
