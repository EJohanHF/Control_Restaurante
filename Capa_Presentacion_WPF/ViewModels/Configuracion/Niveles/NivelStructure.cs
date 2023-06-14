using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Nivel;
using Capa_Negocio.Carta;
using Capa_Negocio.Nivel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Niveles
{
    public class NivelStructure
    {
        #region Negocio
        Neg_Nivel negNivel = new Neg_Nivel();
        Neg_Grupo negGrupo = new Neg_Grupo();
        Neg_Platos negPla = new Neg_Platos();
        
        #endregion
        #region Entidad
        static ObservableCollection<Ent_Nivel> niv = new ObservableCollection<Ent_Nivel>();
        static ObservableCollection<Ent_Nivel> subn = new ObservableCollection<Ent_Nivel>();
        static ObservableCollection<Ent_Nivel> sel = new ObservableCollection<Ent_Nivel>();
        static ObservableCollection<Grupo> gru = new ObservableCollection<Grupo>();
        static ObservableCollection<Platos> pla = new ObservableCollection<Platos>();
        #endregion
        public NivelStructure()
        {
            niv = negNivel.GetNivel();
            subn = negNivel.GetSubNivel();
            sel = negNivel.GetSeleccionNivel();
            gru = negGrupo.GetGupo();
            pla = negPla.GetPlatos();
        }
        public List<Ent_Nivel> GetNiveles(int op)
        {
            if (op == 1) { niv = negNivel.GetNivel(); }
            return niv.ToList();
        }
        
        public List<Ent_Nivel> GetSeleccionNiveles()
        {
            return sel.Where(w => w.SS_ACT == true).ToList();
        }
        public List<Ent_Nivel> GetSubNiveles()
        {
            return subn.Where(w => w.SN_ACT == true).ToList();
        }
        public List<Ent_Nivel> GetSubNivelesxNivel(int _id,int op)
        {
            if (op == 1) { subn = negNivel.GetSubNivel(); }
            return subn.Where(w => w.SN_ID_NIVEL == _id).ToList();
        }
        public string GetNombreNivelSelect(int _id)
        {
            return niv.Where(w => w.ID == _id).FirstOrDefault().N_NOM;
        }
        public List<Grupo> GetGrupos()
        {
            return gru.ToList();
        }
        public List<Platos> GetPlatosxGrupo(int id)
        {
            string _id = id.ToString();
            return pla.Where(w => w.idgrup == _id && w.estadoplato == 1).ToList();
        }
    }
}
