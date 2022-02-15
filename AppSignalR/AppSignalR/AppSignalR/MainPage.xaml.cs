using AppSignalR.Models;
using AppSignalR.Services;
using AppSignalR.ViewModels;
using System;
using Xamarin.Forms;


namespace AppSignalR
{
    public partial class MainPage : ContentPage
    {
        private readonly ISignalRService signalRService;
        public RoomViewModel roomViewModel;

        public MainPage()
        {
            signalRService = DependencyService.Get<ISignalRService>();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            signalRService.MessageReceived += SignalRService_MessageReceived;
            signalRService.Connecting += SignalRService_Connecting;
            signalRService.Connected += SignalRService_Connected;
        }

        protected override void OnDisappearing()
        {
            signalRService.MessageReceived -= SignalRService_MessageReceived; 
            signalRService.Connecting -= SignalRService_Connecting;
            signalRService.Connected -= SignalRService_Connected;
            base.OnDisappearing();
        }

        private void btConnect_Clicked(object sender, EventArgs e)
        {

            SignalRService.DeviceId = roomViewModel.id_cuenta;
            //SignalRService.DeviceId = Convert.ToInt32(enId.Text);
            signalRService.StartWithReconnectionAsync();
        }

        private void SignalRService_Connected(object sender, EventArgs e)
        {
            lbState.Text = "Connected";
        }

        private void SignalRService_Connecting(object sender, EventArgs e)
        {
            lbState.Text = "Connecting...";
        }

        private void btSentToAll_Clicked(object sender, EventArgs e)
        {
            signalRService.SendMessageToAll(new Mensaje
            {
                mensaje = enMessage.Text,
                id_cuenta = Convert.ToInt32(enId.Text),
                id_sala = Convert.ToInt32(enTargetId.Text)
            });
        }

        private void btSentToDevice_Clicked(object sender, EventArgs e)
        {
            signalRService.SendMessageToDevice(new Mensaje
            {
                mensaje = enMessage.Text,
                id_cuenta = Convert.ToInt32(enId.Text),
                id_sala = Convert.ToInt32(enTargetId.Text)
            });
        }

        private async void SignalRService_MessageReceived(object sender, Mensaje e)
        {
            await DisplayAlert("Message reveived", e.mensaje, "Ok");
        }
    }
}
