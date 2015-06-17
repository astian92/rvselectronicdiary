using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.Responses
{
    public class ActionResponse
    {
        public bool IsSuccess { get; set; }

        public string SuccessMsg { get; set; }

        public ActionError Error { get; set; }

        public object ResponseObject { get; set; }

        public ActionResponse()
        {
            this.Error = new ActionError();
            this.IsSuccess = false;
        }
    }
}