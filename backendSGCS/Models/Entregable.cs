using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Entregable
    {
        public Entregable()
        {
            ElementoConfiguracion = new HashSet<ElementoConfiguracion>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int IdEntregable { get; set; }
        public int IdFaseMetodologia { get; set; }
        public string Nomenclatura { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual FaseMetodologia IdFaseMetodologiaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ElementoConfiguracion> ElementoConfiguracion { get; set; }
        [JsonIgnore]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
