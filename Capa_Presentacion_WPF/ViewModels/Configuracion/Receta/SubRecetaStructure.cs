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
    public class SubRecetaStructure
    {
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_Receta negReceta = new Neg_Receta();

        #region Listas
        static ObservableCollection<Insumos> ins = new ObservableCollection<Insumos>();
        static ObservableCollection<Recetas> rec = new ObservableCollection<Recetas>();
        static ObservableCollection<Detalle_SubReceta_Insumo> srdi = new ObservableCollection<Detalle_SubReceta_Insumo>();
        #endregion

        public SubRecetaStructure()
        {
            ins = negInsumo.GetInsumo();
            rec = negReceta.GetReceta();
            srdi = negReceta.GetDetalleSubRecetaInsumo();
        }
        public List<Recetas> GetSubRecetas()
        {
            rec = negReceta.GetSubReceta();
            return rec.ToList();
        }
        public List<Detalle_SubReceta_Insumo> GetInsumosxSR(int id)
        {
            srdi = negReceta.GetDetalleSubRecetaInsumo();
            return srdi.Where(w => w.DSI_ID_SUB_RECETA == id).ToList();
        }
        public string GetNombreSR(int id)
        {
            try
            {
                var t = "";

                srdi = negReceta.GetDetalleSubRecetaInsumo();

                if (srdi.Count() > 0)
                {
                    //t= srdi.Where(w => w.DSI_ID_SUB_RECETA == id).FirstOrDefault().SR_DESCR;
                    t = srdi.Where(i => i.DSI_ID_SUB_RECETA == id).FirstOrDefault().SR_DESCR;
                }
                return t;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public List<Insumos> GetInsumos()
        {
            ins = negInsumo.GetInsumo();
            return ins.ToList();
        }
        public List<Insumos> GetInsumoySubReceta()
        {
            ins = negInsumo.GetInsumoySubReceta();
            return ins.ToList();
        }
        public List<Insumos> GetInsumosS(int idins)
        {
            ins = negInsumo.GetInsumo();
            return ins.Where(w => w.idins == idins).ToList();
        }
        public decimal CantidadInsumo(int id)
        {
            rec = negReceta.GetReceta();
            var receta = rec.Where(w => w.ID == id).First();
            decimal cant = receta.RE_CANT_INS;
            return cant;
        }
        public List<Recetas> GetReceta(int idCarta)
        {
            rec = negReceta.GetReceta();
            return rec.Where(w => w.RE_ID_CARTA == idCarta && w.RE_SUB_RECETA == true).ToList();
        }
        public List<Recetas> GetRecetaTotal()
        {
            rec = negReceta.GetReceta();
            return rec.ToList();
        }
        public decimal GetCostoReceta(int idSubReceta)
        {
            rec = negReceta.GetReceta();
            var recc = rec.Where(w => w.ID == idSubReceta).ToList();
            decimal d = recc.Sum(x => x.RE_COSTO_RECETA);
            return d;
        }

        public List<Detalle_SubReceta_Insumo> GetDetalleSubRecetaInsumo(int IdSubReceta)
        {
            srdi = negReceta.GetDetalleSubRecetaInsumo();
            return srdi.Where(w => w.DSI_ID_SUB_RECETA == IdSubReceta).ToList();
        }
    }
}
