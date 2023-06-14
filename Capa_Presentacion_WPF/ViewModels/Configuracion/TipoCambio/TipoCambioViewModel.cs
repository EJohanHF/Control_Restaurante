using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.TipoCambio
{
    public class TipoCambioViewModel : IGeneric
    {
        #region Negocio
        NegTipoCambio neg_tc = new NegTipoCambio();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Lista
        public ObservableCollection<Capa_Entidades.Models.Configuracion.TipoCambio> DataTipoCambio { get; set; }
        #endregion
        #region Entidad
        private Capa_Entidades.Models.Configuracion.TipoCambio _tipocambio;
        public Capa_Entidades.Models.Configuracion.TipoCambio TipoCambio
        {
            get => _tipocambio;
            set
            {
                _tipocambio = value;
            }
        }
        #endregion
        #region Commands
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion
        #region Objetos
        private string _operacion;
        public string Operacion
        {
            get => _operacion;
            set
            {
                if (value == "Lista")
                {
                    GetLista();
                }
                _operacion = value;
            }
        }
        #endregion
        public TipoCambioViewModel()
        {
            Operacion = "Lista";
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            TipoCambio = DataTipoCambio.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault();
        }
        private void Guardar()
        {
            if (TipoCambio.TC_CAMBIO == null || TipoCambio.TC_CAMBIO.ToString() == "")
            {
                TipoCambio.TC_CAMBIO = 0;
            }
            bool x = neg_tc.setTipoCambio(TipoCambio);
            if (x == false)
            {
                globales.Mensaje("SOS-FOOD Información", "Hubo un error al actualizar el tipo de cambio", 2);
            }
            else
            {
                Operacion = "Lista";
            }
        }
        private void Cancelar()
        {
            TipoCambio = new Capa_Entidades.Models.Configuracion.TipoCambio();
            this.Operacion = "Lista";
        }
        private void GetLista()
        {
            DataTipoCambio = neg_tc.GetTiposCambio();
        }
    }
}
