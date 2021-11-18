using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class ElementoConfiguracion
    {
        public int IdElementoConfiguracion { get; set; }
        public int IdProyecto { get; set; }
        public int IdLineaBase { get; set; }
        public int IdEntregable { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual Entregable IdEntregableNavigation { get; set; } = null!;
        public virtual LineaBase IdLineaBaseNavigation { get; set; } = null!;
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    }
}
