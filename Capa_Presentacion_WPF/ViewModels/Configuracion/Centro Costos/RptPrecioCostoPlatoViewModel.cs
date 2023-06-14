using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Centro_Costos;
using Capa_Entidades.Models.Pedido;
using Capa_Negocio;
using Capa_Negocio.Centro_Costos;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos
{
    public class RptPrecioCostoPlatoViewModel : INotifyPropertyChanged
    {
        CentroCostosStructure ccstructure = new CentroCostosStructure();

        #region Negocio
        Neg_CentroCostos negcfijos = new Neg_CentroCostos();
        Neg_Combo negCombo = new Neg_Combo();
        #endregion
        #region Entidad
        private SCategoria _SCatSelected;
        private Categoria _CatSelected;
        private Grupo _GrupSelected;
        private Platos _PlatoSelected;
        #endregion
        #region Objetos
        private string operacion;
        private CentroCostos _CentroCostos;
        public string rbbtdia { get; set; }
        public string rbbtmes { get; set; }
        public string rbbtdesdehasta { get; set; }
        public int Idradiobuton { get; set; }
        public string MensajeError { get; set; }
        public Tiempo _AñoSelected;
        public Tiempo _MesSelected;
        
        
        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => operacion;
            set 
            {
                if (value == "Lista")
                {
                    getLista();
                }
                operacion = value;
            }
        }
        public CentroCostos CentroCostos
        {
            get => _CentroCostos;
            set 
            {
                _CentroCostos = value;
            }
        }
        #endregion
        #region SeleccionObjetos
        public SCategoria SCatSelected
        {
            get => _SCatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboCat = new List<Categoria>();
                    ComboCat = ccstructure.GetCategoria(((SCategoria)value).idscat);
                    this.ComboGrupo = new List<Grupo>();
                    this.ComboPlato = new List<Platos>();
                }
                _SCatSelected = value;
            }
        }
        public Categoria CatSelected
        {
            get => _CatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboGrupo = new List<Grupo>();
                    ComboGrupo = negCombo.GetComboGrupoxC(((Categoria)value).idcat);
                    this.ComboPlato = new List<Platos>();
                }
                _CatSelected = value;
            }
        }
        public Grupo GrupSelected
        {
            get => _GrupSelected;
            set
            {
                if (value != null)
                {
                    this.ComboPlato = new List<Platos>();
                    ComboPlato = negCombo.GetComboPlato(((Grupo)value).idgrup);
                }
                _GrupSelected = value;
            }
        }

        public Platos PlatoSelected
        {
            get => _PlatoSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.ID_PLATO = ((Platos)value).idplato;
                    //Data(CentroCostos.ID_PLATO, 0);
                }
                _PlatoSelected = value;
            }
        }
        public Tiempo AñoSelected
        {
            get => _AñoSelected;
            set
            {
                if (value != null)
                {
                    ListarMeses(Convert.ToInt32(((Tiempo)value).DESCRPCION));
                    CentroCostos.Año = Convert.ToInt32(((Tiempo)value).DESCRPCION);
                }
                _AñoSelected = value;
            }
        }
        public Tiempo MesSelected
        {
            get => _MesSelected;
            set
            {
                if (value != null)
                {
                    CentroCostos.Mes = Convert.ToInt32(((Tiempo)value).ID);
                }
                _MesSelected = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<CentroCostos> DataDetallePlato { get; set; }
        public List<SCategoria> ComboSuperCat { get; set; }
        public List<Categoria> ComboCat { get; set; }
        public List<Grupo> ComboGrupo { get; set; }
        public List<Platos> ComboPlato { get; set; }
        public List<Tiempo> DataMeses { get; set; }
        public List<Tiempo> DataAños { get; set; }
        #endregion
        #region INotify
        //Para el INotifyPropertyChanged sea Inicializado.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        //

        private string _PorcentajeUtilidad;
        public event PropertyChangedEventHandler PropertyChanged_PorcentajeUtilidad;
        public string PorcentajeUtilidad
        { get { return _PorcentajeUtilidad; } set 
            {
                int temp = 0;
                if (value.ToString() == "" )
                { _PorcentajeUtilidad = "0"; }else { if (int.TryParse(value.ToString(), out temp)) { _PorcentajeUtilidad = value; } }

                OnPropertyChanged_PorcentajeUtilidad("PorcentajeUtilidad"); } }

        public void OnPropertyChanged_PorcentajeUtilidad(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_PorcentajeUtilidad;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));

            CentroCostos.CostoUnitarioProduccion = Math.Round(CentroCostos.CostoTotalUnitario, 2);
            CentroCostos.CostoTotalVenta = Math.Round(CentroCostos.CostoUnitarioProduccion, 2);
            CentroCostos.Utilidad = Math.Round((Convert.ToDecimal(PorcentajeUtilidad) / 100) * CentroCostos.CostoTotalVenta, 2);
            CentroCostos.PrecioVentaSinIVA = Math.Round(CentroCostos.Utilidad + CentroCostos.CostoTotalVenta, 2);
            CentroCostos.IGV = Math.Round((Convert.ToDecimal(PorcentajeIgv) / 100) * CentroCostos.PrecioVentaSinIVA, 2);
            CentroCostos.PrecioVentaConIVA = Math.Round(CentroCostos.PrecioVentaSinIVA + (CentroCostos.PrecioVentaSinIVA * (Convert.ToDecimal(PorcentajeIgv) / 100)), 2);
        }


        private string _PorcentajeIgv;
        public event PropertyChangedEventHandler PropertyChanged_PorcentajeIgv;
        public string PorcentajeIgv
        { get { return _PorcentajeIgv; } set 
            {
                int temp = 0;
                if (value.ToString() == "" )
                { _PorcentajeIgv = "0"; }else { if (int.TryParse(value.ToString(), out temp)) { _PorcentajeIgv = value; } }

                OnPropertyChanged_PorcentajeIgv("PorcentajeIgv"); } }

        public void OnPropertyChanged_PorcentajeIgv(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_PorcentajeIgv;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));

            CentroCostos.CostoUnitarioProduccion = Math.Round(CentroCostos.CostoTotalUnitario, 2);
            CentroCostos.CostoTotalVenta = Math.Round(CentroCostos.CostoUnitarioProduccion, 2);
            CentroCostos.Utilidad = Math.Round((Convert.ToDecimal(PorcentajeUtilidad) / 100) * CentroCostos.CostoTotalVenta, 2);
            
            CentroCostos.PrecioVentaSinIVA = Math.Round(CentroCostos.Utilidad + CentroCostos.CostoTotalVenta, 2);
            CentroCostos.IGV = Math.Round((Convert.ToDecimal(PorcentajeIgv) / 100) * CentroCostos.PrecioVentaSinIVA, 2);
            CentroCostos.PrecioVentaConIVA = Math.Round(CentroCostos.PrecioVentaSinIVA + (CentroCostos.PrecioVentaSinIVA * (Convert.ToDecimal(PorcentajeIgv) / 100)), 2);
        }
        private string _PorcentajeRC;
        public event PropertyChangedEventHandler PropertyChanged_PorcentajeRC;
        public string PorcentajeRC
        { get { return _PorcentajeRC; } set 
            {
                int temp = 0;
                if (value.ToString() == "" )
                { _PorcentajeRC = "0"; }else { if (int.TryParse(value.ToString(), out temp)) { _PorcentajeRC = value; } }

                OnPropertyChanged_PorcentajeRC("PorcentajeRC"); } }

        public void OnPropertyChanged_PorcentajeRC(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_PorcentajeRC;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));

            CentroCostos.CostoUnitarioProduccion = Math.Round(CentroCostos.CostoTotalUnitario, 2);
            CentroCostos.CostoTotalVenta = Math.Round(CentroCostos.CostoUnitarioProduccion, 2);
            CentroCostos.Utilidad = Math.Round((Convert.ToDecimal(PorcentajeUtilidad) / 100) * CentroCostos.CostoTotalVenta, 2);
            
            CentroCostos.PrecioVentaSinIVA = Math.Round(CentroCostos.Utilidad + CentroCostos.CostoTotalVenta, 2);
            CentroCostos.IGV = Math.Round((Convert.ToDecimal(PorcentajeIgv) / 100) * CentroCostos.PrecioVentaSinIVA, 2);
            
            CentroCostos.RC = Math.Round((Convert.ToDecimal(PorcentajeRC) / 100) * CentroCostos.PrecioVentaSinIVA, 2);
            CentroCostos.PrecioVentaConIVA = Math.Round(CentroCostos.PrecioVentaSinIVA + CentroCostos.IGV + CentroCostos.RC, 2);
        }
        #endregion
        #region Commands
        public ICommand BuscarCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand BuscarPorFechaCommand { get; set; }
      
        #endregion

        public RptPrecioCostoPlatoViewModel() 
        {
            valorbt();
            this.CentroCostos = new CentroCostos();
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            this.BuscarPorFechaCommand = new RelayCommand(new Action(BuscarPorFecha));
            DataAños = new List<Tiempo>();
            DataMeses = new List<Tiempo>();
            this.Operacion = "Lista";
        }
        public void BuscarPorFecha()
        {
            if (Idradiobuton == 0 || CentroCostos.ID_PLATO == 0 || CentroCostos.ID_PLATO.ToString() == null)
            {
                var view = new MessageDialog { Mensaje = { Text = "Ingrese todos los datos" } };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            Data(CentroCostos.ID_PLATO, Idradiobuton);

        }
        public void Data(int idplato, int opFecha) 
        {
            CentroCostos.CantidadUnidadesProd = ccstructure.GetCantidadUniProd(idplato, opFecha, CentroCostos);
            if (CentroCostos.CantidadUnidadesProd != 0)
            {
                DataDetallePlato = negcfijos.GetDetPrecioProd(idplato,opFecha,CentroCostos);
                //MensajeError = "";
                //if (DataDetallePlato.Where(w => w.ID_TIPO == 0).Count() == 0)
                //{
                //    MensajeError = "El producto no tiene una receta asociada";
                //}
                //else if (DataDetallePlato.Where(w => w.ID_TIPO != 0).Count() == 0)
                //{
                //    MensajeError = "No se han agregado los costos este mes";
                //}
                //else if (DataDetallePlato.Where(w => w.ID_TIPO != 0).Count() == 0 && DataDetallePlato.Where(w => w.ID_TIPO == 0).Count() == 0)
                //{
                //    MensajeError = "No se han agregado los costos este mes y este producto no tiene una receta asociada";
                //}
                //else 
                //{
                //    MensajeError = "";
                //}

                CentroCostos.TotalPrecio = DataDetallePlato.Sum(s => s.RE_COSTO_RECETA);
                CentroCostos.TotalCostoFijo = DataDetallePlato.Sum(s => s.COSTO_FIJO);
                CentroCostos.TotalCostoVariable = DataDetallePlato.Sum(s => s.COSTO_VARIABLE);

                CentroCostos.CostoTotalUnitario = CentroCostos.TotalPrecio / CentroCostos.CantidadUnidadesProd;
                CentroCostos.CostoTotalUnitario = Math.Round(CentroCostos.CostoTotalUnitario, 2);

                CentroCostos.CostoFijoUnitario = CentroCostos.TotalCostoFijo / CentroCostos.CantidadUnidadesProd;
                CentroCostos.CostoFijoUnitario = Math.Round(CentroCostos.CostoFijoUnitario, 2);

                CentroCostos.CostoVariableUnitario = CentroCostos.TotalCostoVariable / CentroCostos.CantidadUnidadesProd;
                CentroCostos.CostoVariableUnitario = Math.Round(CentroCostos.CostoVariableUnitario, 2);

                CentroCostos.CostoUnitarioProduccion = Math.Round(CentroCostos.CostoTotalUnitario, 2);
                CentroCostos.CostoTotalVenta = Math.Round(CentroCostos.CostoUnitarioProduccion, 2);
                CentroCostos.Utilidad = Math.Round((Convert.ToDecimal(PorcentajeUtilidad) / 100) * CentroCostos.CostoTotalVenta, 2);
                CentroCostos.IGV = Math.Round((Convert.ToDecimal(PorcentajeIgv) / 100) * CentroCostos.CostoTotalVenta, 2);
                CentroCostos.PrecioVentaSinIVA = Math.Round(CentroCostos.Utilidad + CentroCostos.CostoTotalVenta, 2);
                CentroCostos.PrecioVentaConIVA = Math.Round(CentroCostos.IGV + CentroCostos.PrecioVentaSinIVA, 2);
            }
            else
            {
                DataDetallePlato = new ObservableCollection<CentroCostos>();
                MensajeError = "Este producto no tiene ventas realizadas";
                CentroCostos.TotalPrecio = 0;
                CentroCostos.TotalCostoFijo = 0;
                CentroCostos.TotalCostoVariable = 0;

                CentroCostos.CostoTotalUnitario = 0;
                CentroCostos.CostoFijoUnitario = 0;
                CentroCostos.CostoVariableUnitario = 0;

                CentroCostos.CostoUnitarioProduccion = 0;
                CentroCostos.CostoTotalVenta = 0;
                CentroCostos.Utilidad = 0;
                CentroCostos.IGV = 0;
                CentroCostos.PrecioVentaSinIVA = 0;
                CentroCostos.PrecioVentaConIVA = 0;
            }
        }
        
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = Convert.ToInt32(id);
        }
        void valorbt()
        {
            this.rbbtdia = "1";
            this.rbbtmes = "2";
            this.rbbtdesdehasta = "3";
        }
        public void getLista() 
        {
            string desdet = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            ComboSuperCat = ccstructure.GetSuperCategoria();
            PorcentajeUtilidad = "30";
            PorcentajeIgv = "18";
            PorcentajeRC = "13";
            this.Idradiobuton = 1;
            ListarAños();

        }
        private void ListarMeses(int año)
        {
            List<Pedidos> p = new List<Pedidos>();
            p = ccstructure.getDataAñosxPedidos();

            List<Pedidos> u = new List<Pedidos>();
            foreach (var f in p.GroupBy(c => c.PED_FECH_PED.Month)
                   .Select(grp => grp.First())
                   .Where(w=> w.PED_FECH_PED.Year == año)
                   .ToList())
            {
                Tiempo time = new Tiempo();
                time.ID = Convert.ToInt32(f.PED_FECH_PED.Month);
                time.DESCRPCION_MES = f.PED_FECH_PED.ToString("MMMM");
                DataMeses.Add(time);
            }
        }
        private void ListarAños()
        {
            List<Pedidos> p = new List<Pedidos>();
            p = ccstructure.getDataAñosxPedidos();

            List<Pedidos> u = new List<Pedidos>();
            foreach (var f in p.GroupBy(c => c.PED_FECH_PED.Year)
                   .Select(grp => grp.First()).ToList()) 
            {
                Tiempo time = new Tiempo();
                time.DESCRPCION = f.PED_FECH_PED.Year.ToString();
                DataAños.Add(time);
            }
        }
    }
}
