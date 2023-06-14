using Capa_Datos;
using Capa_Datos.Delivery_Web_Service;
using Capa_Entidades;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Delivery_Web_Service;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Negocio.Ambientes;
using Capa_Negocio.Facturacion_Electronica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Delivery_Web_Service
{
    public class Neg_DeliveryWebService
    {
        MEnsaje mensaje = new MEnsaje();
        Dat_DeliveryWebService dataDeliveryWebService = new Dat_DeliveryWebService();
        public List<Tipo_Doc_Electronico> TipDocElectr = new List<Tipo_Doc_Electronico>();
        public List<Ent_Combo> ComboTipoDoc { get; set; }
        Neg_Combo negCombo = new Neg_Combo();
        public Neg_Tip_Doc_Electronico negTipDocElec = new Neg_Tip_Doc_Electronico();
        public ObservableCollection<DeliveryWebService> GetDeliveryWebService()

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_delivery();
                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();

                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.img_entrega = "TruckDelivery";
                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    _datadelivery.paga_con = row["paga_con"].ToString();
                    _datadelivery.vuelto = row["vuelto"].ToString();
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        public ObservableCollection<DeliveryWebService> GetRecojoWebService()

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_recojo();
                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();

                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.img_entrega = "HouseMapMarker";


                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        static List<MesasItem> mesasItem = new List<MesasItem>();
        Neg_Mesa negM = new Neg_Mesa();
        public ObservableCollection<DeliveryWebService> GetMesaWebService()

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                mesasItem = negM.GetMesasActiva();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_mesa();

                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();

                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.tipo_sosfood = row["tipo_sosfood"].ToString();
                    _datadelivery.img_entrega = "ChairSchool";
                    //_datadelivery.NomMesa = mesasItem.Where(w => w.ID == Convert.ToInt32(row["nombre_mesa"]) && w.M_TIPO == 1).First().M_NOM;
                    _datadelivery.NomMesa = mesasItem.Where(w => w.ID == Convert.ToInt32(row["nombre_mesa"])).First().M_NOM; //Para que acepte en las sub mesas

                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        public ObservableCollection<DeliveryWebService> GetMesaWebServiceTotal()

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_mesa_total();
                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();

                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.tipo_sosfood = row["tipo_sosfood"].ToString();
                    _datadelivery.img_entrega = "ChairSchool";


                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        public ObservableCollection<DeliveryWebService> GetDeliveryWebServicexFecha(DateTime f1,DateTime f2)

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_delivery_x_fecha(f1,f2);
                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();
                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.img_entrega = "TruckDelivery";
                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        public ObservableCollection<DeliveryWebService> GetRecojoWebServicexFecha(DateTime f1, DateTime f2)

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_recojo_x_fecha(f1, f2);
                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();
                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.img_entrega = "HouseMapMarker";
                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        public ObservableCollection<DeliveryWebService> GetMesaWebServicexFecha(DateTime f1, DateTime f2)

        {
            ObservableCollection<DeliveryWebService> datadelivery = new ObservableCollection<DeliveryWebService>();
            try
            {
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
                mesasItem = negM.GetMesasActiva();
                DataTable dt = dataDeliveryWebService.sp_get_pedido_mesa_x_fecha(f1, f2);
                foreach (DataRow row in dt.Rows)
                {
                    DeliveryWebService _datadelivery = new DeliveryWebService();
                    _datadelivery.cod_orden = Convert.ToInt32(row["id"]);
                    _datadelivery.fname = row["nombre"].ToString();
                    _datadelivery.mobile = row["telefono"].ToString();
                    _datadelivery.address = row["direccion"].ToString();
                    _datadelivery.status = row["estado"].ToString();
                    _datadelivery.date = Convert.ToDateTime(row["fecha_pedido"]);
                    _datadelivery.total = Convert.ToDecimal(row["total"]);
                    _datadelivery.id_business = Convert.ToInt32(row["idempresa"]);
                    _datadelivery.latitud = row["latitud"].ToString();
                    _datadelivery.longitud = row["longitud"].ToString();
                    _datadelivery.tipo_doc_electronico = row["tipo_doc_electronico"].ToString();
                    _datadelivery.tipo_doc_identidad = row["tipo_doc_identidad"].ToString();
                    _datadelivery.nro_identidad_cliente = row["nro_identidad_cliente"].ToString();
                    _datadelivery.deno_cliente = row["deno_cliente"].ToString();
                    _datadelivery.deno_entrega = row["tipo_entrega"].ToString();
                    _datadelivery.nombre_mesa = row["nombre_mesa"].ToString();
                    _datadelivery.metodo_pago = row["metodo_pago"].ToString();
                    _datadelivery.token = row["token"].ToString();
                    _datadelivery.img_entrega = "ChairSchool";
                    _datadelivery.tipo_sosfood = row["tipo_sosfood"].ToString();
                    _datadelivery.NomMesa = mesasItem.Where(w => w.ID == Convert.ToInt32(row["nombre_mesa"]) && w.M_TIPO == 1).First().M_NOM;
                    _datadelivery.NomTipDoc = this.TipDocElectr.Where(t => Convert.ToInt32(t.VALOR_TIPO_DOC) == Convert.ToInt32(row["tipo_doc_electronico"].ToString())).ToList().FirstOrDefault().NOM_TIPO_DOC;
                    _datadelivery.NomTipIde = this.ComboTipoDoc.Where(t => Convert.ToInt32(t.valor1.ToString()) == Convert.ToInt32(row["tipo_doc_identidad"].ToString())).ToList().FirstOrDefault().nombre;

                    if (row["estado"].ToString() == "EN PROCESO")
                    {
                        _datadelivery.Habilitado = "true";
                    }
                    else
                    {
                        _datadelivery.Habilitado = "false";
                    }
                    datadelivery.Add(_datadelivery);
                }
            }
            catch (Exception ex)
            {
                datadelivery = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return datadelivery;
        }
        public bool sp_insert_pedido(DeliveryWebService datadelivery)
        {
            bool result = false;
            result = dataDeliveryWebService.sp_insert_pedido(datadelivery);
            return result;
        }
        public bool sp_aceptar_pedido(int id)
        {
            bool result = false;
            result = dataDeliveryWebService.sp_aceptar_pedido(id);
            return result;
        }
        public bool sp_rechazar_pedido(int id)
        {
            bool result = false;
            result = dataDeliveryWebService.sp_rechazar_pedido(id);
            return result;
        }
        public bool sp_delivery_pedido(int id, int idpedido)
        {
            bool result = false;
            result = dataDeliveryWebService.sp_delivery_pedido(id,idpedido);
            return result;
        }
        public bool sp_cambiar_estado_pedido_delivery(int id,string estado)
        {
            bool result = false;
            result = dataDeliveryWebService.sp_cambiar_estado_pedido_delivery(id,estado);
            return result;
        }
        public bool sp_cambiar_mesa_pedido_delivery(int id, string cod_mesa)
        {
            bool result = false;
            result = dataDeliveryWebService.sp_cambiar_mesa_pedido_delivery(id, cod_mesa);
            return result;
        }
    }
}
