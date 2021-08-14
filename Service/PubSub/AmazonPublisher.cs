using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PubSub
{
    public class AmazonPublisher : IAmazonPublisher
    {
        public string Name { get; set; }
        public event EventHandler<PublisherEventArguments> MyEvent;

        public AmazonPublisher()
        {
            Name = "Amazon Publisher";
        }

        public void Notify(AmazonPublisherMessage message)
        {
            var publisherEventArguments = new PublisherEventArguments(message);

            if(MyEvent != null)
            {
                MyEvent(this, publisherEventArguments);
            }
        }
    }
}
