using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class MiembroProyecto
    {
        public int IdMiembroProyecto { get; set; }
        public int IdUsuario { get; set; }
        public int IdProyecto { get; set; }
        public int IdCargo { get; set; }

        public virtual Cargo? IdCargoNavigation { get; set; } = null!;
        public virtual Proyecto? IdProyectoNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;
    }
}
