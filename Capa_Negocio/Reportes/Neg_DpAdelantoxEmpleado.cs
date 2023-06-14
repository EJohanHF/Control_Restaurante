using Capa_Datos.Data.Reportes.Pedidos;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes
{
    public class Neg_DpAdelantoxEmpleado
    {
        Dat_DpAdelantoxEmpleado dataAdelanto = new Dat_DpAdelantoxEmpleado();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<DpAdelantoxEmpleado> GetAdelantoxEmpleado(string desde, string hasta, string mes, string dia)
        {
            ObservableCollection<DpAdelantoxEmpleado> Adelanto = new ObservableCollection<DpAdelantoxEmpleado>();
            try
            {
                DataTable dt = dataAdelanto.GetAdelantoxEmpleado(desde, hasta, mes, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpAdelantoxEmpleado _Adel = new DpAdelantoxEmpleado();
                    _Adel.id = row["id"].ToString();
                    _Adel.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _Adel.CC_MONTO = row["CC_MONTO"].ToString();
                    _Adel.CC_DESCR = row["CC_DESCR"].ToString();
                    _Adel.CC_F_CREATE = row["CC_F_CREATE"].ToString();
                    Adelanto.Add(_Adel);
                }
            }
            catch (Exception ex)
            {
                Adelanto = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return Adelanto;
        }

        public ObservableCollection<DpAdelantoxEmpleado> GetTotalAdelantoxEmpleado(string desde, string hasta, string dia)
        {
            ObservableCollection<DpAdelantoxEmpleado> TipoAdelanto = new ObservableCollection<DpAdelantoxEmpleado>();
            try
            {
                DataTable dt = dataAdelanto.GetTotalAdelantoxEmpleado(desde, hasta, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpAdelantoxEmpleado _Adel = new DpAdelantoxEmpleado();
                    _Adel.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _Adel.CC_MONTO = row["CC_MONTO"].ToString();
                    TipoAdelanto.Add(_Adel);
                }
            }
            catch (Exception ex)
            {
                TipoAdelanto = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return TipoAdelanto;
        }
    }
}
