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

    public class ChatViewModel : BaseViewModel
    {
        #region Attributes
        public int id_cuenta;
        public string mensaje;
        public string estado;
        public ObservableCollection<Mensaje> listaMensajes;
        #endregion

        #region Properties
        public ObservableCollection<Mensaje> ListaMensajes
        {
            set { SetValue(ref this.listaMensajes, value); }
            get { return this.listaMensajes; }
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

            signalRService.StartWithReconnectionAsync();
            this.ListaMensajes = new ObservableCollection<Mensaje>();
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
            signalRService.SendMessageToDevice(new Mensaje
            {
                mensaje = this.mensaje,
                id_cuenta = this.Room.id_cuenta,
                id_sala = this.Room.id_sala
            });
            return;
        }
        private async void SignalRService_MessageReceived(object sender, Mensaje mensaje)
        {
           await Application.Current.MainPage.DisplayAlert(
                   "Message received",
                   mensaje.mensaje,
                   "Acept");
            if (mensaje.id_cuenta != this.id_cuenta)
            {
                
                this.ListaMensajes.Add(mensaje);
               // NuevoMensajeLista(mensaje);
            }
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

        #endregion
    }
}
