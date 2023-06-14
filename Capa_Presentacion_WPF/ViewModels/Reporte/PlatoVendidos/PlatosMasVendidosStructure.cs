using Capa_Negocio.Pedido;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Capa_Entidades.Models.Pedido;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades.Models.Carta;
using Capa_Negocio;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.PlatoVendidos
{
    public class PlatosMasVendidosStructure
    {
        #region Negocio
        Neg_Pedido negPed = new Neg_Pedido();
        Neg_Combo negCombo = new Neg_Combo();
        #endregion

        #region Entidad
        public ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos> detpedidos = new ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos>();
        static List<Categoria> cat = new List<Categoria>();
        static List<Grupo> gru = new List<Grupo>();
        #endregion
        public PlatosMasVendidosStructure()
        {
            detpedidos = negPed.GET_DETALLEPEDIDO2();
            cat = negCombo.GetCategoria();
            gru = negCombo.GetComboGrupo();
        }
        public List<Categoria> GetCategorias()
        {
            return cat.ToList();
        }
        public List<Grupo> GetGrupoxCategoria(int idCat)
        {
            return gru.Where(w=> w.idcat == idCat.ToString()).ToList();
        }
        public List<Capa_Entidades.Models.Pedido.Pedidos> GetPlatosMasVendidos(int Idradiobuton,int idGrup,int idCat,DateTime Dia,DateTime Desde, DateTime Hasta)
        {
            List<Capa_Entidades.Models.Pedido.Pedidos> data = new List<Capa_Entidades.Models.Pedido.Pedidos>();
            data = negPed.get_platos_vendidos(Idradiobuton,idGrup, idCat,Dia,Desde,Hasta);
            return data;
        }

        public List<Capa_Entidades.Models.Pedido.Pedidos> GetPlatosMarinosCriollos(int op, DateTime Dia, DateTime Desde, DateTime Hasta)
        {
            List<Capa_Entidades.Models.Pedido.Pedidos> data = new List<Capa_Entidades.Models.Pedido.Pedidos>();
            data = negPed.getplatoscriollosmarinos(op, Dia, Desde, Hasta);
            return data;
        }
        public List<Capa_Entidades.Models.Pedido.Pedidos> GetPlatosMasVendidosTodos(int op,DateTime Dia,DateTime Desde, DateTime Hasta)
        {
            List<Capa_Entidades.Models.Pedido.Pedidos> data = new List<Capa_Entidades.Models.Pedido.Pedidos>();
            data = negPed.get_platos_vendidos_todos(op, Dia, Desde, Hasta);
            return data;
        }
        public List<Capa_Entidades.Models.Pedido.Pedidos> GetDetallePedidos()
        {
            return detpedidos.ToList();
        }
    }
}
