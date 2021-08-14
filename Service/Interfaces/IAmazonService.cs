
namespace Service.Interfaces
{
    public interface IAmazonService
    {
        public void SendMessage(string command, string message);
    }
}
