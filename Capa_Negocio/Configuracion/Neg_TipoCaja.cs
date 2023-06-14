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
    public class Neg_TipoCaja
    {
        Dat_TipoCaja dataTipoCaja = new Dat_TipoCaja();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<TipoCaja> GetSTipoCaja()
        {
            ObservableCollection<TipoCaja> Tipocajasep = new ObservableCollection<TipoCaja>();
            try
            {
                DataTable dt = dataTipoCaja.GetTipoCaja();
                foreach (DataRow row in dt.Rows)
                {
                    TipoCaja _scat = new TipoCaja();
                    _scat.id = Convert.ToInt32(row["id"]);
                    _scat.MC_DESCR = row["MC_DESCR"].ToString();
                    _scat.TM_DESCR = row["TM_DESCR"].ToString();
                    _scat.MC_ACT = Convert.ToByte(row["MC_ACT"]);
                    _scat.MC_ID_TIPO = Convert.ToInt32(row["MC_ID_TIPO"]);
                    Tipocajasep.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                Tipocajasep = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return Tipocajasep;
        }
 

        public bool SetTipoCaja(int operacion, TipoCaja Tipocajasep, ref string _mensaje)
        {
            bool result = false;
            result = dataTipoCaja.SetTipoCaja(operacion, Tipocajasep, ref _mensaje);
            if (result)
            {
                _mensaje = "¡Operacion Realizada con Éxito!";
            }
            return result;
        }
    }
}
