using System;
using System.ComponentModel.DataAnnotations;

namespace RED.Models.ElectronicDiary
{
    public class AcreditationMetaW
    {
        public int Id { get; set; }

        [Required]
        public DateTime Registered { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }
    }
}