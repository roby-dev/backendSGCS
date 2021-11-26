using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class VersionElementoConfiguracion
    {
        public string Fecha { get; set; } = null!;
        public int IdSolicitud { get; set; }
        public string Enlace { get; set; } = null!;
        public string Version { get; set; } = null!;
        public int IdVersion { get; set; }
        [JsonPropertyName("solicitud")]
        public virtual Solicitud? IdSolicitudNavigation { get; set; } = null!;
    }
}
