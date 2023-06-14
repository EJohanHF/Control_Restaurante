using Capa_Entidades;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Stock.chek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Insumo_
{
    public class InsumosStructure
    {
        Neg_Check_inAl negAlm = new Neg_Check_inAl();
        static List<Ent_Combo> checkItems = new List<Ent_Combo>();

        public InsumosStructure()
        {
            checkItems = negAlm.GetCheck();
        }
        public List<Ent_Combo> GetLogicalDrives()
        {
            //var x = checkItems.Select(s => new { s.nombre, s.id , s.valor1}).ToList();
            return checkItems.ToList();
        }

        public static List<Ent_Combo> GetDirectoryContents(int idcheck)
        {
            var items = new List<Ent_Combo>();
            try
            {
                items = checkItems.ToList();
            }
            catch { }
            return items;
        }
    }
}
