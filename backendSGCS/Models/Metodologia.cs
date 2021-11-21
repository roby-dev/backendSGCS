using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Metodologia
    {
        public Metodologia()
        {
            FaseMetodologia = new HashSet<FaseMetodologia>();
            Proyecto = new HashSet<Proyecto>();
        }

        public int IdMetodologia { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }
        [JsonIgnore]
        public virtual ICollection<FaseMetodologia> FaseMetodologia { get; set; }
        [JsonIgnore]
        public virtual ICollection<Proyecto> Proyecto { get; set; }
    }
}
