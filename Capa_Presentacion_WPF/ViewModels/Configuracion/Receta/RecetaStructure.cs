using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Receta;
using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Receta;
using Capa_Negocio.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Receta
{
    public class RecetaStructure
    {
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Platos negPlatos = new Neg_Platos();
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_Receta negReceta = new Neg_Receta();

        #region Listas
        static List<SCategoria> scat = new List<SCategoria>();
        static List<Categoria> cat = new List<Categoria>();
        static List<Grupo> gru = new List<Grupo>();
        static List<Platos> pla = new List<Platos>();
        static ObservableCollection<Insumos> ins = new ObservableCollection<Insumos>();
        static ObservableCollection<Insumos> inssubreceta = new ObservableCollection<Insumos>();
        static ObservableCollection<Recetas> rec = new ObservableCollection<Recetas>();
        #endregion

        public RecetaStructure()
        {
            scat = negCombo.GetComboSuperCategoria();
            cat = negCombo.GetCategoria();
            gru = negCombo.GetComboGrupo();
            pla = negCombo.GetPlato();
            ins = negInsumo.GetInsumo();
            inssubreceta = negInsumo.GetInsumoySubReceta();
            rec = negReceta.GetReceta();
        }

        public List<SCategoria> GetSuperCategoria()
        {
            return scat.ToList();
        }

        public List<Categoria> GetCategoria(int idscat)
        {
            return cat.Where(w => w.idscat == idscat).ToList();
        }

        public List<Grupo> GetGrupo(string idcat)
        {
            return gru.Where(w => w.idcat == idcat).ToList();
        }
        public List<Platos> GetPlatos(string idgru)
        {
            return pla.Where(w => w.idgrup == idgru).ToList();
        }
        public List<Insumos> GetInsumos()
        {
            return ins.ToList();
        }
        public List<Insumos> GetInsumosYSubRecetas()
        {
            return inssubreceta.ToList();
        }

        public decimal CantidadInsumo(int id)
        {
            var receta = rec.Where(w => w.ID == id).First();
            decimal cant = receta.RE_CANT_INS;
            return cant;
        }
        public List<Recetas> GetReceta(int idCarta)
        {
            rec = negReceta.GetReceta();
            return rec.Where(w => w.RE_ID_CARTA == idCarta).ToList();
        }
        public List<Recetas> GetRecetaTotal()
        {
            return rec.ToList();
        }
        public decimal GetCostoReceta(int idCarta)
        {
            var recc = rec.Where(w => w.RE_ID_CARTA == idCarta).ToList();
            decimal d = recc.Sum(x => x.RE_COSTO_RECETA);
            return d;
        }

    }
}
