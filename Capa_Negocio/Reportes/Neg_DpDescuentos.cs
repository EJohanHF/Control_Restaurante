using Capa_Datos.Data.Reportes.Pedidos;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes
{
    public class Neg_DpDescuentos
    {
        Dat_DpDescuento dataDescuento= new Dat_DpDescuento();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<DpDescuento> GetPdDescuento(string desde, string hasta, string mes, string dia)
        {
            ObservableCollection<DpDescuento> Descuentos = new ObservableCollection<DpDescuento>();
            try
            {
                DataTable dt = dataDescuento.GetDpDescuentos(desde, hasta, mes, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpDescuento _Desc = new DpDescuento();
                    _Desc.ID = Convert.ToInt32(row["ID"]);
                    _Desc.PED_NUM_DIARIO = row["PED_NUM_DIARIO"].ToString();
                    _Desc.M_NOM = row["M_NOM"].ToString();
                    _Desc.DP_DESCU = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _Desc.DP_IMPORT = Convert.ToDecimal(row["PED_IMPORTE"]);
                    _Desc.DENO_PAGO = row["DENO_PAGO"].ToString();
                    _Desc.DESC_EST = row["DESC_EST"].ToString();
                    _Desc.DP_FEC_REG = Convert.ToDateTime(row["PED_FECH_PED"]).ToString("dd/MM/yyyy HH:mm:ss");
                    //_Desc.DP_FEC_REG2 = _Desc.DP_FEC_REG.ToString("dd/MM/yyyy HH:mm:ss");
                    //_Desc.efectivo = row["NOM_CARGO"].ToString();
                    //_Desc.descuento = row["NOM_CARGO"].ToString();
                    _Desc.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _Desc.NOM_CARGO = row["NOM_CARGO"].ToString();
                    _Desc.DC_F_CREATE = Convert.ToDateTime(row["DC_F_CREATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                    _Desc.IDDC = row["IDDC"].ToString();
                    _Desc.TD_DESCR = row["TD_DESCR"].ToString();
                    _Desc.TD_ID = Convert.ToInt32(row["TD_ID"]);
                    Descuentos.Add(_Desc);
                }
            }
            catch (Exception ex)
            {
                Descuentos = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return Descuentos;
        }

        public ObservableCollection<DpDescuento> GetTipoPdDescuento(string desde, string hasta, string dia)
        {
            ObservableCollection<DpDescuento> TipoDescuentos = new ObservableCollection<DpDescuento>();
            try
            {
                DataTable dt = dataDescuento.GetTipoDpDescuentos(desde, hasta, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpDescuento _Desc = new DpDescuento();
                    _Desc.idDescuento = Convert.ToInt32(row["ID"]);
                    _Desc.Descripcion = row["Descripcion"].ToString();
                    _Desc.Cantidad = row["Cantidad"].ToString();
                    _Desc.Monto = row["Monto"].ToString();
                    TipoDescuentos.Add(_Desc);
                }
            }
            catch (Exception ex)
            {
                TipoDescuentos = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return TipoDescuentos;
        }
    }
}
