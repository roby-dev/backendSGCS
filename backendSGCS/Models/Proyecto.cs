using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            ElementoConfiguracion = new HashSet<ElementoConfiguracion>();
            LineaBase = new HashSet<LineaBase>();
            MiembroProyecto = new HashSet<MiembroProyecto>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdMetodologia { get; set; }
        public bool? Estado { get; set; }
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;

        public virtual Metodologia IdMetodologiaNavigation { get; set; } = null!;
        public virtual ICollection<ElementoConfiguracion> ElementoConfiguracion { get; set; }
        public virtual ICollection<LineaBase> LineaBase { get; set; }
        public virtual ICollection<MiembroProyecto> MiembroProyecto { get; set; }
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
