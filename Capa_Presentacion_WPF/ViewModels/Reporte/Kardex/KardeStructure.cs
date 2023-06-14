using Capa_Entidades.Models.Stock;
using Capa_Negocio.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Kardex
{
    public class KardeStructure
    {
        #region Negocio
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_InsumoAlmacen negAlmacenInsumo = new Neg_InsumoAlmacen();
        Neg_Almacen negAlm = new Neg_Almacen();
        #endregion

        #region Entidad
        static ObservableCollection<Insumos> ins = new ObservableCollection<Insumos>();
        static ObservableCollection<TipoMovimientoInsumo> tipmov = new ObservableCollection<TipoMovimientoInsumo>();
        static ObservableCollection<Almacen> alm = new ObservableCollection<Almacen>();
        static ObservableCollection<MovimientoInsumoAlmacen> movinsalm = new ObservableCollection<MovimientoInsumoAlmacen>();
        static ObservableCollection<MovimientoInsumoAlmacen> cierre_insumos = new ObservableCollection<MovimientoInsumoAlmacen>();
        static ObservableCollection<Insumos> insAlm = new ObservableCollection<Insumos>();
        #endregion
        public KardeStructure()
        {
            ins = negInsumo.GetInsumo();
            tipmov = negAlmacenInsumo.GetTipoMovimientoInsumo();
            alm = negAlm.GetAlmacen();
            movinsalm = negAlmacenInsumo.GetMovimientoInsumoAlmacen();
            cierre_insumos = negAlmacenInsumo.GetCierresInsumos();
            insAlm = negInsumo.GetAlmacenInsumo();
        }
        public List<Insumos> GetTodosInsumos()
        {
            return ins.ToList();
        }
        public List<TipoMovimientoInsumo> GetTipoMovimientoInsumoAlmacen()
        {
            return tipmov.ToList();
        }
        public List<Almacen> GetAlmacen()
        {
            return alm.ToList();
        }
        public List<MovimientoInsumoAlmacen> GetDataGeneral(int IdInsumo, int[] Movimiento, int[] Almacen, DateTime Desde, DateTime Hasta, int CantReg,int idtip)
        {
            movinsalm = negAlmacenInsumo.GetMovimientoInsumoAlmacen();
            List<MovimientoInsumoAlmacen> h = new List<MovimientoInsumoAlmacen>();
            if (idtip == 1)
            {
                if (Movimiento[0] == 0)
                {
                    h = movinsalm.Where(w => w.MI_ID_INS == IdInsumo && Almacen.Contains(w.MI_ID_ALM) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).ToList();
                }
                else
                {
                    h = movinsalm.Where(w => w.MI_ID_INS == IdInsumo && Movimiento.Contains(w.MI_TIP_MOV) && Almacen.Contains(w.MI_ID_ALM) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).ToList();
                }
            }
            else
            {
                if (Movimiento[0] == 0)
                {
                    h = movinsalm.Where(w => w.MI_ID_INS == IdInsumo && Almacen.Contains(w.MI_ID_ALM) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).Take(CantReg).ToList();
                }
                else
                {
                    h = movinsalm.Where(w => w.MI_ID_INS == IdInsumo && Movimiento.Contains(w.MI_TIP_MOV) && Almacen.Contains(w.MI_ID_ALM) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).Take(CantReg).ToList();
                }
                    
            }
            return h;
        }
        public List<MovimientoInsumoAlmacen> GetDataGeneralGeneral(int IdInsumo, int[] Movimiento, int Almacen, DateTime Desde, DateTime Hasta, int CantReg,int idtip)
        {
            movinsalm = negAlmacenInsumo.GetMovimientoInsumoAlmacen();
            List<MovimientoInsumoAlmacen> h = new List<MovimientoInsumoAlmacen>();
            if (idtip == 1)
            {
                if (Movimiento[0] == 0)
                {
                    if (Almacen == 0)
                    {
                        h = movinsalm.Where(w => w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).ToList();
                    }
                    else
                    {
                        h = movinsalm.Where(w => w.MI_ID_ALM == Almacen && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).ToList();
                    }
                }
                else
                {
                    if (Almacen == 0)
                    {
                        h = movinsalm.Where(w => Movimiento.Contains(w.MI_TIP_MOV) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).ToList();
                    }
                    else
                    {
                        h = movinsalm.Where(w => Movimiento.Contains(w.MI_TIP_MOV) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).ToList();
                    }
                }
            }
            else
            {
                if (Movimiento[0] == 0)
                {
                    if (Almacen == 0)
                    {
                        h = movinsalm.Where(w => w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).Take(CantReg).ToList();
                    }
                    else 
                    {
                        h = movinsalm.Where(w => w.MI_ID_ALM == Almacen && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).Take(CantReg).ToList();
                    }
                }
                else
                {
                    if (Almacen == 0)
                    {
                        h = movinsalm.Where(w => Movimiento.Contains(w.MI_TIP_MOV) && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).Take(CantReg).ToList();
                    }
                    else
                    {
                        h = movinsalm.Where(w => Movimiento.Contains(w.MI_TIP_MOV) && w.MI_ID_ALM == Almacen && w.MI_F_CREATE.Date >= Desde.Date && w.MI_F_CREATE.Date <= Hasta.Date).Take(CantReg).ToList();
                    }

                        
                }
                    
            }
            return h;
        }
        public List<Insumos> GetInsumos_x_Almacen(int[] id_almacen)
        {

            var Lista = insAlm.GroupBy(c => c.idins)
                   .Select(grp => grp.First()).Where(w => id_almacen.Contains(w.ID_ALMA))
                   .ToList();
            //var Lista = insAlm.Where(w => id_almacen.Contains(w.ID_ALMA)).ToList();
            return Lista;
        }
        public List<Insumos> GetInsumos()
        {
            var Lista = insAlm.ToList();
            return Lista;
        }
        public decimal GetStockCierreInsumo(int IdInsumo, int Almacen, DateTime Desde, DateTime Hasta)
        {
            decimal stockInsumo;
            if (cierre_insumos.Count() == 0)
            {
                stockInsumo = 0;
            }
            else
            { 
                stockInsumo = cierre_insumos.Where(w => w.CI_ID_INS == IdInsumo && w.CI_F_CREATE.Date >= Desde && w.CI_F_CREATE.Date <= Hasta).OrderBy(o => o.CI_F_CREATE).Take(1).FirstOrDefault().CI_CANT;
            }
            return stockInsumo;
        }
    }
}
