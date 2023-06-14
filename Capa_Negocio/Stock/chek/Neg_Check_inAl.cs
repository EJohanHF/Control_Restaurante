using Capa_Datos.Stock;
using Capa_Entidades;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Stock.chek
{
    public class Neg_Check_inAl
    {
        #region Getcheck
        Dat_Almacen datInal = new Dat_Almacen();
        Funcion_Global globales = new Funcion_Global();
        public List<Ent_Combo> GetCheck()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datInal.GetAlmacen();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            idchek = Convert.ToInt32(dr["ID"]),
                            nomchek = dr["ALM_NOM"].ToString(),
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
