using Capa_Entidades;
using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using Capa_Negocio.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Consumo_Interno
{
    public class MermasViewModel : IGeneric
    {
        #region Objetos
        private string _ope;
        public string Operacion
        {
            get => _ope;
            set
            {
                if (value == "Lista")
                {
                    getLista();
                }
                _ope = value;
            }
        }
        public DateTime Fecha_Desde { get; set; } = DateTime.Now;
        public DateTime Fecha_Hasta { get; set; } = DateTime.Now;
        public int IdInsumo { get; set; }
        #endregion
        #region Negocio
        Neg_Merma negMermas = new Neg_Merma();
        Neg_Combo negCombo = new Neg_Combo();
        #endregion
        #region Entidad
        private Ent_Merma _entMerma;
        public Ent_Merma Ent_Merma 
        {
            get => _entMerma;
            set {
                _entMerma = value;
            }
        }   
        #endregion
        #region Listas
        public List<Ent_Merma> DataMermas { get; set; }
        public List<Insumos> DataInsumos { get; set; }
        public List<Ent_Combo> ComboAlmacen { get; set; }
        public List<Ent_Combo> ComboInsumo { get; set; }
        #endregion
        #region Commands
        public ICommand NuevoCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        #endregion
        #region Seleccion
        private Ent_Combo _InsumoSelected;
        public Ent_Combo InsumoSelected {
            get => _InsumoSelected;
            set {
                if (value != null) {
                    IdInsumo = Convert.ToInt32(((Ent_Combo)value).id);
                    DataTable dt = new DataTable();
                    dt = negMermas.GetCantidadxInsumo(IdInsumo, Convert.ToInt32(idalm));
                    Cantidad = Convert.ToDecimal(dt.Rows[0]["CANT"]);
                }
                _InsumoSelected = value;
            }
        }
        private Insumos _InsumosSelected;
        public Insumos InsumosSelected {
            get => _InsumosSelected;
            set {
                if (value != null) {
                    IdInsumo = ((Insumos)value).ID;
                }
                _InsumosSelected = value;
            }
        }
        private object _cant;
        public object Cantidad
        {
            get => _cant;
            set
            {
                _cant = value;
            }
        }
        public string idalm { get; set; }
        private Ent_Combo _almacenSelected;
        public Ent_Combo AlmacenSelected
        {
            get => _almacenSelected;
            set
            {
                this.Cantidad = 0;
                if (value != null)
                {

                    idalm = ((Ent_Combo)value).id.ToString();
                    this.ComboInsumo = negCombo.GetComboAlmacenInsumo(value.id.ToString());
                }
                _almacenSelected = value;
            }
        }
        #endregion
        public MermasViewModel()
        {
            this.Operacion = "Lista";
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            Ent_Merma = new Ent_Merma();
        }
        private void Editar(object _id) {
            int id = Convert.ToInt32(_id);
        }
        private void Guardar() {
            int id = 0;
            id = Ent_Merma.ID;
            Ent_Merma.MI_ID_USU = Convert.ToInt32(Application.Current.Properties["IdUsuario"]);
            bool res = negMermas.setMerma((id == 0)? 1 : 2,Ent_Merma);
            if (res == true) {
                Cancelar();
            }

        }
        private void Cancelar() {
            Ent_Merma = new Ent_Merma();
            this.Operacion = "Lista";
        }
        private void Buscar() {
            DataMermas = negMermas.GetMermas(Fecha_Desde, Fecha_Hasta, IdInsumo);

        }
        private void Nuevo() {
            this.Operacion = "Nuevo";
        }
        private void getLista()
        {
            DataMermas = negMermas.GetMermas(Fecha_Desde,Fecha_Hasta, IdInsumo);
            DataInsumos = negMermas.GetComboInsumos();
            ComboAlmacen = negCombo.GetCombo_Almacen();

        }
    }
}
