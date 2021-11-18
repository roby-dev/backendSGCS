using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            ElementoConfiguracions = new HashSet<ElementoConfiguracion>();
            LineaBases = new HashSet<LineaBase>();
            MiembroProyectos = new HashSet<MiembroProyecto>();
            Solicituds = new HashSet<Solicitud>();
        }

        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdMetodologia { get; set; }
        public bool? Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public virtual Metodologium IdMetodologiaNavigation { get; set; } = null!;
        public virtual ICollection<ElementoConfiguracion> ElementoConfiguracions { get; set; }
        public virtual ICollection<LineaBase> LineaBases { get; set; }
        public virtual ICollection<MiembroProyecto> MiembroProyectos { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
