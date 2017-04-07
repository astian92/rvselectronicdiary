using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RED.Models.Admin.Roles;
using RED.Models.Admin.Users;
using RED.Models.DataContext;
using RED.Repositories.Abstract;
using RED.Models.DataContext.Abstract;
using RED.Models.Account;

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
            var role = Db.Roles.FirstOrDefault(r => r.Id == id);
            var roleW = new RoleW(role);
            roleW.Connections = Db.RolesFeatures.Where(x => x.RoleId == id).ToList();
            
            return roleW;
        }

        public IEnumerable<RoleW> GetRoles()
        {
            var roles = Db.Roles.ToList();
            return roles.Select(r => new RoleW(r));
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
            var user = Db.Users.FirstOrDefault(x => x.Id == id);
            return new UserW(user);
        }

        public IEnumerable<UserW> GetUsers()
        {
            var users = Db.Users.Where(x => x.Id.ToString() != RvsPrincipal.MasterId && x.Id.ToString() != RvsPrincipal.SuperUserId).Include(x => x.Role)
                .OrderBy(u => u.Username)
                .ToList();
            return users.Select(u => new UserW(u));
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
            User user = this.GetBaseUser(id);
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
            return Db.Features.OrderBy(x => x.DisplayName).ToList();
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