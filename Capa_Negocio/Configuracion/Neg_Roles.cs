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
    public class Neg_Roles
    {
        Dat_Roles datRoles = new Dat_Roles();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Roles> GetRoles()
        {
            ObservableCollection<Roles> roles = new ObservableCollection<Roles>();
            try
            {
                DataTable dt = datRoles.GetRoles();
                foreach (DataRow row in dt.Rows)
                {
                    Roles _roles = new Roles();
                    _roles.idrol = Convert.ToInt32(row["ID"]);
                    _roles.nomrol = row["NOM_ROL"].ToString();
                    _roles.estadorol = Convert.ToByte(row["EST_ROL"]);
                    _roles.estadopriv = Convert.ToByte(row["PRIVILEGIO_ROL"]);
                    roles.Add(_roles);
                }

            }
            catch (Exception ex)
            {
                roles = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return roles;
        }
        public bool SetRoles(int operacion, Roles roles, ref string _mensaje)
        {
            bool result = false;
            result = datRoles.SetRoles(operacion, roles, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public List<Menu> getMenuRol(string id, ref string _mensaje)
        {
            List<Menu> menu = null;
            try
            {
                menu = new List<Menu>();
                DataTable dt = datRoles.getMenuRoles(id);
                foreach (DataRow item in dt.Rows)
                {
                    Menu _menu = new Menu();
                    _menu.idmenu = Convert.ToInt32(item["ID_MENU"]);
                    menu.Add(_menu);
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
    }
}