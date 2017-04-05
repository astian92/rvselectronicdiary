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

        }

        public RoleW(Role dbRole)
        {
            this.Id = dbRole.Id;
            this.DisplayName = dbRole.DisplayName;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името на ролята е задължително.")]
        [Display(Name = "Име")]
        public string DisplayName { get; set; }

        public List<RolesFeature> Connections = new List<RolesFeature>();

        public Role ToBase()
        {
            var dbRole = new Role();
            dbRole.Id = this.Id;
            dbRole.DisplayName = this.DisplayName;

            return dbRole;
        }
    }
}