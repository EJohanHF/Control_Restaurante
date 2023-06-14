using Capa_Entidades.Models.Centro_Costos;
using Capa_Negocio;
using Capa_Negocio.Centro_Costos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos
{
    public class RptCalculoCostoProduccionAnualViewModel : INotifyPropertyChanged
    {
        #region Negocio
        Neg_CentroCostos negcfijos = new Neg_CentroCostos();
        Neg_Combo negCombo = new Neg_Combo();
        #endregion
        #region Entidad
        #endregion
        #region Objetos
        private string operacion;
        private CentroCostos _CentroCostos;
        public string rbbtdia { get; set; }
        public string rbbtmes { get; set; }
        public string rbbtdesdehasta { get; set; }
        public int Idradiobuton { get; set; }

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
            //algun codigo
        }
        #endregion
        #region Listas
        public ObservableCollection<CentroCostos> DetalleAnual { get; set; }
        #endregion
        #region Commands
        public ICommand BuscarCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand BuscarPorFechaCommand { get; set; }

        #endregion

        public RptCalculoCostoProduccionAnualViewModel() 
        {
            this.Operacion = "Lista";
        }
        private void getLista() 
        {
            
        }
    }
}
