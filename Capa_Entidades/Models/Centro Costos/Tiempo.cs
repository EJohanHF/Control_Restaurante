using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Centro_Costos
{
    public class Tiempo
    {
        //#region INotifyPropertyChanged Impl
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
        public Tiempo()
        {

        }
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                //if (value == _ID)
                //    return;

                _ID = value;
                //OnPropertyChanged();
            }
        }
        private string _DESCRPCION;
        public string DESCRPCION
        {
            get { return _DESCRPCION; }
            set
            {
                //if (value == _DESCRPCION)
                //    return;

                _DESCRPCION = value;
                //OnPropertyChanged();
            }
        }
        private string _DESCRPCION_MES;
        public string DESCRPCION_MES
        {
            get { return _DESCRPCION_MES; }
            set
            {
                //if (value == _DESCRPCION_MES)
                //    return;

                _DESCRPCION_MES = value;
                //OnPropertyChanged();
            }
        }

    }
}
