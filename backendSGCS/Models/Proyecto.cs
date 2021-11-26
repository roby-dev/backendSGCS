using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            LineaBase = new HashSet<LineaBase>();
            MiembroProyecto = new HashSet<MiembroProyecto>();
        }

        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdMetodologia { get; set; }
        public bool? Estado { get; set; }
        public string FechaInicio { get; set; } = null!;
        public string? Repositorio { get; set; }
        public string FechaFin { get; set; } = null!;
        [JsonPropertyName("metodologia")]
        public virtual Metodologia? IdMetodologiaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<LineaBase> LineaBase { get; set; }
        [JsonIgnore]
        public virtual ICollection<MiembroProyecto> MiembroProyecto { get; set; }
    }
}
