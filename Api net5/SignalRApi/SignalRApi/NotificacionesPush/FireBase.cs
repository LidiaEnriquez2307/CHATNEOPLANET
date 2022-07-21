using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace SignalRApi.NotificacionesPush
{
    public class FireBase : ControllerBase
    {
        private string stringConection;
        public FireBase()
        {
            stringConection = "server=localhost;port=3306;database=neopruebas;uid=root;CHARSET=utf8;convert zero datetime=True";
        }
        public void NotificarSala(Mensaje mensaje)
        {
            string autor = TraerAutor(mensaje.id_cuenta).ToString();


            var listaTokens = TraerTokens(mensaje);
            if (listaTokens != null)
            {
                foreach (string token in listaTokens)
                {
                    SendNotification(token, autor, mensaje);
                }
            }
            else
            {
                string token = "f5XziTjdSGKoKcmWwNTlIP:APA91bFhQCKE5msI7saUXTOBXR_LnYZX43BLWDeB3u18IOt8Vh-N86ljUuq-Spp79puT35GlPqsFzx0qO_y3uok2oez1BDpvk-GYmxM6FxGgtCTaXTGr2TEcVikZv3_RhVneytU4xFxN";
                SendNotification(token, autor, mensaje);
            }
        }
        private List<string> TraerTokens(Mensaje mensaje)
        {
            List<string> tokens = new();

            using (MySqlConnection conexion = new MySqlConnection(stringConection))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand _sql = new MySqlCommand();
                    _sql.CommandText = @"call sp_mostrar_tokens(?_id_cuenta,?_id_sala)";
                    _sql.Parameters.Add("?_id_cuenta", MySqlDbType.Int32).Value = mensaje.id_cuenta;
                    _sql.Parameters.Add("?_id_sala", MySqlDbType.Int32).Value = mensaje.id_sala;
                    _sql.Connection = conexion;
                    using (var reader = _sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tokens.Add(reader["token"].ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
            return tokens;
        }
        public string TraerAutor(int id_cuenta)
        {
            string autor = "";
            
            using (MySqlConnection conexion = new MySqlConnection(stringConection))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand _sql = new MySqlCommand();
                    _sql.CommandText = @"call sp_nombre_cuenta(@_id_cuenta)";
                    _sql.Parameters.Add("?_id_cuenta", MySqlDbType.Int32).Value = id_cuenta;
                    _sql.Connection = conexion;
                    using (var reader = _sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            autor = reader["nombre"].ToString();
                        }
                    }
                } catch (Exception e)
                {
                    e.ToString();
                }
            }
            return autor;
        }
        public void SendNotification(string token, string autor, Mensaje mensaje)
        {
            try
            {
                dynamic fieldsFirebase = new
                {
                    to = token,
                    data = new
                    {
                        autor = autor,

                        id_mensaje=mensaje.id_mensaje,
                        id_cuenta=mensaje.id_cuenta,
                        id_sala=mensaje.id_sala,
                        mensaje=mensaje.mensaje,
                        fecha=mensaje.fecha,
                        activo=mensaje.activo,
                        leido=mensaje.leido
                    }
                    
                };

                var json = JsonConvert.SerializeObject(fieldsFirebase);
                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);

                string SERVER_API_KEY = "AAAAV5kk180:APA91bFDkR4vqTEkx_NhxHmEDk78FsYK4LheCfOwaI8bzQTRGcDuS5tZfIkzf0rAQ0ra9a2aP-9JfP0wAxtz1ttTZJ9Tt6BgF6bZ5R1pnTj6K4WnKWxzSIx4xgJLu0Pdmn-q88Vq5RTt";
                string SENDER_ID = "376231483341";

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
