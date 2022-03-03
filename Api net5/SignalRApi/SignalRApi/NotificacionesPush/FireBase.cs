using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SignalRApi.Data;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Data.Repositorios;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalRApi.NotificacionesPush
{
    public class FireBase : ControllerBase
    {        
        public void NotificarSala(Mensaje mensaje)
        {
            string autor = mensaje.id_cuenta.ToString();//TraerAutor(mensaje.id_cuenta);
            string token = "f5XziTjdSGKoKcmWwNTlIP:APA91bFhQCKE5msI7saUXTOBXR_LnYZX43BLWDeB3u18IOt8Vh-N86ljUuq-Spp79puT35GlPqsFzx0qO_y3uok2oez1BDpvk-GYmxM6FxGgtCTaXTGr2TEcVikZv3_RhVneytU4xFxN";
 //           var ListaTokens = TraerTokens(mensaje);
 //           foreach(string token in ListaTokens)
 //           {
                SendNotification(token, autor, mensaje.mensaje);
 //           }
        }
        private List<string> TraerTokens(Mensaje mensaje)
        {
            return null;
        }
        public string TraerAutor(int id_cuenta)
        {
            return null;
        }
        public void SendNotification(string token,string autor, string mensaje)
        {
            try
            {
                dynamic fieldsFirebase = new
                {
                    to = token,//YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
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
