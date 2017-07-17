using System;
using System.Collections.Generic;
using System.Linq;
using RED.Models.Account;
using RED.Models.Admin.Roles;
using RED.Models.Admin.Users;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;
using RED.Repositories.Abstract;

namespace RED.Repositories.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RvsDbContext Db;

        public AdminRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public Role GetBaseRole(Guid id)
        {
            return Db.Roles.FirstOrDefault(r => r.Id == id);
        }

        public RoleW GetRole(Guid id)
        {
            var roleW = Db.Roles.Where(r => r.Id == id).Select(x => new RoleW() { Id = x.Id, DisplayName = x.DisplayName, Connections = x.RolesFeatures }).FirstOrDefault();
            return roleW;
        }

        public IEnumerable<RoleW> GetRoles()
        {
            var roles = Db.Roles.Select(x => new RoleW() { Id = x.Id, DisplayName = x.DisplayName });
            return roles;
        }

        public void AddRole(RoleW role, string[] features)
        {
            role.Id = Guid.NewGuid();
            Db.Roles.Add(role.ToBase());
            MakeRoleFeatureConnections(role.Id, features);

            Db.SaveChanges();
        }

        public void EditRole(RoleW roleW, string[] features)
        {
            var role = this.GetBaseRole(roleW.Id);
            role.DisplayName = roleW.DisplayName;
            MakeRoleFeatureConnections(role.Id, features);

            Db.SaveChanges();
        }

        public bool DeleteRole(Guid id)
        {
            var role = this.GetBaseRole(id);
            Db.Roles.Remove(role);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public User GetBaseUser(Guid id)
        {
            return Db.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserW GetUser(Guid id)
        {
            var user = Db.Users.Where(x => x.Id == id)
                               .Select(x => new UserW()
                                   {
                                       Id = x.Id,
                                       Username = x.Username,
                                       Password = x.Password,
                                       FirstName = x.FirstName,
                                       MiddleName = x.MiddleName,
                                       LastName = x.LastName,
                                       Position = x.Position,
                                       RoleId = x.RoleId,
                                       Role = x.Role
                                   })
                               .FirstOrDefault();
            return user;
        }

        public IEnumerable<UserW> GetUsers()
        {
            var users = Db.Users.Where(x => x.Id.ToString() != RvsPrincipal.MasterId && x.Id.ToString() != RvsPrincipal.SuperUserId)
                .Select(x => new UserW()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Password = x.Password,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        LastName = x.LastName,
                        Position = x.Position,
                        RoleId = x.RoleId,
                        Role = x.Role
                    })
                .OrderBy(u => u.Username);

            return users;
        }

        public void AddUser(UserW user)
        {
            user.Id = Guid.NewGuid();
            Db.Users.Add(user.ToBase());
            Db.SaveChanges();
        }

        public void EditUser(UserW userW)
        {
            var user = this.GetBaseUser(userW.Id);
            user.Username = userW.Username;
            user.Password = userW.Password;
            user.FirstName = userW.FirstName;
            user.MiddleName = userW.MiddleName;
            user.LastName = userW.LastName;
            user.Position = userW.Position;
            user.RoleId = userW.RoleId;
            user.Role = userW.Role;

            Db.SaveChanges();
        }

        public bool DeleteUser(Guid id)
        {
            var user = GetBaseUser(id);
            Db.Users.Remove(user);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            return Db.Features.OrderBy(x => x.DisplayName);
        }

        private void MakeRoleFeatureConnections(Guid roleId, string[] features)
        {
            if (features.Count() > 0)
            {
                Db.RolesFeatures.RemoveRange(Db.RolesFeatures.Where(x => x.RoleId == roleId));

                foreach (var item in features)
                {
                    bool isParseable = false;
                    Guid featureId;
                    isParseable = Guid.TryParse(item, out featureId);
                    if (isParseable)
                    {
                        RolesFeature con = new RolesFeature();
                        con.RoleId = roleId;
                        con.FeatureId = featureId;
                        Db.RolesFeatures.Add(con);
                    }
                }

                Db.SaveChanges();
            }
        }
    }
}