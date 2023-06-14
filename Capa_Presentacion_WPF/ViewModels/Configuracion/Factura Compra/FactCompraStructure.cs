using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Factura_Compra;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Factura_Compra;
using Capa_Negocio.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Factura_Compra
{
    public class FactCompraStructure
    {
        #region Negocio
        Neg_Proveedor negProv = new Neg_Proveedor();
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_Tipodoc negTipoDoc = new Neg_Tipodoc();
        Neg_FactCompra negFactComp = new Neg_FactCompra();
        Neg_Almacen neg_alm = new Neg_Almacen();

        #endregion
        ObservableCollection<FactCompra> fc = new ObservableCollection<FactCompra>();
        #region Entidad
        static ObservableCollection<Proveedor> prov = new ObservableCollection<Proveedor>();
        static ObservableCollection<Insumos> ins = new ObservableCollection<Insumos>();
        static ObservableCollection<Insumos> insAlm = new ObservableCollection<Insumos>();
        static ObservableCollection<TipoDoc> tipoDoc = new ObservableCollection<TipoDoc>();
        static ObservableCollection<FactCompra> fCompra = new ObservableCollection<FactCompra>();
        static ObservableCollection<FactCompraDet> fCompraDet = new ObservableCollection<FactCompraDet>();
        static ObservableCollection<Almacen> alm = new ObservableCollection<Almacen>();
        static ObservableCollection<FactCompra> conpago = new ObservableCollection<FactCompra>();
        static ObservableCollection<FactCompra> fCompraEstado = new ObservableCollection<FactCompra>();
        static ObservableCollection<FactCompra> fCompraTipoDoc = new ObservableCollection<FactCompra>();
        #endregion
        public FactCompraStructure()
        {
            prov = negProv.GetProv();
            ins = negInsumo.GetInsumo();
            tipoDoc = negTipoDoc.GetTipoDoc();
            fCompra = negFactComp.GetFacturaCompra();
            fCompraEstado = negFactComp.GetFacturaCompraEstado();
            fCompraDet = negFactComp.GetFacturaCompraDet();
            alm = neg_alm.GetAlmacen();
            conpago = negFactComp.GetCondicionPago();
            insAlm = negInsumo.GetAlmacenInsumo();
            fc = new ObservableCollection<FactCompra>();
        }
        public List<Proveedor> GetProveedor()
        {
            prov  = negProv.GetProv();
            return prov.ToList();
        }
        public string GetDocProveedor(int id)
        {
            return prov.Where(w => w.idp == id).FirstOrDefault().prov_nrdoc;
        }
        public List<Insumos> GetInsumos()
        {
            return ins.ToList();
        }
        public List<TipoDoc> GetTipoDocumento()
        {
            return tipoDoc.ToList();
        }
        public List<FactCompraDet> GetFactCompraDet()
        {
            return fCompraDet.ToList();
        }
        public List<FactCompra> GetFactCompraEstado()
        {
            fCompraEstado = negFactComp.GetFacturaCompraEstado();
            return fCompraEstado.ToList();
        }
        public string GetSerie(int id)
        {
            return tipoDoc.Where(w => w.ID == id).First().SERIE;
        }
        public List<FactCompra> GetFactCompra()
        {
            fCompra = negFactComp.GetFacturaCompra();
            return fCompra.ToList();
        }
        public List<FactCompra> GetFactCompraTipoDoc()
        {
            fCompraTipoDoc = negFactComp.GetFacturaCompraTipDoc();
            return fCompraTipoDoc.ToList();
        }
        public List<FactCompra> GetFactCompraxId(int id)
        {
            fCompra = negFactComp.GetFacturaCompra();
            return fCompra.Where(w => w.ID == id).ToList();
        }
        public List<FactCompraDet> GetFactCompraDetxFC(int id)
        {
            fCompraDet = negFactComp.GetFacturaCompraDet();
            var Lista = fCompraDet.Where(w => w.FCD_ID_FACT_COMPRA == id).ToList();
            return Lista;
        }
        public List<FactCompraDet> GetFactCompraDetxFCTotal(int tipo, DateTime desde, DateTime hasta)
        {
            fCompraDet = negFactComp.GetFacturaCompraDetTotal(tipo, desde, hasta);
            var Lista = fCompraDet.ToList();
            return Lista;
        }
        public List<Insumos> GetInsumos_x_Almacen(int id_almacen)
        {
            var Lista = insAlm.Where(w => w.ID_ALMA == id_almacen).ToList();
            return Lista;
        }
        public List<FactCompra> GetCondicionPago()
        {
            return conpago.ToList();
        }

        public int GetDocAbastecido()
        {
            return fCompra.Where(w => w.FC_ESTADO_DOC == 2).Count();
        }
        public int GetDocEmitido()
        {
            return fCompra.Where(w => w.FC_ESTADO_DOC == 1).Count();
        }
        public int GetDocAnulado()
        {
            return fCompra.Where(w => w.FC_ESTADO_DOC == 0).Count();
        }
        public List<FactCompra> GetCantidadFactCompra()
        {
            var groupedCustomerList = fCompra
                                      .GroupBy(u => u.FC_ESTADO_DOC)
                                      .Select(grp => new FactCompra()).ToList();
            return groupedCustomerList;
        }

        public string GetUniMedxIns(int id)
        {
            string p = "";
            if (id > 0)
            {
                p = ins.Where(w => w.idins == id).FirstOrDefault().nomaum;
            }

            return p;
        }

    }
}
