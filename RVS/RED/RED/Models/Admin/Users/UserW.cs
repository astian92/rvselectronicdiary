using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.Admin.Users
{
    public class UserW
    {
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
        public Nullable<Guid> RoleId { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual ICollection<ActionLog> ActionLogs { get; set; }

        public UserW()
        {
            this.ActionLogs = new HashSet<ActionLog>();
        }

        public UserW(User user) : this()
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Password = user.Password;
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.Position = user.Position;
            this.RoleId = user.RoleId;
            this.Role = user.Role;
        }

        public User ToBase()
        {
            User user = new User();

            user.Id = this.Id;
            user.Username = this.Username;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.MiddleName = this.MiddleName;
            user.LastName = this.LastName;
            user.Position = this.Position;
            user.RoleId = this.RoleId;
            user.Role = this.Role;

            return user;
        }
    
    }
}