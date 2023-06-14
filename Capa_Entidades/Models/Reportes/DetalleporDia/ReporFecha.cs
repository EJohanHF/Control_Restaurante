using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
  public  class ReporFecha
    {
        public DateTime desde { get; set; } = DateTime.Now;

        public DateTime hasta { get; set; } = DateTime.Now;
    }
}
