using Service.Interfaces;
using Service.PubSub;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Service
{
    public class AmazonService : IAmazonService
    {
        private IAmazonPublisher _pub;
        private IAmazonSubscriber _sub;

        private IList<string> _relevantCommandsList;

        public AmazonService(IAmazonSubscriber sub, IAmazonPublisher pub)
        {
            _pub = pub;
            _sub = sub;
            _sub.Subscribe(pub);
            _relevantCommandsList = new List<string>();
            _relevantCommandsList.Add("Start");
            _relevantCommandsList.Add("Stop");
        }

        public void SendMessage(string command, string message)
        {
            if(string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Trying to send empty message");
                return;
            }
            else if (string.IsNullOrEmpty(command) || !_relevantCommandsList.Contains(command))
            {
                Console.WriteLine("Trying to send wrong command");
                return;
            }
            _pub.Notify(new AmazonPublisherMessage { Command = command, Date = message });
        }
    }
}
