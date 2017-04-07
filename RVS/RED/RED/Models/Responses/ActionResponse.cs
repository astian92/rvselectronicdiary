namespace RED.Models.Responses
{
    public class ActionResponse
    {
        public ActionResponse()
        {
            this.Error = new ActionError();
            this.IsSuccess = false;
        }

        public bool IsSuccess { get; set; }

        public string SuccessMsg { get; set; }

        public ActionError Error { get; set; }

        public object ResponseObject { get; set; }
    }
}