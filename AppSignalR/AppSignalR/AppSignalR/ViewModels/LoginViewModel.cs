

namespace AppSignalR.ViewModels
{
    using AppSignalR.Models;
    using AppSignalR.Services;
    using AppSignalR.Views;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

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

            this.Email = "usuario1@1";
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

            if (this.Email != "usuario1@1" || this.Password != "1234")
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
               "http://192.168.0.198",
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
                
                //Recuperar TOKEN
                string token = Preferences.Get("TokenFirebase","") ;
                //Guardar
                GuardarToken(token, Cuenta[0].id_cuenta);

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
        private async void GuardarToken(string _token,int _id_cuenta)
        {
            if (string.IsNullOrEmpty(_token))
            {
                return;
            }
            //Guardar el token en la base de datos
            Dispositivo dispositivo = new Dispositivo {id_dispositivo=0,id_cuenta=_id_cuenta,token=_token};
            Uri requestUri = new Uri("http://192.168.0.198/API/api/Dispositivo");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(dispositivo);
            var contentJson = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await client.PostAsync(requestUri,contentJson);
            if(response.StatusCode==HttpStatusCode.Created)
            {
                Console.WriteLine("Token guardado: ");
            }
            else
            {
                Console.WriteLine("No se pudo guardar: ");
            }
        }
        #endregion    
    }
}
