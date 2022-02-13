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
    public class RoomViewModel : BaseViewModel
    {
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
            this.LoadRooms();
        }
        #endregion

        #region Methods
        private void LoadRooms()
        {
            this.IsRefreshing = true;
            List<RoomItemViewModel> listaroom = new List<RoomItemViewModel>
            {
                new RoomItemViewModel() {Id = 1, Name = "Usuario 1"},
                new RoomItemViewModel() {Id = 2, Name = "Usuario 2"},
                new RoomItemViewModel() {Id = 3, Name = "Usuario 3"}
            };
            this.roomList = listaroom;
            this.Rooms = new ObservableCollection<RoomItemViewModel>(listaroom);
            this.isRefreshing = false;
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
                        l => l.Name.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
