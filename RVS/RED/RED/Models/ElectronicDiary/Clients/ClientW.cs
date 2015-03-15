using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Clients
{
    public class ClientW
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage="Името е задължително")]
        [Display(Name="Име")]
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