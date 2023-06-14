using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Entidades.Models.Reportes.Rpt_BoletayFactura;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes.Report_BoletayFactura;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Report_BoletayFactura
{
    public class BoletaFacturaStructure
    {
        #region Negocio
        Neg_BoletayFactura negBoletaFactura = new Neg_BoletayFactura();
        Funcion_Global globales = new Funcion_Global();
        #endregion

        #region Entidad
        static ObservableCollection<Ent_RptBoletayFactura> bolfact = new ObservableCollection<Ent_RptBoletayFactura>();
        static ObservableCollection<Estado_Doc_Electronico> estado_doc = new ObservableCollection<Estado_Doc_Electronico>();
        #endregion
        public BoletaFacturaStructure()
        {
            bolfact = negBoletaFactura.GetDocElectronico();
        }
        public List<Ent_RptBoletayFactura> GetDocElectronicos(int op)
        {
            if (op == 1) { bolfact = negBoletaFactura.GetDocElectronico(); }
            return bolfact.ToList();
        }
        public List<Ent_RptBoletayFactura> GetDocElectronicosxFiltro(DateTime startDate, DateTime finishDate, string tipoDoc, int estadoDoc)
        {
            List<Ent_RptBoletayFactura> h = new List<Ent_RptBoletayFactura>();
            try
            {
                ObservableCollection<Ent_RptBoletayFactura> _h = new ObservableCollection<Ent_RptBoletayFactura>();
                _h = negBoletaFactura.GetDocElectronicoRpt(startDate, finishDate, tipoDoc, estadoDoc);
                List<Ent_RptBoletayFactura> hh = new List<Ent_RptBoletayFactura>(_h);
                h = hh;
                
            }
            catch (Exception)
            {
                //globales.Mensaje("SOS-FOOD - Error", "Error al traer los datos", 3);
            }
            return h;
        }
        public List<Ent_RptBoletayFactura> GetDocElectronicosxFiltroSerie(string serieDocumento)
        {
            List<Ent_RptBoletayFactura> h;
            ObservableCollection<Ent_RptBoletayFactura> _h = new ObservableCollection<Ent_RptBoletayFactura>();
            _h = negBoletaFactura.GetDocElectronicosxFiltroSerie(serieDocumento);
            List<Ent_RptBoletayFactura> hh = new List<Ent_RptBoletayFactura>(_h);
            h = hh;
            return h;
        }
        public List<Ent_RptBoletayFactura> GetDocElectronicosxInicop()
        {
            List<Ent_RptBoletayFactura> h;
            ObservableCollection<Ent_RptBoletayFactura> _h = new ObservableCollection<Ent_RptBoletayFactura>();
            _h = negBoletaFactura.GetDocElectronicoHoy();
            List<Ent_RptBoletayFactura> hh = new List<Ent_RptBoletayFactura>(_h);
            h = hh;
            return h;
        }
        public ObservableCollection<Estado_Doc_Electronico> GetEstadoDocumentos()
        {
            estado_doc = negBoletaFactura.GetEstadoDocumento();
            return estado_doc;
        }
        public ObservableCollection<Ent_RptBoletayFactura> GetConsolidadoDocElectronicos(DateTime desde, DateTime hasta)
        {
            ObservableCollection<Ent_RptBoletayFactura> h;
            h = negBoletaFactura.GetConsolidadoDocElectronicos(desde, hasta);
            return h;
        }
    }
}
