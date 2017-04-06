using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;

namespace RED.Models.Account
{
    public class RvsPrincipal : GenericPrincipal
    {
        private readonly RvsDbContext Db;

        public RvsPrincipal(IIdentity identity, IRvsContextFactory factory)
            : base(identity, null)
        {
            Db = factory.CreateConcrete();
        }

        public override bool IsInRole(string role)
        {
            //DO NOT USE THIS METHOD
            var user = Db.Users.FirstOrDefault(u => u.Username == Identity.Name);

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
            var roleId = GetRoleId();
            if (Db.RolesFeatures.Any(f => f.RoleId == roleId && f.FeatureId == featureId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAuthorize(string featureId = null)
        {
            try
            {
                if (!IsGod())
                {
                    if (featureId == null || HasFeature(Guid.Parse(featureId)) == false)
                    {
                        return false;
                    }
                 }
            }
            catch (Exception ex)
            {
                var exception = new Exception("There was a problem with filtering " + HttpContext.Current.User.Identity.Name + "'s role. See inner exception for more details", ex);
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
                return false;
            }

            return true;
        }

        public User GetUserData()
        {
            var user = Db.Users.FirstOrDefault(u => u.Username == Identity.Name);
            if (user == null)
            {
                user = new User();
                user.FirstName = "Unknown";
            }

            return user;
        }

        public Guid GetId()
        {
            var user = Db.Users.FirstOrDefault(u => u.Username == Identity.Name);
            if (user == null)
            {
                return Guid.Empty;
            }

            return user.Id;
        }

        private bool IsGod()
        {
            var user = Db.Users.FirstOrDefault(u => u.Username == Identity.Name);
            if (user != null)
            {
                if (user.Id.ToString() == "613b0faa-8828-44a9-8bbe-09ba68cc33ae")
                {
                    return true;
                }
            }

            return false;
        }

        private Guid GetRoleId()
        {
            var user = Db.Users.FirstOrDefault(u => u.Username == this.Identity.Name);
            if (user != null)
            {
                return user.RoleId;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}