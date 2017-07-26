using System;
using System.Linq;
using RED.Mappings;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;
using RED.Models.Responses;
using RED.Repositories.Abstract;

namespace RED.Models.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RvsDbContext Db;

        public AccountRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public ActionResponse Authenticate(string username, string password)
        {
            var response = new ActionResponse();
            try
            {
                if (Db.Users.Any(u => u.Username == username && u.Password == password))
                {
                    var user = Db.Users.Where(u => u.Username == username && u.Password == password)
                                       .Select(UserMappings.ToUserW)
                                       .FirstOrDefault();

                    response.ResponseObject = user;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Error = ErrorFactory.InvalidUsernameOrPassword;
                }
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                response.Error = ErrorFactory.ConnectingToDatabaseFailure;
            }

            return response;
        }
    }
}