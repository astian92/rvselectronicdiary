namespace RED.Models.Responses
{
    public class ActionError
    {
        public ActionError()
        {
        }

        public ActionError(string errorMsg, ErrorTypes errorType)
            : this()
        {
            this.ErrorText = errorMsg;
            this.ErrorType = errorType;
        }

        public string ErrorText { get; set; }

        public ErrorTypes ErrorType { get; set; }
    }
}