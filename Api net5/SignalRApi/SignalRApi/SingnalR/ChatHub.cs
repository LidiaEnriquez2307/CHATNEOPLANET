using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SignalRApi.Modelos;
using SignalRApi.NotificacionesPush;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.SingnalR
{
    public class ChatHub:Hub
    {
        private static Dictionary<int, string> deviceConnections;
        private static Dictionary<string, int> connectionDevices;
        private FireBase fireBase = new FireBase();

        public ChatHub()
        {
            deviceConnections = deviceConnections ?? new Dictionary<int, string>();
            connectionDevices = connectionDevices ?? new Dictionary<string, int>();
        }

        public override Task OnConnectedAsync()
        {
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
            await base.OnDisconnectedAsync(exception);
        }
        //conectar
        [HubMethodName("Init")]
        public Task Init(Mensaje mensaje)
        {
            deviceConnections.AddOrUpdate(mensaje.id_cuenta, Context.ConnectionId);
            connectionDevices.AddOrUpdate(Context.ConnectionId, mensaje.id_cuenta);
            RegistrarCuenta_a_Sala(mensaje.id_sala);
            return Task.CompletedTask;
        }
        //Enviar mensaje a todos
        [HubMethodName("SendMessageToAll")]
        public async Task SendMessageToAll(Mensaje mensaje)
        {
            await Clients.All.SendAsync("NewMessage", mensaje);
            //Notificar
            fireBase.NotificarSala(mensaje);
        }
        //enviar mensaje a una SALA
        [HubMethodName("SendMessageToSala")]
        public async Task SendMessageToSala(Mensaje mensaje)
        {
            await Clients.Group(mensaje.id_sala.ToString()).SendAsync("NewMessage", mensaje);
            //Notificar
            fireBase.NotificarSala(mensaje);
        }
        //Reistrar usuarios en grupos
        [HubMethodName("RegistrarCuenta_a_Sala")]
        public void RegistrarCuenta_a_Sala(int grupo)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, grupo.ToString());
        }
    }
}
