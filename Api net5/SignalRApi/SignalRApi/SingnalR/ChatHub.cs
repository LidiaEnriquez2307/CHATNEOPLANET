﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SignalRApi.Modelos;
using SignalRApi.NotificacionesPush;
using Microsoft.AspNetCore.SignalR;
using MySql.Data.MySqlClient;

namespace SignalRApi.SingnalR
{
    public class ChatHub:Hub
    {
        private static Dictionary<int, string> deviceConnections;
        private static Dictionary<string, int> connectionDevices;
        private FireBase fireBase = new FireBase();
        private string stringConection;
        public ChatHub()
        {
            deviceConnections = deviceConnections ?? new Dictionary<int, string>();
            connectionDevices = connectionDevices ?? new Dictionary<string, int>();
            stringConection = "server=localhost;port=3306;database=neopruebas;uid=root;CHARSET=utf8;convert zero datetime=True";
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            int? deviceId = connectionDevices.ContainsKey(Context.ConnectionId) ?
                            (int?)connectionDevices[Context.ConnectionId] :
                            null;

            if (deviceId.HasValue)
            {
                deviceConnections.Remove(deviceId.Value);
                connectionDevices.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
        //conectar
        [HubMethodName("Init")]
        public Task Init(int id_cuenta)
        {
            deviceConnections.AddOrUpdate(id_cuenta, Context.ConnectionId);
            connectionDevices.AddOrUpdate(Context.ConnectionId, id_cuenta);

            //--
            var listaSalas = TraerIdSalas(id_cuenta);
            if (listaSalas != null)
            {
                foreach (string id_sala in listaSalas)
                {
                    RegistrarCuenta_a_Sala(id_sala);
                }
            }
            else
            {
                //RegistrarCuenta_a_Sala(mensaje.id_sala);
            }
            //--
            return Task.CompletedTask;
        }
        //Enviar mensaje a todos
        [HubMethodName("SendMessageToAll")]
        public async Task SendMessageToAll(Mensaje mensaje)
        {
            await Clients.All.SendAsync("NewMessage", mensaje);
            //Notificar
            fireBase.NotificarSala(mensaje);
        }
        //enviar mensaje a una SALA
        [HubMethodName("SendMessageToSala")]
        public async Task SendMessageToSala(Mensaje mensaje)
        {
            await Clients.OthersInGroup(mensaje.id_sala.ToString()).SendAsync("NewMessage", mensaje);
            //Notificar
            fireBase.NotificarSala(mensaje);
        }
        //Reistrar usuarios en grupos
        [HubMethodName("RegistrarCuenta_a_Sala")]
        public void RegistrarCuenta_a_Sala(string grupo)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, grupo);
        }


        private List<string> TraerIdSalas(int id_cuenta)
        {
            List<string> salas = new();

            using (MySqlConnection conexion = new MySqlConnection(stringConection))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand _sql = new MySqlCommand();
                    _sql.CommandText = @"sp_salas_de_una_cuenta(@_id_cuenta)";
                    _sql.Parameters.Add("?_id_cuenta", MySqlDbType.Int32).Value = id_cuenta;
                    _sql.Connection = conexion;
                    using (var reader = _sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salas.Add(reader["id_sala"].ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
            return salas;
        }
    }
}
