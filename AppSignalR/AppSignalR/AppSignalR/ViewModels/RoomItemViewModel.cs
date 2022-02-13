namespace AppSignalR.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class RoomItemViewModel : Room
    {
        public ICommand SelectRoomCommand
        {
            get
            {
                return new RelayCommand(SelectRoom);
            }
        }

        private async void SelectRoom()
        {
            MainViewModel.GetInstance().Chat = new ChatViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new ChatPage());
        }
    }
}
