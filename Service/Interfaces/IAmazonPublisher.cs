using Service.PubSub;

namespace Service.Interfaces
{
    public interface IAmazonPublisher
    {
        void Notify(AmazonPublisherMessage message);
    }
}
