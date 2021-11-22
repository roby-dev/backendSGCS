using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class LineaBase
    {
        public LineaBase()
        {
            ElementoConfiguracion = new HashSet<ElementoConfiguracion>();
        }

        public int IdLineaBase { get; set; }
        public int IdProyecto { get; set; }
        public int IdEntregable { get; set; }
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        [JsonPropertyName("entregable")]
        public virtual Entregable IdEntregableNavigation { get; set; } = null!;
        [JsonPropertyName("proyecto")]
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ElementoConfiguracion> ElementoConfiguracion { get; set; }
    }
}
