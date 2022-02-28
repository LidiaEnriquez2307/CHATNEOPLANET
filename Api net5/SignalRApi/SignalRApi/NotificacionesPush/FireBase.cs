using Newtonsoft.Json;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SignalRApi.NotificacionesPush
{
    public class FireBase
    {
        //inyectar el repositorio
        private readonly InterfaceDispositivo _repoDispositivo;
        private readonly InterfaceCuenta _repoCuenta;
        public FireBase(){}
        public void NotificarSala(Mensaje mensaje)
        {
            string autor= TraerAutor(mensaje.id_cuenta);
            var ListaTokens = TraerTokens(mensaje);
            foreach(string token in ListaTokens)
            {
                SendNotification(token, autor, mensaje.mensaje);
                Debug.WriteLine(token, autor, mensaje.mensaje);
            }
        }
        private IEnumerable<string> TraerTokens(Mensaje mensaje)
        {
            return _repoDispositivo.mostrar_tokens(mensaje).Result;
        }
        private string TraerAutor(int id_cuenta)
        {
            string autor = _repoCuenta.mostrar_cuenta(id_cuenta).Result.First();
            return autor;
        }
        public void SendNotification(string token,string autor, string mensaje)
        {
            try
            {
                dynamic fieldsFirebase = new
                {
                    to = "ew15t2sDQzuYUT68HG9DSc:APA91bG66xXmDXpjEhD8Cloix2qmp8vLF_CYgDYJ6EjTOqqebPm6H-klp1euFWwFGNYtr0eXv2EPypxOYHXoT0isBxTkMB08zDEKEYYXF24Bg2Zri0PHeDirzRjDaSJpLcua17O9AAuX",//YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                                                                                                                                                                                    // registration_ids = singlebatch, // this is for multiple user 
                    data = new
                    {
                        notiTitle = autor,    
                       notiBody = mensaje   
                     // link = ""       // When click on notification user redirect to this link
                    }
                };

                var json = JsonConvert.SerializeObject(fieldsFirebase);
                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);

                string SERVER_API_KEY = "AAAA713rtM8:APA91bHgjggLV-5e1EsQVrQpNsFteMqrqnVIdr5b6H1qMwA9lGd9WjwfksaA-3Y-wTNube7M0tSwy0i0nihGAlCAOipwi1WoJSiWbBosPf5pB21JWGVXJ1WUkrDDcCzdQFmk2bBlzvoE";
                string SENDER_ID = "1028072912079";

                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
