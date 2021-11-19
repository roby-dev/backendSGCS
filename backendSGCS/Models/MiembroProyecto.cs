using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class MiembroProyecto
    {
        public int? IdMiembroProyecto { get; set; }
        public int IdUsuario { get; set; }
        public int IdProyecto { get; set; }
        public int IdCargo { get; set; }
        [JsonIgnore]
        public virtual Cargo? IdCargoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Proyecto? IdProyectoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;
    }
}
