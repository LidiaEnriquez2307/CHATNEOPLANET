namespace AppSignalR.ViewModels
{
    using AppSignalR.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Text;
    using Xamarin.Forms;
    using Services;
    public class RoomViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributes
        private ObservableCollection<RoomItemViewModel> rooms;
        private List<RoomItemViewModel> roomList;
        private bool isRefreshing;
        private string filter;

        #endregion

        #region Properties
        public ObservableCollection<RoomItemViewModel> Rooms
        {
            get { return this.rooms; }
            set { SetValue(ref this.rooms, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set 
            { 
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Constructors
        public RoomViewModel()
        {
            this.apiService = new ApiService();
            this.LoadRooms();
        }
        #endregion

        #region Methods
        private async void LoadRooms()
        {
            /*this.IsRefreshing = true;
            List<RoomItemViewModel> listaroom = new List<RoomItemViewModel>
            {
                new RoomItemViewModel() {id_sala = 1, nombre = "Usuario 1"},
                new RoomItemViewModel() {id_sala = 2, nombre = "Usuario 2"},
                new RoomItemViewModel() {id_sala = 3, nombre = "Usuario 3"}
            };
            this.roomList = listaroom;
            this.Rooms = new ObservableCollection<RoomItemViewModel>(listaroom);
            this.isRefreshing = false;*/
            this.IsRefreshing = true;
            /*var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }*/
            var response = await this.apiService.GetList<Sala>(
                "http://192.168.11.117",
                "/Api2",
                "/api/CuentaSala/1");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            var list = (List<RoomItemViewModel>)response.Result;
            this.rooms = new ObservableCollection<RoomItemViewModel>(list);

        }



        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRooms);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Rooms = new ObservableCollection<RoomItemViewModel>(roomList);
            }
            else
            {
                this.Rooms = new ObservableCollection<RoomItemViewModel>(
                    roomList.Where(
                        l => l.nombre.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
