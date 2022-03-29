using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using SignalRApi.Modelos;

namespace SignalRApi.ClienteParaPruebas
{
    public class SignalRService : ISignalRService
    {
        public event EventHandler<Mensaje> MessageReceived;
        public bool IsConnected { get; set;}
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);
        HubConnection hub;
        public async Task InitAsync(string id)
        {
            await semaphoreSlim.WaitAsync();
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/chatHub")
                .WithAutomaticReconnect(new SignalRretryPolicy());
            hub = connection.Build();
            await hub.StartAsync();
            await hub.InvokeAsync("Init",new Mensaje { id_cuenta = 2, id_sala = 1 });
            IsConnected = true;
            semaphoreSlim.Release();
            
        }
        public async Task DisconnectAsync()
        {
            try
            {
                await hub.DisposeAsync();
            }catch(Exception exe)
            {
                Console.WriteLine(exe);
            }
            IsConnected = false;
        }
        private void NewMessage(Mensaje obj)
        {
            MessageReceived?.Invoke(this, obj);
        }

        public async Task SendMessageToAll(Mensaje item)
        {
            try
            {
                await hub.InvokeAsync("SendMessageToSala", item);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
