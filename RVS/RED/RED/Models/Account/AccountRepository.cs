﻿using System;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using RED.Models.Responses;

namespace RED.Models.Account
{
    public class AccountRepository : RepositoryBase
    {
        public ActionResponse Authenticate(string username, string password)
        {
            ActionResponse response = new ActionResponse();

            try
            {
                var context = DbContextFactory.GetDbContext();
                bool hasAny = context.Users.Any(u => u.Username == username && u.Password == password);

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