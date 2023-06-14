using Capa_Datos.Data.Reportes.Rpt_BoletayFactura;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Entidades.Models.Reportes.Rpt_BoletayFactura;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes.Report_BoletayFactura
{
    public class Neg_BoletayFactura
    {
        Dat_BoletayFactura datDoc = new Dat_BoletayFactura();
        Funcion_Global globales = new Funcion_Global();

        public ObservableCollection<Ent_RptBoletayFactura> GetDocElectronico()
        {
            ObservableCollection<Ent_RptBoletayFactura> facCompra = new ObservableCollection<Ent_RptBoletayFactura>();
            try
            {
                DataTable dt = datDoc.GetDocElectronico();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_RptBoletayFactura _td = new Ent_RptBoletayFactura();
                    _td.id_doc_electronico = Convert.ToInt32(row["id_doc_electronico"]);
                    _td.serieNumero = row["serieNumero"].ToString();
                    _td.tipoDocumento = row["tipoDocumento"].ToString();
                    _td.fechaEmision = Convert.ToDateTime(row["fechaEmision"]);
                    _td.numeroDocIdentidadEmisor = row["numeroDocIdentidadEmisor"].ToString();
                    _td.tipoDocIdentidadEmisor = row["tipoDocIdentidadEmisor"].ToString(); ;
                    _td.numeroDocIdentidadReceptor = row["numeroDocIdentidadReceptor"].ToString();
                    _td.razonSocialReceptor = row["razonSocialReceptor"].ToString();
                    _td.direccionReceptor = row["direccionReceptor"].ToString();
                    _td.correoReceptor = row["correoReceptor"].ToString();
                    _td.tipoDocIdentidadReceptor = row["tipoDocIdentidadReceptor"].ToString();
                    _td.telefono = row["telefono"].ToString();
                    _td.idCliente = Convert.ToInt32(row["idCliente"]);
                    _td.NOMINA = row["NOMINA"].ToString();
                    _td.totalOPGravadas = Convert.ToDecimal(row["totalOPGravadas"]);
                    _td.totalOPExoneradas = Convert.ToDecimal(row["totalOPExoneradas"]);
                    _td.totalOPNoGravadas = Convert.ToDecimal(row["totalOPNoGravadas"]);
                    _td.totalOPGratuitas = Convert.ToDecimal(row["totalOPGratuitas"]);
                    _td.sumatoriaIGV = Convert.ToDecimal(row["sumatoriaIGV"]);
                    _td.sumatoriaISC = Convert.ToDecimal(row["sumatoriaISC"]);
                    _td.importeTotal = Convert.ToDecimal(row["importeTotal"]);
                    _td.importeTotalVenta = Convert.ToDecimal(row["importeTotalVenta"]);
                    _td.totalDescuentos = Convert.ToDecimal(row["totalDescuentos"]);
                    _td.descuentosGlobales = Convert.ToDecimal(row["descuentosGlobales"]);
                    _td.sumatoriaOtrosCargos = Convert.ToDecimal(row["sumatoriaOtrosCargos"]);
                    _td.porcentajeOtrosCargos = Convert.ToDecimal(row["porcentajeOtrosCargos"]);
                    _td.sumatoriaImpuestoBolsas = Convert.ToDecimal(row["sumatoriaImpuestoBolsas"]);
                    _td.tipoMoneda = row["tipoMoneda"].ToString();
                    _td.codigoPaisReceptor = row["codigoPaisReceptor"].ToString();
                    _td.codigoTipoOperacion = row["codigoTipoOperacion"].ToString();
                    _td.montoEnLetras = row["montoEnLetras"].ToString();
                    _td.idPedido = Convert.ToInt32(row["idPedido"]);
                    _td.Doc_Estado = Convert.ToBoolean(row["Doc_Estado"]);
                    _td.Doc_Estado_nom = row["Doc_Estado_nom"].ToString(); ;
                    _td.Doc_id_cierre = Convert.ToInt32(row["Doc_id_cierre"]);
                    if (_td.Doc_Estado == false)
                    {
                        _td.ColorLetraEstado = "Red";
                    }
                    else if (_td.Doc_Estado == true)
                    {
                        _td.ColorLetraEstado = "Black";
                    }
                    _td.EnabledAnular = Convert.ToBoolean(row["Doc_Estado"]);
                    //_td.EnabledImprimir = Convert.ToBoolean(row["Doc_Estado"]);

                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return facCompra;
        }
        public ObservableCollection<Ent_RptBoletayFactura> GetDocElectronicoRpt(DateTime desde, DateTime hasta, string tipoDoc, int EstadoDoc)
        {
            ObservableCollection<Ent_RptBoletayFactura> facCompra = new ObservableCollection<Ent_RptBoletayFactura>();
            try
            {
                DataTable dt = datDoc.GetDocElectronicoRpt(desde, hasta, tipoDoc, EstadoDoc);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_RptBoletayFactura _td = new Ent_RptBoletayFactura();
                    _td.id_doc_electronico = Convert.ToInt32(row["id_doc_electronico"]);
                    _td.serieNumero = row["serieNumero"].ToString();
                    _td.tipoDocumento = row["tipoDocumento"].ToString();
                    _td.fechaEmision = Convert.ToDateTime(row["fechaEmision"]);
                    _td.numeroDocIdentidadEmisor = row["numeroDocIdentidadEmisor"].ToString();
                    _td.tipoDocIdentidadEmisor = row["tipoDocIdentidadEmisor"].ToString(); ;
                    _td.numeroDocIdentidadReceptor = row["numeroDocIdentidadReceptor"].ToString();
                    _td.razonSocialReceptor = row["razonSocialReceptor"].ToString();
                    _td.direccionReceptor = row["direccionReceptor"].ToString();
                    _td.correoReceptor = row["correoReceptor"].ToString();
                    _td.tipoDocIdentidadReceptor = row["tipoDocIdentidadReceptor"].ToString();
                    _td.telefono = row["telefono"].ToString();
                    _td.idCliente = Convert.ToInt32(row["idCliente"]);
                    _td.NOMINA = row["NOMINA"].ToString();
                    _td.totalOPGravadas = Convert.ToDecimal(row["totalOPGravadas"]);
                    _td.totalOPExoneradas = Convert.ToDecimal(row["totalOPExoneradas"]);
                    _td.totalOPNoGravadas = Convert.ToDecimal(row["totalOPNoGravadas"]);
                    _td.totalOPGratuitas = Convert.ToDecimal(row["totalOPGratuitas"]);
                    _td.sumatoriaIGV = Convert.ToDecimal(row["sumatoriaIGV"]);
                    _td.sumatoriaISC = Convert.ToDecimal(row["sumatoriaISC"]);
                    _td.importeTotal = Convert.ToDecimal(row["importeTotal"]);
                    _td.importeTotalVenta = Convert.ToDecimal(row["importeTotalVenta"]);
                    _td.totalDescuentos = Convert.ToDecimal(row["totalDescuentos"]);
                    _td.descuentosGlobales = Convert.ToDecimal(row["descuentosGlobales"]);
                    _td.sumatoriaOtrosCargos = Convert.ToDecimal(row["sumatoriaOtrosCargos"]);
                    _td.porcentajeOtrosCargos = Convert.ToDecimal(row["porcentajeOtrosCargos"]);
                    _td.sumatoriaImpuestoBolsas = Convert.ToDecimal(row["sumatoriaImpuestoBolsas"]);
                    _td.tipoMoneda = row["tipoMoneda"].ToString();
                    _td.codigoPaisReceptor = row["codigoPaisReceptor"].ToString();
                    _td.codigoTipoOperacion = row["codigoTipoOperacion"].ToString();
                    _td.montoEnLetras = row["montoEnLetras"].ToString();
                    _td.idPedido = Convert.ToInt32(row["idPedido"]);
                    _td.Doc_Estado = Convert.ToBoolean(row["Doc_Estado"]);
                    _td.Doc_Estado_nom = row["Doc_Estado_nom"].ToString(); ;
                    _td.Doc_id_cierre = Convert.ToInt32(row["Doc_id_cierre"]);
                    if (_td.Doc_Estado == false)
                    {
                        _td.ColorLetraEstado = "Red";
                    }
                    else if (_td.Doc_Estado == true)
                    {
                        _td.ColorLetraEstado = "Black";
                    }
                    _td.EnabledAnular = Convert.ToBoolean(row["Doc_Estado"]);
                    //_td.EnabledImprimir = Convert.ToBoolean(row["Doc_Estado"]);

                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return facCompra;
        }
        public ObservableCollection<Ent_RptBoletayFactura> GetDocElectronicosxFiltroSerie(string serieDocumento)
        {
            ObservableCollection<Ent_RptBoletayFactura> facCompra = new ObservableCollection<Ent_RptBoletayFactura>();
            try
            {
                DataTable dt = datDoc.GetDocElectronicosxFiltroSerie(serieDocumento);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_RptBoletayFactura _td = new Ent_RptBoletayFactura();
                    _td.id_doc_electronico = Convert.ToInt32(row["id_doc_electronico"]);
                    _td.serieNumero = row["serieNumero"].ToString();
                    _td.tipoDocumento = row["tipoDocumento"].ToString();
                    _td.fechaEmision = Convert.ToDateTime(row["fechaEmision"]);
                    _td.numeroDocIdentidadEmisor = row["numeroDocIdentidadEmisor"].ToString();
                    _td.tipoDocIdentidadEmisor = row["tipoDocIdentidadEmisor"].ToString(); ;
                    _td.numeroDocIdentidadReceptor = row["numeroDocIdentidadReceptor"].ToString();
                    _td.razonSocialReceptor = row["razonSocialReceptor"].ToString();
                    _td.direccionReceptor = row["direccionReceptor"].ToString();
                    _td.correoReceptor = row["correoReceptor"].ToString();
                    _td.tipoDocIdentidadReceptor = row["tipoDocIdentidadReceptor"].ToString();
                    _td.telefono = row["telefono"].ToString();
                    _td.idCliente = Convert.ToInt32(row["idCliente"]);
                    _td.NOMINA = row["NOMINA"].ToString();
                    _td.totalOPGravadas = Convert.ToDecimal(row["totalOPGravadas"]);
                    _td.totalOPExoneradas = Convert.ToDecimal(row["totalOPExoneradas"]);
                    _td.totalOPNoGravadas = Convert.ToDecimal(row["totalOPNoGravadas"]);
                    _td.totalOPGratuitas = Convert.ToDecimal(row["totalOPGratuitas"]);
                    _td.sumatoriaIGV = Convert.ToDecimal(row["sumatoriaIGV"]);
                    _td.sumatoriaISC = Convert.ToDecimal(row["sumatoriaISC"]);
                    _td.importeTotal = Convert.ToDecimal(row["importeTotal"]);
                    _td.importeTotalVenta = Convert.ToDecimal(row["importeTotalVenta"]);
                    _td.totalDescuentos = Convert.ToDecimal(row["totalDescuentos"]);
                    _td.descuentosGlobales = Convert.ToDecimal(row["descuentosGlobales"]);
                    _td.sumatoriaOtrosCargos = Convert.ToDecimal(row["sumatoriaOtrosCargos"]);
                    _td.porcentajeOtrosCargos = Convert.ToDecimal(row["porcentajeOtrosCargos"]);
                    _td.sumatoriaImpuestoBolsas = Convert.ToDecimal(row["sumatoriaImpuestoBolsas"]);
                    _td.tipoMoneda = row["tipoMoneda"].ToString();
                    _td.codigoPaisReceptor = row["codigoPaisReceptor"].ToString();
                    _td.codigoTipoOperacion = row["codigoTipoOperacion"].ToString();
                    _td.montoEnLetras = row["montoEnLetras"].ToString();
                    _td.idPedido = Convert.ToInt32(row["idPedido"]);
                    _td.Doc_Estado = Convert.ToBoolean(row["Doc_Estado"]);
                    _td.Doc_Estado_nom = row["Doc_Estado_nom"].ToString(); ;
                    _td.Doc_id_cierre = Convert.ToInt32(row["Doc_id_cierre"]);
                    if (_td.Doc_Estado == false)
                    {
                        _td.ColorLetraEstado = "Red";
                    }
                    else if (_td.Doc_Estado == true)
                    {
                        _td.ColorLetraEstado = "Black";
                    }
                    _td.EnabledAnular = Convert.ToBoolean(row["Doc_Estado"]);
                    //_td.EnabledImprimir = Convert.ToBoolean(row["Doc_Estado"]);

                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return facCompra;
        }

        public ObservableCollection<Ent_RptBoletayFactura> GetDocElectronicoHoy()
        {
            ObservableCollection<Ent_RptBoletayFactura> facCompra = new ObservableCollection<Ent_RptBoletayFactura>();
            try
            {
                DataTable dt = datDoc.GetDocElectronicoHoy();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_RptBoletayFactura _td = new Ent_RptBoletayFactura();
                    _td.id_doc_electronico = Convert.ToInt32(row["id_doc_electronico"]);
                    _td.serieNumero = row["serieNumero"].ToString();
                    _td.tipoDocumento = row["tipoDocumento"].ToString();
                    _td.fechaEmision = Convert.ToDateTime(row["fechaEmision"]);
                    _td.numeroDocIdentidadEmisor = row["numeroDocIdentidadEmisor"].ToString();
                    _td.tipoDocIdentidadEmisor = row["tipoDocIdentidadEmisor"].ToString(); ;
                    _td.numeroDocIdentidadReceptor = row["numeroDocIdentidadReceptor"].ToString();
                    _td.razonSocialReceptor = row["razonSocialReceptor"].ToString();
                    _td.direccionReceptor = row["direccionReceptor"].ToString();
                    _td.correoReceptor = row["correoReceptor"].ToString();
                    _td.tipoDocIdentidadReceptor = row["tipoDocIdentidadReceptor"].ToString();
                    _td.telefono = row["telefono"].ToString();
                    _td.idCliente = Convert.ToInt32(row["idCliente"]);
                    _td.NOMINA = row["NOMINA"].ToString();
                    _td.totalOPGravadas = Convert.ToDecimal(row["totalOPGravadas"]);
                    _td.totalOPExoneradas = Convert.ToDecimal(row["totalOPExoneradas"]);
                    _td.totalOPNoGravadas = Convert.ToDecimal(row["totalOPNoGravadas"]);
                    _td.totalOPGratuitas = Convert.ToDecimal(row["totalOPGratuitas"]);
                    _td.sumatoriaIGV = Convert.ToDecimal(row["sumatoriaIGV"]);
                    _td.sumatoriaISC = Convert.ToDecimal(row["sumatoriaISC"]);
                    _td.importeTotal = Convert.ToDecimal(row["importeTotal"]);
                    _td.importeTotalVenta = Convert.ToDecimal(row["importeTotalVenta"]);
                    _td.totalDescuentos = Convert.ToDecimal(row["totalDescuentos"]);
                    _td.descuentosGlobales = Convert.ToDecimal(row["descuentosGlobales"]);
                    _td.sumatoriaOtrosCargos = Convert.ToDecimal(row["sumatoriaOtrosCargos"]);
                    _td.porcentajeOtrosCargos = Convert.ToDecimal(row["porcentajeOtrosCargos"]);
                    _td.sumatoriaImpuestoBolsas = Convert.ToDecimal(row["sumatoriaImpuestoBolsas"]);
                    _td.tipoMoneda = row["tipoMoneda"].ToString();
                    _td.codigoPaisReceptor = row["codigoPaisReceptor"].ToString();
                    _td.codigoTipoOperacion = row["codigoTipoOperacion"].ToString();
                    _td.montoEnLetras = row["montoEnLetras"].ToString();
                    _td.idPedido = Convert.ToInt32(row["idPedido"]);
                    _td.Doc_Estado = Convert.ToBoolean(row["Doc_Estado"]);
                    _td.Doc_Estado_nom = row["Doc_Estado_nom"].ToString(); ;
                    _td.Doc_id_cierre = Convert.ToInt32(row["Doc_id_cierre"]);
                    if (_td.Doc_Estado == false)
                    {
                        _td.ColorLetraEstado = "Red";
                    }
                    else if (_td.Doc_Estado == true)
                    {
                        _td.ColorLetraEstado = "Black";
                    }
                    _td.EnabledAnular = Convert.ToBoolean(row["Doc_Estado"]);
                    //_td.EnabledImprimir = Convert.ToBoolean(row["Doc_Estado"]);

                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return facCompra;
        }
        public ObservableCollection<Ent_RptBoletayFactura> GetConsolidadoDocElectronicos(DateTime desde, DateTime hasta)
        {
            ObservableCollection<Ent_RptBoletayFactura> cons = new ObservableCollection<Ent_RptBoletayFactura>();
            try
            {
                DataTable dt = datDoc.GetConsolidadoDocElectronicos(desde,hasta);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_RptBoletayFactura _td = new Ent_RptBoletayFactura();
                    _td.igv = Convert.ToDecimal(row["IGV"]);
                    _td.rc = Convert.ToDecimal(row["RC"]);
                    _td.icbper = Convert.ToDecimal(row["ICBPER"]);
                    _td.total = Convert.ToDecimal(row["TOTAL"]);
                    cons.Add(_td);
                }
            }
            catch (Exception ex)
            {
                cons = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return cons;
        }


        public ObservableCollection<Estado_Doc_Electronico> GetEstadoDocumento()
        {
            ObservableCollection<Estado_Doc_Electronico> estadoDoc = new ObservableCollection<Estado_Doc_Electronico>();
            try
            {
                DataTable dt = datDoc.GetEstadoDoc();
                foreach (DataRow row in dt.Rows)
                {
                    Estado_Doc_Electronico _td = new Estado_Doc_Electronico();
                    _td.DOC_ESTADO = Convert.ToInt32(row["Doc_Estado"]);
                    _td.NOM_ESTADO = row["NOM_ESTADO"].ToString();
                    estadoDoc.Add(_td);
                }
            }
            catch (Exception ex)
            {
                estadoDoc = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return estadoDoc;
        }

        public bool SetFacturaBoleta(int operacion, Ent_RptBoletayFactura bolfact)
        {
            bool result = false;
            result = datDoc.SetDocElectronico(operacion, bolfact);
            return result;
        }

    }
}
