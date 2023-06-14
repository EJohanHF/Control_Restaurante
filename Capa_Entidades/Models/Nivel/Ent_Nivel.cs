using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Nivel
{
    public class Ent_Nivel: INotifyPropertyChanged
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
        public Ent_Nivel()
        {

        }
        #region niveles
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
        private string _N_NOM;
        public string N_NOM
        {
            get { return _N_NOM; }
            set
            {
                if (value == _N_NOM)
                    return;

                _N_NOM = value;
                OnPropertyChanged();
            }
        }
        private bool _N_ACT;
        public bool N_ACT
        {
            get { return _N_ACT; }
            set
            {
                if (value == _N_ACT)
                    return;

                _N_ACT = value;
                OnPropertyChanged();
            }
        }
        private DateTime _N_F_CREATE;
        public DateTime N_F_CREATE
        {
            get { return _N_F_CREATE; }
            set
            {
                if (value == _N_F_CREATE)
                    return;

                _N_F_CREATE = value;
                OnPropertyChanged();
            }
        }
        private DateTime _N_F_MODIFICACION;
        public DateTime N_F_MODIFICACION
        {
            get { return _N_F_MODIFICACION; }
            set
            {
                if (value == _N_F_MODIFICACION)
                    return;

                _N_F_MODIFICACION = value;
                OnPropertyChanged();
            }
        }
        private int _N_TIPO_SELEC;
        public int N_TIPO_SELEC
        {
            get { return _N_TIPO_SELEC; }
            set
            {
                if (value == _N_TIPO_SELEC)
                    return;

                _N_TIPO_SELEC = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SUB NIVELES
        private int _ID_SN;
        public int ID_SN
        {
            get { return _ID_SN; }
            set
            {
                if (value == _ID_SN)
                    return;

                _ID_SN = value;
                OnPropertyChanged();
            }
        }
        private int _SN_ID_NIVEL;
        public int SN_ID_NIVEL
        {
            get { return _SN_ID_NIVEL; }
            set
            {
                if (value == _SN_ID_NIVEL)
                    return;

                _SN_ID_NIVEL = value;
                OnPropertyChanged();
            }
        }
        private string _SN_NOM;
        public string SN_NOM
        {
            get { return _SN_NOM; }
            set
            {
                if (value == _SN_NOM)
                    return;

                _SN_NOM = value;
                OnPropertyChanged();
            }
        }

        private bool _SN_ACT;
        public bool SN_ACT
        {
            get { return _SN_ACT; }
            set
            {
                if (value == _SN_ACT)
                    return;

                _SN_ACT = value;
                OnPropertyChanged();
            }
        }
        private byte[] _SN_IMAGEN;
        public byte[] SN_IMAGEN
        {
            get { return _SN_IMAGEN; }
            set
            {
                if (value == _SN_IMAGEN)
                    return;

                _SN_IMAGEN = value;
                OnPropertyChanged();
            }
        }
        private DateTime _SN_F_CREATE;
        public DateTime SN_F_CREATE
        {
            get { return _SN_F_CREATE; }
            set
            {
                if (value == _SN_F_CREATE)
                    return;

                _SN_F_CREATE = value;
                OnPropertyChanged();
            }
        }
        private DateTime _SN_F_MODIFICACION;
        public DateTime SN_F_MODIFICACION
        {
            get { return _SN_F_MODIFICACION; }
            set
            {
                if (value == _SN_F_MODIFICACION)
                    return;

                _SN_F_MODIFICACION = value;
                OnPropertyChanged();
            }
        }

        private int _SN_ID_CARTA;
        public int SN_ID_CARTA
        {
            get { return _SN_ID_CARTA; }
            set
            {
                if (value == _SN_ID_CARTA)
                    return;

                _SN_ID_CARTA = value;
                OnPropertyChanged();
            }
        }
        private int _SN_ID_GRUPO;
        public int SN_ID_GRUPO
        {
            get { return _SN_ID_GRUPO; }
            set
            {
                if (value == _SN_ID_GRUPO)
                    return;

                _SN_ID_GRUPO = value;
                OnPropertyChanged();
            }
        }
        
        private string _CAR_NOM;
        public string CAR_NOM
        {
            get { return _CAR_NOM; }
            set
            {
                if (value == _CAR_NOM)
                    return;

                _CAR_NOM = value;
                OnPropertyChanged();
            }
        }
        
        private string _GR_NOM;
        public string GR_NOM
        {
            get { return _GR_NOM; }
            set
            {
                if (value == _GR_NOM)
                    return;

                _GR_NOM = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region TIPO SELECCION
        private int _ID_SS;
        public int ID_SS
        {
            get { return _ID_SS; }
            set
            {
                if (value == _ID_SS)
                    return;

                _ID_SS = value;
                OnPropertyChanged();
            }
        }
        private string _SS_DESCR;
        public string SS_DESCR
        {
            get { return _SS_DESCR; }
            set
            {
                if (value == _SS_DESCR)
                    return;

                _SS_DESCR = value;
                OnPropertyChanged();
            }
        }
        private bool _SS_ACT;
        public bool SS_ACT
        {
            get { return _SS_ACT; }
            set
            {
                if (value == _SS_ACT)
                    return;

                _SS_ACT = value;
                OnPropertyChanged();
            }
        }
        private DateTime _SS_F_CREATE;
        public DateTime SS_F_CREATE
        {
            get { return _SS_F_CREATE; }
            set
            {
                if (value == _SS_F_CREATE)
                    return;

                _SS_F_CREATE = value;
                OnPropertyChanged();
            }
        }
        private DateTime _SS_F_MODIFICACION;
        public DateTime SS_F_MODIFICACION
        {
            get { return _SS_F_MODIFICACION; }
            set
            {
                if (value == _SS_F_MODIFICACION)
                    return;

                _SS_F_MODIFICACION = value;
                OnPropertyChanged();
            }
        }
        private bool _selec_ischeck;
        public bool selec_ischeck
        {
            get { return _selec_ischeck; }
            set
            {
                if (value == _selec_ischeck)
                    return;

                _selec_ischeck = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        private int _cant;
        public int cant
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
        private Boolean _combo_imprimir;
        public Boolean combo_imprimir
        {
            get { return _combo_imprimir; }
            set
            {
                if (value == _combo_imprimir)
                    return;

                _combo_imprimir = value;
                OnPropertyChanged();
            }
        }
    }
}
