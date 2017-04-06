using RED.Models.Responses;

namespace RED.Repositories.Abstract
{
    public interface IAccountRepository
    {
        ActionResponse Authenticate(string username, string password);
    }
}