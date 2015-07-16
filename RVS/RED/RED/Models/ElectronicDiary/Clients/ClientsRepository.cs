using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Clients
{
    public class ClientsRepository : RepositoryBase
    {
        public ClientW GetClient(Guid id)
        {
            var client = db.Clients.Single(c => c.Id == id);
            return new ClientW(client);
        }

        public IEnumerable<ClientW> GetClients()
        {
            var clients = db.Clients.ToList();
            var result = clients.Select(c => new ClientW(c));

            return result;
        }
        
        public void Add(ClientW clientW)
        {
            clientW.Id = Guid.NewGuid();
            db.Clients.Add(clientW.ToBase());

            db.SaveChanges();
        }

        public void Edit(ClientW clientW)
        {
            var client = db.Clients.Single(c => c.Id == clientW.Id);
            client.Name = clientW.Name;
            
            db.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var client = db.Clients.Single(c => c.Id == id);
            db.Clients.Remove(client);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsExisting(ClientW client)
        {
            var foundedClient = db.Clients.FirstOrDefault(x => x.Name.ToLower() == client.Name.ToLower());
            if (foundedClient != null)
                return true;

            return false;
        }
    }
}