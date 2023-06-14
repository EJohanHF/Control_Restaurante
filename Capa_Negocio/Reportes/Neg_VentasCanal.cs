using Capa_Datos.Data.Reportes.Pedidos;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reportes.DetalleporDia;
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
    public class Neg_VentasCanal
    {
        Dat_VentasxCanal dtVentas = new Dat_VentasxCanal();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Detalle_Pedido> getVentasCanal(DateTime desde, DateTime hasta, int id_canal, int id_tipo_pago)
        {
            ObservableCollection<Detalle_Pedido> detallepedido = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = dtVentas.getVentasCanal(desde, hasta, id_canal, id_tipo_pago);
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_Pedido _detPed = new Detalle_Pedido();
                    _detPed.id_ped = Convert.ToInt32(row["ID"]);
                    _detPed.num_dia_ped = row["PED_NUM_DIARIO"].ToString();

                    _detPed.id_mesa = row["IDMESA"].ToString();
                    _detPed.nom_mesa = row["M_NOM"].ToString();
                    _detPed.id_cliente = row["PED_ID_CLIENTE"].ToString();
                    _detPed.id_emple = row["IDEMP"].ToString();
                    _detPed.nom_emple = row["EMPL_NOM"].ToString();
                    _detPed.subtotal_ped = row["PED_SUBTOTAL"].ToString();
                    _detPed.f_ped = Convert.ToDateTime(row["PED_FECH_PED"].ToString());
                    _detPed.impor_ped = row["PED_IMPORTE"].ToString();
                    _detPed.desc_ped = row["PED_DESCUENTO"].ToString();
                    _detPed.id_tip_desc = row["IDTDESC"].ToString();
                    _detPed.nom_tip_desc = row["TD_DESCR"].ToString();
                    _detPed.id_estado_f = row["PED_ID_ESTADO"].ToString();
                    _detPed.nom_estado_f = row["DESC_EST"].ToString();
                    _detPed.total_ped = row["PED_TOTAL"].ToString();
                    _detPed.id_fpago = row["IDFPAGO"].ToString();
                    _detPed.nom_fpago = row["DENO_PAGO"].ToString();
                    _detPed.id_usu = row["IDUSU"].ToString();
                    _detPed.nom_usu = row["USU_NOM"].ToString();
                    _detPed.nro_personas = row["PED_NRO_PERSONAS"].ToString();
                    _detPed.nro_tarjeta = row["DT_NRO_TARJETA"].ToString();
                    _detPed.nom_tipo_Doc = row["tipoDocumento"].ToString();
                    _detPed.importe_Total_Doc_Elec = row["importeTotal"].ToString();
                    _detPed.nomllevar = row["nomllevar"].ToString();
                    _detPed.telefcli = row["telefcli"].ToString();
                    detallepedido.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detallepedido = null;
            }
            return detallepedido;
        }
        public ObservableCollection<Detalle_Pedido> getPlatosVendidos(DateTime desde, DateTime hasta, int id_canal)
        {
            ObservableCollection<Detalle_Pedido> detallepedido = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = dtVentas.getPlatosVendidos(desde, hasta, id_canal);
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_Pedido _detPed = new Detalle_Pedido();
                    _detPed.id_carta = Convert.ToInt32(row["ID"]);
                    _detPed.CANTIDAD = Convert.ToDecimal(row["CANTIDAD"]);
                    _detPed.PLATO = row["PLATO"].ToString();
                    _detPed.IMPORTE = Convert.ToDecimal(row["IMPORTE"]);
                    _detPed.DESCUENTO = Convert.ToDecimal(row["DESCUENTO"]);
                    detallepedido.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detallepedido = null;
            }
            return detallepedido;
        }
        public ObservableCollection<Ambiente> getComboCanal()
        {
            ObservableCollection<Ambiente> amb = new ObservableCollection<Ambiente>();
            try
            {
                DataTable dt = dtVentas.getComboCanal();
                foreach (DataRow row in dt.Rows)
                {
                    Ambiente _amb = new Ambiente();
                    _amb.id = Convert.ToInt32(row["ID"]);
                    _amb.nombre = row["A_NOM"].ToString();
                    amb.Add(_amb);
                }
            }
            catch (Exception ex)
            {
                amb = null;
            }
            return amb;
        }
        public ObservableCollection<SJesus> getComboTipoPago()
        {
            ObservableCollection<SJesus> tp = new ObservableCollection<SJesus>();
            try
            {
                DataTable dt = dtVentas.getComboTipoPago();
                foreach (DataRow row in dt.Rows)
                {
                    SJesus _tp = new SJesus();
                    _tp.id = Convert.ToInt32(row["ID"]);
                    _tp.deno_pago = row["DENO_PAGO"].ToString();
                    tp.Add(_tp);
                }
            }
            catch (Exception ex)
            {
                tp = null;
            }
            return tp;
        }
    }
}
