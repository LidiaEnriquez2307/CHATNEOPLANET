using SignalRApi.Modelos;
using System;
using System.Threading.Tasks;

namespace SignalRApi.ClienteParaPruebas
{
    public interface ISignalRService
    {
        bool IsConnected { get; set; }
       
        Task InitAsync(string id);
        Task DisconnectAsync();
        Task SendMessageToAll(Mensaje item);
    }
}