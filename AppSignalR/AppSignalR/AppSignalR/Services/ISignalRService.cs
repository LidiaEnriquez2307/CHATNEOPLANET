using AppSignalR.Models;
using System;
using System.Threading.Tasks;
using AppSignalR.Services;

namespace AppSignalR.Services
{
    public interface ISignalRService
    {
        event EventHandler<Mensaje> MessageReceived;
        event EventHandler Connecting;
        event EventHandler Connected;

        void StartWithReconnectionAsync();
        void NewMessage(Mensaje mensaje);
        Task OnConnectionClosed(Exception ex);
        Task OnConnectionReconnected(string connectionId);
        Task Init(int id_cuenta);
        Task SendMessageToRoom(Mensaje mensaje);
        Task SubscribeToRoom(string room);
        Task StopAsync();
    }
}
