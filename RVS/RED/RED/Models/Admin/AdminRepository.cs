using RED.Models.Admin.Roles;
using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using RED.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.Admin
{
    public class AdminRepository : RepositoryBase
    {
        public Role GetBaseRole(Guid id)
        {
            return db.Roles.FirstOrDefault(r => r.Id == id);
        }

        public RoleW GetRole(Guid id)
        {
            var dbRole = db.Roles.FirstOrDefault(r => r.Id == id);
            return new RoleW(dbRole);
        }

        public IEnumerable<RoleW> GetRoles()
        {
            var roles = db.Roles.ToList();
            return roles.Select(r => new RoleW(r));
        }

        public void AddRole(RoleW role)
        {
            role.Id = Guid.NewGuid();
            db.Roles.Add(role.ToBase());
            db.SaveChanges();
        }

        public void EditRole(RoleW role)
        {
            var dbRole = this.GetBaseRole(role.Id);
            dbRole.DisplayName = role.DisplayName;

            db.SaveChanges();
        }

        public void DeleteRole(Guid id)
        {
            var dbRole = this.GetBaseRole(id);
            db.Roles.Remove(dbRole);

            db.SaveChanges();
        }



    }
}