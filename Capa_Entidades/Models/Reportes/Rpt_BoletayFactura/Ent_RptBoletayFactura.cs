using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.Rpt_BoletayFactura
{
    public class Ent_RptBoletayFactura : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region DOC ELECTRONICO
        private int _id_doc_electronico;
        public int id_doc_electronico
        {
            get { return _id_doc_electronico; }
            set
            {
                if (value == _id_doc_electronico)
                    return;

                _id_doc_electronico = value;
                OnPropertyChanged();
            }
        }
        private string _serieNumero;
        public string serieNumero
        {
            get { return _serieNumero; }
            set
            {
                if (value == _serieNumero)
                    return;

                _serieNumero = value;
                OnPropertyChanged();
            }
        }
        private string _tipoDocumento;
        public string tipoDocumento
        {
            get { return _tipoDocumento; }
            set
            {
                if (value == _tipoDocumento)
                    return;

                _tipoDocumento = value;
                OnPropertyChanged();
            }
        }
        private DateTime _fechaEmision;
        public DateTime fechaEmision
        {
            get { return _fechaEmision; }
            set
            {
                if (value == _fechaEmision)
                    return;

                _fechaEmision = value;
                OnPropertyChanged();
            }
        }
        private string _numeroDocIdentidadEmisor;
        public string numeroDocIdentidadEmisor
        {
            get { return _numeroDocIdentidadEmisor; }
            set
            {
                if (value == _numeroDocIdentidadEmisor)
                    return;

                _numeroDocIdentidadEmisor = value;
                OnPropertyChanged();
            }
        }
        private string _tipoDocIdentidadEmisor;
        public string tipoDocIdentidadEmisor
        {
            get { return _tipoDocIdentidadEmisor; }
            set
            {
                if (value == _tipoDocIdentidadEmisor)
                    return;

                _tipoDocIdentidadEmisor = value;
                OnPropertyChanged();
            }
        }
        private string _numeroDocIdentidadReceptor;
        public string numeroDocIdentidadReceptor
        {
            get { return _numeroDocIdentidadReceptor; }
            set
            {
                if (value == _numeroDocIdentidadReceptor)
                    return;

                _numeroDocIdentidadReceptor = value;
                OnPropertyChanged();
            }
        }
        private string _razonSocialReceptor;
        public string razonSocialReceptor
        {
            get { return _razonSocialReceptor; }
            set
            {
                if (value == _razonSocialReceptor)
                    return;

                _razonSocialReceptor = value;
                OnPropertyChanged();
            }
        }
        private string _direccionReceptor;
        public string direccionReceptor
        {
            get { return _direccionReceptor; }
            set
            {
                if (value == _direccionReceptor)
                    return;

                _direccionReceptor = value;
                OnPropertyChanged();
            }
        }
        private string _correoReceptor;
        public string correoReceptor
        {
            get { return _correoReceptor; }
            set
            {
                if (value == _correoReceptor)
                    return;

                _correoReceptor = value;
                OnPropertyChanged();
            }
        }
        private string _tipoDocIdentidadReceptor;
        public string tipoDocIdentidadReceptor
        {
            get { return _tipoDocIdentidadReceptor; }
            set
            {
                if (value == _tipoDocIdentidadReceptor)
                    return;

                _tipoDocIdentidadReceptor = value;
                OnPropertyChanged();
            }
        }
        private string _telefono;
        public string telefono
        {
            get { return _telefono; }
            set
            {
                if (value == _telefono)
                    return;

                _telefono = value;
                OnPropertyChanged();
            }
        }
        private int _idCliente;
        public int idCliente
        {
            get { return _idCliente; }
            set
            {
                if (value == _idCliente)
                    return;

                _idCliente = value;
                OnPropertyChanged();
            }
        }
        private string _NOMINA;
        public string NOMINA
        {
            get { return _NOMINA; }
            set
            {
                if (value == _NOMINA)
                    return;

                _NOMINA = value;
                OnPropertyChanged();
            }
        }
        private decimal _totalOPGravadas;
        public decimal totalOPGravadas
        {
            get { return _totalOPGravadas; }
            set
            {
                if (value == _totalOPGravadas)
                    return;

                _totalOPGravadas = value;
                OnPropertyChanged();
            }
        }
        private Decimal _totalOPExoneradas;
        public decimal totalOPExoneradas
        {
            get { return _totalOPExoneradas; }
            set
            {
                if (value == _totalOPExoneradas)
                    return;

                _totalOPExoneradas = value;
                OnPropertyChanged();
            }
        }
        private decimal _totalOPNoGravadas;
        public decimal totalOPNoGravadas
        {
            get { return _totalOPNoGravadas; }
            set
            {
                if (value == _totalOPNoGravadas)
                    return;

                _totalOPNoGravadas = value;
                OnPropertyChanged();
            }
        }
        private decimal _totalOPGratuitas;
        public decimal totalOPGratuitas
        {
            get { return _totalOPGratuitas; }
            set
            {
                if (value == _totalOPGratuitas)
                    return;

                _totalOPGratuitas = value;
                OnPropertyChanged();
            }
        }
        private decimal _sumatoriaIGV;
        public decimal sumatoriaIGV
        {
            get { return _sumatoriaIGV; }
            set
            {
                if (value == _sumatoriaIGV)
                    return;

                _sumatoriaIGV = value;
                OnPropertyChanged();
            }
        }
        private decimal _sumatoriaISC;
        public decimal sumatoriaISC
        {
            get { return _sumatoriaISC; }
            set
            {
                if (value == _sumatoriaISC)
                    return;

                _sumatoriaISC = value;
                OnPropertyChanged();
            }
        }
        private decimal _importeTotal;
        public decimal importeTotal
        {
            get { return _importeTotal; }
            set
            {
                if (value == _importeTotal)
                    return;

                _importeTotal = value;
                OnPropertyChanged();
            }
        }
        private decimal _importeTotalVenta;
        public decimal importeTotalVenta
        {
            get { return _importeTotalVenta; }
            set
            {
                if (value == _importeTotalVenta)
                    return;

                _importeTotalVenta = value;
                OnPropertyChanged();
            }
        }
        private decimal _totalDescuentos;
        public decimal totalDescuentos
        {
            get { return _totalDescuentos; }
            set
            {
                if (value == _totalDescuentos)
                    return;

                _totalDescuentos = value;
                OnPropertyChanged();
            }
        }
        private decimal _descuentosGlobales;
        public decimal descuentosGlobales
        {
            get { return _descuentosGlobales; }
            set
            {
                if (value == _descuentosGlobales)
                    return;

                _descuentosGlobales = value;
                OnPropertyChanged();
            }
        }
        private decimal _sumatoriaOtrosCargos;
        public decimal sumatoriaOtrosCargos
        {
            get { return _sumatoriaOtrosCargos; }
            set
            {
                if (value == _sumatoriaOtrosCargos)
                    return;

                _sumatoriaOtrosCargos = value;
                OnPropertyChanged();
            }
        }
        private decimal _porcentajeOtrosCargos;
        public decimal porcentajeOtrosCargos
        {
            get { return _porcentajeOtrosCargos; }
            set
            {
                if (value == _porcentajeOtrosCargos)
                    return;

                _porcentajeOtrosCargos = value;
                OnPropertyChanged();
            }
        }
        private decimal _sumatoriaImpuestoBolsas;
        public decimal sumatoriaImpuestoBolsas
        {
            get { return _sumatoriaImpuestoBolsas; }
            set
            {
                if (value == _sumatoriaImpuestoBolsas)
                    return;

                _sumatoriaImpuestoBolsas = value;
                OnPropertyChanged();
            }
        }
        private string _tipoMoneda;
        public string tipoMoneda
        {
            get { return _tipoMoneda; }
            set
            {
                if (value == _tipoMoneda)
                    return;

                _tipoMoneda = value;
                OnPropertyChanged();
            }
        }
        private string _codigoPaisReceptor;
        public string codigoPaisReceptor
        {
            get { return _codigoPaisReceptor; }
            set
            {
                if (value == _codigoPaisReceptor)
                    return;

                _codigoPaisReceptor = value;
                OnPropertyChanged();
            }
        }
        private string _codigoTipoOperacion;
        public string codigoTipoOperacion
        {
            get { return _codigoTipoOperacion; }
            set
            {
                if (value == _codigoTipoOperacion)
                    return;

                _codigoTipoOperacion = value;
                OnPropertyChanged();
            }
        }
        private string _montoEnLetras;
        public string montoEnLetras
        {
            get { return _montoEnLetras; }
            set
            {
                if (value == _montoEnLetras)
                    return;

                _montoEnLetras = value;
                OnPropertyChanged();
            }
        }
        private int _idPedido;
        public int idPedido
        {
            get { return _idPedido; }
            set
            {
                if (value == _idPedido)
                    return;

                _idPedido = value;
                OnPropertyChanged();
            }
        }
        private bool _Doc_Estado;
        public bool Doc_Estado
        {
            get { return _Doc_Estado; }
            set
            {
                if (value == _Doc_Estado)
                    return;

                _Doc_Estado = value;
                OnPropertyChanged();
            }
        }
        private string _Doc_Estado_nom;
        public string Doc_Estado_nom
        {
            get { return _Doc_Estado_nom; }
            set
            {
                if (value == _Doc_Estado_nom)
                    return;

                _Doc_Estado_nom = value;
                OnPropertyChanged();
            }
        }
        private int _Doc_id_cierre;
        public int Doc_id_cierre
        {
            get { return _Doc_id_cierre; }
            set
            {
                if (value == _Doc_id_cierre)
                    return;

                _Doc_id_cierre = value;
                OnPropertyChanged();
            }
        }
        
        private bool _EnabledAnular;
        public bool EnabledAnular
        {
            get { return _EnabledAnular; }
            set
            {
                if (value == _EnabledAnular)
                    return;

                _EnabledAnular = value;
                OnPropertyChanged();
            }
        }
        
        private bool _EnabledImprimir;
        public bool EnabledImprimir
        {
            get { return _EnabledImprimir; }
            set
            {
                if (value == _EnabledImprimir)
                    return;

                _EnabledImprimir = value;
                OnPropertyChanged();
            }
        }
        
        private string _ColorLetraEstado;
        public string ColorLetraEstado
        {
            get { return _ColorLetraEstado; }
            set
            {
                if (value == _ColorLetraEstado)
                    return;

                _ColorLetraEstado = value;
                OnPropertyChanged();
            }
        }

        //CONSOLIDADO
        public decimal igv { get; set; } = 0;
        public decimal rc { get; set; } = 0;
        public decimal icbper { get; set; } = 0;
        public decimal total { get; set; } = 0;
        
        #endregion

        #region DOC ELECTRONICO DET
        private int _ID_DOC_ELECTRONICO_DET;
        public int ID_DOC_ELECTRONICO_DET
        {
            get { return _ID_DOC_ELECTRONICO_DET; }
            set
            {
                if (value == _ID_DOC_ELECTRONICO_DET)
                    return;

                _ID_DOC_ELECTRONICO_DET = value;
                OnPropertyChanged();
            }
        }
        private int _ID_DOC_ELECTRONICO;
        public int ID_DOC_ELECTRONICO
        {
            get { return _ID_DOC_ELECTRONICO; }
            set
            {
                if (value == _ID_DOC_ELECTRONICO)
                    return;

                _ID_DOC_ELECTRONICO = value;
                OnPropertyChanged();
            }
        }
        private string _ordenItem;
        public string ordenItem
        {
            get { return _ordenItem; }
            set
            {
                if (value == _ordenItem)
                    return;

                _ordenItem = value;
                OnPropertyChanged();
            }
        }
        private int _codigoProductoItem;
        public int codigoProductoItem
        {
            get { return _codigoProductoItem; }
            set
            {
                if (value == _codigoProductoItem)
                    return;

                _codigoProductoItem = value;
                OnPropertyChanged();
            }
        }
        private string _descripcionItem;
        public string descripcionItem
        {
            get { return _descripcionItem; }
            set
            {
                if (value == _descripcionItem)
                    return;

                _descripcionItem = value;
                OnPropertyChanged();
            }
        }
        private string _unidadMedidaItem;
        public string unidadMedidaItem
        {
            get { return _unidadMedidaItem; }
            set
            {
                if (value == _unidadMedidaItem)
                    return;

                _unidadMedidaItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _cantidadItem;
        public decimal cantidadItem
        {
            get { return _cantidadItem; }
            set
            {
                if (value == _cantidadItem)
                    return;

                _cantidadItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _valorUnitarioSinIgv;
        public decimal valorUnitarioSinIgv
        {
            get { return _valorUnitarioSinIgv; }
            set
            {
                if (value == _valorUnitarioSinIgv)
                    return;

                _valorUnitarioSinIgv = value;
                OnPropertyChanged();
            }
        }
        private decimal _precioUnitarioConIgv;
        public decimal precioUnitarioConIgv
        {
            get { return _precioUnitarioConIgv; }
            set
            {
                if (value == _precioUnitarioConIgv)
                    return;

                _precioUnitarioConIgv = value;
                OnPropertyChanged();
            }
        }
        private string _codTipoPrecioVtaUnitarioItem;
        public string codTipoPrecioVtaUnitarioItem
        {
            get { return _codTipoPrecioVtaUnitarioItem; }
            set
            {
                if (value == _codTipoPrecioVtaUnitarioItem)
                    return;

                _codTipoPrecioVtaUnitarioItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _importeIGVItem;
        public decimal importeIGVItem
        {
            get { return _importeIGVItem; }
            set
            {
                if (value == _importeIGVItem)
                    return;

                _importeIGVItem = value;
                OnPropertyChanged();
            }
        }
        private string _codigoAfectacionIGVItem;
        public string codigoAfectacionIGVItem
        {
            get { return _codigoAfectacionIGVItem; }
            set
            {
                if (value == _codigoAfectacionIGVItem)
                    return;

                _codigoAfectacionIGVItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _descuentoItem_det;
        public decimal descuentoItem_det
        {
            get { return _descuentoItem_det; }
            set
            {
                if (value == _descuentoItem_det)
                    return;

                _descuentoItem_det = value;
                OnPropertyChanged();
            }
        }
        private decimal _valorVentaItem;
        public decimal valorVentaItem
        {
            get { return _valorVentaItem; }
            set
            {
                if (value == _valorVentaItem)
                    return;

                _valorVentaItem = value;
                OnPropertyChanged();
            }
        }
        private decimal __descuentoItem;
        public decimal _descuentoItem
        {
            get { return __descuentoItem; }
            set
            {
                if (value == __descuentoItem)
                    return;

                __descuentoItem = value;
                OnPropertyChanged();
            }
        }
        private decimal __precioUnitario;
        public decimal _precioUnitario
        {
            get { return __precioUnitario; }
            set
            {
                if (value == __precioUnitario)
                    return;

                __precioUnitario = value;
                OnPropertyChanged();
            }
        }
        private decimal __valorVentaSinIGV;
        public decimal _valorVentaSinIGV
        {
            get { return __valorVentaSinIGV; }
            set
            {
                if (value == __valorVentaSinIGV)
                    return;

                __valorVentaSinIGV = value;
                OnPropertyChanged();
            }
        }
        private decimal _montoReferenciaItem;
        public decimal montoReferenciaItem
        {
            get { return _montoReferenciaItem; }
            set
            {
                if (value == _montoReferenciaItem)
                    return;

                _montoReferenciaItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _montoReferencialUnitarioItem;
        public decimal montoReferencialUnitarioItem
        {
            get { return _montoReferencialUnitarioItem; }
            set
            {
                if (value == _montoReferencialUnitarioItem)
                    return;

                _montoReferencialUnitarioItem = value;
                OnPropertyChanged();
            }
        }
        private string _clasificacionProductoItem;
        public string clasificacionProductoItem
        {
            get { return _clasificacionProductoItem; }
            set
            {
                if (value == _clasificacionProductoItem)
                    return;

                _clasificacionProductoItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _montoTotalItem;
        public decimal montoTotalItem
        {
            get { return _montoTotalItem; }
            set
            {
                if (value == _montoTotalItem)
                    return;

                _montoTotalItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _cantidadBolsasItem;
        public decimal cantidadBolsasItem
        {
            get { return _cantidadBolsasItem; }
            set
            {
                if (value == _cantidadBolsasItem)
                    return;

                _cantidadBolsasItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _montoUnitarioBolsasItem;
        public decimal montoUnitarioBolsasItem
        {
            get { return _montoUnitarioBolsasItem; }
            set
            {
                if (value == _montoUnitarioBolsasItem)
                    return;

                _montoUnitarioBolsasItem = value;
                OnPropertyChanged();
            }
        }
        private decimal _importeBolsasItem;
        public decimal importeBolsasItem
        {
            get { return _importeBolsasItem; }
            set
            {
                if (value == _importeBolsasItem)
                    return;

                _importeBolsasItem = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
