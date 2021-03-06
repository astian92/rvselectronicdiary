﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.Admin.Users
{
    public class UserW
    {
        public UserW()
        {
            this.ActionLogs = new HashSet<ActionLog>();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(100, ErrorMessage = "Потребителското име трябва да бъде поне 6 символа.", MinimumLength = 5)]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, ErrorMessage = "Паролата трябва да бъде поне 6 символа.", MinimumLength = 6)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Длъжността е задължителна.")]
        [Display(Name = "Длъжност")]
        public string Position { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<ActionLog> ActionLogs { get; set; }

        public User ToBase()
        {
            var user = new User();
            user.Id = Id;
            user.Username = Username;
            user.Password = Password;
            user.FirstName = FirstName;
            user.MiddleName = MiddleName;
            user.LastName = LastName;
            user.Position = Position;
            user.RoleId = RoleId;
            user.Role = Role;

            return user;
        }
    }
}