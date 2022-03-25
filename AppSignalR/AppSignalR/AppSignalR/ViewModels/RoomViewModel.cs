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
        public int id_cuenta;

        #endregion

        #region Properties
        public ObservableCollection<RoomItemViewModel> Rooms
        {
            get { return this.rooms; }
            set { SetValue(ref this.rooms, value); }
        }

        public List<RoomItemViewModel> RoomList
        {
            get { return this.roomList; }
            set { SetValue(ref this.roomList, value); }
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
        public RoomViewModel(int id_cuenta)
        {
            this.apiService = new ApiService();
            this.id_cuenta = id_cuenta;
            Console.WriteLine(this.id_cuenta);
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
                "http://192.168.1.112",
                "/API",
                "/api/CuentaSala/"+ this.id_cuenta);
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
            MainViewModel.GetInstance().RoomsList = (List<Sala>)response.Result;
            this.Rooms = new ObservableCollection<RoomItemViewModel>(
                this.ToRoomItemViewModel());
            this.IsRefreshing = false;            
        }
        private IEnumerable<RoomItemViewModel> ToRoomItemViewModel()
        {
            return MainViewModel.GetInstance().RoomsList.Select(l => new RoomItemViewModel
            {
                id_sala = l.id_sala,
                id_tipo_sala = l.id_tipo_sala,
                nombre = l.nombre,
                fecha = l.fecha,
                activo = l.activo,
                id_cuenta = this.id_cuenta
            });
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
