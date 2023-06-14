using Capa_Datos.Carta;
using Capa_Entidades;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Carta
{
   public class Neg_Check
    {
        #region Getcheck
        Dat_Impresora datImpre = new Dat_Impresora();
        Funcion_Global globales = new Funcion_Global();
        public List<Ent_Combo> GetCheck()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datImpre.GetImpresora();
                //ID,NOMBRE,ICONO,UC_CONTROL,ID_PADRE, ORDEN 
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            idchek = Convert.ToInt32(dr["IDIMP"]),
                            nomchek = dr["NOMEQUIPOIMP"].ToString(),
                            valor1 = false
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
    }
}
