using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using Xamarin.Essentials;

namespace AppSignalR.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        AndroidNotificationManager androidNotification = new AndroidNotificationManager();
        public override void OnMessageReceived(RemoteMessage message)
        {

            IDictionary<string, string> MensajeData = message.Data;
            string Titulo = "";
            string SubTitulo = "";
            if (message.GetNotification()!=null)
            {
                Titulo = message.GetNotification().Title;
                SubTitulo = message.GetNotification().Body;
            }
            else
            {
                Titulo = MensajeData["notiTitle"];
                SubTitulo = MensajeData["notiBody"];
            }
            androidNotification.CrearNotificacionLocal(Titulo, SubTitulo);
            //androidNotification.CrearNotificacionLocal(message.GetNotification().Title,message.GetNotification().Body);

        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            Preferences.Set("TokenFirebase", token);
            sedRegisterToken(token);
        }
        public void sedRegisterToken(string token)
        {
            //Tu código para registrar el token a tu servidor y base de datos
        }
    }
}