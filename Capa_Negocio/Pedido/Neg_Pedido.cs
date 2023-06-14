using Capa_Datos.Pedido;
using System;
using Capa_Entidades.Models.Pedido;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Reportes.DetalleporDia;

namespace Capa_Negocio.Pedido
{
    public class Neg_Pedido
    {
        MEnsaje mensaje = new MEnsaje();
        public void ActualizarMesaOcupada(int id_mesa)
        {
            dataPedido.ActualizarMesaAtendida(id_mesa);
        }
        public void ActualizarMesaLibre(int id_mesa)
        {
            dataPedido.ActualizarMesaLibre(id_mesa);
        }
        Dat_Pedido dataPedido = new Dat_Pedido();
        public ObservableCollection<Pedidos> GetPedidoxId(int id_pedido)
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GET_PEDIDO_X_ID(id_pedido);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString() + " " + row["DP_COMENTARIO"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.DP_DESCU = Convert.ToDecimal(row["DP_DESCU"]);
                    _pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _pedido.DETEMPL = row["DETEMPL"].ToString();
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.PED_NRO_PERSONAS = Convert.ToInt32(row["PED_NRO_PERSONAS"]);
                    _pedido.PED_NUM_DIARIO = Convert.ToInt32(row["PED_NUM_DIARIO"]);
                    _pedido.C_NOM = row["C_NOMINA"].ToString();
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _pedido.nomllevar = row["nomllevar"].ToString();
                    _pedido.telllevar = row["telefcli"].ToString();
                    _pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public ObservableCollection<Pedidos> GetPedidoxIdTotal(int id_pedido)
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GET_PEDIDO_X_ID_TOTAL(id_pedido);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString() + " " + row["DP_COMENTARIO"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.DP_DESCU = Convert.ToDecimal(row["DP_DESCU"]);
                    _pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _pedido.DETEMPL = row["DETEMPL"].ToString();
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.PED_NRO_PERSONAS = Convert.ToInt32(row["PED_NRO_PERSONAS"]);
                    _pedido.PED_NUM_DIARIO = Convert.ToInt32(row["PED_NUM_DIARIO"]);
                    _pedido.C_NOM = row["C_NOMINA"].ToString();
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _pedido.nomllevar = row["nomllevar"].ToString();
                    _pedido.telllevar = row["telefcli"].ToString();
                    _pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public ObservableCollection<Pedidos>SP_SELECT_PEDIDO_X_ID_CUENTA(int id_pedido)
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.SP_SELECT_PEDIDO_X_ID_CUENTA(id_pedido);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString() + " " + row["DP_COMENTARIO"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.DP_DESCU = Convert.ToDecimal(row["DP_DESCU"]);
                    _pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _pedido.DETEMPL = row["DETEMPL"].ToString();
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.PED_NRO_PERSONAS = Convert.ToInt32(row["PED_NRO_PERSONAS"]);
                    _pedido.PED_NUM_DIARIO = Convert.ToInt32(row["PED_NUM_DIARIO"]);
                    _pedido.C_NOM = row["C_NOMINA"].ToString();
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _pedido.nomllevar = row["nomllevar"].ToString();
                    _pedido.telllevar = row["telefcli"].ToString();
                    _pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    _pedido.isCuenta = "SI";
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public ObservableCollection<Pedidos> SP_SELECT_DET_X_ID_CUENTA2(int id_pedido,int id_cuenta)
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.SP_SELECT_DET_X_ID_CUENTA2(id_pedido, id_cuenta);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString() + " " + row["DP_COMENTARIO"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.DP_DESCU = Convert.ToDecimal(row["DP_DESCU"]);
                    _pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _pedido.DETEMPL = row["DETEMPL"].ToString();
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.PED_NRO_PERSONAS = Convert.ToInt32(row["PED_NRO_PERSONAS"]);
                    _pedido.PED_NUM_DIARIO = Convert.ToInt32(row["PED_NUM_DIARIO"]);
                    _pedido.C_NOM = row["C_NOMINA"].ToString();
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _pedido.nomllevar = row["nomllevar"].ToString();
                    _pedido.telllevar = row["telefcli"].ToString();
                    _pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    _pedido.isCuenta = "NO";
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public ObservableCollection<Detalle_Pedido> getInformeVentas(int caja, string comprobante, int estado, int canal_venta, string serie, string numero, DateTime desde, DateTime hasta, DateTime HoraInicio, DateTime HoraFin)
        {
            ObservableCollection<Detalle_Pedido> detallepedido = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = dataPedido.getInformeVentas(caja, comprobante, estado, canal_venta, serie, numero, desde, hasta, HoraInicio, HoraFin);
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
                    _detPed.PED_NOM_EQUIPO = row["PED_NOM_EQUIPO"].ToString();
                    _detPed.igv = Convert.ToDecimal(row["sumatoriaIGV"]);
                    _detPed.icbper = Convert.ToDecimal(row["icbper"]);
                    _detPed.totalOPGravadas = Convert.ToDecimal(row["totalOPGravadas"]);
                    _detPed.rc = Convert.ToDecimal(row["sumatoriaOtrosCargos"]);
                    if (Convert.ToInt32(_detPed.id_estado_f) == 1)
                    {
                        _detPed.color_chips = "#26A001";
                    }
                    else if (Convert.ToInt32(_detPed.id_estado_f) == 2)
                    {
                        _detPed.color_chips = "#0371C2";
                    }
                    else if (Convert.ToInt32(_detPed.id_estado_f) == 3)
                    {
                        _detPed.color_chips = "#D73506";
                    }
                    detallepedido.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detallepedido = null;
            }
            return detallepedido;
        }
        public ObservableCollection<Detalle_Pedido> getVentaProductos(int caja, string comprobante, int canal_venta, int IdPlato, DateTime desde, DateTime hasta, DateTime HoraInicio, DateTime HoraFin)
        {
            ObservableCollection<Detalle_Pedido> detallepedido = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = dataPedido.getVentaProductos(caja, comprobante, canal_venta,IdPlato, desde, hasta, HoraInicio, HoraFin);
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_Pedido _detPed = new Detalle_Pedido();
                    _detPed.id_carta = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _detPed.PLATO = row["PRODUCTO"].ToString();
                    _detPed.GRUPO = row["GRUPO"].ToString();
                    _detPed.CANTIDAD = Convert.ToDecimal(row["CANTIDAD"]);
                    _detPed.DProd_pre_uni = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _detPed.DESCUENTO = Convert.ToDecimal(row["DESCUENTO"]);
                    _detPed.DELIVERY = Convert.ToDecimal(row["DELIVERY"]);
                    _detPed.LLEVAR = Convert.ToDecimal(row["LLEVAR"]);
                    _detPed.SALON = Convert.ToDecimal(row["SALON"]);
                    detallepedido.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detallepedido = null;
            }
            return detallepedido;
        }
        public DataTable getVentaProductosConsolidado(int caja, string comprobante, int canal_venta, int IdPlato, DateTime desde, DateTime hasta, DateTime HoraInicio, DateTime HoraFin)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.getVentaProductosConsolidado(caja, comprobante, canal_venta,IdPlato, desde, hasta, HoraInicio, HoraFin);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public ObservableCollection<Pedidos> GetPedidoxMesa(int id_mesa)
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GET_PEDIDO_X_MESA(id_mesa);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString() + " " + row["DP_COMENTARIO"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.DETEMPL = row["DETEMPL"].ToString();
                    _pedido.PED_NUM_DIARIO = (int)row["PED_NUM_DIARIO"];
                    _pedido.PED_NRO_PERSONAS = (int)row["PED_NRO_PERSONAS"];
                    _pedido.PED_ID_CLIENTE = (int)row["PED_ID_CLIENTE"];
                    _pedido.C_NOM = row["C_NOMINA"].ToString();
                    _pedido.PED_ID_EMPL = Convert.ToInt32(row["IDEMPL"].ToString());
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _pedido.DP_DESCU = Convert.ToDecimal(row["DP_DESCU"]);
                    _pedido.nomllevar = row["nomllevar"].ToString();
                    _pedido.telllevar = row["telefcli"].ToString();
                    _pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    _pedido.MOTORIZADO = row["MOTORIZADO"].ToString();
                    pedido.Add(_pedido);
                }

            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public DataTable getInfoClienteDelivery(int id_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_INFO_PEDIDO_DELIVERY(id_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable getPedidoxCliente(int idcli, int idpedido)
        {
            DataTable dt = new DataTable();
            try
            {
                 dt = dataPedido.getPedidoxCliente(idcli, idpedido);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public List<Pedidos> get_platos_vendidos_todos(int op,DateTime dia,DateTime desde,DateTime hasta)
        {
            List<Pedidos> pedido = new List<Pedidos>();
            try
            {
                DataTable dt = dataPedido.get_platos_vendidos_todos(op,dia,desde,hasta);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    //_pedido.ID = Convert.ToInt32(row["ID"]);
                    //_pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    //_pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString() + " " + row["DP_COMENTARIO"].ToString();
                    //
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_DESCRIP = row["CAR_NOM"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.GR_NOM = row["GR_NOM"].ToString();
                    _pedido.CAT_NOM = row["CAT_NOM"].ToString();
                    //_pedido.DP_EST = row["DP_EST"].ToString();
                    //_pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    //_pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    //_pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    //_pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    //_pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    //_pedido.DETEMPL = row["DETEMPL"].ToString();
                    //_pedido.PED_NUM_DIARIO = (int)row["PED_NUM_DIARIO"];
                    //_pedido.PED_NRO_PERSONAS = (int)row["PED_NRO_PERSONAS"];
                    //_pedido.PED_ID_CLIENTE = (int)row["PED_ID_CLIENTE"];
                    //_pedido.C_NOM = row["C_NOMINA"].ToString();
                    //_pedido.PED_ID_EMPL = Convert.ToInt32(row["IDEMPL"].ToString());
                    //_pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    //_pedido.nomllevar = row["nomllevar"].ToString();
                    //_pedido.telllevar = row["telefcli"].ToString();
                    pedido.Add(_pedido);
                }

            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public List<Pedidos> get_platos_vendidos(int Idradiobuton, int idgrup,int idcat,DateTime dia,DateTime desde,DateTime hasta)
        {
            List<Pedidos> pedido = new List<Pedidos>();
            try
            {
                DataTable dt = dataPedido.get_platos_vendidos(Idradiobuton,idgrup, idcat,dia,desde,hasta);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.DP_DESCRIP = row["CAR_NOM"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _pedido.GR_NOM = row["GR_NOM"].ToString();
                    _pedido.CAT_NOM = row["CAT_NOM"].ToString();
                    pedido.Add(_pedido);
                }

            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }

        public List<Pedidos> getplatoscriollosmarinos(int op, DateTime dia, DateTime desde, DateTime hasta)
        {
            List<Pedidos> pedido = new List<Pedidos>();
            try
            {
                DataTable dt = dataPedido.getplatoscriollosmarinos(op, dia, desde, hasta);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.DP_DESCRIP = row["CATEGORIA"].ToString();
                    _pedido.DP_CANT = Convert.ToDecimal(row["DP_CANT"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public int SetPedido(int idmesa, int id_usuario, int cod_empleado, int cod_cliente, DataTable dt,
            string opcion,string nro_personas,int isreserva, string nomcliente,string telefcliente, string nomequipo, string motorizado, ref string _mensaje)
        {
            int result = 0;
            result = dataPedido.SetPedido(idmesa, id_usuario, cod_empleado, cod_cliente, dt, opcion, nro_personas,isreserva, nomcliente, telefcliente, nomequipo, motorizado, ref _mensaje);
            if (result != 0)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public int SetPedidoSinInsumo(int idmesa, int id_usuario, int cod_empleado, int cod_cliente, DataTable dt, string opcion, string nro_personas, int isreserva, ref string _mensaje,int iddet, string nomcliente, string telefcliente, string nomequipo, string motorizado)
        {
            int result = 0;
            result = dataPedido.SetPedidoSinInsumo(idmesa, id_usuario, cod_empleado, cod_cliente, dt, opcion, nro_personas, isreserva, ref _mensaje,iddet, nomcliente, telefcliente, nomequipo, motorizado);
            if (result != 0)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public DataTable GetImpresoraxPedido(int cod_pedido,int tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                 dt = dataPedido.GetImpresoraxIdPedido(cod_pedido, tipo);
            }
            catch (Exception ex)
            {
                dt = null;
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GetReImpresoraxIdPedido(int cod_id_pedido, int tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GetReImpresoraxIdPedido(cod_id_pedido, tipo);
            }
            catch (Exception ex)
            {
                dt = null;
                // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GetDeliveryxPedidoApp(int cod_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GetDeliveryxPedidoApp(cod_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GetImpresoraxPedidoAnulado(int cod_pedido, int tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GetImpresoraxIdPedidoAnulado(cod_pedido, tipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GET_DETALLE_X_PEDIDO(int cod_pedido,int id_impresora)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_DETALLE_X_PEDIDO(cod_pedido, id_impresora);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GET_REIMP_DETALLE_X_PEDIDO(int cod_pedido, int id_impresora)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_REIMP_DETALLE_X_PEDIDO(cod_pedido, id_impresora);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GetImpresoraxPlato(int cod_carta)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GetImpresoraxPlato(cod_carta);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable DescontarStockSubNivel(int cod_carta)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.DescontarStockSubNivel(cod_carta);
            }
            catch (Exception ex)
            {
                dt = null;
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GET_DETALLE_X_PEDIDO_ANULADA(int cod_pedido, int id_impresora)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_DETALLE_X_PEDIDO_ANULADA(cod_pedido, id_impresora);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable GET_MESA_OCUPADA(int cod_mesa)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_MESA_OCUPADA(cod_mesa);
            }
            catch (Exception ex)
            {
                dt = null;
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public void ActualizarDetPedidoImp(int id_pedido)
        {
            dataPedido.ActualizarDetPedidoImp(id_pedido);
        }
        public void ActualizarDetPedidoReImp(int id_det_pedido)
        {
            dataPedido.ActualizarDetPedidoReImp(id_det_pedido);
        }
        public void ActualizarDetPedidoImpAnul(int id_det_pedido)
        {
            dataPedido.ActualizarDetPedidoImpAnul(id_det_pedido);
        }
        public void ActualizarCuentaPedidoImp(int id_pedido)
        {
            dataPedido.ActualizarCuentaPedidoImp(id_pedido);
        }
        public bool EliminarPlatoPedido(int IdDetPed, int IdPed,string comentario ,ref string _mensaje,int idsuarioanulacion)
        {
            bool result = false;
            result = dataPedido.EliminarPlatoPedido(IdDetPed, IdPed, comentario, ref _mensaje, idsuarioanulacion);
            if (result != false)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool DescuentoPedido(int IdPed,decimal nuevo_monto, int id_tip_desc,ref string _mensaje)
        {
            bool result = false;
            result = dataPedido.DescuentoPedido(IdPed, nuevo_monto, id_tip_desc, ref _mensaje);
            if (result != false)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool DescuentoPlato(int IdDetPed, decimal nuevo_monto, int id_tip_desc, ref string _mensaje)
        {
            bool result = false;
            result = dataPedido.DescuentoPlato(IdDetPed, nuevo_monto, id_tip_desc, ref _mensaje);
            if (result != false)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool EliminarPlatoPedidoCantidad(int IdDetPed, int IdPed, string comentario,decimal cantidad, ref string _mensaje,int idusuarioanulacion)
        {
            bool result = false;
            result = dataPedido.EliminarPlatoPedidoxCantidad(IdDetPed, IdPed, comentario, cantidad, ref _mensaje, idusuarioanulacion);
            if (result != false)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
       public bool CambiarPlatoMesa(int IdPed, Decimal CantAnu, int IdUsu, int IdMesa)
        {
            bool result = false;
            result = dataPedido.CambiarPlatoMesa(IdPed,CantAnu,IdUsu,IdMesa);
            return result;
        }
        public bool QuitarPlatoMesa(int IdPed, Decimal CantAnu, int IdUsu, int IdMesa)
        {
            bool result = false;
            result = dataPedido.QuitarPlatoMesa(IdPed, CantAnu, IdUsu, IdMesa);
            return result;
        }
        public ObservableCollection<Pedidos> GET_PEDIDO()
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GET_PEDIDO();
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.ID = Convert.ToInt32(row["ID"]);
                    _pedido.PED_ID_MESA = Convert.ToInt32(row["PED_ID_MESA"]);
                    _pedido.M_NOM = row["M_NOM"].ToString();
                    _pedido.PED_ID_EMPL = Convert.ToInt32(row["PED_ID_EMPL"]);
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.PED_FECH_PED = Convert.ToDateTime(row["PED_FECH_PED"]);
                    _pedido.PED_ID_ESTADO = Convert.ToInt32(row["PED_ID_ESTADO"]);
                    _pedido.PED_IMPORTE = Convert.ToDecimal(row["PED_IMPORTE"]);
                    _pedido.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _pedido.PED_ID_TIP_DESC = Convert.ToInt32(row["PED_ID_TIP_DESC"]);
                    _pedido.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _pedido.PED_ID_CLIENTE = Convert.ToInt32(row["PED_ID_CLIENTE"]);
                    _pedido.C_NOM = row["C_NOM"].ToString();
                    _pedido.PED_ID_CIERRE = Convert.ToInt32(row["PED_ID_CIERRE"]);
                    _pedido.PED_ID_USU = Convert.ToInt32(row["PED_ID_USU"]);
                    _pedido.USU_NOM = row["USU_NOM"].ToString();
                    _pedido.PED_ID_TURNO = Convert.ToInt32(row["PED_ID_TURNO"]);
                    _pedido.PED_FECH_MODIFI = Convert.ToDateTime(row["PED_FECH_MODIFI"]);
                    _pedido.PED_ID_CAMBIO_MONE = Convert.ToInt32(row["PED_ID_CAMBIO_MONE"]);
                    _pedido.TC_CAMBIO = Convert.ToDecimal(row["TC_CAMBIO"]);
                    _pedido.PED_NUM_DIARIO = Convert.ToInt32(row["PED_NUM_DIARIO"]);
                    _pedido.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _pedido.PED_NRO_PERSONAS = Convert.ToInt32(row["PED_NRO_PERSONAS"]);
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                
                pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return pedido;
        }
        public ObservableCollection<Pedidos> GET_DETALLEPEDIDO()
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GET_DETALLEPEDIDO();
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.DP_ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.CAR_NOM = row["CAR_NOM"].ToString();
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString();
                    _pedido.DP_CANT = Convert.ToInt32(row["DP_CANT"]);
                    _pedido.DP_DESCU = Convert.ToInt32(row["DP_DESCU"]);
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.DP_ID_EMPL = Convert.ToInt32(row["DP_ID_EMPL"]);
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.DP_FEC_MODIFI = Convert.ToDateTime(row["DP_FEC_MODIFI"]);
                    _pedido.DP_USU_MODIFI = Convert.ToInt32(row["DP_USU_MODIFI"]);
                    _pedido.USU_NOM = row["USU_NOM"].ToString();
                    _pedido.DP_IMP = Convert.ToInt32(row["DP_IMP"]);
                    _pedido.DP_COMENTARIO = row["DP_COMENTARIO"].ToString();
                    _pedido.IDCAT = Convert.ToInt32(row["IDCAT"]);
                    _pedido.IDGRUP = Convert.ToInt32(row["IDGRUP"]);
                    _pedido.GR_NOM = row["GR_NOM"].ToString();
                    _pedido.IDSCAT = Convert.ToInt32(row["IDSCAT"]);
                    _pedido.CAT_NOM = row["CAT_NOM"].ToString();
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                pedido = null;
            }
            return pedido;
        }
        public ObservableCollection<Pedidos> GET_DETALLEPEDIDO2()
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GET_DETALLEPEDIDO2();
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.DP_ID = Convert.ToInt32(row["ID"]);
                    _pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _pedido.CAR_NOM = row["CAR_NOM"].ToString();
                    _pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString();
                    _pedido.DP_CANT = Convert.ToInt32(row["DP_CANT"]);
                    _pedido.DP_DESCU = Convert.ToInt32(row["DP_DESCU"]);
                    _pedido.DP_EST = row["DP_EST"].ToString();
                    _pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _pedido.DP_ID_EMPL = Convert.ToInt32(row["DP_ID_EMPL"]);
                    _pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _pedido.DP_FEC_MODIFI = Convert.ToDateTime(row["DP_FEC_MODIFI"]);
                    _pedido.DP_USU_MODIFI = Convert.ToInt32(row["DP_USU_MODIFI"]);
                    _pedido.USU_NOM = row["USU_NOM"].ToString();
                    _pedido.DP_IMP = Convert.ToInt32(row["DP_IMP"]);
                    _pedido.DP_COMENTARIO = row["DP_COMENTARIO"].ToString();
                    _pedido.IDCAT = Convert.ToInt32(row["IDCAT"]);
                    _pedido.IDGRUP = Convert.ToInt32(row["IDGRUP"]);
                    _pedido.GR_NOM = row["GR_NOM"].ToString();
                    _pedido.IDSCAT = Convert.ToInt32(row["IDSCAT"]);
                    _pedido.CAT_NOM = row["CAT_NOM"].ToString();
                    _pedido.DP_PRE_UNI = Convert.ToDecimal(row["DP_PRE_UNI"]);
                    _pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                pedido = null;
            }
            return pedido;
        }
        public ObservableCollection<Pedidos> GetRecurrenciaClientes()
        {
            ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.GetRecurrenciaClientes();
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _pedido = new Pedidos();
                    _pedido.PRIMER_CONSUMO = Convert.ToDateTime(row["PRIMER_CONSUMO"]);
                    _pedido.ULTIMO_CONSUMO = Convert.ToDateTime(row["ULTIMO_CONSUMO"]);
                    _pedido.CANTIDAD_CONSUMO = Convert.ToInt32(row["CANTIDAD_CONSUMO"]);
                    _pedido.IMPORTE_CONSUMO = Convert.ToDecimal(row["IMPORTE_CONSUMO"]);
                    _pedido.ID_CLIENTE = Convert.ToInt32(row["ID_CLIENTE"]);
                    _pedido.NOMBRE_CLIENTE= row["NOMBRE_CLIENTE"].ToString();
                    pedido.Add(_pedido);
                }
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                pedido = null;
            }
            return pedido;
        }
        public DataTable GetImpresoraxIdDetPedido(int cod_det_pedido,int tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GetImpresoraxIdDetPedido(cod_det_pedido, tipo);
            }
            catch (Exception ex)
            {
                dt = null;
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }

        public DataTable GET_DETALLE_X_PLATO_ANULADA(int cod_det_pedido, int id_impresora)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_DETALLE_X_PLATO_ANULADA(cod_det_pedido, id_impresora);
            }
            catch (Exception ex)
            {
                dt = null;
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }

        public DataTable GET_MESA_EXISTE(string nom_mesa)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.GET_MESA_EXISTE(nom_mesa);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public bool CambioMesa(int op, int CodMesaAntes, int CodMesaNuevo, int idPedido, ref string _mensaje)
        {
            bool result = false;
            result = dataPedido.CambioMesa(op,CodMesaAntes, CodMesaNuevo, idPedido, ref _mensaje);
            if (result != false)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }


        public bool SetPagoCliente(int operacion, int id_pedido, int tipo_pago, decimal monto, decimal vuelto)
        {
            bool result = false;
            result = dataPedido.SetPagoCliente(operacion, id_pedido, tipo_pago, monto, vuelto);
            return result;
        }


        public DataTable getBoletaxPedido(int id_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.getBoletaxPedido(id_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable sp_verificar_plato_cuenta(int id_det_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.sp_verificar_plato_cuenta(id_det_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public DataTable sp_verificar_pedido_cuenta(int id_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.sp_verificar_pedido_cuenta(id_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return dt;
        }
        public ObservableCollection<Pedidos> GET_PEDIDOS_PENDIENTES(string NombreImpresora)
        {
            ObservableCollection<Pedidos> det_pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.get_pedidos_pendientes(NombreImpresora);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _det_pedido = new Pedidos();
                    _det_pedido.ID = Convert.ToInt32(row["ID_DET_PED"]);
                    _det_pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _det_pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _det_pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString();
                    _det_pedido.DP_CANT = Convert.ToInt32(row["DP_CANT"]);
                    _det_pedido.DP_PRE_UNI = Convert.ToInt32(row["DP_PRE_UNI"]);
                    _det_pedido.DP_DESCU = Convert.ToInt32(row["DP_DESCU"]);
                    _det_pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _det_pedido.DP_EST = row["DP_EST"].ToString();
                    _det_pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _det_pedido.DP_ID_EMPL = Convert.ToInt32(row["DP_ID_EMPL"]);
                    _det_pedido.DP_FEC_MODIFI = Convert.ToDateTime(row["DP_FEC_MODIFI"]);
                    _det_pedido.DP_IMP = Convert.ToInt32(row["DP_IMP"]);
                    _det_pedido.DP_COMENTARIO = row["DP_COMENTARIO"].ToString();
                    _det_pedido.idPadreDetalle = Convert.ToInt32(row["idPadreDetalle"]);
                    _det_pedido.ID_CARTA_SN = row["ID_CARTA_SN"].ToString();
                    _det_pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    _det_pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _det_pedido.M_NOM = row["M_NOM"].ToString();
                    _det_pedido.PED_ID_MESA = Convert.ToInt32(row["ID_MESA"]);
                    _det_pedido.M_TEXTO = row["M_TEXTO"].ToString();
                    _det_pedido.PED_ID_CLIENTE = Convert.ToInt32(row["PED_ID_CLIENTE"]);
                    _det_pedido.NOMBRE_CLIENTE = row["NOMBRE_CLIENTE"].ToString();
                    _det_pedido.ColorPedido = "White";
                    det_pedido.Add(_det_pedido);
                }
            }
            catch (Exception ex)
            {
                det_pedido = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return det_pedido;
        }
        public ObservableCollection<Pedidos> getPedidosEntregados(string NombreImpresora)
        {
            ObservableCollection<Pedidos> det_pedido = new ObservableCollection<Pedidos>();
            try
            {
                DataTable dt = dataPedido.getPedidosEntregados(NombreImpresora);
                foreach (DataRow row in dt.Rows)
                {
                    Pedidos _det_pedido = new Pedidos();
                    _det_pedido.ID = Convert.ToInt32(row["ID_DET_PED"]);
                    _det_pedido.DP_ID_PED = Convert.ToInt32(row["DP_ID_PED"]);
                    _det_pedido.DP_ID_CARTA = Convert.ToInt32(row["DP_ID_CARTA"]);
                    _det_pedido.DP_DESCRIP = row["DP_DESCRIP"].ToString();
                    _det_pedido.DP_CANT = Convert.ToInt32(row["DP_CANT"]);
                    _det_pedido.DP_PRE_UNI = Convert.ToInt32(row["DP_PRE_UNI"]);
                    _det_pedido.DP_DESCU = Convert.ToInt32(row["DP_DESCU"]);
                    _det_pedido.DP_IMPORT = row["DP_IMPORT"].ToString();
                    _det_pedido.DP_EST = row["DP_EST"].ToString();
                    _det_pedido.DP_FEC_REG = Convert.ToDateTime(row["DP_FEC_REG"]);
                    _det_pedido.DP_ID_EMPL = Convert.ToInt32(row["DP_ID_EMPL"]);
                    _det_pedido.DP_FEC_MODIFI = Convert.ToDateTime(row["DP_FEC_MODIFI"]);
                    _det_pedido.DP_IMP = Convert.ToInt32(row["DP_IMP"]);
                    _det_pedido.DP_COMENTARIO = row["DP_COMENTARIO"].ToString();
                    _det_pedido.idPadreDetalle = Convert.ToInt32(row["idPadreDetalle"]);
                    _det_pedido.ID_CARTA_SN = row["ID_CARTA_SN"].ToString();
                    _det_pedido.EST_ENTREGADO = Convert.ToBoolean(row["EST_ENTREGADO"]);
                    _det_pedido.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _det_pedido.M_NOM = row["M_NOM"].ToString();
                    _det_pedido.PED_ID_CLIENTE = Convert.ToInt32(row["PED_ID_CLIENTE"]);
                    _det_pedido.NOMBRE_CLIENTE = row["NOMBRE_CLIENTE"].ToString();
                    det_pedido.Add(_det_pedido);
                }
            }
            catch (Exception ex)
            {
                det_pedido = null;
            }
            return det_pedido;
        }
        public ObservableCollection<Impresora> getPantallas()
        {
            ObservableCollection<Impresora> imp = new ObservableCollection<Impresora>();
            try
            {
                DataTable dt = dataPedido.getPantallas();
                foreach (DataRow row in dt.Rows)
                {
                    Impresora _imp = new Impresora();
                    _imp.idimp = Convert.ToInt32(row["IDIMP"]);
                    _imp.nomimp = row["IDIMP"].ToString();
                    _imp.estadoimp = row["ESTADOIMP"].ToString();
                    _imp.nomequipoimp = row["NOMEQUIPOIMP"].ToString();
                    _imp.nomimpppedido = row["NOMIMPPPEDIDO"].ToString();
                    imp.Add(_imp);
                }
            }
            catch (Exception ex)
            {
                imp = null;
            }
            return imp;
        }
        public DataTable getEmpleadoPedido(int id_mesa)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataPedido.getEmpleadoPedido(id_mesa);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;

        }
        public bool SET_ENTREGAR_PEDIDO(int id_detalle_pedido)
        {
            bool result = false;
            result = dataPedido.SET_ENTREGAR_PEDIDO(id_detalle_pedido);
            return result;
        }
        public bool setNomCliente(int id_pedido, string NombreCliente)
        {
            bool result = false;
            result = dataPedido.setNomCliente(id_pedido,NombreCliente);
            return result;
        }
        public bool setMotorizado(int id_pedido, string Motorizado)
        {
            bool result = false;
            result = dataPedido.setMotorizado(id_pedido, Motorizado);
            return result;
        }
        public bool QuitarDescuentoPlato(int id_det_pedido)
        {
            bool result = false;
            result = dataPedido.QuitarDescuentoPlato(id_det_pedido);
            return result;
        }
        public bool ResetearPlatoPedido(int id_det_pedido)
        {
            bool result = false;
            result = dataPedido.ResetearPlatoPedido(id_det_pedido);
            return result;
        }

        public ObservableCollection<Prioridades> GetPrioridades()
        {
            ObservableCollection<Prioridades> dat_prioridad = new ObservableCollection<Prioridades>();
            try
            {
                DataTable dt = dataPedido.GetPrioridades();
                if (dt != null) {
                    foreach (DataRow row in dt.Rows)
                    {
                        Prioridades _det_pedido = new Prioridades();
                        _det_pedido.ID = Convert.ToInt32(row["ID"]);
                        _det_pedido.DESCR = row["DESCR"].ToString();
                        _det_pedido.F_CREATE = Convert.ToDateTime(row["F_CREATE"]);
                        dat_prioridad.Add(_det_pedido);
                    }
                }
                
            }
            catch (Exception ex)
            {
                dat_prioridad = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dat_prioridad;
        }

        public bool SetPrioridades(string prioridad)
        {
            bool result = false;
            result = dataPedido.SET_PRIORIDAD(prioridad);
            return result;
        }
        public bool EliminarPrioridad(int id_prioridad)
        {
            bool result = false;
            result = dataPedido.EliminarPrioridad(id_prioridad);
            return result;
        }
        public bool setPedidoPendiente(int id)
        {
            bool result = false;
            result = dataPedido.setPedidoPendiente(id);
            return result;
        }
    }
}

