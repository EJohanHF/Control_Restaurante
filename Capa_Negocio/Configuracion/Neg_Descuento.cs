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
    public class Neg_Descuento
    {
        Dat_Descuento dataDescuento = new Dat_Descuento();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Descuento> GetSDesc()
        {
            ObservableCollection<Descuento> Descuentosep = new ObservableCollection<Descuento>();
            try
            {
                DataTable dt = dataDescuento.GetDescuento();
                foreach (DataRow row in dt.Rows)
                {
                    Descuento _scat = new Descuento();
                    _scat.id = Convert.ToInt32(row["id"]);
                    _scat.TD_DESCR = row["TD_DESCR"].ToString();
                    _scat.TD_ACT = Convert.ToByte(row["TD_ACT"]);
                    _scat.TD_PORCENTAJE = Convert.ToDecimal(row["TD_PORCENTAJE"]);
                    Descuentosep.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                Descuentosep = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return Descuentosep;
        }
        public bool SetDescuento(int operacion, Descuento Descuentosep, ref string _mensaje)
        {
            bool result = false;
            result = dataDescuento.SetDescuento(operacion, Descuentosep, ref _mensaje);
            if (result)
            {
                _mensaje = "¡Operacion Realizada con Éxito!";
            }
            return result;
        }
    }
}
