using System;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;

namespace RED.Repositories.Abstract
{
    public interface IClientsRepository
    {
        void Add(ClientW clientW);

        bool Delete(Guid id);

        void Edit(ClientW clientW);

        ClientW GetClient(Guid id);

        IQueryable<Client> GetClients();

        bool IsExisting(ClientW client);
    }
}