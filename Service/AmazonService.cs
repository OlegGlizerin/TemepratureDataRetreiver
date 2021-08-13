using Service.Event;
using Service.Interfaces;
using System;

namespace Service
{
    public class AmazonService : IAmazonService
    {
        private AmazonPublisher _pub;
        private AmazonSubscriber _sub;

        public AmazonService()
        {
            _pub = new AmazonPublisher();
            _pub.Name = "AmazonPublisher";

            _sub = new AmazonSubscriber();
            _sub.Subscribe(_pub);
        }

        public void SendMessage(string command, string message)
        {
            _pub.Notify(new AmazonPublisherMessage { Command = command, Date = message });
        }

        public void NotifyFinishedGetFile()
        {
            _sub.UnSubscribe(_pub);
        }
    }
}
