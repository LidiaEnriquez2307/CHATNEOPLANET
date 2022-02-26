using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SignalRApi.NotificacionesPush
{
    public class FireBase
    {
        public FireBase(){}
        public void SendNotification(string titulo,string cuerpo)
        {
            try
            {
                dynamic fieldsFirebase = new
                {
                    to = "ew15t2sDQzuYUT68HG9DSc:APA91bG66xXmDXpjEhD8Cloix2qmp8vLF_CYgDYJ6EjTOqqebPm6H-klp1euFWwFGNYtr0eXv2EPypxOYHXoT0isBxTkMB08zDEKEYYXF24Bg2Zri0PHeDirzRjDaSJpLcua17O9AAuX",//YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                                                                                                                                                                                    // registration_ids = singlebatch, // this is for multiple user 
                    data = new
                    {
                        notiTitle = titulo,    
                       notiBody = cuerpo   
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
