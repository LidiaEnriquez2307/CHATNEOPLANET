

namespace AppSignalR.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using AppSignalR.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Models;
    using Services;

    public class LoginViewModel: BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion


        #region Attributes
        private Cuenta cuenta;
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public Cuenta Cuenta
        {
            get { return this.cuenta; }
            set { SetValue(ref this.cuenta, value); }
        }

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
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();


            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "lidia";
            this.Password = "1234";
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

            if (this.Email != "lidia" || this.Password != "1234")
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
            this.Email = string.Empty;
            this.Password = string.Empty;

            var response = await this.apiService.Get<Cuenta>(
                "http://192.168.11.117",
                "/Api2",
                "/api/Cuenta/usuario1@1");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            MainViewModel.GetInstance().Room = new RoomViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RoomPage());



        }
        #endregion    
    }
}
