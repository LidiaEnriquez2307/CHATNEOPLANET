using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SignalRApi.Modelos;

namespace SignalRApi.SignalR
{
    public class TestHub : Hub
    {
        private static Dictionary<int, string> deviceConnections;
        private static Dictionary<string, int> connectionDevices;

        public TestHub()
        {
            deviceConnections = deviceConnections ?? new Dictionary<int, string>();
            connectionDevices = connectionDevices ?? new Dictionary<string, int>();
        }

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("SignalR server connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            int? deviceId = connectionDevices.ContainsKey(Context.ConnectionId) ?
                            (int?)connectionDevices[Context.ConnectionId] :
                            null;

            if (deviceId.HasValue)
            {
                deviceConnections.Remove(deviceId.Value);
                connectionDevices.Remove(Context.ConnectionId);
            }

            Debug.WriteLine($"SignalR server disconnected. Device: {deviceId}.");
            await base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("Init")]
        public async Task Init(Cuenta cuenta)
        {
            deviceConnections.AddOrUpdate(cuenta.id_cuenta, Context.ConnectionId);
            connectionDevices.AddOrUpdate(Context.ConnectionId, cuenta.id_cuenta);

            if (deviceConnections.ContainsKey(cuenta.id_cuenta))
                await Clients.Client(deviceConnections[cuenta.id_cuenta]).SendAsync("Conectado", cuenta);

            await Task.CompletedTask;
        }

        [HubMethodName("SendMessageToAll")]
        public async Task SendMessageToAll(Mensaje item)
        {
            await Clients.All.SendAsync("NewMessage", item);
        }

        [HubMethodName("SendMessageToDevice")]
        public async Task SendMessageToDevice(Mensaje item)
        {
            Debug.WriteLine($"SignalR server send message {item.Message} from {item.id_cuenta} to  {item.id_sala}");

            if (deviceConnections.ContainsKey(item.id_sala))
                await Clients.Client(deviceConnections[item.id_sala]).SendAsync("NewMessage", item);
        }
    }
}
