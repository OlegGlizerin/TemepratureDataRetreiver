using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Event
{
    public class AmazonPublisher
    {
        public string Name { get; set; }
        public event EventHandler<PublisherEventArguments> MyEvent;

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
