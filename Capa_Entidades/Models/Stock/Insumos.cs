using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class Insumos
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
        public Insumos()
        { }
        //public int idins { get; set; }
        private int _idins = 0;
        public int idins
        {
            get { return _idins; }
            set
            {
                if (value == _idins)
                    return;

                _idins = value;
                OnPropertyChanged();
            }
        }
        //public string nomins { get; set; }
        private string _nomins;
        public string nomins
        {
            get { return _nomins; }
            set
            {
                if (value == _nomins)
                    return;

                _nomins = value;
                OnPropertyChanged();
            }
        }

        //public string idcat { get; set; }
        private string _idcat;
        public string idcat
        {
            get { return _idcat; }
            set
            {
                if (value == _idcat)
                    return;

                _idcat = value;
                OnPropertyChanged();
            }
        }
        //public string nomcat { get; set; }
        private string _nomcat;
        public string nomcat
        {
            get { return _nomcat; }
            set
            {
                if (value == _nomcat)
                    return;

                _nomcat = value;
                OnPropertyChanged();
            }
        }

        //public string idaum { get; set; }
        private string _idaum;
        public string idaum
        {
            get { return _idaum; }
            set
            {
                if (value == _idaum)
                    return;

                _idaum = value;
                OnPropertyChanged();
            }
        }
        //public string nomaum{ get; set; }
        private string _nomaum;
        public string nomaum
        {
            get { return _nomaum; }
            set
            {
                if (value == _nomaum)
                    return;

                _nomaum = value;
                OnPropertyChanged();
            }
        }

        //public decimal cantminins { get; set; }
        private object _cantminins = 0;
        public object cantminins
        {
            get { return _cantminins; }
            set
            {
                if (value == _cantminins)
                    return;

                _cantminins = value;
                OnPropertyChanged();
            }
        }
        //public string condins { get; set; }
        private string _condins;
        public string condins
        {
            get { return _condins; }
            set
            {
                if (value == _condins)
                    return;

                _condins = value;
                OnPropertyChanged();
            }
        }
        //public byte estadoins { get; set; } = 1;
        private byte _estadoins = 1;
        public byte estadoins
        {
            get { return _estadoins; }
            set
            {
                if (value == _estadoins)
                    return;

                _estadoins = value;
                OnPropertyChanged();
            }
        }
        //public string tipoins { get; set; }
        private string _tipoins;
        public string tipoins
        {
            get { return _tipoins; }
            set
            {
                if (value == _tipoins)
                    return;

                _tipoins = value;
                OnPropertyChanged();
            }
        }
        private string _nomtipo;
        public string nomtipo
        {
            get { return _nomtipo; }
            set
            {
                if (value == _nomtipo)
                    return;

                _nomtipo = value;
                OnPropertyChanged();
            }
        }
        //public decimal INS_PRECIO { get; set; }
        private decimal _INS_PRECIO;
        public decimal INS_PRECIO
        {
            get { return _INS_PRECIO; }
            set
            {
                if (value == _INS_PRECIO)
                    return;

                _INS_PRECIO = value;
                OnPropertyChanged();
            }
        }
        //public decimal INS_CANTIDAD { get; set; }
        private decimal _INS_CANTIDAD;
        public decimal INS_CANTIDAD
        {
            get { return _INS_CANTIDAD; }
            set
            {
                if (value == _INS_CANTIDAD)
                    return;

                _INS_CANTIDAD = value;
                OnPropertyChanged();
            }
        }
        private bool _SUBRECETA;
        public bool SUBRECETA
        {
            get { return _SUBRECETA; }
            set
            {
                if (value == _SUBRECETA)
                    return;

                _SUBRECETA = value;
                OnPropertyChanged();
            }
        }
        private object _precio = 0;
        public object precio
        {
            get { return _precio; }
            set
            {
                if (value == _precio)
                    return;

                _precio = value;
                OnPropertyChanged();
            }
        }
        private string _almacen;
        public string almacen
        {
            get { return _almacen; }
            set
            {
                if (value == _almacen)
                    return;

                _almacen = value;
                OnPropertyChanged();
            }
        }
        private string _sren;
        public string sren
        {
            get { return _sren; }
            set
            {
                if (value == _sren)
                    return;

                _sren = value;
                OnPropertyChanged();
            }
        }
       //

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

        private int _ID_ALMA;
        public int ID_ALMA
        {
            get { return _ID_ALMA; }
            set
            {
                if (value == _ID_ALMA)
                    return;

                _ID_ALMA = value;
                OnPropertyChanged();
            }
        }

        private DateTime _F_CREATE;
        public DateTime F_CREATE
        {
            get { return _F_CREATE; }
            set
            {
                if (value == _F_CREATE)
                    return;

                _F_CREATE = value;
                OnPropertyChanged();
            }
        }

        private DateTime _F_MODIFICACION;
        public DateTime F_MODIFICACION
        {
            get { return _F_MODIFICACION; }
            set
            {
                if (value == _F_MODIFICACION)
                    return;

                _F_MODIFICACION = value;
                OnPropertyChanged();
            }
        }


        //Detalle insumos almacen

        private decimal _DIA_CANT;
        public decimal DIA_CANT
        {
            get { return _DIA_CANT; }
            set
            {
                if (value == _DIA_CANT)
                    return;

                _DIA_CANT = value;
                OnPropertyChanged();
            }
        }
    }
}
