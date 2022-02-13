namespace AppSignalR.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    public class ChatViewModel
    {

        #region Properties
        public Room Room
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public ChatViewModel(Room room)
        {
            this.Room = room;
        }
        #endregion
    }
}
