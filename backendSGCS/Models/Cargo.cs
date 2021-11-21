using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Cargo
    {
        public Cargo()
        {
            MiembroProyecto = new HashSet<MiembroProyecto>();
        }

        public int IdCargo { get; set; }
        public string Nombre { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<MiembroProyecto> MiembroProyecto { get; set; }
    }
}
