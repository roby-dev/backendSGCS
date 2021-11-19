using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backendSGCS.Models
{
    public partial class Usuario
    {
        [JsonConstructor]
        public Usuario()
        {
            MiembroProyecto = new HashSet<MiembroProyecto>();
        }

        
        //public Usuario( int _id,string _nombres, string _apellidos, string _celular,
        //                string _correo, bool _estado, string _rol) {
        //    this.IdUsuario = _id;
        //    this.Nombres = _nombres;
        //    this.Apellidos = _apellidos;
        //    this.Celular = _celular;
        //    this.Correo = _correo;             
        //    this.Estado = _estado;
        //    this.Rol = _rol;
        //}

        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Correo { get; set; } = null!;        
        public string? Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public string Rol { get; set; } = null!;
        
        [JsonIgnore]
        public virtual ICollection<MiembroProyecto> MiembroProyecto { get; set; }
    }
}
