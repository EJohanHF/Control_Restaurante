using Capa_Entidades;
using Capa_Negocio.Carta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel
{
   public class PlatosStructure
    {
        Neg_Check neg = new Neg_Check();
        static List<Ent_Combo> checkItems = new List<Ent_Combo>();

        public PlatosStructure()
        {
            checkItems = neg.GetCheck();
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
