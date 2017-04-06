using System;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;
using RED.Repositories.Abstract;
using RED.Models.DataContext.Abstract;

namespace RED.Repositories.Concrete
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly RvsDbContext Db;

        public ClientsRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public ClientW GetClient(Guid id)
        {
            var client = Db.Clients.Single(c => c.Id == id);
            return new ClientW(client);
        }

        public IQueryable<Client> GetClients()
        {
            var clients = Db.Clients.AsQueryable();
            return clients;
        }
        
        public void Add(ClientW clientW)
        {
            clientW.Id = Guid.NewGuid();
            Db.Clients.Add(clientW.ToBase());

            Db.SaveChanges();
        }

        public void Edit(ClientW clientW)
        {
            var client = Db.Clients.Single(c => c.Id == clientW.Id);
            client.Name = clientW.Name;
            client.Mobile = clientW.Mobile;
            
            Db.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var client = Db.Clients.Single(c => c.Id == id);
            Db.Clients.Remove(client);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                return false;
            }

            return true;
        }

        public bool IsExisting(ClientW client)
        {
            var foundedClient = Db.Clients.FirstOrDefault(x => x.Name.ToLower() == client.Name.ToLower() && x.Id != client.Id);
            if (foundedClient != null)
            {
                return true;
            }

            return false;
        }
    }
}