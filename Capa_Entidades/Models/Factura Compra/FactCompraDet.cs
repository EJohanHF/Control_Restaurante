using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Capa_Entidades.Models.Factura_Compra
{
    public class FactCompraDet : INotifyPropertyChanged
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
        public FactCompraDet()
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
        //public int FCD_ID_FACT_COMPRA { get; set; }
        private int _FCD_ID_FACT_COMPRA;
        public int FCD_ID_FACT_COMPRA
        {
            get { return _FCD_ID_FACT_COMPRA; }
            set
            {
                if (value == _FCD_ID_FACT_COMPRA)
                    return;

                _FCD_ID_FACT_COMPRA = value;
                OnPropertyChanged();
            }
        }
        //public int FCD_ORDEN_ITEM { get; set; }
        private int _FCD_ORDEN_ITEM;
        public int FCD_ORDEN_ITEM
        {
            get { return _FCD_ORDEN_ITEM; }
            set
            {
                if (value == _FCD_ORDEN_ITEM)
                    return;

                _FCD_ORDEN_ITEM = value;
                OnPropertyChanged();
            }
        }

        //public int FCD_ID_INS { get; set; }
        private int _FCD_ID_INS = 0;
        public int FCD_ID_INS
        {
            get { return _FCD_ID_INS; }
            set
            {
                if (value == _FCD_ID_INS)
                    return;

                _FCD_ID_INS = value;
                OnPropertyChanged();
            }
        }
        private string _INS_NOM;
        public string INS_NOM
        {
            get { return _INS_NOM; }
            set
            {
                if (value == _INS_NOM)
                    return;

                _INS_NOM = value;
                OnPropertyChanged();
            }
        }


        //public string FCD_DESCR_INS { get; set; }
        private string _FCD_DESCR_INS;
        public string FCD_DESCR_INS
        {
            get { return _FCD_DESCR_INS; }
            set
            {
                if (value == _FCD_DESCR_INS)
                    return;

                _FCD_DESCR_INS = value;
                OnPropertyChanged();
            }
        }
        //public string FCD_UN_MED_INS { get; set; }
        private string _FCD_UN_MED_INS;
        public string FCD_UN_MED_INS
        {
            get { return _FCD_UN_MED_INS; }
            set
            {
                if (value == _FCD_UN_MED_INS)
                    return;

                _FCD_UN_MED_INS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_CANT_ITEM { get; set; }
        private decimal _FCD_CANT_ITEM;
        public decimal FCD_CANT_ITEM
        {
            get { return _FCD_CANT_ITEM; }
            set
            {
                if (value == _FCD_CANT_ITEM)
                    return;

                _FCD_CANT_ITEM = value;
                OnPropertyChanged();
              
            }
        }
       
        //public decimal FCD_VALOR_UNITARIO_SIN_IGV { get; set; }
        private decimal _FCD_VALOR_UNITARIO_SIN_IGV;
        public decimal FCD_VALOR_UNITARIO_SIN_IGV
        {
            get { return _FCD_VALOR_UNITARIO_SIN_IGV; }
            set
            {
                if (value == _FCD_VALOR_UNITARIO_SIN_IGV)
                    return;

                _FCD_VALOR_UNITARIO_SIN_IGV = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_VALOR_UNITARIO_CON_IGV { get; set; }
        private decimal _FCD_VALOR_UNITARIO_CON_IGV;
        public decimal FCD_VALOR_UNITARIO_CON_IGV
        {
            get { return _FCD_VALOR_UNITARIO_CON_IGV; }
            set
            {
                if (value == _FCD_VALOR_UNITARIO_CON_IGV)
                    return;

                _FCD_VALOR_UNITARIO_CON_IGV = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_IMPORTE_IGV_INS { get; set; }
        private decimal _FCD_IMPORTE_IGV_INS;
        public decimal FCD_IMPORTE_IGV_INS
        {
            get { return _FCD_IMPORTE_IGV_INS; }
            set
            {
                if (value == _FCD_IMPORTE_IGV_INS)
                    return;

                _FCD_IMPORTE_IGV_INS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_COD_AFECTACION_IGV_ITEM { get; set; }
        private decimal _FCD_COD_AFECTACION_IGV_ITEM;
        public decimal FCD_COD_AFECTACION_IGV_ITEM
        {
            get { return _FCD_COD_AFECTACION_IGV_ITEM; }
            set
            {
                if (value == _FCD_COD_AFECTACION_IGV_ITEM)
                    return;

                _FCD_COD_AFECTACION_IGV_ITEM = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_DESC_INS { get; set; }
        private decimal _FCD_DESC_INS;
        public decimal FCD_DESC_INS
        {
            get { return _FCD_DESC_INS; }
            set
            {
                if (value == _FCD_DESC_INS)
                    return;

                _FCD_DESC_INS = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_VALOR_COMPRA_ITEM { get; set; }
        private decimal _FCD_VALOR_COMPRA_ITEM;
        public decimal FCD_VALOR_COMPRA_ITEM
        {
            get { return _FCD_VALOR_COMPRA_ITEM; }
            set
            {
                if (value == _FCD_VALOR_COMPRA_ITEM)
                    return;

                _FCD_VALOR_COMPRA_ITEM = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_PRE_UNI { get; set; }
        private decimal _FCD_PRE_UNI;
        public decimal FCD_PRE_UNI
        {
            get { return _FCD_PRE_UNI; }
            set
            {
                if (value == _FCD_PRE_UNI)
                    return;

                _FCD_PRE_UNI = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_IMPORTE_ITEM { get; set; }
        private decimal _FCD_IMPORTE_ITEM;
        public decimal FCD_IMPORTE_ITEM
        {
            get { return _FCD_IMPORTE_ITEM; }
            set
            {
                if (value == _FCD_IMPORTE_ITEM)
                    return;

                _FCD_IMPORTE_ITEM = value;
                OnPropertyChanged();
            }
        }
        //public decimal FCD_VALOR_COMPRA_SIN_IGV { get; set; }
        private decimal _FCD_VALOR_COMPRA_SIN_IGV;
        public decimal FCD_VALOR_COMPRA_SIN_IGV
        {
            get { return _FCD_VALOR_COMPRA_SIN_IGV; }
            set
            {
                if (value == _FCD_VALOR_COMPRA_SIN_IGV)
                    return;

                _FCD_VALOR_COMPRA_SIN_IGV = value;
                OnPropertyChanged();
            }
        }
        private int _FCD_ID_ALM;
        public int FCD_ID_ALM
        {
            get { return _FCD_ID_ALM; }
            set
            {
                if (value == _FCD_ID_ALM)
                    return;

                _FCD_ID_ALM = value;
                OnPropertyChanged();
            }
        }
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

    }
}
