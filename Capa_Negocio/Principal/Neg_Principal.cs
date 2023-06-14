using Capa_Entidades.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Principal;
using System.Data;
using Capa_Negocio.Funciones_Globales;

namespace Capa_Negocio.Principal
{
    public class Neg_Principal
    {
        Dat_Principal datPrin = new Dat_Principal();
        Funcion_Global globales = new Funcion_Global();
        public List<MenuItem> GetMenu(int idrol)
        {
            List<MenuItem> menu = new List<MenuItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datPrin.GetMenu(idrol);
                //ID,NOMBRE,ICONO,UC_CONTROL,ID_PADRE, ORDEN 
                menu = (from DataRow dr in dt.Rows
                        select new MenuItem()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            icono = Convert.ToString(dr["icono"]),
                            vista = Convert.ToString(dr["vista"]),
                            orden = Convert.ToInt32(dr["orden"]),
                            idPadre = Convert.ToInt32(dr["idPadre"]),
                            colorIcon = Convert.ToString(dr["colorIcon"]),
                            value = false
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

    }
}
