using Capa_Datos;
using Capa_Datos.Data.Reportes.VentasPorDia;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes.VentasporDia
{
    public class Neg_CierresDias
    {
        MEnsaje mensaje = new MEnsaje();
        Dat_CierresDias data_cierre = new Dat_CierresDias();
        public ObservableCollection<Ent_CierresDias> GET_CIERRES()
        {
            ObservableCollection<Ent_CierresDias> empresa = new ObservableCollection<Ent_CierresDias>();
            try
            {

                DataTable dt = data_cierre.GET_CIERRES();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_CierresDias _c = new Ent_CierresDias();
                    _c.ID = Convert.ToInt32(row["ID"]);
                    _c.DC_MONTO_EFECTIVO= Convert.ToDecimal(row["DC_MONTO_EFECTIVO"]);
                    _c.DC_MONTO_TARJETA= Convert.ToDecimal(row["DC_MONTO_TARJETA"]);
                    _c.DC_MONTO_TOTAL= Convert.ToDecimal(row["DC_MONTO_TOTAL"]);
                    _c.DC_ID_TIP_MONEDA = Convert.ToDecimal(row["DC_ID_TIP_MONEDA"]);
                    _c.DC_ID_TIPO_CAMBIO = Convert.ToInt32(row["DC_ID_TIPO_CAMBIO"]);
                    _c.DC_ID_USU = Convert.ToInt32(row["DC_ID_USU"]);
                    _c.USU_NOM = row["USU_NOM"].ToString();
                    _c.DC_F_CREATE = Convert.ToDateTime(row["DC_F_CREATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                    empresa.Add(_c);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
                //mensaje.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }
        public ObservableCollection<Ent_CierresDias> GET_CIERRES_X_DIA(string dia)
        {
            ObservableCollection<Ent_CierresDias> empresa = new ObservableCollection<Ent_CierresDias>();
            try
            {

                DataTable dt = data_cierre.GET_CIERRES_X_DIA(dia);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_CierresDias _c = new Ent_CierresDias();
                    _c.ID = Convert.ToInt32(row["ID"]);
                    _c.DC_MONTO_EFECTIVO = Convert.ToDecimal(row["DC_MONTO_EFECTIVO"]);
                    _c.DC_MONTO_TARJETA = Convert.ToDecimal(row["DC_MONTO_TARJETA"]);
                    _c.DC_MONTO_TOTAL = Convert.ToDecimal(row["DC_MONTO_TOTAL"]);
                    _c.DC_ID_TIP_MONEDA = Convert.ToDecimal(row["DC_ID_TIP_MONEDA"]);
                    _c.DC_ID_TIPO_CAMBIO = Convert.ToInt32(row["DC_ID_TIPO_CAMBIO"]);
                    _c.DC_ID_USU = Convert.ToInt32(row["DC_ID_USU"]);
                    _c.USU_NOM = row["USU_NOM"].ToString();
                    _c.DC_F_CREATE = Convert.ToDateTime(row["DC_F_CREATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                    empresa.Add(_c);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
                //mensaje.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }
        public ObservableCollection<Ent_CierresDias> GET_CIERRES_ENTRE_DIA(string fecha, string fecha2)
        {
            ObservableCollection<Ent_CierresDias> empresa = new ObservableCollection<Ent_CierresDias>();
            try
            {

                DataTable dt = data_cierre.GET_CIERRES_ENTRE_DIA(fecha,fecha2);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_CierresDias _c = new Ent_CierresDias();
                    _c.ID = Convert.ToInt32(row["ID"]);
                    _c.DC_MONTO_EFECTIVO = Convert.ToDecimal(row["DC_MONTO_EFECTIVO"]);
                    _c.DC_MONTO_TARJETA = Convert.ToDecimal(row["DC_MONTO_TARJETA"]);
                    _c.DC_MONTO_TOTAL = Convert.ToDecimal(row["DC_MONTO_TOTAL"]);
                    _c.DC_ID_TIP_MONEDA = Convert.ToDecimal(row["DC_ID_TIP_MONEDA"]);
                    _c.DC_ID_TIPO_CAMBIO = Convert.ToInt32(row["DC_ID_TIPO_CAMBIO"]);
                    _c.DC_ID_USU = Convert.ToInt32(row["DC_ID_USU"]);
                    _c.USU_NOM = row["USU_NOM"].ToString();
                    _c.DC_F_CREATE = Convert.ToDateTime(row["DC_F_CREATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                    empresa.Add(_c);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
            }
            return empresa;
        }
    }
}
