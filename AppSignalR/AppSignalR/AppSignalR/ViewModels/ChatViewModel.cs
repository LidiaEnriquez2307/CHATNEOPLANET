namespace AppSignalR.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    public class ChatViewModel
    {

        #region Properties
        public Sala Room
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public ChatViewModel(Sala room)
        {
            this.Room = room;
        }
        #endregion
    }
}
