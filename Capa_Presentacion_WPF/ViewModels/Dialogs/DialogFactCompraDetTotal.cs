using Capa_Entidades.Models.Factura_Compra;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogFactCompraDetTotal
    {
        public ObservableCollection<FactCompraDet> DataDetprod { get; set; }
        public DialogFactCompraDetTotal()
        {
            DataDetprod = (ObservableCollection<FactCompraDet>)Application.Current.Properties["detFactCompra"];
        }
    }
}
