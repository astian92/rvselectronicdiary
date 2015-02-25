using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.Responses
{
    public enum ErrorTypes
    {
        UnkownError = 0,
        InvalidUsernameOrPassword, 
        ConnectingToDatabaseFailure
    }

    public static class ErrorFactory
    {
        public static ActionError UnknownError
        {
            get
            {
                return new ActionError("Възникна грешка", ErrorTypes.UnkownError);
            }
        }

        public static ActionError InvalidUsernameOrPassword
        {
            get
            {
                return new ActionError("Потребителското име или парола са грешни!", ErrorTypes.InvalidUsernameOrPassword);
            }
        }

        public static ActionError ConnectingToDatabaseFailure
        {
            get
            {
                return new ActionError("Грешка при свързване с базата данни", ErrorTypes.ConnectingToDatabaseFailure);
            }
        }
        
    }
}