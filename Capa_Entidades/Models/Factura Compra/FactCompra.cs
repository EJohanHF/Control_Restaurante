using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Factura_Compra
{
    public class FactCompra : INotifyPropertyChanged
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
        public FactCompra()
        {

        }
        //public int ID { get; set; }
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if (value == _ID)
                    return;

                _ID = value;
                OnPropertyChanged();
            }
        }
        //public string FC_NRO_DOC_EMISOR { get; set; }
        private string _FC_NRO_DOC_EMISOR;
        public string FC_NRO_DOC_EMISOR
        {
            get { return _FC_NRO_DOC_EMISOR; }
            set
            {
                if (value == _FC_NRO_DOC_EMISOR)
                    return;

                _FC_NRO_DOC_EMISOR = value;
                OnPropertyChanged();
            }
        }
        //public int FC_TIP_DOC_EMISOR { get; set; }
        private int _FC_TIP_DOC_EMISOR;
        public int FC_TIP_DOC_EMISOR
        {
            get { return _FC_TIP_DOC_EMISOR; }
            set
            {
                if (value == _FC_TIP_DOC_EMISOR)
                    return;

                _FC_TIP_DOC_EMISOR = value;
                OnPropertyChanged();
            }
        }
        //public int FC_TIP_DOC { get; set; }
        private int _FC_TIP_DOC;
        public int FC_TIP_DOC
        {
            get { return _FC_TIP_DOC; }
            set
            {
                if (value == _FC_TIP_DOC)
                    return;

                _FC_TIP_DOC = value;
                OnPropertyChanged();
            }
        }
        //public string FC_SER_NRO { get; set; }
        private string _FC_SER_NRO = "";
        public string FC_SER_NRO
        {
            get { return _FC_SER_NRO; }
            set
            {
                if (value == _FC_SER_NRO)
                    return;

                _FC_SER_NRO = value;
                OnPropertyChanged();
            }
        }
        //public DateTime FC_F_EMISION { get; set; }
        private DateTime _FC_F_EMISION = DateTime.Now.Date;
        public DateTime FC_F_EMISION
        {
            get { return _FC_F_EMISION; }
            set
            {
                if (value == _FC_F_EMISION)
                    return;

                _FC_F_EMISION = value;
                OnPropertyChanged();
            }
        }
        //public string FC_TIP_MONEDA { get; set; }
        private string _FC_TIP_MONEDA;
        public string FC_TIP_MONEDA
        {
            get { return _FC_TIP_MONEDA; }
            set
            {
                if (value == _FC_TIP_MONEDA)
                    return;

                _FC_TIP_MONEDA = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_TOTAL_OP_GRABADAS { get; set; }
        private decimal _FC_TOTAL_OP_GRABADAS;
        public decimal FC_TOTAL_OP_GRABADAS
        {
            get { return _FC_TOTAL_OP_GRABADAS; }
            set
            {
                if (value == _FC_TOTAL_OP_GRABADAS)
                    return;

                _FC_TOTAL_OP_GRABADAS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_TOTAL_OP_EXONERADAS { get; set; }
        private decimal _FC_TOTAL_OP_EXONERADAS;
        public decimal FC_TOTAL_OP_EXONERADAS
        {
            get { return _FC_TOTAL_OP_EXONERADAS; }
            set
            {
                if (value == _FC_TOTAL_OP_EXONERADAS)
                    return;

                _FC_TOTAL_OP_EXONERADAS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_TOTAL_OP_NO_GRABADAS { get; set; }
        private decimal _FC_TOTAL_OP_NO_GRABADAS;
        public decimal FC_TOTAL_OP_NO_GRABADAS
        {
            get { return _FC_TOTAL_OP_NO_GRABADAS; }
            set
            {
                if (value == _FC_TOTAL_OP_NO_GRABADAS)
                    return;

                _FC_TOTAL_OP_NO_GRABADAS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_TOTAL_OP_GRATUITAS { get; set; }
        private decimal _FC_TOTAL_OP_GRATUITAS;
        public decimal FC_TOTAL_OP_GRATUITAS
        {
            get { return _FC_TOTAL_OP_GRATUITAS; }
            set
            {
                if (value == _FC_TOTAL_OP_GRATUITAS)
                    return;

                _FC_TOTAL_OP_GRATUITAS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_SUMA_IGV { get; set; }
        private decimal _FC_SUMA_IGV;
        public decimal FC_SUMA_IGV
        {
            get { return _FC_SUMA_IGV; }
            set
            {
                if (value == _FC_SUMA_IGV)
                    return;

                _FC_SUMA_IGV = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_IMPORTE_TOTAL_COMPRA { get; set; }
        private decimal _FC_IMPORTE_TOTAL_COMPRA;
        public decimal FC_IMPORTE_TOTAL_COMPRA
        {
            get { return _FC_IMPORTE_TOTAL_COMPRA; }
            set
            {
                if (value == _FC_IMPORTE_TOTAL_COMPRA)
                    return;

                _FC_IMPORTE_TOTAL_COMPRA = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_TOTAL_DESC { get; set; }
        private decimal _FC_TOTAL_DESC;
        public decimal FC_TOTAL_DESC
        {
            get { return _FC_TOTAL_DESC; }
            set
            {
                if (value == _FC_TOTAL_DESC)
                    return;

                _FC_TOTAL_DESC = value;
                OnPropertyChanged();
            }
        }
        //public decimal FC_DESCUENTOS_GLOBALES { get; set; }
        private decimal _FC_DESCUENTOS_GLOBALES;
        public decimal FC_DESCUENTOS_GLOBALES
        {
            get { return _FC_DESCUENTOS_GLOBALES; }
            set
            {
                if (value == _FC_DESCUENTOS_GLOBALES)
                    return;

                _FC_DESCUENTOS_GLOBALES = value;
                OnPropertyChanged();
            }
        }
        //public string FC_MONTO_LETRA { get; set; }
        private string _FC_MONTO_LETRA;
        public string FC_MONTO_LETRA
        {
            get { return _FC_MONTO_LETRA; }
            set
            {
                if (value == _FC_MONTO_LETRA)
                    return;

                _FC_MONTO_LETRA = value;
                OnPropertyChanged();
            }
        }
        //public string FC_CORREO_EMISOR { get; set; }
        private string _FC_CORREO_EMISOR;
        public string FC_CORREO_EMISOR
        {
            get { return _FC_CORREO_EMISOR; }
            set
            {
                if (value == _FC_CORREO_EMISOR)
                    return;

                _FC_CORREO_EMISOR = value;
                OnPropertyChanged();
            }
        }
        //public string FC_COD_PAIS_EMISOR { get; set; }
        private string _FC_COD_PAIS_EMISOR;
        public string FC_COD_PAIS_EMISOR
        {
            get { return _FC_COD_PAIS_EMISOR; }
            set
            {
                if (value == _FC_COD_PAIS_EMISOR)
                    return;

                _FC_COD_PAIS_EMISOR = value;
                OnPropertyChanged();
            }
        }
        //public int FC_ID_PROVEEDOR { get; set; }
        private int _FC_ID_PROVEEDOR;
        public int FC_ID_PROVEEDOR
        {
            get { return _FC_ID_PROVEEDOR; }
            set
            {
                if (value == _FC_ID_PROVEEDOR)
                    return;

                _FC_ID_PROVEEDOR = value;
                OnPropertyChanged();
            }
        }
        private string _P_NOM;
        public string P_NOM
        {
            get { return _P_NOM; }
            set
            {
                if (value == _P_NOM)
                    return;

                _P_NOM = value;
                OnPropertyChanged();
            }
        }

        //public bool FC_ESTADO_DOC { get; set; }
        private int _FC_ESTADO_DOC;
        public int FC_ESTADO_DOC
        {
            get { return _FC_ESTADO_DOC; }
            set
            {
                if (value == _FC_ESTADO_DOC)
                    return;

                _FC_ESTADO_DOC = value;
                OnPropertyChanged();
            }
        }
        //public DateTime FC_VENCIMIENTO { get; set; }
        private DateTime _FC_VENCIMIENTO = DateTime.Now.Date;
        public DateTime FC_VENCIMIENTO
        {
            get { return _FC_VENCIMIENTO; }
            set
            {
                if (value == _FC_VENCIMIENTO)
                    return;

                _FC_VENCIMIENTO = value;
                OnPropertyChanged();
            }
        }
        private decimal _FC_TOTAL_PAGADO;
        public decimal FC_TOTAL_PAGADO
        {
            get { return _FC_TOTAL_PAGADO; }
            set
            {
                if (value == _FC_TOTAL_PAGADO)
                    return;

                _FC_TOTAL_PAGADO = value;
                OnPropertyChanged();
            }
        }
        private decimal _FC_SALDO_X_PAGAR;
        public decimal FC_SALDO_X_PAGAR
        {
            get { return _FC_SALDO_X_PAGAR; }
            set
            {
                if (value == _FC_SALDO_X_PAGAR)
                    return;

                _FC_SALDO_X_PAGAR = value;
                OnPropertyChanged();
            }
        }
        private bool _FC_ESTADO_CARGA;
        public bool FC_ESTADO_CARGA
        {
            get { return _FC_ESTADO_CARGA; }
            set
            {
                if (value == _FC_ESTADO_CARGA)
                    return;

                _FC_ESTADO_CARGA = value;
                OnPropertyChanged();
            }
        }

        private int _FC_CONDICION_PAGO;
        public int FC_CONDICION_PAGO
        {
            get { return _FC_CONDICION_PAGO; }
            set
            {
                if (value == _FC_CONDICION_PAGO)
                    return;

                _FC_CONDICION_PAGO = value;
                OnPropertyChanged();
            }
        }

        private string _FC_NOMBRE_TIP_DOC;
        public string FC_NOMBRE_TIP_DOC
        {
            get { return _FC_NOMBRE_TIP_DOC; }
            set
            {
                if (value == _FC_NOMBRE_TIP_DOC)
                    return;

                _FC_NOMBRE_TIP_DOC = value;
                OnPropertyChanged();
            }
        }


        private string _FC_NOMBRE_ESTADO;
        public string FC_NOMBRE_ESTADO
        {
            get { return _FC_NOMBRE_ESTADO; }
            set
            {
                if (value == _FC_NOMBRE_ESTADO)
                    return;

                _FC_NOMBRE_ESTADO = value;
                OnPropertyChanged();
            }
        }
        private string _FC_COLOR_ESTADO;
        public string FC_COLOR_ESTADO
        {
            get { return _FC_COLOR_ESTADO; }
            set
            {
                if (value == _FC_COLOR_ESTADO)
                    return;

                _FC_COLOR_ESTADO = value;
                OnPropertyChanged();
            }
        }
        private bool _EnabledCarga;
        public bool EnabledCarga
        {
            get { return _EnabledCarga; }
            set
            {
                if (value == _EnabledCarga)
                    return;

                _EnabledCarga = value;
                OnPropertyChanged();
            }
        }

        //CONDICION DE PAGO
        private int _condicionID;
        public int CONDICION_ID
        {
            get { return _condicionID; }
            set
            {
                if (value == _condicionID)
                    return;

                _condicionID = value;
                OnPropertyChanged();
            }
        }
        private string _condicionDESCR;
        public string CONDICION_DESCR
        {
            get { return _condicionDESCR; }
            set
            {
                if (value == _condicionDESCR)
                    return;

                _condicionDESCR = value;
                OnPropertyChanged();
            }
        }
        private DateTime _condicionFCREATE;
        public DateTime CONDICION_FCREATE
        {
            get { return _condicionFCREATE; }
            set
            {
                if (value == _condicionFCREATE)
                    return;

                _condicionFCREATE = value;
                OnPropertyChanged();
            }
        }
        private DateTime _condicionFMODIF;
        public DateTime CONDICION_FMODIF
        {
            get { return _condicionFMODIF; }
            set
            {
                if (value == _condicionFMODIF)
                    return;

                _condicionFMODIF = value;
                OnPropertyChanged();
            }
        }
        public bool ischeckEstadoDoc { get; set; } = true;
        public bool ischeckTipoDoc { get; set; } = true;


    }
}
