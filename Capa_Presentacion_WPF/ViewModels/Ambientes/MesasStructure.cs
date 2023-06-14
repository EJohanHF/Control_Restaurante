using Capa_Entidades.Models.Ambientes;
using Capa_Negocio.Ambientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Ambientes
{
    public class MesasStructure
    {
        Neg_Mesa neg = new Neg_Mesa();
        static List<MesasItem> mesasItems = new List<MesasItem>();
        static List<MesasItem> tipoMesas = new List<MesasItem>();
        public MesasStructure()
        {
            mesasItems = neg.GetMesas();
            tipoMesas = neg.GetTipoMesas();
        }
        public List<MesasItem> GetLogicalDrives()
        {

            //var x = menuItems.Select(s => new { s.nombre, s.id }).ToList();
            return mesasItems.ToList();
        }
        public List<MesasItem> GetMesas()
        {
            mesasItems = neg.GetMesas();
            return mesasItems.ToList();
        }
        public List<MesasItem> GetMesasxEstado(bool est)
        {
            mesasItems = neg.GetMesas();
            return mesasItems.Where(w => w.M_ACT == est).ToList();
        }
        public List<MesasItem> GetMesasxEstadoAmbiente(int idambiente,bool est)
        {
            mesasItems = neg.GetMesas();
            return mesasItems.Where(w => w.M_ID_AMB == idambiente && w.M_ACT == est).ToList();
        }

        public List<MesasItem> GetMesasNoSubMesas(int idAmbiente)
        {
            return mesasItems.Where(w => w.M_TIPO == 1 && w.M_ACT == true && w.M_ID_AMB == idAmbiente).ToList();
        }
        public List<MesasItem> GetTipoMesa()
        {
            return tipoMesas.ToList();
        }
        public List<MesasItem> GetEstadosMesas()
        {
            mesasItems = neg.GetMesas();
            return mesasItems.GroupBy(m => m.ESTADO_MESA)
                    .Select(grp => grp.First())
                    .ToList();
        }
        public static List<MesasItem> GetDirectoryContents(int idAmbiente)
        {
            var items = new List<MesasItem>();
            try
            {
                items = mesasItems.Where(w => w.M_ID_AMB == idAmbiente).ToList();
            }
            catch { }
            return items;
        }
        #region Helpers
        public static string GetFileFolderName(int _id)
        {
            var name = "";
            try
            {
                name = mesasItems.Where(w => w.ID == _id).Select(s => s.M_NOM).FirstOrDefault().ToString();
            }
            catch { }
            return name;
        }
        public static int GetNumChildrens(int idAmbiente)
        {
            var numChildrens = 0;
            try
            {
                numChildrens = mesasItems.Where(w => w.M_ID_AMB == idAmbiente).Count();
            }
            catch { }
            return numChildrens;
        }

        internal static string GetVista(int _id)
        {
            var vista = "";
            try
            {
                vista = mesasItems.Where(w => w.ID == _id).Select(s => s.M_NOM).FirstOrDefault().ToString();
            }
            catch { }
            return vista;
        }
        #endregion
    }
}
