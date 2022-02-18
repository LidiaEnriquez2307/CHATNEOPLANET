

namespace AppSignalR.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using AppSignalR.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using AppSignalR.Services;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using AppSignalR.Models;
    using System;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private List<Cuenta> cuentas;
       

        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public List<Cuenta> Cuenta
        {
            get { return this.cuentas; }
            set { SetValue(ref this.cuentas, value);  }
            
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            

            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "usuario2@2";
            this.Password = "1234";

            this.apiService = new ApiService();

        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        #endregion

        #region metodo
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                   

                    "Error",
                    "You must enter a email",
                    "Acept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                   

                    "Error",
                    "You must enter a password",
                    "Acept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "usuario2@2" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(


                    "Error",
                    "Email or password incorrect",
                    "Acept");
                this.Password = string.Empty;
                return;

            }
            this.IsRunning = false;
            this.IsEnabled = true;
            

          var response = await this.apiService.GetList<Cuenta>(
               "http://192.168.1.6",
               "/API",
               "/api/Cuenta/"+this.Email);
            if (!response.IsSuccess)
            {
                
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

           // MainViewModel.GetInstance().Cuenta = (Cuenta)response.Result;
            this.Cuenta = (List<Cuenta>) response.Result;
           

            if(this.Cuenta != null)
            {
                //roomviewmodel.id_cuenta = Cuenta[0].id_cuenta;
                Console.WriteLine(Cuenta[0].id_cuenta);
                MainViewModel.GetInstance().Room = new RoomViewModel(Cuenta[0].id_cuenta);
                await Application.Current.MainPage.Navigation.PushAsync(new RoomPage());
                
            }
             else
            {
                await Application.Current.MainPage.DisplayAlert(


                    "Error",
                    "Cuenta no registrada",
                    "Acept");

            }
            this.Email = string.Empty;
            this.Password = string.Empty;



           



        }
        #endregion    
    }
}
