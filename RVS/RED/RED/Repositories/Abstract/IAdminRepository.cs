using System;
using System.Collections.Generic;
using RED.Models.Admin.Roles;
using RED.Models.Admin.Users;
using RED.Models.DataContext;

namespace RED.Repositories.Abstract
{
    public interface IAdminRepository
    {
        void AddRole(RoleW role, string[] features);

        void AddUser(UserW user);

        bool DeleteRole(Guid id);

        bool DeleteUser(Guid id);

        void EditRole(RoleW roleW, string[] features);

        void EditUser(UserW userW);

        Role GetBaseRole(Guid id);

        User GetBaseUser(Guid id);

        IEnumerable<Feature> GetFeatures();

        RoleW GetRole(Guid id);

        IEnumerable<RoleW> GetRoles();

        UserW GetUser(Guid id);

        IEnumerable<object> GetUsers();
    }
}