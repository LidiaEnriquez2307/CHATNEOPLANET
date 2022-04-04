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
            SignalRService.mensaje = new Mensaje {id_cuenta=this.Room.id_cuenta,id_sala=this.Room.id_sala};
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
        }
  
        /*private void btConnect_Clicked(object sender, EventArgs e)
        {

            SignalRService.DeviceId = this.Room.id_sala;
            //SignalRService.DeviceId = Convert.ToInt32(enId.Text);
            signalRService.StartWithReconnectionAsync();
        }*/

        /*private void SignalRService_Connected(object sender, EventArgs e)
        {
            lbState.Text = "Connected";
        }

        private void SignalRService_Connecting(object sender, EventArgs e)
        {
            lbState.Text = "Connecting...";
        }*/

        /*private void btSentToAll_Clicked(object sender, EventArgs e)
        {
            signalRService.SendMessageToAll(new Mensaje
            {
                mensaje = enMessage.Text,
                id_cuenta = Convert.ToInt32(enId.Text),
                id_sala = Convert.ToInt32(enTargetId.Text)
            });
        }*/

        private void Send()
        {
            Mensaje mensaje = new Mensaje
            {
                mensaje = this.mensaje,
                id_cuenta = this.Room.id_cuenta,
                id_sala = this.Room.id_sala,
                fecha = DateTime.Now,
            };
            signalRService.SendMessageToDevice(mensaje);
            guardar_mensaje(mensaje);
            this.LoadMensajes();
            this.Mensaje = "";
        }
        private async void SignalRService_MessageReceived(object sender, Mensaje mensaje)
        {
            this.LoadMensajes();                   
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
