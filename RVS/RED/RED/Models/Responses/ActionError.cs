using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.Responses
{
    public class ActionError
    {
        public string ErrorText { get; set; }

        public ErrorTypes ErrorType { get; set; }

        public ActionError()
        {

        }

        public ActionError(string errorMsg, ErrorTypes errorType)
            : this()
        {
            this.ErrorText = errorMsg;
            this.ErrorType = errorType;
        }
    }
}