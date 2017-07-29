using System;
using System.Linq.Expressions;
using RED.Models.Admin.Users;
using RED.Models.DataContext;

namespace RED.Mappings
{
    public static class UserMappings
    {
        public static readonly Expression<Func<User, UserW>> ToUserW =
            entity => new UserW()
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Position = entity.Position,
                RoleId = entity.RoleId
            };

        public static readonly Expression<Func<User, UserW>> ToUserWithRole =
            entity => new UserW()
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Position = entity.Position,
                RoleId = entity.RoleId,
                Role = entity.Role
            };
    }
}