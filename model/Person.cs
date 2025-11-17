using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Activator.model
{
    public class Person
    {

        public string Nom {  get; set; }
        public string Id { get; } = "cli"+Random.Shared.Next(1000, 10000000).ToString(); 

        public Person(string nom)
        {
            Nom = nom;
            MyMessenger<string>.instance.Register(this, Receive);
        }
        
        private void Receive(string sender, string messenger)
        {
            var p = MyMessenger<string>.instance.GetUserName(sender);

            Console.WriteLine($"{p} à reçu un message de {Nom} => {messenger}");
        }


        public void SendMessage(string message, string? idFriend = null) 
        {
            MyMessenger<string>.instance.Send(Id, idFriend, message);
        }

       

        

    }
}
