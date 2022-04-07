namespace AppSignalR.ViewModels
{
    using AppSignalR.Models;
    using AppSignalR.Services;
    using AppSignalR.ViewModels;
    using System;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Text;
    using AppSignalR.Views;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Net;
    using System.Linq;

    public class ChatViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public int id_cuenta;
        public string mensaje;
        public string estado;
        public ObservableCollection<Mensaje> listaMensajes;
        #endregion

        #region Properties
        public ObservableCollection<Mensaje> ListaMensajes
        {
            get { return this.listaMensajes; }
            set { SetValue(ref this.listaMensajes, value); }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
            set { SetValue(ref this.mensaje, value); }
        }
        public Sala Room
        {
            get;
            set;
        }


        private readonly ISignalRService signalRService;
        
        #endregion

        #region Constructors
        public ChatViewModel(Sala room)
        {
            signalRService = DependencyService.Get<ISignalRService>();
            this.Room = room;
            signalRService.MessageReceived += SignalRService_MessageReceived;
            SignalRService.id_cuenta = Room.id_cuenta;
            this.apiService = new ApiService();
            signalRService.StartWithReconnectionAsync();
            this.LoadMensajes();           
        }
        #endregion

        #region Commands
        public ICommand SendCommand
        {
            get
            {
                return new RelayCommand(Send);
            }
        }
        #endregion

        #region Methods
        private async void LoadMensajes()
        {
            var response = await this.apiService.GetList<Mensaje>(
                "http://192.168.11.117",
                "/Api3",
                "/api/Mensaje/" + Room.id_sala);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            this.ListaMensajes = new ObservableCollection<Mensaje>((List<Mensaje>)response.Result);
            for (int i = 0; i < this.ListaMensajes.Count(); i++)
            {
                if (this.ListaMensajes[i].id_cuenta == this.Room.id_cuenta)
                {
                    this.ListaMensajes[i].remitente = true;
                }
                else
                {
                    this.ListaMensajes[i].remitente = false;
                }
            }
        }
  
        
        private async void Send()
        {
            Mensaje mensaje = new Mensaje
            {
                mensaje = this.mensaje,
                id_cuenta = this.Room.id_cuenta,
                id_sala = this.Room.id_sala,
                fecha = DateTime.Now,
            };
            await signalRService.SendMessageToRoom(mensaje);
            guardar_mensaje(mensaje);
            mensaje.remitente = true;
            this.ListaMensajes.Add(mensaje);
            this.Mensaje = "";
        }
        private async void SignalRService_MessageReceived(object sender, Mensaje mensaje)
        {
            mensaje.remitente = false;
            this.ListaMensajes.Add(mensaje);                   
        }
        private void SignalRService_Connected(object sender, EventArgs e)
        {
            estado = "Connected";
        }

        private void SignalRService_Connecting(object sender, EventArgs e)
        {
            estado = "Connecting...";
        }
        private void NuevoMensajeLista(Mensaje mensaje)
        {
            ListaMensajes.Add(mensaje);
        }

        public async void guardar_mensaje(Mensaje mensaje)
        {
            Uri requestUri = new Uri("http://192.168.11.117/Api3/api/Mensaje");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(mensaje);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, contentJson);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.WriteLine("Mensaje guardado: ");
            }
            else
            {
                Console.WriteLine("No se pudo guardar: ");
            }
        }

        #endregion
    }
}
