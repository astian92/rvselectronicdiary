using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.Admin.Roles
{
    public class RoleW
    {
        public Guid Id { get; set; }

        [Display(Name="Име")]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; }

        public RoleW()
        {

        }

        public RoleW(Role dbRole)
        {
            this.Id = dbRole.Id;
            this.DisplayName = dbRole.DisplayName;
        }

        public Role ToBase()
        {
            var dbRole = new Role();
            dbRole.Id = this.Id;
            dbRole.DisplayName = this.DisplayName;

            return dbRole;
        }
    }
}