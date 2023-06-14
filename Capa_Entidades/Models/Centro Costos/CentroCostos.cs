using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Centro_Costos
{
    public class CentroCostos : INotifyPropertyChanged
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
        public CentroCostos()
        {

        }
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
        private string _CAT_DESCR;
        public string CAT_DESCR
        {
            get { return _CAT_DESCR; }
            set
            {
                if (value == _CAT_DESCR)
                    return;

                _CAT_DESCR = value;
                OnPropertyChanged();
            }
        }
        private string _CC_MES;
        public string CC_MES
        {
            get { return _CC_MES; }
            set
            {
                if (value == _CC_MES)
                    return;

                _CC_MES = value;
                OnPropertyChanged();
            }
        }
        
        private string _TP_DENOMINACION;
        public string TP_DENOMINACION
        {
            get { return _TP_DENOMINACION; }
            set
            {
                if (value == _TP_DENOMINACION)
                    return;

                _TP_DENOMINACION = value;
                OnPropertyChanged();
            }
        }
        private int _CATEGORIA;
        public int CATEGORIA
        {
            get { return _CATEGORIA; }
            set
            {
                if (value == _CATEGORIA)
                    return;

                _CATEGORIA = value;
                OnPropertyChanged();
            }
        }
        private int _CC_TIPO;
        public int CC_TIPO
        {
            get { return _CC_TIPO; }
            set
            {
                if (value == _CC_TIPO)
                    return;

                _CC_TIPO = value;
                OnPropertyChanged();
            }
        }
        private decimal _CC_MONTO;
        public decimal CC_MONTO
        {
            get { return _CC_MONTO; }
            set
            {
                if (value == _CC_MONTO)
                    return;

                _CC_MONTO = value;
                OnPropertyChanged();
            }
        }
        private DateTime _CC_F_REG;
        public DateTime CC_F_REG
        {
            get { return _CC_F_REG; }
            set
            {
                if (value == _CC_F_REG)
                    return;

                _CC_F_REG = value;
                OnPropertyChanged();
            }
        }
        private DateTime _CC_F_MODIFICACION;
        public DateTime CC_F_MODIFICACION
        {
            get { return _CC_F_MODIFICACION; }
            set
            {
                if (value == _CC_F_MODIFICACION)
                    return;

                _CC_F_MODIFICACION = value;
                OnPropertyChanged();
            }
        }
        private string _CC_AÑO;
        public string CC_AÑO
        {
            get { return _CC_AÑO; }
            set
            {
                if (value == _CC_AÑO)
                    return;

                _CC_AÑO = value;
                OnPropertyChanged();
            }
        }
        
        private string _CC_OBS;
        public string CC_OBS
        {
            get { return _CC_OBS; }
            set
            {
                if (value == _CC_OBS)
                    return;

                _CC_OBS = value;
                OnPropertyChanged();
            }
        }
        
        private string _Cantidad;
        public string Cantidad
        {
            get { return _Cantidad; }
            set
            {
                if (value == _Cantidad)
                    return;

                _Cantidad = value;
                OnPropertyChanged();
            }
        }














        ///////////////////////////
        private decimal _MP ;
        public decimal MP
        {
            get { return _MP; }
            set
            {
                if (value == _MP)
                    return;

                _MP = value;
                OnPropertyChanged();
            }
        }
        private decimal _MO;
        public decimal MO
        {
            get { return _MO; }
            set
            {
                if (value == _MO)
                    return;

                _MO = value;
                OnPropertyChanged();
            }
        }
        private decimal _GIF;
        public decimal GIF
        {
            get { return _GIF; }
            set
            {
                if (value == _GIF)
                    return;

                _GIF = value;
                OnPropertyChanged();
            }
        }

        private decimal _MPI;
        public decimal MPI
        {
            get { return _MPI; }
            set
            {
                if (value == _MPI)
                    return;

                _MPI = value;
                OnPropertyChanged();
            }
        }
        private decimal _MOI;
        public decimal MOI
        {
            get { return _MOI; }
            set
            {
                if (value == _MOI)
                    return;

                _MOI = value;
                OnPropertyChanged();
            }
        }
        private decimal _OTROGIFREL;
        public decimal OTROGIFREL
        {
            get { return _OTROGIFREL; }
            set
            {
                if (value == _OTROGIFREL)
                    return;

                _OTROGIFREL = value;
                OnPropertyChanged();
            }
        }
        private decimal _SEGURO;
        public decimal SEGURO
        {
            get { return _SEGURO; }
            set
            {
                if (value == _SEGURO)
                    return;

                _SEGURO = value;
                OnPropertyChanged();
            }
        }
        private decimal _ENERGIA;
        public decimal ENERGIA
        {
            get { return _ENERGIA; }
            set
            {
                if (value == _ENERGIA)
                    return;

                _ENERGIA = value;
                OnPropertyChanged();
            }
        }
        private decimal _DEPÑREE;
        public decimal DEPÑREE
        {
            get { return _DEPÑREE; }
            set
            {
                if (value == _DEPÑREE)
                    return;

                _DEPÑREE = value;
                OnPropertyChanged();
            }
        }
        private decimal _DEPREL;
        public decimal DEPREL
        {
            get { return _DEPREL; }
            set
            {
                if (value == _DEPREL)
                    return;

                _DEPREL = value;
                OnPropertyChanged();
            }
        }

        private decimal _cant;
        public decimal cant
        {
            get { return _cant; }
            set
            {
                if (value == _cant)
                    return;

                _cant = value;
                OnPropertyChanged();
            }
        }




        //RPT PRECIO VENTA X PRODUCTO
        private int _ID_PLATO;
        public int ID_PLATO
        {
            get { return _ID_PLATO; }
            set
            {
                if (value == _ID_PLATO)
                    return;

                _ID_PLATO = value;
                OnPropertyChanged();
            }
        }
        private decimal _RE_CANT_INS;
        public decimal RE_CANT_INS
        {
            get { return _RE_CANT_INS; }
            set
            {
                if (value == _RE_CANT_INS)
                    return;

                _RE_CANT_INS = value;
                OnPropertyChanged();
            }
        }
        private string _TM_DESC;
        public string TM_DESC
        {
            get { return _TM_DESC; }
            set
            {
                if (value == _TM_DESC)
                    return;

                _TM_DESC = value;
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

        private decimal _RE_PRE_UNIT;
        public decimal RE_PRE_UNIT
        {
            get { return _RE_PRE_UNIT; }
            set
            {
                if (value == _RE_PRE_UNIT)
                    return;

                _RE_PRE_UNIT = value;
                OnPropertyChanged();
            }
        }

        private decimal _RE_COSTO_RECETA;
        public decimal RE_COSTO_RECETA
        {
            get { return _RE_COSTO_RECETA; }
            set
            {
                if (value == _RE_COSTO_RECETA)
                    return;

                _RE_COSTO_RECETA = value;
                OnPropertyChanged();
            }
        }

        private decimal _COSTO_FIJO;
        public decimal COSTO_FIJO
        {
            get { return _COSTO_FIJO; }
            set
            {
                if (value == _COSTO_FIJO)
                    return;

                _COSTO_FIJO = value;
                OnPropertyChanged();
            }
        }

        private decimal _COSTO_VARIABLE;
        public decimal COSTO_VARIABLE
        {
            get { return _COSTO_VARIABLE; }
            set
            {
                if (value == _COSTO_VARIABLE)
                    return;

                _COSTO_VARIABLE = value;
                OnPropertyChanged();
            }
        }



        //OBJETOS
        private decimal _CostoFijoUnitario = 0;
        public decimal CostoFijoUnitario {
            get { return _CostoFijoUnitario; }
            set
            {
                if (value == _CostoFijoUnitario)
                    return;

                _CostoFijoUnitario = value;
                OnPropertyChanged();
            }
        }
        private decimal _CostoVariableUnitario = 0;
        public decimal CostoVariableUnitario 
        { 
            get{ return _CostoVariableUnitario; }
            set
            {
                if (value == _CostoVariableUnitario)
                    return;

                _CostoVariableUnitario = value;
                OnPropertyChanged();
            } 
        }
        private decimal _CostoTotalUnitario = 0;
        public decimal CostoTotalUnitario 
        { 
            get { return _CostoTotalUnitario; }
            set
            {
                if (value == _CostoTotalUnitario)
                    return;

                _CostoTotalUnitario = value;
                OnPropertyChanged();
            } 
        }
        private decimal _TotalPrecio = 0;
        public decimal TotalPrecio 
        { 
            get { return _TotalPrecio; }
            set
            {
                if (value == _TotalPrecio)
                    return;

                _TotalPrecio = value;
                OnPropertyChanged();
            } 
        }
        private decimal _TotalCostoFijo = 0;
        public decimal TotalCostoFijo 
        { 
            get { return _TotalCostoFijo; }
            set
            {
                if (value == _TotalCostoFijo)
                    return;

                _TotalCostoFijo = value;
                OnPropertyChanged();
            } 
        }
        private decimal _TotalCostoVariable = 0;
        public decimal TotalCostoVariable 
        { get { return _TotalCostoVariable; }
            set
            {
                if (value == _TotalCostoVariable)
                    return;

                _TotalCostoVariable = value;
                OnPropertyChanged();
            } 
        }
        private decimal _CantidadUnidadesProd = 0;
        public decimal CantidadUnidadesProd 
        { get { return _CantidadUnidadesProd; }
            set
            {
                if (value == _CantidadUnidadesProd)
                    return;

                _CantidadUnidadesProd = value;
                OnPropertyChanged();
            } 
        }

        private decimal _CostoUnitarioProduccion = 0;
        public decimal CostoUnitarioProduccion 
        { get { return _CostoUnitarioProduccion; }
            set
            {
                if (value == _CostoUnitarioProduccion)
                    return;

                _CostoUnitarioProduccion = value;
                OnPropertyChanged();
            } 
        }
        private decimal _CostoTotalVenta = 0;
        public decimal CostoTotalVenta 
        { get { return _CostoTotalVenta; }
            set
            {
                if (value == _CostoTotalVenta)
                    return;

                _CostoTotalVenta = value;
                OnPropertyChanged();
            } 
        }
        private decimal _Utilidad = 0;
        public decimal Utilidad 
        { get { return _Utilidad; }
            set
            {
                if (value == _Utilidad)
                    return;

                _Utilidad = value;
                OnPropertyChanged();
            } 
        }
        private decimal _IGV = 0;
        public decimal IGV 
        { get { return _IGV; }
            set
            {
                if (value == _IGV)
                    return;

                _IGV = value;
                OnPropertyChanged();
            } 
        }
        private decimal _RC = 0;
        public decimal RC 
        { get { return _RC; }
            set
            {
                if (value == _RC)
                    return;

                _RC = value;
                OnPropertyChanged();
            } 
        }
        private decimal _PrecioVentaSinIVA = 0;
        public decimal PrecioVentaSinIVA 
        { get { return _PrecioVentaSinIVA; }
            set
            {
                if (value == _PrecioVentaSinIVA)
                    return;

                _PrecioVentaSinIVA = value;
                OnPropertyChanged();
            } 
        }
        private decimal _PrecioVentaConIVA = 0;
        public decimal PrecioVentaConIVA
        { get { return _PrecioVentaConIVA; }
            set
            {
                if (value == _PrecioVentaConIVA)
                    return;

                _PrecioVentaConIVA = value;
                OnPropertyChanged();
            } 
        }

        //FECHAS
        private DateTime _Dia = DateTime.Now;
        public DateTime Dia
        {
            get { return _Dia; }
            set
            {
                if (value == _Dia)
                    return;

                _Dia = value;
                OnPropertyChanged();
            }
        }
        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set
            {
                if (value == _Mes)
                    return;

                _Mes = value;
                OnPropertyChanged();
            }
        }
        private int _Año;
        public int Año
        {
            get { return _Año; }
            set
            {
                if (value == _Año)
                    return;

                _Año = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Desde = DateTime.Now;
        public DateTime Desde
        {
            get { return _Desde; }
            set
            {
                if (value == _Desde)
                    return;

                _Desde = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Hasta = DateTime.Now;
        public DateTime Hasta
        {
            get { return _Hasta; }
            set
            {
                if (value == _Hasta)
                    return;

                _Hasta = value;
                OnPropertyChanged();
            }
        }



        private int _CC_ID_MES;
        public int CC_ID_MES
        {
            get { return _CC_ID_MES; }
            set
            {
                if (value == _CC_ID_MES)
                    return;

                _CC_ID_MES = value;
                OnPropertyChanged();
            }
        }
        private DateTime _CC_FEC;
        public DateTime CC_FEC
        {
            get { return _CC_FEC; }
            set
            {
                if (value == _CC_FEC)
                    return;

                _CC_FEC = value;
                OnPropertyChanged();
            }
        }

        private int _ID_TIPO;
        public int ID_TIPO
        {
            get { return _ID_TIPO; }
            set
            {
                if (value == _ID_TIPO)
                    return;

                _ID_TIPO = value;
                OnPropertyChanged();
            }
        }

    }
}
