using Capa_Datos.Data.Reportes.Pedidos;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes.VentasporDia
{
    public class Neg_DpVentPlatosxImpresora
    {
        Dat_DpVentasPlatosxImpresora datVentaPlatosxImp = new Dat_DpVentasPlatosxImpresora();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<VentaPlatosxImpresora> GetVentaPlatosxImp(string desde, string hasta, string dia,int id_impre)
        {
            ObservableCollection<VentaPlatosxImpresora> VentaPlatosxImp = new ObservableCollection<VentaPlatosxImpresora>();
            try
            {

                DataTable dt = datVentaPlatosxImp.GetDpVentaPlatosImpresora(desde, hasta, dia, id_impre);
                foreach (DataRow row in dt.Rows)
                {
                    VentaPlatosxImpresora _cajachica = new VentaPlatosxImpresora();
                    _cajachica.IDIMP = Convert.ToInt32(row["IDIMP"]);
                    _cajachica.NOMEQUIPOIMP = row["NOMEQUIPOIMP"].ToString();
                    _cajachica.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _cajachica.DP_DESCRIP = row["DP_DESCRIP"].ToString();
                    _cajachica.DP_IMPORT = Convert.ToDecimal(row["DP_IMPORT"]);
                    VentaPlatosxImp.Add(_cajachica);
                }
            }
            catch (Exception ex)
            {
                VentaPlatosxImp = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return VentaPlatosxImp;
        }
    }
    
}
