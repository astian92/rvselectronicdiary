using System;
using System.Linq;
using RED.Models.Responses;
using RED.Repositories.Abstract;
using RED.Models.DataContext.Abstract;
using RED.Models.DataContext;

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
                bool hasAny = Db.Users.Any(u => u.Username == username && u.Password == password);

                if (hasAny)
                {
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