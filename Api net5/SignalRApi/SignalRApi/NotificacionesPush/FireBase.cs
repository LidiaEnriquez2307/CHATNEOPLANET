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
        public void SendNotification()
        {
            try
            {
                dynamic fieldsFirebase = new
                {
                    to = "fsjY82FDQdY:APA91bFoYjiOU2hPBXnaJdtOizAT049kojGW7Av-9j0CtZNlUyQB33bUdpCm3kot5-6TnE7vE_Y9Tn8n3IzdO8k3vsaOmHb4QzYrSvoiopQNWgh1N_JDx94A9YUmjM7t5y_h_5ox0lDt",//YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                                                                                                                                                                                    // registration_ids = singlebatch, // this is for multiple user 
                    data = new
                    {
                        notiTitle = "Notificacion desde .NET",     // Notification title
                        notiBody = "Es enviado desde Api REST"    // Notification body data
                                                                  // link = ""       // When click on notification user redirect to this link
                    }
                };

                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var json = serializer.Serialize(data);
                var json = JsonConvert.SerializeObject(fieldsFirebase);
                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);

                string SERVER_API_KEY = "AAAAw-EdaXs:APA91bHs1jf1ifeA4kOpFKOn4WjM71GfqcqmddNS2vsEEOGuj34kt19nY3MzVGYtRAMkeZzxsDhzVFtNPGpO7cJbGduvaizs4fNywF0tPhfudQCSzhOzaWCNEkvNIZggQHiROqeNmAXl";
                string SENDER_ID = "841295423867";

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
