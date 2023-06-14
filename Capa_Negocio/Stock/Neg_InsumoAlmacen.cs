using Capa_Datos.Data.Stock;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Stock
{
public class Neg_InsumoAlmacen
    {
        Dat_InsumoAlmacen datInsumoAl = new Dat_InsumoAlmacen();
        Funcion_Global globales = new Funcion_Global();
        public bool SetInsumoAlmacen(int operacion, InsumoAlmacen insumoalmacen, ref string _mensaje)
        {
            bool result = false;
            result = datInsumoAl.SetInsumo_Almancen(operacion, insumoalmacen, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetMovimientoInsumoAlmacen(int operacion, InsumoAlmacen insumoalmacen, ref string _mensaje)
        {
            bool result = false;
            result = datInsumoAl.SetMovimiento_Insumo_Almancen(operacion, insumoalmacen, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public ObservableCollection<TipoMovimientoInsumo> GetTipoMovimientoInsumo()
        {
            ObservableCollection<TipoMovimientoInsumo> tipmov = new ObservableCollection<TipoMovimientoInsumo>();
            try
            {
                DataTable dt = datInsumoAl.GetTipoMovimientoInsumo();
                foreach (DataRow row in dt.Rows)
                {
                    TipoMovimientoInsumo _tipmov = new TipoMovimientoInsumo();
                    _tipmov.ID = Convert.ToInt32(row["ID"]);
                    _tipmov.MOV_DESCR = row["MOV_DESCR"].ToString();
                    _tipmov.MOV_ACT = Convert.ToBoolean(row["MOV_ACT"]);
                    _tipmov.MOV_F_CREATE = Convert.ToDateTime(row["MOV_F_CREATE"]);
                    _tipmov.MOV_F_MODIFICACION = Convert.ToDateTime(row["MOV_F_MODIFICACION"]);
                    tipmov.Add(_tipmov);
                }
            }
            catch (Exception ex)
            {
                tipmov = null;

                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return tipmov;
        }
        public ObservableCollection<MovimientoInsumoAlmacen> GetMovimientoInsumoAlmacen()
        {
            ObservableCollection<MovimientoInsumoAlmacen> mov = new ObservableCollection<MovimientoInsumoAlmacen>();
            try
            {
                DataTable dt = datInsumoAl.GetMovimientoInsumoAlmacen();
                foreach (DataRow row in dt.Rows)
                {
                    MovimientoInsumoAlmacen _mov = new MovimientoInsumoAlmacen();
                    _mov.ID = Convert.ToInt32(row["ID"]);
                    _mov.MI_ID_INS = Convert.ToInt32(row["MI_ID_INS"]);
                    _mov.INS_NOM =row["INS_NOM"].ToString();
                    _mov.TM_DESC = row["TM_DESC"].ToString();
                    _mov.MI_CANT = Convert.ToDecimal(row["MI_CANT"]);
                    _mov.INS_PRECIO = Convert.ToDecimal(row["INS_PRECIO"]);
                    _mov.MI_ID_USU = Convert.ToInt32(row["MI_ID_USU"]);
                    _mov.USU_NOM = row["USU_NOM"].ToString();
                    _mov.MI_DESCR = row["MI_DESCR"].ToString();
                    _mov.MI_F_CREATE = Convert.ToDateTime(row["MI_F_CREATE"]);
                    _mov.MI_ID_ALM = Convert.ToInt32(row["MI_ID_ALM"]);
                    _mov.ALM_NOM = row["ALM_NOM"].ToString();
                    _mov.MI_TIP_MOV = Convert.ToInt32(row["MI_TIP_MOV"]);
                    _mov.MOV_DESCR = row["MOV_DESCR"].ToString();
                    _mov.ENTRADA = Convert.ToDecimal(row["ENTRADA"]);
                    _mov.SALIDA = Convert.ToDecimal(row["SALIDA"]);
                    _mov.DEVOLUCION = Convert.ToDecimal(row["DEVOLUCION"]);
                    _mov.Salida_texto = "- " + _mov.SALIDA.ToString();
                    _mov.STOCK = Convert.ToDecimal(row["STOCK"]);
                    if (_mov.ENTRADA > 0)
                    {
                        _mov.EntradaForeColor = "#1de9b6";
                    }
                    else 
                    {
                        _mov.EntradaForeColor = "#000";
                    }

                    if (_mov.SALIDA > 0)
                    {
                        _mov.SalidaForeColor = "#ff1744";
                    }
                    else 
                    {
                        _mov.SalidaForeColor = "#000";
                    }
                    
                    if (_mov.STOCK < 0)
                    {
                        _mov.StockForeColor = "#ff1744";
                    }
                    else
                    {
                        _mov.StockForeColor = "#1de9b6";
                    }
                    mov.Add(_mov);
                }
            }
            catch (Exception ex)
            {
                mov = null;

                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return mov;
        }
        public ObservableCollection<MovimientoInsumoAlmacen> GetCierresInsumos()
        {
            ObservableCollection<MovimientoInsumoAlmacen> mov = new ObservableCollection<MovimientoInsumoAlmacen>();
            try
            {
                DataTable dt = datInsumoAl.GetCierresInsumos();
                foreach (DataRow row in dt.Rows)
                {
                    MovimientoInsumoAlmacen _mov = new MovimientoInsumoAlmacen();
                    _mov.CI_ID = Convert.ToInt32(row["CI_ID"]);
                    _mov.CI_CANT = Convert.ToInt32(row["CI_CANT"]);
                    _mov.CI_ID_USU = Convert.ToInt32(row["CI_ID_USU"]);
                    _mov.CI_USU_NOM = row["USU_NOM"].ToString();
                    _mov.CI_F_CREATE = Convert.ToDateTime(row["CI_F_CREATE"]);
                    _mov.CI_ID_CIERRE = Convert.ToInt32(row["CI_ID_CIERRE"]);
                    _mov.CI_ID_ALM = Convert.ToInt32(row["CI_ID_ALM"]);
                    _mov.CI_ALM_NOM = row["ALM_NOM"].ToString();
                    mov.Add(_mov);
                }
            }
            catch (Exception ex)
            {
                mov = null;

                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return mov;
        }
    }
}
