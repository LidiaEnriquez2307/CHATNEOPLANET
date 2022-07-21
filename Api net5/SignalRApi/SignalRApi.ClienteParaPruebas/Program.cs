using SignalRApi.Modelos;
using SignalRApi.SingnalR;
using System;
using System.Threading.Tasks;

namespace SignalRApi.ClienteParaPruebas
{
    class Program
    {
        static string userName;
        static ISignalRService signalRService;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Nombre de usurario");
            userName = Console.ReadLine();
            signalRService = new SignalRService();
            await signalRService.InitAsync(userName);
            Console.WriteLine("Estas conectado ^v^");

            var kepGoing = true;
            do
            {
                var text = Console.ReadLine();
                if(text=="exit")
                {
                    await signalRService.DisconnectAsync();
                    kepGoing = false;
                }else
                {
                   await signalRService.SendMessageToAll(new Mensaje
                    {
                        mensaje = text,
                        id_cuenta = 2,
                        id_sala = 1
                    });
                }
            } while (kepGoing);

        }
    }
}
