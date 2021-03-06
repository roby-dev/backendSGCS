using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class FaseMetodologia
    {
        public FaseMetodologia()
        {
            Entregable = new HashSet<Entregable>();
        }

        public int IdFaseMetodologia { get; set; }
        public int IdMetodologia { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }
        [JsonPropertyName("metodologia")]
        public virtual Metodologia? IdMetodologiaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Entregable> Entregable { get; set; }
    }
}
