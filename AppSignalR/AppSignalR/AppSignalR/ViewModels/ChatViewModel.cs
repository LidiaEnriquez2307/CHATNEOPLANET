namespace AppSignalR.ViewModels
{
    using AppSignalR.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;
    public class ChatViewModel : BaseViewModel
    {
        /*private readonly ISignalRService signalRService;
        signalRService = DependencyService.Get<ISignalRService>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            signalRService.MessageReceived += SignalRService_MessageReceived;
            signalRService.Connecting += SignalRService_Connecting;
            signalRService.Connected += SignalRService_Connected;
            SignalRService.DeviceId = 1;
            Lista.ItemsSource = Messages;

            signalRService.StartWithReconnectionAsync();
        }
        private void SignalRService_Connected(object sender, EventArgs e)
        {
            lbState.Text = "Connected";
        }*/
    }
}
