using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Reportes.VentasporDia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
   public class DialogDetalleProdAnuladosViewModel:IGeneric
    {
        public List<VentasDia> dataDetProdAnulados { get; set; }
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        public DialogDetalleProdAnuladosViewModel()
        {
            try
            {     
                if (System.Windows.Application.Current.Properties["HastaHistorialVentas"] != null)
                {
                    string desde = System.Windows.Application.Current.Properties["DesdeHistorialVentas"].ToString();
                    string hasta = System.Windows.Application.Current.Properties["HastaHistorialVentas"].ToString();
                    this.dataDetProdAnulados = negVentaD.GetDetalleProdAnulados_Historial(desde, hasta);
                }
                else
                {
                    this.dataDetProdAnulados = negVentaD.GetDetalleProdAnulados();
                }

            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
