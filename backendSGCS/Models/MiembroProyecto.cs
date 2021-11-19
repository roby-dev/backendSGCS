using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class MiembroProyecto
    {
        public int IdMiembroProyecto { get; set; }
        public int IdUsuario { get; set; }
        public int IdProyecto { get; set; }
        public int IdCargo { get; set; }
        [JsonPropertyName("cargo")]
        public virtual Cargo IdCargoNavigation { get; set; } = null!;
        [JsonPropertyName("proyecto")]
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        [JsonPropertyName("usuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
