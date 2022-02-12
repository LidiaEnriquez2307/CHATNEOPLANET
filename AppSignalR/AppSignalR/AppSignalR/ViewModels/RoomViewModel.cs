namespace AppSignalR.ViewModels
{
    using AppSignalR.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    public class RoomViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<Room> rooms;
        #endregion

        #region Properties
        public ObservableCollection<Room> Rooms
        {
            get { return this.rooms; }
            set { SetValue(ref this.rooms, value); }
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
            List<Room> listaroom = new List<Room>
            {
                new Room() {Id = 1, Name = "Usuario 1"},
                new Room() {Id = 2, Name = "Usuario 2"},
                new Room() {Id = 3, Name = "Usuario 3"}
            };
            this.Rooms = new ObservableCollection<Room>(listaroom);
        }
        #endregion
    }
}
