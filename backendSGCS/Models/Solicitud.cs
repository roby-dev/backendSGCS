using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            VersionElementoConfiguracion = new HashSet<VersionElementoConfiguracion>();
        }

        public int IdSolicitud { get; set; }
        public string Estado { get; set; } = null!;
        public int IdElementoConfiguracion { get; set; }
        public string Fecha { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Objetivo { get; set; } = null!;
        public int IdMiembroProyecto { get; set; }
        public string? Archivo { get; set; }
        public string? Respuesta { get; set; }
        [JsonPropertyName("elementoConfiguracion")]
        public virtual ElementoConfiguracion? IdElementoConfiguracionNavigation { get; set; } = null!;
        [JsonPropertyName("miembroProyecto")]
        public virtual MiembroProyecto? IdMiembroProyectoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<VersionElementoConfiguracion> VersionElementoConfiguracion { get; set; }
    }
}
