using Capa_Entidades.Models.Centro_Costos;
using Capa_Entidades.Models.Factura_Compra;
using Capa_Negocio.Centro_Costos;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos
{
    public class CentroCostosViewModel : INotifyPropertyChanged
    {
        CentroCostosStructure ccstructure = new CentroCostosStructure();
        #region Negocio
        Neg_CentroCostos negcfijos = new Neg_CentroCostos();
        #endregion
        #region Objetos
        private string operacion;
        private CentroCostos _CostosFijos;
        private Tiempo _tiempo;
        private CentroCostos _MesSelected;
        private CentroCostos _AñoSelected;
        private bool _IsCheckedAño;
        private bool _IsEnabledComboAño;
        private Tiempo _MesesSelected;
        private Tiempo _AñosSelected;
        private TiposCostos _TipoCostoSelected;
        private TiposCostos _TiposCostos;
        private FactCompra _FactCompra;
        private Tiempo _Tiempo;
        public string FechaBuscar { get; set; }
        
        #endregion
        #region SeleccionObjetos
        public CentroCostos MesSelected
        {
            get => _MesSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.CC_MES = ((CentroCostos)value).CC_MES;
                    int n_mes = DateTime.ParseExact(CentroCostos.CC_MES, "MMMM", CultureInfo.CurrentCulture).Month;
                    CentroCostos.CC_ID_MES = n_mes;

                    nom_mes = CentroCostos.CC_MES;
                    num_mes = CentroCostos.CC_ID_MES;
                }
                _MesSelected = value;
            }
        }
        public CentroCostos AñoSelected
        {
            get => _AñoSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.CC_AÑO = ((CentroCostos)value).CC_AÑO;

                    List<CentroCostos> _dmeses = new List<CentroCostos>();
                    _dmeses = ccstructure.getDataMeses(((CentroCostos)value).CC_AÑO);
                    ObservableCollection<CentroCostos> dmeses = new ObservableCollection<CentroCostos>(_dmeses);
                    this.DataMeses = dmeses;
                    año = Convert.ToInt32(CentroCostos.CC_AÑO);
                    num_mes = 0;
                    nom_mes = null;
                }
                _AñoSelected = value;
            }
        }
        public Tiempo MesesSelected
        {
            get => _MesesSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.CC_MES = ((Tiempo)value).DESCRPCION_MES;
                    CentroCostos.CC_ID_MES = ((Tiempo)value).ID;
                    
                }
                _MesesSelected = value;
            }
        }
        public Tiempo AñosSelected
        {
            get => _AñosSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.CC_AÑO = ((Tiempo)value).DESCRPCION;
                    listarMesesAños(2, CentroCostos.CC_AÑO);
                }
                _AñosSelected = value;
            }
        }
        public TiposCostos TipoCostoSelected
        {
            get => _TipoCostoSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.CC_TIPO = ((TiposCostos)value).ID;
                }
                _TipoCostoSelected = value;
            }
        }

        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => operacion;
            set
            {
                operacion = value;
                if (operacion == "Lista") { getLista(); }
            }
        }
        public bool IsCheckedAño
        {
            get => _IsCheckedAño;
            set
            {
                _IsCheckedAño = value;
            }
        }
        public bool IsEnabledComboAño
        {
            get => _IsEnabledComboAño;
            set
            {
                _IsEnabledComboAño = value;
            }
        }
        #endregion
        #region OBJETOS PARA LOS CUADROS
        public decimal MP { get; set; }
        public decimal MO { get; set; }
        public decimal GIF { get; set; }
        public decimal MPI { get; set; }
        public decimal MOI { get; set; }
        public decimal OTROGIFREL { get; set; }
        public decimal SEGURO { get; set; }
        public decimal ENERGIA { get; set; }
        public decimal DEPÑREE { get; set; }
        public decimal DEPREL { get; set; }


        public decimal SueldoAdm { get; set; }
        public decimal SERVAdm { get; set; }
        public decimal DEPRECOMAdm { get; set; }
        public decimal DEPRELAdm { get; set; }
        public decimal TOTAL_GADM { get; set; }


        public decimal SueldoVen { get; set; }
        public decimal PUBVen { get; set; }
        public decimal SERVVen { get; set; }
        public decimal DEPRELVen { get; set; }
        public decimal TOTAL_GVEN { get; set; }


        public decimal CostoProduccion { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal EstVentas { get; set; }
        public decimal EstCostoVenta { get; set; }
        public decimal EstUtilidadBruta { get; set; }
        public decimal EstGastoAdministracion { get; set; }
        public decimal EstGastoVenta { get; set; }
        public decimal EstUtilidadOperacional { get; set; }

        public decimal CantidadUnidadesProducidas { get; set; }
        public decimal EstDeduccionesTributarias { get; set; }



        #endregion
        #region Listas
        public ObservableCollection<CentroCostos> DataCostosFijos { get; set; }
        public ObservableCollection<CentroCostos> DataCostosVariables { get; set; }
        public ObservableCollection<CentroCostos> DataMeses { get; set; }
        public ObservableCollection<Tiempo> Meses { get; set; }
        public ObservableCollection<CentroCostos> DataAños { get; set; }
        public ObservableCollection<TiposCostos> DataTiposCostosFijos { get; set; }
        public ObservableCollection<TiposCostos> DataTiposCostosVariables { get; set; }
        public ObservableCollection<CentroCostos> DataReporteTiposCostos { get; set; }
        public ObservableCollection<Tiempo> ComboMes { get; set; }
        public ObservableCollection<Tiempo> ComboAños { get; set; }
        public CentroCostos CentroCostos
        {
            get => _CostosFijos;
            set
            {
                _CostosFijos = value;
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
        public FactCompra FactCompra
        {
            get => _FactCompra;
            set
            {
                _FactCompra = value;
            }
        }
        public Tiempo Tiempo
        {
            get => _Tiempo;
            set
            {
                _Tiempo = value;
            }
        }
        #endregion
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _startDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_date;
        private DateTime _FinishDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_finishdate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged_date("StartDate"); }
        }
        public void OnPropertyChanged_date(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_date;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        public DateTime FinishDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged_finishdate("FinishDate"); }
        }
        public void OnPropertyChanged_finishdate(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_finishdate;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        #region Commands
        public ICommand BuscarCommand { get; set; }
        public ICommand ComboAñoCheckedCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCVariableCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand BuscarPorFechaReportGeneralCommand { get; set; }
        #endregion
        public CentroCostosViewModel()
        {
            this.Operacion = "Lista";
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.ComboAñoCheckedCommand = new RelayCommand(new Action(ComboAñoChecked));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(GuardarCentroCostos));
            this.EditarCommand = new ParamCommand(new Action<object>(EditarCentroCostos));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.EditarCVariableCommand = new ParamCommand(new Action<object>(EditarCVariable));
            this.BuscarPorFechaReportGeneralCommand = new RelayCommand(new Action(BuscarPorFechaReportGeneral));
            this.CentroCostos = new CentroCostos();
            this.FactCompra = new FactCompra();
            this.TiposCostos = new TiposCostos();
            this.ComboMes = new ObservableCollection<Tiempo>();
            ListarAños();
        }
        public int num_mes { get; set; }
        public int año { get; set; }
        public string nom_mes { get; set; }
       
        private void BuscarPorFechaReportGeneral()
        {
            if (num_mes == 0 || año == 0 || nom_mes == null || nom_mes == "") { return; }
            GetConsolidadoCostoProduccion(num_mes, nom_mes, año);
            decimal h = 1236.0855813953488372093023256M;
            h = Math.Truncate(h * 100) / 100;

            //FechaBuscar = nom_mes+" / " + año;
        }
        public void GetConsolidadoCostoProduccion(int num_mes,string nom_mes, int año)
        {
            FechaBuscar = nom_mes + " / " + año;
            this.CentroCostos = new CentroCostos();

            MP = ccstructure.GetMP(num_mes,año);
            MO = ccstructure.GetMO(nom_mes, año);
            MPI = ccstructure.GetMPI(nom_mes,año);
            MOI = ccstructure.GetMOI(nom_mes,año);
            SEGURO = ccstructure.GetSEGURO(nom_mes,año);
            ENERGIA = ccstructure.GetENERGIA(nom_mes,año);
            DEPÑREE = ccstructure.GetDEPÑREE(nom_mes,año);
            DEPREL = ccstructure.GetDEPREL(nom_mes,año);
            OTROGIFREL = SEGURO + ENERGIA + DEPÑREE + DEPREL;
            GIF = MPI + MOI + OTROGIFREL;


            SueldoAdm = ccstructure.GetSueldoAdm(nom_mes, año);
            SERVAdm = 0;
            DEPRECOMAdm = 0;
            DEPRELAdm = 0;
            TOTAL_GADM = SueldoAdm + SERVAdm + DEPRECOMAdm + DEPRELAdm;

            SueldoVen = ccstructure.GetSueldoComi(nom_mes,año);
            PUBVen = ccstructure.GetPUBVen(nom_mes, año);
            SERVVen = 0;
            DEPRELVen = 0;
            TOTAL_GVEN = SueldoVen + PUBVen + SERVVen + DEPRELVen;


            CostoProduccion = MP + MO + GIF;
                decimal c = ccstructure.GetCantidad(num_mes, año);
                CantidadUnidadesProducidas = c;

            CostoUnitario = Math.Round((CostoProduccion==0)?0:(c==0)?0: CostoProduccion / c, 2, MidpointRounding.ToEven);
                decimal h = Convert.ToDecimal(CostoUnitario) * Convert.ToDecimal(0.8);
                decimal i = Math.Round(h + CostoUnitario, 2, MidpointRounding.ToEven) ;
            ValorVenta = Math.Round(i, 2, MidpointRounding.ToEven);


            EstVentas = ValorVenta * c;
            EstCostoVenta = (c * CostoUnitario);
            EstUtilidadBruta = EstVentas + EstCostoVenta;
            EstGastoAdministracion = TOTAL_GADM;
            EstGastoVenta = TOTAL_GVEN;
            EstDeduccionesTributarias = 0;
            EstUtilidadOperacional = EstUtilidadBruta - EstGastoAdministracion - EstGastoVenta;
    }
        public void Cancelar() 
        {
            this.CentroCostos = new CentroCostos();
            this.Operacion = "Lista";
        }
        public void EditarCVariable(object ID) 
        {
            this.Operacion = "Editar";
            int _id = Convert.ToInt32(ID);
            this.CentroCostos = this.DataCostosVariables.Where(w => w.ID == _id).FirstOrDefault();
            TiposCostos.ID = CentroCostos.CC_TIPO;
        }
        public void Eliminar(object ID) 
        {
            int _id = Convert.ToInt32(ID);
            string _mensaje = "";
            CentroCostos.ID = _id;
            bool res = negcfijos.SetCentroCostos(3, CentroCostos, ref _mensaje);
            if (res)
            {
                this.Operacion = "Lista";
                List<CentroCostos> _cf = new List<CentroCostos>();
                _cf = ccstructure.GetDataCostosFijos(1);
                ObservableCollection<CentroCostos> cf = new ObservableCollection<CentroCostos>(_cf);
                this.DataCostosFijos = cf;

                List<CentroCostos> _cv = new List<CentroCostos>();
                _cv = ccstructure.GetDataCostosVariables(1);
                ObservableCollection<CentroCostos> cv = new ObservableCollection<CentroCostos>(_cv);
                this.DataCostosVariables = cv;
            }
            else
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "No se pudo eliminar el registro" }
                };
                DialogHost.Show(view, "RootDialog");
            }
        }
        public void EditarCentroCostos(object ID) 
        {
            this.Operacion = "Editar";
            int _id = Convert.ToInt32(ID);
            this.CentroCostos = this.DataCostosFijos.Where(w => w.ID == _id).FirstOrDefault();
            TiposCostos.ID = CentroCostos.CC_TIPO;
        }
        public void GuardarCentroCostos() 
        {
            if (CentroCostos.CC_MES == null || CentroCostos.CC_AÑO == null || CentroCostos.CC_MONTO == 0 || CentroCostos.CC_TIPO == 0) 
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Es necesario completar todos los campos" }
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }

            
            string da = "01";
            int mon = CentroCostos.CC_ID_MES;
            string ye = CentroCostos.CC_AÑO;
            string fecha = da + "-" + mon.ToString() + "-" + ye;
            CentroCostos.CC_FEC = Convert.ToDateTime(fecha);

            var i = this.DataCostosFijos.Where(w => w.CC_MES == CentroCostos.CC_MES && w.CC_AÑO == CentroCostos.CC_AÑO && w.CC_TIPO == CentroCostos.CC_TIPO).Count();
            
            if (i > 0) 
            {
                string tp = this.DataCostosFijos.Where(w => w.CC_TIPO == CentroCostos.CC_TIPO).First().TP_DENOMINACION;
                var view = new MessageDialog
                {
                    Mensaje = { Text = "El registro: \n" + tp.ToString() + " \n ya existe la fecha ingresada" }
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }

            string _mensaje = "";
            int _id = CentroCostos.ID;

            bool res = negcfijos.SetCentroCostos((_id == 0 ? 1 : 2), CentroCostos, ref _mensaje);
            if (res)
            {
                this.Operacion = "Lista";
                List<CentroCostos> _cf = new List<CentroCostos>();
                _cf = ccstructure.GetDataCostosFijos(1);
                ObservableCollection<CentroCostos> cf = new ObservableCollection<CentroCostos>(_cf);
                this.DataCostosFijos = cf;

                List<CentroCostos> _cv = new List<CentroCostos>();
                _cv = ccstructure.GetDataCostosVariables(1);
                ObservableCollection<CentroCostos> cv = new ObservableCollection<CentroCostos>(_cv);
                this.DataCostosVariables = cv;
            }
            else 
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "No se pudo registrar el costo"}
                };
                DialogHost.Show(view, "RootDialog");
            }
        }
        public void Nuevo()
        {
            this.CentroCostos = new CentroCostos();
            this.Tiempo = new Tiempo();
            this.Operacion = "Nuevo";
        }
        public void listarMesesAños(int op,string año) 
        {
            int i = DateTime.Now.Month; //mes actual
            int y = DateTime.Now.Year; //año actual

            this.ComboMes = new ObservableCollection<Tiempo>(); 
            int c = 0;
            string[] meses = DateTimeFormatInfo.CurrentInfo.MonthNames;
            
            foreach (var m in meses)
            {
                c = c + 1;
                Tiempo tp = new Tiempo();
                tp.ID = c;
                tp.DESCRPCION_MES = m.ToUpper();
                int yanterior = y - 1;
                if (tp.DESCRPCION_MES != "" && tp.ID <= i && tp.ID >= i - 1 && año == y.ToString())
                {
                    this.ComboMes.Add(tp);
                }
                if (año == yanterior.ToString() && tp.ID == 12)
                {
                    this.ComboMes.Add(tp);
                }
            }
        }
        public void ListarAños() 
        {
            //Listar Años
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            
            this.ComboAños = new ObservableCollection<Tiempo>();
            int cantRows = 0; //cantidad de filas que tendra el array de años
            int ca = 0; // id que tendra cada año
            if (m == 1) { cantRows = 2; } else { cantRows = 1; }
            int[] años = new int[cantRows];
            if (cantRows == 2) { años[0] = y; años[1] = y - 1; } else { años[0] = y; }
            foreach (var a in años)
            {
                ca = ca + 1;
                Tiempo tpa = new Tiempo();
                tpa.ID = ca;
                tpa.DESCRPCION = a.ToString();
                if (tpa.DESCRPCION != "")
                {
                    this.ComboAños.Add(tpa);
                }
            }
        }
        public void ComboAñoChecked() 
        {
            if (IsCheckedAño)
            {
                IsEnabledComboAño = true;
            }
            else 
            { 
                IsEnabledComboAño = false;
                List<CentroCostos> _cf = new List<CentroCostos>();
                _cf = ccstructure.GetDataCostosFijos(0);
                ObservableCollection<CentroCostos> cf = new ObservableCollection<CentroCostos>(_cf);
                this.DataCostosFijos = cf;

                List<CentroCostos> _cv = new List<CentroCostos>();
                _cv = ccstructure.GetDataCostosVariables(0);
                ObservableCollection<CentroCostos> cv = new ObservableCollection<CentroCostos>(_cv);
                this.DataCostosVariables = cv;  
            }
        }
        public void Buscar()
        {
            if (IsEnabledComboAño == false) { CentroCostos.CC_AÑO = "0"; }
            List<CentroCostos> _cf = new List<CentroCostos>();
            _cf = ccstructure.GetDataCostosFijosxFecha(CentroCostos.CC_MES, CentroCostos.CC_AÑO);
            ObservableCollection<CentroCostos> cf = new ObservableCollection<CentroCostos>(_cf);
            this.DataCostosFijos = cf;
        }
        public void ListarMesesyAñosExistentes()
        {
            List<CentroCostos> _dmeses = new List<CentroCostos>();
            int y = DateTime.Now.Year;
            _dmeses = ccstructure.getDataMeses(y.ToString());
            ObservableCollection<CentroCostos> dmeses = new ObservableCollection<CentroCostos>(_dmeses);
            this.DataMeses = dmeses;

            List<CentroCostos> _danos = new List<CentroCostos>();
            _danos = ccstructure.getDataAños();
            ObservableCollection<CentroCostos> danos = new ObservableCollection<CentroCostos>(_danos);
            this.DataAños = danos;
        }

        public void ListarTiposCostos()
        {
            List<TiposCostos> _cf = new List<TiposCostos>();
            _cf = ccstructure.getDataTiposCostosFijos();
            ObservableCollection<TiposCostos> cf = new ObservableCollection<TiposCostos>(_cf);
            this.DataTiposCostosFijos = cf;
            
            List<TiposCostos> _cv = new List<TiposCostos>();
            _cv = ccstructure.getDataTiposCostosVariables();
            ObservableCollection<TiposCostos> cv = new ObservableCollection<TiposCostos>(_cv);
            this.DataTiposCostosVariables = cv;
        }
        public void getLista()
        {
            this.IsEnabledComboAño = false;
            List<CentroCostos> _cf = new List<CentroCostos>();
            _cf = ccstructure.GetDataCostosFijos(0);
            ObservableCollection<CentroCostos> cf = new ObservableCollection<CentroCostos>(_cf);
            this.DataCostosFijos = cf;

            this.IsEnabledComboAño = false;
            List<CentroCostos> _cv = new List<CentroCostos>();
            _cv = ccstructure.GetDataCostosVariables(0);
            ObservableCollection<CentroCostos> cv = new ObservableCollection<CentroCostos>(_cv);
            this.DataCostosVariables = cv;

            List<CentroCostos> _rpt = new List<CentroCostos>();
            _rpt = ccstructure.GetDataReporteTiposCostos();
            ObservableCollection<CentroCostos> rpt = new ObservableCollection<CentroCostos>(_rpt);
            this.DataReporteTiposCostos = rpt;

            ListarMesesyAñosExistentes();
            ListarTiposCostos();
            int year = DateTime.Now.Year;
            //year = year - 1;
            //listarMesesAños(1, year.ToString());
            string[] meses = DateTimeFormatInfo.CurrentInfo.MonthNames;
            GetConsolidadoCostoProduccion(DateTime.Now.Month, meses[DateTime.Now.Month - 1], DateTime.Now.Year);
        }

    }
}
