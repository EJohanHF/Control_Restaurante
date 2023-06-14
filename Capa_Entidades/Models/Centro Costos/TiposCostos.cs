using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Centro_Costos
{
    public class TiposCostos : INotifyPropertyChanged
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
        public TiposCostos()
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
        private string _TP_CODIGO;
        public string TP_CODIGO
        {
            get { return _TP_CODIGO; }
            set
            {
                if (value == _TP_CODIGO)
                    return;

                _TP_CODIGO = value;
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
        
        private int _TP_TIPO;
        public int TP_TIPO
        {
            get { return _TP_TIPO; }
            set
            {
                if (value == _TP_TIPO)
                    return;

                _TP_TIPO = value;
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
        private DateTime _TP_F_CREATE;
        public DateTime TP_F_CREATE
        {
            get { return _TP_F_CREATE; }
            set
            {
                if (value == _TP_F_CREATE)
                    return;

                _TP_F_CREATE = value;
                OnPropertyChanged();
            }
        }
        private DateTime _TP_F_MODIFICACION;
        public DateTime TP_F_MODIFICACION
        {
            get { return _TP_F_MODIFICACION; }
            set
            {
                if (value == _TP_F_MODIFICACION)
                    return;

                _TP_F_MODIFICACION = value;
                OnPropertyChanged();
            }
        }
        private bool _TP_CLASE;
        public bool TP_CLASE
        {
            get { return _TP_CLASE; }
            set
            {
                if (value == _TP_CLASE)
                    return;

                _TP_CLASE = value;
                OnPropertyChanged();
            }
        }

        private String _TP_CLASE_NOM;
        public String TP_CLASE_NOM
        {
            get { return _TP_CLASE_NOM; }
            set
            {
                if (value == _TP_CLASE_NOM)
                    return;

                _TP_CLASE_NOM = value;
                OnPropertyChanged();
            }
        }
        private bool _TP_ACT;
        public bool TP_ACT
        {
            get { return _TP_ACT; }
            set
            {
                if (value == _TP_ACT)
                    return;

                _TP_ACT = value;
                OnPropertyChanged();
            }
        }
        private string _icon;
        public string icon
        {
            get { return _icon; }
            set
            {
                if (value == _icon)
                    return;

                _icon = value;
                OnPropertyChanged();
            }
        }
        private string _tooltipboton;
        public string tooltipboton
        {
            get { return _tooltipboton; }
            set
            {
                if (value == _tooltipboton)
                    return;

                _tooltipboton = value;
                OnPropertyChanged();
            }
        }


    }
}
