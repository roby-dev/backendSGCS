using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Solicitud
    {
        public int IdSolicitud { get; set; }
        public string Solicitante { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int IdProyecto { get; set; }
        public string Fecha { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Objetivo { get; set; } = null!;
        public int IdEntregable { get; set; }
        public string Respuesta { get; set; } = null!;
        [JsonPropertyName("entregable")]
        public virtual Entregable IdEntregableNavigation { get; set; } = null!;
        [JsonPropertyName("proyecto")]
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    }
}
