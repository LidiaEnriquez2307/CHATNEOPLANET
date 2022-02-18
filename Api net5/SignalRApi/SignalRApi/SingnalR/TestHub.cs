using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SignalRApi.Modelos;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.SingnalR
{
    public class TestHub:Hub
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
        //conectar
        [HubMethodName("Init")]
        public Task Init(Cuenta cuenta)
        {
            deviceConnections.AddOrUpdate(cuenta.id_cuenta, Context.ConnectionId);
            connectionDevices.AddOrUpdate(Context.ConnectionId, cuenta.id_cuenta);

            return Task.CompletedTask;
        }
        //Enviar mensaje a todos
        [HubMethodName("SendMessageToAll")]
        public async Task SendMessageToAll(Mensaje mensaje)
        {
            await Clients.All.SendAsync("NewMessage", mensaje);
        }
        //enviar mensaje a una cuenta
        [HubMethodName("SendMessageToDevice")]
        public async Task SendMessageToDevice(Mensaje mensaje)
        {
            Debug.WriteLine($"SignalR server send message {mensaje.mensaje} from {mensaje.id_cueta} to  {mensaje.id_sala}");

            if (deviceConnections.ContainsKey(mensaje.id_sala))
                await Clients.Client(deviceConnections[mensaje.id_sala]).SendAsync("NewMessage", mensaje);
        }
    }
}
