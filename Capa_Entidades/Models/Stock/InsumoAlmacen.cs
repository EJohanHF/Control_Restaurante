using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class InsumoAlmacen : INotifyPropertyChanged
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
        private object _id_almacen { get; set; }
        public object id_almacen
        {
            get { return _id_almacen; }
            set
            {
                if (value == _id_almacen)
                    return;

                _id_almacen = value;
                OnPropertyChanged();
            }
        }
        private object _id_almacensal { get; set; }
        public object id_almacensal
        {
            get { return _id_almacensal; }
            set
            {
                if (value == _id_almacensal)
                    return;

                _id_almacensal = value;
                OnPropertyChanged();
            }
        }
        private object _id_insumo { get; set; }
        public object id_insumo
        {
            get { return _id_insumo; }
            set
            {
                if (value == _id_insumo)
                    return;

                _id_insumo = value;
                OnPropertyChanged();
            }
        }
        private string _cantidad { get; set; }
        public string cantidad
        {
            get { return _cantidad; }
            set
            {
                if (value == _cantidad)
                    return;

                _cantidad = value;
                OnPropertyChanged();
            }
        }
    }
}
