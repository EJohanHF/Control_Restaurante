using Capa_Datos.Configuracion;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Configuracion
{
    public class Neg_Caja
    {
        dat_Caja dataCaja = new dat_Caja();
        Funcion_Global globales = new Funcion_Global();

        //   public List<Caja> GetResumenCaja()
        //{
        //    List<Caja> cajasep = new List<Caja>();
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = dataCaja.GetResumenCaja();
        //        cajasep = (from DataRow dr in dt.Rows
        //                   select new Caja()
        //                   {
        //                       INGRESOS = Convert.ToDecimal(dr["INGRESOS"]),
        //                       EGRESOS = Convert.ToDecimal(dr["EGRESOS"])
        //                        // _scat.TM_DESCR = row["TM_DESCR"].ToString();
        //    }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        cajasep = null;
        //       //throw;
        //    }
        //    return cajasep;
        //}





        public ObservableCollection<Caja> GetSCaja(string NombreEquipo)
        {
            ObservableCollection<Caja> cajasep = new ObservableCollection<Caja>();
            try
            {
                DataTable dt = dataCaja.GetCaja(NombreEquipo);
                foreach (DataRow row in dt.Rows)
                {
                     Caja _scat = new Caja();
                    _scat.id = Convert.ToInt32(row["id"]);
                    _scat.TM_DESCR = row["TM_DESCR"].ToString();
                    _scat.MC_DESCR = row["MC_DESCR"].ToString();
                    _scat.CC_DESCR = row["CC_DESCR"].ToString();
                    _scat.CC_ID_TIPO = Convert.ToInt32(row["CC_ID_TIPO"]);
                    _scat.NOMBRE_EMPLEADO = row["NOMBRE_EMPLEADO"].ToString();
                    _scat.CC_MONTO = Convert.ToDecimal(row["CC_MONTO"]);
                    _scat.CC_F_CREATE = Convert.ToDateTime(row["CC_F_CREATE"].ToString());
                    _scat.CC_MONTO = Convert.ToDecimal(row["CC_MONTO"]);
                    _scat.CC_ID_MOV = Convert.ToInt32(row["CC_ID_MOV"]);
                    _scat.CC_ID_EMPL = Convert.ToInt32(row["CC_ID_EMPL"]);

                    cajasep.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                cajasep = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return cajasep;
        }
        public bool SetCaja(int operacion, Caja cajasep,string NombreEquipo, ref string _mensaje)
        {
            bool result = false;
            result = dataCaja.SetCaja(operacion, cajasep, NombreEquipo, ref _mensaje);
            if (result)
            {
                _mensaje = "¡Operacion Realizada con Éxito!";
            }
            return result;
        }
    }
}
