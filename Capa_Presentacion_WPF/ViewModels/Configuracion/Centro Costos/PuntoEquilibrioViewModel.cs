using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Centro_Costos;
using Capa_Entidades.Models.Receta;
using Capa_Negocio;
using Capa_Negocio.Centro_Costos;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos
{
    public class PuntoEquilibrioViewModel : INotifyPropertyChanged
    {
        CentroCostosStructure ccstructure = new CentroCostosStructure();
        #region Objetos
        private CentroCostos CentroCostos { get; set; }
        public string PrecioUnitarioPlato { get; set; }
        public decimal PuntoEquilibrio { get; set; }
        public decimal TotalIngresosxMes { get; set; }
        public decimal TotalCostosxMes { get; set; }
        public decimal TotalGananciaxMes { get; set; }
        public int ID_CARTA { get; set; }
        public decimal cfijos { get; set; }
        public decimal cvariables { get; set; }
        public decimal creceta { get; set; }
        #endregion
        #region GetSetObjetos
        #endregion
        #region Negocio
        Neg_Combo negCombo = new Neg_Combo();
        Neg_CentroCostos negcfijos = new Neg_CentroCostos();
        #endregion
        #region Lista
        public ObservableCollection<CentroCostos> DataDetallePlato { get; set; }
        public List<SCategoria> ComboSuperCat { get; set; }
        public List<Categoria> ComboCat { get; set; }
        public List<Grupo> ComboGrupo { get; set; }
        public List<Platos> ComboPlato { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public List<CentroCostos> DataCostosVariables { get; set; }
        public List<CentroCostos> DataCostosFijos { get; set; }
        public List<Recetas> DataReceta { get; set; }
        #endregion
        #region Entidad
        private SCategoria _SCatSelected;
        private Categoria _CatSelected;
        private Grupo _GrupSelected;
        private Platos _PlatoSelected;
        #endregion
        #region SeleccionObtetos
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
                    CentroCostos.Mes = DateTime.Now.Month;
                    CentroCostos.Año = DateTime.Now.Year;
                    this.ID_CARTA = ((Platos)value).idplato;
                    Data(((Platos)value).idplato, 2);
                    
                }
                _PlatoSelected = value;
            }
        }
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
        {
            get { return _PorcentajeUtilidad; }
            set
            {
                int temp = 0;
                if (value.ToString() == "")
                { _PorcentajeUtilidad = "0"; }
                else { if (int.TryParse(value.ToString(), out temp)) { _PorcentajeUtilidad = value; } }

                OnPropertyChanged_PorcentajeUtilidad("PorcentajeUtilidad");
            }
        }

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
        {
            get { return _PorcentajeIgv; }
            set
            {
                int temp = 0;
                if (value.ToString() == "")
                { _PorcentajeIgv = "0"; }
                else { if (int.TryParse(value.ToString(), out temp)) { _PorcentajeIgv = value; } }

                OnPropertyChanged_PorcentajeIgv("PorcentajeIgv");
            }
        }

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
        #endregion
        public PuntoEquilibrioViewModel()
        {
            this.CentroCostos = new CentroCostos();
            GetLista();
        }
        public void Data(int idplato, int opFecha)
        {
            PorcentajeUtilidad = "30";
            PorcentajeIgv = "18";
            CentroCostos.CantidadUnidadesProd = ccstructure.GetCantidadUniProd(idplato, opFecha, CentroCostos);
            if (CentroCostos.CantidadUnidadesProd != 0)
            {
                DataDetallePlato = negcfijos.GetDetPrecioProd(idplato, opFecha, CentroCostos);
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

                PrecioUnitarioPlato = CentroCostos.PrecioVentaConIVA.ToString();


                cfijos = ccstructure.GetCostosFijosxMes(2,CentroCostos);
                creceta = ccstructure.GetCostoReceta(ID_CARTA);
                DataReceta = ccstructure.GetRecetaxPlato(ID_CARTA);
                DataCostosFijos = ccstructure.GetCostosFijosxMesLista(2, CentroCostos);
                PuntoEquilibrio = Math.Round(cfijos / (Convert.ToDecimal(PrecioUnitarioPlato) - creceta),2);

                TotalIngresosxMes = CentroCostos.CantidadUnidadesProd * Convert.ToDecimal(PrecioUnitarioPlato);
                TotalCostosxMes = cfijos + creceta;
                TotalGananciaxMes = TotalIngresosxMes - TotalCostosxMes;
                datosbacis();
            }
            else
            {
                DataDetallePlato = new ObservableCollection<CentroCostos>();
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
        public void datosbacis()
        {

            SeriesCollection = new SeriesCollection
            {

                new LineSeries
                {
                    Title = "Total Ingresos",
                    Values = new ChartValues<decimal> { 0, TotalIngresosxMes },
                    DataLabels = true,
                    LabelPoint = point => "" + point.Y,
                },
                new LineSeries
                {
                    Title = "Total Costos",
                    Values = new ChartValues<decimal> { cfijos, TotalCostosxMes },
                    DataLabels = true,
                    LabelPoint = point => "" + point.Y,
                },
                new OhlcSeries()
                {
                    Title = "Lineas",
                    Values = new ChartValues<OhlcPoint>
                    {
                        new OhlcPoint(0, Convert.ToDouble(cfijos), 0, 0),
                        //new OhlcPoint(0, Convert.ToDouble(TotalIngresosxMes), Convert.ToDouble(TotalCostosxMes), 0)
                    },
                    DataLabels = true,
                    LabelPoint = point => "Perdida: " + (TotalCostosxMes - cfijos),
                },
                new OhlcSeries()
                {
                    Title = "Lineas",
                    Values = new ChartValues<OhlcPoint>
                    {
                        //new OhlcPoint(0, 0, 0, 0),
                        new OhlcPoint(0, Convert.ToDouble(TotalIngresosxMes), Convert.ToDouble(TotalCostosxMes), 0)
                    },
                    DataLabels = true,
                    LabelPoint = point => "Ganancia: " + (TotalIngresosxMes - TotalCostosxMes),
                }
            };

            Labels = new[] { DateTime.Now.ToString("MMM") };
            YFormatter = value => value.ToString("C");
        }
        private void GetLista()
        {
            datosbacis();
            ComboSuperCat = ccstructure.GetSuperCategoria();
        }
    }
}
