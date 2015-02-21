using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace RED.Models.Account
{
    public class RvsPrincipal : GenericPrincipal
    {
        private Guid GetRoleId()
        {
            var context = DbContextFactory.GetDbContext();
            var user = context.Users.FirstOrDefault(u => u.Username == this.Identity.Name);

            if (user != null)
            {
                return user.RoleId ?? Guid.Empty;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public RvsPrincipal(IIdentity identity)
            : base(identity, null)
        {

        }

        public override bool IsInRole(string role)
        {
            var context = DbContextFactory.GetDbContext();
            var user = context.Users.FirstOrDefault(u => u.Username == Identity.Name);

            if (user != null)
            {
                if (user.RoleId.ToString() == role)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasFeature(Guid featureId)
        {
            var context = DbContextFactory.GetDbContext();
            
            if (context.RolesFeatures.Any(f => f.RoleId == GetRoleId() && f.FeatureId == featureId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}