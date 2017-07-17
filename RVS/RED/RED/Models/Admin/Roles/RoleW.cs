using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.Admin.Roles
{
    public class RoleW
    {
        public RoleW()
        {
            Connections = new HashSet<RolesFeature>();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името на ролята е задължително.")]
        [Display(Name = "Име")]
        public string DisplayName { get; set; }

        public ICollection<RolesFeature> Connections { get; set; }

        public Role ToBase()
        {
            var role = new Role();
            role.Id = this.Id;
            role.DisplayName = this.DisplayName;

            return role;
        }
    }
}