using DP.Activator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Activator
{
    

    public class MyMessenger<TMessage>
    {
        private Dictionary<string, Person> _persons = new();

        public static MyMessenger<TMessage> instance = new();

        private readonly Object _lock = new();

        // broadcast
        private Action<string, TMessage>? _broadcastHandlers;

        // private room
        private Dictionary<string, Action<string, TMessage>> _privatehandlers = new();


        //**REGISTERS
        public void Register(Person p, Action<string, TMessage> onMessage)
        {
            lock (_lock)
            {
                _persons[p.Id] = p;

                // broadcast recoit tout le monde
                _broadcastHandlers += onMessage;

                // broadcast message destiné à cette id
                _privatehandlers[p.Id] = onMessage;

            }
        }
        //**UNREGISTER
        public void Unregister(string id, Action<string, TMessage> onMessage)
        {
            lock (_lock)
            {
                _broadcastHandlers -= onMessage;
                _privatehandlers.Remove(id);
            }
        }

        //** SEND MESSAGE
        public void Send(string senderID, string? recipientID, TMessage message) 
        {
            if (recipientID == null)
            {
                //==== on broadcast =====
                Action<string, TMessage>? handlers;
                lock (_lock)
                    handlers = _broadcastHandlers;
                handlers?.Invoke(senderID, message);
            }
            else
            {

                //=== on envoie en privé ====


                Action<string,TMessage>? handler = null;
                lock (_lock)
                    _privatehandlers.TryGetValue(senderID , out handler);
                handler?.Invoke(recipientID, message);
            }
        }

      
        public string GetUserName(string id)
        {
           return _persons[id].Nom;
        }
        public void displayAllUser()
        {
            foreach (Person p in _persons.Values)
            {
                Console.WriteLine($"{p.Id} - {p.Nom}");
            }
        }


    }
}
