using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class ElementoConfiguracion
    {
        public ElementoConfiguracion()
        {
            Solicitud = new HashSet<Solicitud>();
        }

        public int IdElementoConfiguracion { get; set; }
        public int IdLineaBase { get; set; }        
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        [JsonPropertyName("lineaBase")]
        public virtual LineaBase IdLineaBaseNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
