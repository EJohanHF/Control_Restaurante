using Capa_Datos.Configuracion;
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
   public class Neg_RolCargo
    {
        Dat_RolCargo datCargo = new Dat_RolCargo();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<RolCargo> GetRolCargo()
        {
            ObservableCollection<RolCargo> rolcargo = new ObservableCollection<RolCargo>();
            try
            {
                DataTable dt = datCargo.GetRolCargo();
                foreach (DataRow row in dt.Rows)
                {
                    RolCargo _rolcargo = new RolCargo();
                    _rolcargo.id = Convert.ToInt32(row["id"]);
                    _rolcargo.nom_cargo = row["NOM_CARGO"].ToString();
                    rolcargo.Add(_rolcargo);
                }

            }
            catch (Exception ex)
            {
                rolcargo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return rolcargo;
        }
        public bool SetRolCargo(int operacion, RolCargo rolcargo, ref string _mensaje)
        {
            bool result = false;
            result = datCargo.SetRolCargo(operacion, rolcargo, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}
