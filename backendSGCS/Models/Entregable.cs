using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Entregable
    {
        public Entregable()
        {
            LineaBase = new HashSet<LineaBase>();
        }

        public int IdEntregable { get; set; }
        public int IdFaseMetodologia { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Nomenclatura { get; set; } = null!;
        public bool? Estado { get; set; }

        [JsonPropertyName("faseMetodologia")]
        public virtual FaseMetodologia IdFaseMetodologiaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<LineaBase> LineaBase { get; set; }
    }
}
