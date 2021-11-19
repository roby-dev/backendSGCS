﻿using System;
using System.Collections.Generic;

namespace backendSGCS.Models
{
    public partial class LineaBase
    {
        public LineaBase()
        {
            ElementoConfiguracion = new HashSet<ElementoConfiguracion>();
        }

        public int IdLineaBase { get; set; }
        public int IdProyecto { get; set; }
        public int IdFaseMetodologia { get; set; }
        public DateTime Fecha { get; set; }

        public virtual FaseMetodologia IdFaseMetodologiaNavigation { get; set; } = null!;
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        public virtual ICollection<ElementoConfiguracion> ElementoConfiguracion { get; set; }
    }
}
