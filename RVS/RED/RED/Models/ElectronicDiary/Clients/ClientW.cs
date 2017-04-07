using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Clients
{
    public class ClientW
    {
        public ClientW()
        {
        }

        public ClientW(Client client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Diaries = client.Diaries;
            this.Mobile = client.Mobile;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage = "Телефонът на клиента трябва да е не по-дълъг от 30 символа.")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Телефонът трябва да бъде във формат +359123456789.")]
        [Display(Name = "Телефон")]
        public string Mobile { get; set; }

        public virtual ICollection<Diary> Diaries { get; set; }

        public Client ToBase()
        {
            Client client = new Client();

            client.Id = this.Id;
            client.Name = this.Name;
            client.Diaries = this.Diaries;
            client.Mobile = this.Mobile;

            return client;
        }
    }
}