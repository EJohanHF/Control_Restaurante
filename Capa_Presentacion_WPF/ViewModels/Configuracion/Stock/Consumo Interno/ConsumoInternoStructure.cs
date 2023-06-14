using Capa_Entidades.Models;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Stock;
using Capa_Entidades.Models.Stock.Reporte;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Stock;
using Capa_Negocio.Stock.Reporte;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Consumo_Interno
{
    public class ConsumoInternoStructure
    {
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Platos negPlatos = new Neg_Platos();
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_Almacen alm = new Neg_Almacen();
        Neg_Empleado neg_emp = new Neg_Empleado();
        Neg_ReporteInsumoAlmacen negReportInsAlm = new Neg_ReporteInsumoAlmacen();
        #region Listas
        static List<SCategoria> scat = new List<SCategoria>();
        static List<Categoria> cat = new List<Categoria>();
        static List<Grupo> gru = new List<Grupo>();
        static List<Platos> pla = new List<Platos>();
        static ObservableCollection<Insumos> ins = new ObservableCollection<Insumos>();
        static ObservableCollection<ConsumoInterno> cinterno = new ObservableCollection<ConsumoInterno>();
        static ObservableCollection<TipoConsumo> tconsumo = new ObservableCollection<TipoConsumo>();
        static ObservableCollection<Empleado> emp = new ObservableCollection<Empleado>();
        static ObservableCollection<Almacen> almacen = new ObservableCollection<Almacen>();
        static ObservableCollection<ReporteInsumoAlmacen> insumo_almacen = new ObservableCollection<ReporteInsumoAlmacen>();

        #endregion

        public ConsumoInternoStructure()
        {
            scat = negCombo.GetComboSuperCategoria();
            cat = negCombo.GetCategoria();
            gru = negCombo.GetComboGrupo();
            pla = negCombo.GetPlato();
            ins = negInsumo.GetInsumoTotal();
            cinterno = alm.GetConsumoInterno();
            tconsumo = alm.GetTipoConsumo();
            emp = neg_emp.GetEmpleados();
            insumo_almacen = negReportInsAlm.GetListaInsumoAlmacen();
            almacen = alm.GetAlmacen();
        }

        public List<SCategoria> GetSuperCategoria()
        {
            return scat.ToList();
        }
        public List<ConsumoInterno> GetConsumoInterno()
        {
            cinterno = alm.GetConsumoInterno();
            return cinterno.ToList();
        }
        public List<ConsumoInterno> GetConsumoInternoFecha(DateTime? desde, DateTime? hasta)
        {
            cinterno = alm.GetConsumoInternoFecha(desde,hasta);
            return cinterno.ToList();
        }
        public List<Empleado> GetEmpleados()
        {
            return emp.ToList();
        }
        public List<TipoConsumo> GetTipoConsumo()
        {
            return tconsumo.ToList();
        }
        public List<Insumos> GetInsumos()
        {
            return ins.ToList();
        }
        public List<Almacen> GetAlmacenes()
        {
            return almacen.ToList();
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
        public decimal GetStockInsumo(int id_det)
        {
            insumo_almacen = negReportInsAlm.GetListaInsumoAlmacen();
            int c = insumo_almacen.Where(w => w.id == id_det).Count();
            decimal i = 0;
            if (c > 0)
            {
                i = insumo_almacen.Where(w => w.id == id_det).FirstOrDefault().cant;
            }
            return i;
        }
    }
}