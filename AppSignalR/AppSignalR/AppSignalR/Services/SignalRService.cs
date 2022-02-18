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
       // private const string INIT_OPERATION = "Init";
        private const string SEND_MESSAGE_TO_DEVICE_OPERATION = "SendMessageToDevice";
        private const string SEND_MESSAGE_TO_ALL_OPERATION = "SendMessageToAll";

        public static int DeviceId { get; set; }
        private HubConnection connection;

        public SignalRService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.1.6/API/testHub")
                .WithAutomaticReconnect(new SignalRretryPolicy())
                .Build();
        }

        public async void StartWithReconnectionAsync()
        {
            if (connection.State != HubConnectionState.Disconnected)
                return;

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
            await Init();
            Connected?.Invoke(this, null);
        }

        private Task OnConnectionClosed(Exception ex)
        {
            StartWithReconnectionAsync();
            return Task.CompletedTask;
        }

        private async Task OnConnectionReconnected(string connectionId)
        {
            await Init();
        }

        private async Task Init()
        {
            try
            {
                await connection.InvokeAsync(INIT_OPERATION, new Cuenta { id_cuenta = DeviceId  });
            }
            catch (Exception ex)
            {

            }
        }

        private void NewMessage(Mensaje obj)
        {
            MessageReceived?.Invoke(this, obj);
        }

        public async Task SendMessageToAll(Mensaje item)
        {
            try
            {
                await connection.InvokeAsync(SEND_MESSAGE_TO_ALL_OPERATION, item);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendMessageToDevice(Mensaje item)
        {
            try
            {
                await connection.InvokeAsync(SEND_MESSAGE_TO_DEVICE_OPERATION, item);
            }
            catch (Exception ex)
            {

            }
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
