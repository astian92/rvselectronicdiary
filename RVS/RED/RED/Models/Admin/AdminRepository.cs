using RED.Models.Admin.Roles;
using RED.Models.Admin.Users;
using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using RED.Models.Responses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            RoleW role = new RoleW(dbRole);
            role.Connections = db.RolesFeatures.Where(x => x.RoleId == id).ToList();
            
            return role;
        }

        public IEnumerable<RoleW> GetRoles()
        {
            var roles = db.Roles.ToList();
            return roles.Select(r => new RoleW(r));
        }

        public void AddRole(RoleW role, string[] features)
        {
            role.Id = Guid.NewGuid();
            db.Roles.Add(role.ToBase());
            MakeRoleFeatureConnections(role.Id, features);

            db.SaveChanges();
        }

        public void EditRole(RoleW role, string[] features)
        {
            var dbRole = this.GetBaseRole(role.Id);
            dbRole.DisplayName = role.DisplayName;
            MakeRoleFeatureConnections(dbRole.Id, features);

            db.SaveChanges();
        }

        public bool DeleteRole(Guid id)
        {
            var dbRole = this.GetBaseRole(id);
            db.Roles.Remove(dbRole);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public User GetBaseUser(Guid id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserW GetUser(Guid id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            return new UserW(user);
        }

        public IEnumerable<UserW> GetUsers()
        {
            var users = db.Users.Include(x => x.Role).ToList();
            return users.Select(u => new UserW(u));
        }

        public void AddUser(UserW user)
        {
            user.Id = Guid.NewGuid();
            db.Users.Add(user.ToBase());
            db.SaveChanges();
        }

        public void EditUser(UserW user)
        {
            var dbUser = this.GetBaseUser(user.Id);
            dbUser.Username = user.Username;
            dbUser.Password = user.Password;
            dbUser.FirstName = user.FirstName;
            dbUser.MiddleName = user.MiddleName;
            dbUser.LastName = user.LastName;
            dbUser.Position = user.Position;
            dbUser.RoleId = user.RoleId;
            dbUser.Role = user.Role;

            db.SaveChanges();
        }

        public bool DeleteUser(Guid id)
        {
            User user = this.GetBaseUser(id);
            db.Users.Remove(user);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            return db.Features.ToList();
        }

        private void MakeRoleFeatureConnections(Guid roleId, string[] features)
        {
            if (features.Count() > 0)
            {
                db.RolesFeatures.RemoveRange(db.RolesFeatures.Where(x => x.RoleId == roleId));

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
                        db.RolesFeatures.Add(con);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}