using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace RED.Models.UserExtensions
{
    interface ICustomPrincipal : IPrincipal
    {
        Guid Id { get; set; }
        string Username { get; set; }
        bool IsInRole(string roleName);
        bool HasFeature(string featureName);
    }
}