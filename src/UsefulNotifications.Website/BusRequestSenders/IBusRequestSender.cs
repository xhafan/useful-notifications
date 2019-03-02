using System.Threading.Tasks;

namespace UsefulNotifications.Website.BusRequestSenders
{
    public interface IBusRequestSender
    {
        Task<TReply> SendRequest<TReply>(object message);
    }
}