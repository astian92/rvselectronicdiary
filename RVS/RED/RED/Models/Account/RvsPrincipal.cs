using System;
using System.Linq;
using System.Web;
using RED.Models.Admin.Users;

namespace RED.Models.Account
{
    public static class RvsPrincipal
    {
        public static readonly Guid MasterGuid = new Guid("613b0faa-8828-44a9-8bbe-09ba68cc33ae");
        public static readonly Guid SuperUserGuid = new Guid("0f68da69-5c82-480b-9474-54c133439b0c");

        public static UserW User
        {
            get
            {
                var _user = HttpContext.Current.Session["user"] as UserW;
                return _user;
            }
        }

        public static bool IsAuthorize(string featureId = null)
        {
            var _user = HttpContext.Current.Session["user"] as UserW;
            try
            {
                if (!_user.Id.Equals(MasterGuid))
                {
                    var featureGuid = Guid.Parse(featureId);
                    if (string.IsNullOrEmpty(featureId) || !_user.Role.RolesFeatures.Any(f => f.RoleId == _user.RoleId && f.FeatureId == featureGuid))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = new Exception("There was a problem with filtering " + _user.Username + "'s role. See inner exception for more details", ex);
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
                return false;
            }

            return true;
        }

        public static bool IsSuperUser()
        {
            var _user = HttpContext.Current.Session["user"] as UserW;
            if (_user.Id.Equals(SuperUserGuid) || _user.Id.Equals(MasterGuid))
            {
                return true;
            }

            return false;
        }
    }
}