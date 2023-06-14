using Capa_Entidades.Models.Pedido;
using Capa_Negocio.Pedido;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.MisPedidos
{
    public class RptMisPedidosViewModel : IGeneric
    {
        Neg_Pedido negPedido = new Neg_Pedido();
        public ObservableCollection<Pedidos> DataPedidosEntregados { get; set; }
        public ICommand PendientePlatoCommand { get; set; }
        public RptMisPedidosViewModel() {
            getLista();
            this.PendientePlatoCommand = new ParamCommand(new Action<object>(PendientePlato));
        }
        private void PendientePlato(object _id) {
            int id = Convert.ToInt32(_id);
            bool res = negPedido.setPedidoPendiente(id);
            if (res == true) {
                getLista();
            }
        }
        private void getLista() {
            string NombreImpresora = ConfigurationManager.AppSettings["NombreImpresoraPedido"].ToString();
            DataPedidosEntregados = negPedido.getPedidosEntregados(NombreImpresora);
        }
    }
}
