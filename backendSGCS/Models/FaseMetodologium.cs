using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class FaseMetodologium
    {
        public FaseMetodologium()
        {
            Entregables = new HashSet<Entregable>();
            LineaBases = new HashSet<LineaBase>();
        }

        public int IdFaseMetodologia { get; set; }
        public int IdMetodologia { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual Metodologium IdMetodologiaNavigation { get; set; } = null!;
        public virtual ICollection<Entregable> Entregables { get; set; }
        public virtual ICollection<LineaBase> LineaBases { get; set; }
    }
}
