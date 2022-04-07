[assembly: Xamarin.Forms.Dependency(typeof(AppSignalR.Services.SignalRService))]
namespace AppSignalR.Services
{
    using AppSignalR.Models;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class SignalRService : ISignalRService
    {
        public event EventHandler<Mensaje> MessageReceived;
        public event EventHandler Connecting;
        public event EventHandler Connected;

        private const string INIT_OPERATION = "Init";
        private const string SEND_MESSAGE_TO_ROOM_OPERATION = "SendMessageToSala";
        private const string SUBSCRIBE_TO_ROOM = "RegistrarCuenta_a_Sala";
        public static int id_cuenta { get; set; }
        private HubConnection connection;

        public SignalRService()//Mensaje mensaje)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.100.172/API/chatHub")
                .WithAutomaticReconnect(new SignalRretryPolicy())
                .Build();
            //   mensaje = new Mensaje {id_cuenta=mensaje.id_cuenta,id_sala=mensaje.id_sala };
        }

        public async void StartWithReconnectionAsync()
        {
            if (connection.State != HubConnectionState.Disconnected)
            {
                await connection.StopAsync();
                return;
            }
            Connecting?.Invoke(this, null);
            connection.Closed += OnConnectionClosed;
            connection.Reconnected += OnConnectionReconnected;

            connection.On<Mensaje>("NewMessage", NewMessage);

            while (true)
            {
                try
                {
                    await connection.StartAsync();
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"SignalR error {ex.Message}");
                    await Task.Delay(3000);
                }
            }

            Debug.WriteLine($"SignalR connected");
            await Init(id_cuenta);
            Connected?.Invoke(this, null);
        }

        public void NewMessage(Mensaje mensaje)
        {
            MessageReceived?.Invoke(this, mensaje);
        }

        public Task OnConnectionClosed(Exception ex)
        {
            StartWithReconnectionAsync();
            return Task.CompletedTask;
        }

        public async Task OnConnectionReconnected(string connectionId)
        {
            await Init(id_cuenta);
        }

        public async Task Init(int id_cuenta)
        {
            try
            {
                await connection.InvokeAsync(INIT_OPERATION, id_cuenta);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        public async Task SendMessageToRoom(Mensaje mensaje)
        {
            try
            {
                await connection.InvokeAsync(SEND_MESSAGE_TO_ROOM_OPERATION, mensaje);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            };
        }
        public async Task SubscribeToRoom(string room)
        {
            try
            {
                await connection.InvokeAsync(SUBSCRIBE_TO_ROOM, room);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            };
        }
        public async Task StopAsync()
        {
            if (connection.State == HubConnectionState.Disconnected)
                return;

            await connection.StopAsync();
            connection.Closed -= OnConnectionClosed;
            connection.Reconnected -= OnConnectionReconnected;
        }
    }
}
