using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Clients
{
    public class ClientW
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Diary> Diaries { get; set; }

        public ClientW()
        {

        }

        public ClientW(Client client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Diaries = client.Diaries;
        }

        public Client ToBase()
        {
            Client client = new Client();

            client.Id = this.Id;
            client.Name = this.Name;
            client.Diaries = this.Diaries;

            return client;
        }
    }
}