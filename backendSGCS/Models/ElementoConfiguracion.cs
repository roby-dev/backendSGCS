using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class ElementoConfiguracion
    {
        public int IdElementoConfiguracion { get; set; }
        public int IdProyecto { get; set; }
        public int IdLineaBase { get; set; }
        public int IdEntregable { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        [JsonPropertyName("entregable")]
        public virtual Entregable IdEntregableNavigation { get; set; } = null!;
        [JsonPropertyName("lineaBase")]
        public virtual LineaBase IdLineaBaseNavigation { get; set; } = null!;
        [JsonPropertyName("proyecto")]
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    }
}
