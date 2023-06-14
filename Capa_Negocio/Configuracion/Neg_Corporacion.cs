using Capa_Datos.Configuracion;
using Capa_Entidades.Models;
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
   public class Neg_Corporacion
    {
        Dat_Corporacion datCorp = new Dat_Corporacion();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Corporacion> GetCorporacion()
        {
            ObservableCollection<Corporacion> corporacion = new ObservableCollection<Corporacion>();
            try
            {
                DataTable dt = datCorp.GetCorporacion();
                foreach (DataRow row in dt.Rows)
                {
                    Corporacion _corp = new Corporacion();
                    _corp.id = Convert.ToInt32(row["id"]);
                    _corp.corp_nom = row["corp_nom"].ToString();
                    //_corp.corp_f_create = Convert.ToDateTime(row["corp_f_create"].ToString());
                    corporacion.Add(_corp);
                }

            }
            catch (Exception ex)
            {
                corporacion = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return corporacion;
        }
        public bool SetCorporacion(int operacion, Corporacion corporacion, ref string _mensaje)
        {
            bool result = false;
            result = datCorp.SetCorporacion(operacion, corporacion,ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }

    }
}
