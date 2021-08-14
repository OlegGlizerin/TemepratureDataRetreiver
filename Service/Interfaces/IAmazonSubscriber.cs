using Service.PubSub;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IAmazonSubscriber
    {
        void Subscribe(IAmazonPublisher publisher);
        void UnSubscribe(IAmazonPublisher publisher);
        void Update(object sender, PublisherEventArguments args);
    }
}
