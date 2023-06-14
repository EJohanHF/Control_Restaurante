using Capa_Entidades.Models.Centro_Costos;
using Capa_Negocio.Centro_Costos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos
{
    public class TipoCostosViewModel : IGeneric
    {
        CentroCostosStructure ccstructure = new CentroCostosStructure();
        #region Negocio
        Neg_CentroCostos negcc = new Neg_CentroCostos();
        #endregion
        #region Objetos
        private string _operacion;
        private CentroCostos _MesSelected;
        private CentroCostos _AñoSelected;
        private CentroCostos _CentroCostos;
        private TiposCostos _TiposCostos;
        private TiposCostos _TipoSelected;
        private TiposCostos _ClaseSelected;
        public int op { get; set; } = 0;

        public int num_mes { get; set; }
        public int año { get; set; }
        public string nom_mes { get; set; }

        #endregion
        #region GetSetObjteos
        public string Operacion
        {
            get => _operacion;
            set
            {
                if (value == "Lista")
                {
                    getLista();
                }
                _operacion = value;
            }
        }
        #endregion
        #region Entidad
        public CentroCostos CentroCostos
        {
            get => _CentroCostos;
            set 
            {
                _CentroCostos = value;
            }
        }
        public TiposCostos TiposCostos
        {
            get => _TiposCostos;
            set 
            {
                _TiposCostos = value;
            }
        }
        #endregion
        #region SeleccionObjetos
        public TiposCostos TipoSelected
        {
            get => _TipoSelected;
            set
            {
                if (value != null)
                {
                    this.TiposCostos.TP_TIPO = ((TiposCostos)value).TP_TIPO;
                }
                _TipoSelected = value;
            }
        }
        public TiposCostos ClaseSelected
        {
            get => _ClaseSelected;
            set
            {
                if (value != null)
                {
                    this.TiposCostos.TP_CLASE = ((TiposCostos)value).TP_CLASE;
                }
                _ClaseSelected = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<TiposCostos> DataTiposCostos { get; set; }
        public ObservableCollection<TiposCostos> DataCategoriaTipoCosto { get; set; }
        public ObservableCollection<TiposCostos> DataClaseTipoCosto { get; set; }
        #endregion
        #region Commands
        public ICommand NuevoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        #endregion

        public TipoCostosViewModel()
        {
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.CentroCostos = new CentroCostos();
            this.TiposCostos = new TiposCostos();
            this.Operacion = "Lista";
        }
        private void Eliminar(object _id)
        {
            int id = Convert.ToInt32(_id);
            TiposCostos.ID = id;
            string _mensaje = "";
            bool res = negcc.setTiposCostos(3, TiposCostos, ref _mensaje);
            getLista();
            this.op = 1;
        }
        private void Guardar()
        {
            int id = TiposCostos.ID;
            string _mensaje = "";
            bool res = negcc.setTiposCostos((id == 0 ? 1 : 2), TiposCostos, ref _mensaje);
            getLista();
            this.op = 1;
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
            this.TiposCostos = new TiposCostos();
        }
        private void Editar(object ID) 
        {
            int id = Convert.ToInt32(ID);
            this.Operacion = "Editar";
            this.TiposCostos = DataTiposCostos.Where(w => w.ID == id).First();
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
        }
        private void getLista()
        {
            this.TiposCostos = new TiposCostos();
            //ListarMesesyAñosExistentes();
            List<TiposCostos> _tc = new List<TiposCostos>();
            _tc = ccstructure.GetDataTiposCostos(op);
            ObservableCollection<TiposCostos> tc = new ObservableCollection<TiposCostos>(_tc);
            this.DataTiposCostos = tc;

            List<TiposCostos> _ctc = new List<TiposCostos>();
            _ctc = ccstructure.GetCategoriaTiposCostos();
            ObservableCollection<TiposCostos> ctc = new ObservableCollection<TiposCostos>(_ctc);
            this.DataCategoriaTipoCosto = ctc;

            List<TiposCostos> _cctc = new List<TiposCostos>();
            _cctc = ccstructure.GetDataClaseTipoCosto();
            ObservableCollection<TiposCostos> cctc = new ObservableCollection<TiposCostos>(_cctc);
            this.DataClaseTipoCosto = cctc;
        }
    }
}
