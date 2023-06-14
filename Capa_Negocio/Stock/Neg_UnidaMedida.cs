using Capa_Datos.Stock;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Stock
{
  public class Neg_UnidaMedida
    {

        Dat_UnidadMedida dataUnidadMedida = new Dat_UnidadMedida();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<UnidadMedida> GetUnidadMedida()
        {
            ObservableCollection<UnidadMedida> unidadmedida = new ObservableCollection<UnidadMedida>();
            try
            {
                DataTable dt = dataUnidadMedida.GetUnidadMedida();
                foreach (DataRow row in dt.Rows)
                {
                    UnidadMedida _unidadmedida = new UnidadMedida();
                    _unidadmedida.idum = Convert.ToInt32(row["ID"]);
                    _unidadmedida.descum= row["TM_DESC"].ToString();
                    _unidadmedida.denom= row["TM_DENOM"].ToString();
                    _unidadmedida.estadoum= Convert.ToByte(row["TM_ESTADO"]);
                    unidadmedida.Add(_unidadmedida);
                }

            }
            catch (Exception ex)
            {
                unidadmedida = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return unidadmedida;
        }
        public bool SetUnidadMedida(int operacion, UnidadMedida unidadmedida, ref string _mensaje)
        {
            bool result = false;
            result = dataUnidadMedida.SetUnidadMedida(operacion, unidadmedida, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}
