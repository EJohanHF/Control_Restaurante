using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
    public class Cliente : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public Cliente()
        {

        }
        public int contador { get; set; }
        //public int idcli { get; set; }
        private int _idcli;
        public int idcli
        {
            get { return _idcli; }
            set
            {
                if (value == _idcli)
                    return;
                _idcli = value;
                OnPropertyChanged();
            }
        }

        //public string denominacion { get; set; }
        private string _denominacion;
        public string denominacion
        {
            get { return _denominacion; }
            set
            {
                if (value == _denominacion)
                    return;

                _denominacion = value;
                OnPropertyChanged();

            }
        }

        //public string denominacion2 { get; set; }
        private string _denominacion2;
        public string denominacion2
        {
            get { return _denominacion2; }
            set
            {
                if (value == _denominacion2)
                    return;

                _denominacion2 = value;
                OnPropertyChanged();

            }
        }

        //public string ndoc { get; set; }
        private string _ndoc;
        public string ndoc
        {
            get { return _ndoc; }
            set
            {
                if (value == _ndoc)
                    return;

                _ndoc = value;
                OnPropertyChanged();

            }
        }

        //public int idtd { get; set; }
        private int _idtd;
        public int idtd
        {
            get { return _idtd; }
            set
            {
                if (value == _idtd)
                    return;

                _idtd = value;
                OnPropertyChanged();

            }
        }

        //public string tipodoc { get; set; }
        private string _tipodoc;
        public string tipodoc
        {
            get { return _tipodoc; }
            set
            {
                if (value == _tipodoc)
                    return;

                _tipodoc = value;
                OnPropertyChanged();

            }
        }

        //public string dircli { get; set; }
        private string _dircli;
        public string dircli
        {
            get { return _dircli; }
            set
            {
                if (value == _dircli)
                    return;

                _dircli = value;
                OnPropertyChanged();

            }
        }

        //public string distritocli { get; set; }
        private string _distritocli;
        public string distritocli
        {
            get { return _distritocli; }
            set
            {
                if (value == _distritocli)
                    return;

                _distritocli = value;
                OnPropertyChanged();

            }
        }

        //public string callecli { get; set; }
        private string _callecli;
        public string callecli
        {
            get { return _callecli; }
            set
            {
                if (value == _callecli)
                    return;

                _callecli = value;
                OnPropertyChanged();

            }
        }
        //public string referenciacli { get; set; }
        private string _referenciacli;
        public string referenciacli
        {
            get { return _referenciacli; }
            set
            {
                if (value == _referenciacli)
                    return;

                _referenciacli = value;
                OnPropertyChanged();
            }
        }
        //public string telcli { get; set; }
        private string _telcli;
        public string telcli
        {
            get { return _telcli; }
            set
            {
                if (value == _telcli)
                    return;

                _telcli = value;
                OnPropertyChanged();

            }
        }
        //public string corcli { get; set; }
        private string _corcli;
        public string corcli
        {
            get { return _corcli; }
            set
            {
                if (value == _corcli)
                    return;

                _corcli = value;
                OnPropertyChanged();
            }
        }
        //public byte estadocli { get; set; } = 1;
        private byte _estadocli;
        public byte estadocli
        {
            get { return _estadocli; }
            set
            {
                if (value == _estadocli)
                    return;

                _estadocli = value;
                OnPropertyChanged();
            }
        }
    }
}
