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
    public class ChatViewModel : BaseViewModel
    {
        #region Attributes
        public int id_cuenta;
        public string mensaje;
        #endregion

        #region Properties
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
            SignalRService.DeviceId = this.Room.id_sala;
            signalRService.StartWithReconnectionAsync();
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

        private async void SignalRService_MessageReceived(object sender, Mensaje e)
        {
            await Application.Current.MainPage.DisplayAlert(
                   "Message received",
                   e.mensaje,
                   "Acept");
        }
        #endregion
    }
}
