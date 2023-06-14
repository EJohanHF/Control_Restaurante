using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Acceso;
using Capa_Entidades.Acceso;
using System.Data;

namespace Capa_Negocio.Acceso
{
    public class Neg_Modulos
    {
        Modulos dat_modulos = new Modulos();

        public DataTable N_Menus()
        {
            return dat_modulos.listarMenu();

        }

        public DataTable N_SubMenus(int id)
        {
            return dat_modulos.listarSubMenu(id);

        }
    }
}
