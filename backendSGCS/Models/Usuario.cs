using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            MiembroProyectos = new HashSet<MiembroProyecto>();
        }

        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Correo { get; set; } = null!;
        [JsonIgnore]
        public string Clave { get; set; } = null!;
        public bool? Estado { get; set; }
        public string Rol { get; set; } = null!;
        [JsonIgnore]        
        public virtual ICollection<MiembroProyecto> MiembroProyectos { get; set; }
    }
}
