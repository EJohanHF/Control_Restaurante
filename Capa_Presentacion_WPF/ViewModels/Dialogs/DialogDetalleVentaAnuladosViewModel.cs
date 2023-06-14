using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Reportes.VentasporDia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
   public class DialogDetalleVentaAnuladosViewModel:IGeneric
    {
        public List<VentasDia> dataDetVentasAnulados { get; set; }
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        public DialogDetalleVentaAnuladosViewModel()
        {
            if (System.Windows.Application.Current.Properties["HastaHistorialVentas"] != null)
            {
                string desde = System.Windows.Application.Current.Properties["DesdeHistorialVentas"].ToString();
                string hasta = System.Windows.Application.Current.Properties["HastaHistorialVentas"].ToString();
                this.dataDetVentasAnulados = negVentaD.GetDetalleVentasAnulados_Historial(desde, hasta);
            }
            else
            {
                this.dataDetVentasAnulados = negVentaD.GetDetalleVentasAnulados();
            }
           
        }
    }
}
