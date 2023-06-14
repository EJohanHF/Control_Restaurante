using Capa_Datos.Data.Reportes.VentasPorDia;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes.VentasporDia
{
  public  class Neg_BoletaFactura
    {
        Dat_BoletaFactura DatBolFac = new Dat_BoletaFactura();
        Funcion_Global globales = new Funcion_Global();
        public List<BoletaFactura> GetBoletaFactura1(int id_pedido)
        {
            List<BoletaFactura> menu = new List<BoletaFactura>();
            try
            {
                DataTable dt = new DataTable();
                dt = DatBolFac.GetBoletaFactura(id_pedido);
                menu = (from DataRow dr in dt.Rows
                        select new BoletaFactura()
                        {
                            id_bf = Convert.ToInt32(dr["id"]),
                            cant = Convert.ToInt32(dr["CANTIDAD_DETALLEPEDIDO"]),
                            descripcion = Convert.ToString(dr["DP_DESCRIP"]),
                            preciouni = Convert.ToDecimal(dr["DP_PRE_UNI"]),
                            descuento = Convert.ToDecimal(dr["PED_DESCUENTO"]),
                            importe = Convert.ToDecimal(dr["PED_TOTAL"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return menu;
        }

        public ObservableCollection<BoletaFactura> GetBoletaFactura( int id_pedido)
        {
            ObservableCollection<BoletaFactura> boletafactura = new ObservableCollection<BoletaFactura>();
            try
            {
                DataTable dt = DatBolFac.GetBoletaFactura(id_pedido);
                foreach (DataRow row in dt.Rows)
                {
                    BoletaFactura _datBolFac = new BoletaFactura();
                    _datBolFac.id_bf= Convert.ToInt32(row["ID"]);
                    _datBolFac.cant = Convert.ToInt32(row["CANTIDAD_DETALLEPEDIDO"]);
                    _datBolFac.descripcion = row["DP_DESCRIP"].ToString();
                    _datBolFac.preciouni = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _datBolFac.descuento = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _datBolFac.importe = Convert.ToDecimal(row["PED_TOTAL"]);
                    boletafactura.Add(_datBolFac);
                }
            }
            catch (Exception ex)
            {
                boletafactura = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return boletafactura;
        }
        //public ObservableCollection<BoletaFactura> GetBoletaFactura2()
        //{
        //    ObservableCollection<BoletaFactura> boletafactura = new ObservableCollection<BoletaFactura>();
        //    try
        //    {
        //        DataTable dt = DatBolFac.GetBoletaFactura2();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            BoletaFactura _datBolFac = new BoletaFactura();
        //            _datBolFac.id_bf = Convert.ToInt32(row["ID"]);
        //            _datBolFac.cant = Convert.ToInt32(row["CANTIDAD_DETALLEPEDIDO"]);
        //            _datBolFac.descripcion = row["DP_DESCRIP"].ToString();
        //            _datBolFac.preciouni = Convert.ToDecimal(row["DP_PRE_UNI"]);
        //            _datBolFac.descuento = Convert.ToDecimal(row["PED_DESCUENTO"]);
        //            _datBolFac.importe = Convert.ToDecimal(row["PED_TOTAL"]);
        //            boletafactura.Add(_datBolFac);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        boletafactura = null;
        //    }
        //    return boletafactura;
        //}
    }
}
