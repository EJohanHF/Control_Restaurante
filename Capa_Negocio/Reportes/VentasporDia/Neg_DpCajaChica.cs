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
    public class Neg_DpCajaChica
    {
        Dat_DpCajaChica datCajaChica = new Dat_DpCajaChica();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<CajaChica> GetCajaChica(string desde, string hasta, string dia)
        {
            ObservableCollection<CajaChica> caja = new ObservableCollection<CajaChica>();
            try
            {

                DataTable dt = datCajaChica.GetDpCajaChica(desde, hasta, dia);
                foreach (DataRow row in dt.Rows)
                {
                    CajaChica _cajachica = new CajaChica();
                    _cajachica.IDTMCAJA = row["IDTMCAJA"].ToString();
                    _cajachica.TM_DESCR = row["TM_DESCR"].ToString();

                    _cajachica.IDMOVCAJA = row["IDMOVCAJA"].ToString();

                    _cajachica.MC_DESCR = row["MC_DESCR"].ToString();
                    _cajachica.IDCAJACHICA = row["IDCAJACHICA"].ToString();

                    _cajachica.CC_MONTO = Convert.ToDecimal(row["CC_MONTO"]);
                    _cajachica.CC_DESCR = row["CC_DESCR"].ToString();
                    _cajachica.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _cajachica.CC_F_CREATE = row["CC_F_CREATE"].ToString();
                    caja.Add(_cajachica);
                }
            }
            catch (Exception ex)
            {
                caja = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return caja;
        }
    }
}
