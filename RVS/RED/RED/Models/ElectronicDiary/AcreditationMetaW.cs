using System;
using System.ComponentModel.DataAnnotations;

namespace RED.Models.ElectronicDiary
{
    public class AcreditationMetaW
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Регистриран")]
        public DateTime Registered { get; set; }

        [Required]
        [Display(Name = "Валиден до")]
        public DateTime ValidTo { get; set; }
    }
}