using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Capa_Presentacion_WPF
{
    public class IGeneric : INotifyPropertyChanged
    {
        #region Implementacion de INotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void RaisePropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        internal HttpWebRequest GetWebRequest(Uri uri)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
