using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            MiembroProyecto = new HashSet<MiembroProyecto>();
        }

        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public string Rol { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<MiembroProyecto> MiembroProyecto { get; set; }
    }
}
