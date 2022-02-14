using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.ViewModels
{
    using System.Collections.Generic;
    using Models;
    public class MainViewModel
    {
        #region Properties
        public List<Sala> RoomsList
        {
            get;
            set;
        }
        #endregion
        #region ViewModels

        public LoginViewModel Login
        {
            get;
            set;

        }
        public RoomViewModel Room
        {
            get;
            set;
        }


        public ChatViewModel Chat
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion

    }
}
