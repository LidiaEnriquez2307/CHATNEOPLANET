using System;
using System.Threading.Tasks;
namespace AppSignalR.Services
{
    public interface ISignalRService
    {
        event EventHandler<Mensaje> MessageReceived;
        event EventHandler Connecting;
        event EventHandler Connected;

        void StartWithReconnectionAsync();
        Task SendMessageToAll(Mensaje item);
        Task SendMessageToDevice(Mensaje item);
        Task StopAsync();
    }
}
