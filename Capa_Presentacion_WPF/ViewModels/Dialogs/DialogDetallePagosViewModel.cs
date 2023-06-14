using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Reportes.VentasporDia;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogDetallePagosViewModel
    {
        public List<VentasDia> dataTipopago { get; set; }
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        public string titulo { get; set; }
        public DialogDetallePagosViewModel()
        {
            titulo = "Detalle de Pagos";
            if (Application.Current.Properties["DesdeHistorialVentas"] == null && Application.Current.Properties["DetallePropinas"] == null)
            {
                this.dataTipopago = negVentaD.GetVentasDia_TipoPago2();
                titulo = "Detalle de Pagos";
            }
            else if (Application.Current.Properties["DesdeHistorialVentas"] == null && Application.Current.Properties["DetallePropinas"] != null)
            {
                DataTable dtPropinas = new DataTable();
                dtPropinas = negVentaD.GetPropinas(ConfigurationManager.AppSettings["NombreEquipo"].ToString());
                dataTipopago = new List<VentasDia>();
                foreach (DataRow dr in dtPropinas.Rows)
                {
                    VentasDia vd = new VentasDia();
                    vd.TP_id = dr["P_IDFPPROPINA"].ToString();
                    vd.TP_deno = dr["DENO_PAGO"].ToString();
                    vd.TP_monto = dr["P_PROPINA"].ToString();
                    dataTipopago.Add(vd);
                }
                titulo = "Detalle de Propinas";
            }
            else if (Application.Current.Properties["DesdeHistorialVentas"] != null && Application.Current.Properties["DetallePropinas"] != null)
            {
                DateTime desde = Convert.ToDateTime(Application.Current.Properties["DesdeHistorialVentas"]);
                DateTime hasta = Convert.ToDateTime(Application.Current.Properties["HastaHistorialVentas"]);
                DataTable dtPropinas = new DataTable();
                dtPropinas = negVentaD.GetPropinasHistorial(desde,hasta,null);
                dataTipopago = new List<VentasDia>();
                foreach (DataRow dr in dtPropinas.Rows)
                {
                    VentasDia vd = new VentasDia();
                    vd.TP_id = dr["P_IDFPPROPINA"].ToString();
                    vd.TP_deno = dr["DENO_PAGO"].ToString();
                    vd.TP_monto = dr["P_PROPINA"].ToString();
                    dataTipopago.Add(vd);
                }
                titulo = "Detalle de Propinas";
            }
            else
            {
                DateTime desde = Convert.ToDateTime(Application.Current.Properties["DesdeHistorialVentas"]);
                DateTime hasta = Convert.ToDateTime(Application.Current.Properties["HastaHistorialVentas"]);
                this.dataTipopago = negVentaD.GetVentasDia_TipoPago3(desde, hasta);
                titulo = "Detalle de Pagos";
            }
        }
    }
}
