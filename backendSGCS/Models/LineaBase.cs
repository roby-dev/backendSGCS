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
        public int IdFaseMetodologia { get; set; }
        public DateTime Fecha { get; set; }

        [JsonPropertyName("faseMetodologia")]
        public virtual FaseMetodologia? IdFaseMetodologiaNavigation { get; set; } = null!;
        [JsonPropertyName("proyecto")]
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ElementoConfiguracion> ElementoConfiguracion { get; set; }
    }
}
