using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class Metodologium
    {
        public Metodologium()
        {
            FaseMetodologia = new HashSet<FaseMetodologium>();
            Proyectos = new HashSet<Proyecto>();
        }

        public int IdMetodologia { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<FaseMetodologium> FaseMetodologia { get; set; }
        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
