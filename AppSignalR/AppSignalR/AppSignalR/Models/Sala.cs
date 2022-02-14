using System;
using Newtonsoft.Json;

namespace AppSignalR.Models
{
    public class Sala
    {
        [JsonProperty(PropertyName = "id_sala")]
        public int id_sala { get; set; }
        [JsonProperty(PropertyName = "id_tiposala")]
        public int id_tipo_sala { get; set; } //1=privado; 2= publica
        [JsonProperty(PropertyName = "nombre")]
        public string nombre { get; set; }
        [JsonProperty(PropertyName = "fecha")]
        public DateTime fecha { get; set; }
        [JsonProperty(PropertyName = "activo")]
        public bool activo { get; set; }


    }
}
