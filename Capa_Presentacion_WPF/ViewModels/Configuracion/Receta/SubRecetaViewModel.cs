using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Receta;
using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Receta;
using Capa_Negocio.Stock;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Receta
{
    public class SubRecetaViewModel : IGeneric
    {
        SubRecetaStructure srstructure = new SubRecetaStructure();
        #region Negocio
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Receta negReceta = new Neg_Receta();
        #endregion
        #region Entidad
        private Recetas _SRSelected;
        private Insumos _InsumoSelected;
        private Recetas recetas;
        private Recetas _subrecetas;
        private Insumos insumos;
        private Detalle_SubReceta_Insumo _InsumosEnter;
        #endregion
        #region GetSetEntidades
        public Recetas Recetas
        {
            get => recetas;
            set
            {
                recetas = value;
            }
        }
        public Recetas SubRecetas
        {
            get => _subrecetas;
            set
            {
                _subrecetas = value;
            }
        }
        public Insumos Insumos
        {
            get => insumos;
            set
            {
                insumos = value;
            }
        }
        #endregion
        #region Objetos
        private string operacion;
        private decimal _CantidadInsumo;
        private string _AccionBoton;
        private int _recSelect;
        private decimal _CostoReceta;
        private string visibilityBoton;
        private bool _isOpenDialogSubMesa;
        private bool _subreceta;
        private int _InsumoSeleccionado;
        private bool _IsEnabledCombo;
        private bool _EnabledTextSR;
        private string _NombreSubReceta;
        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                    GetLista();
                operacion = value;
            }
        }
        public string AccionBoton
        {
            get => _AccionBoton;
            set
            { _AccionBoton = value; }
        }
        public decimal CantidadInsumo
        {
            get => _CantidadInsumo;
            set { _CantidadInsumo = value; }
        }
        public int RecSelect //Receta seleccionada
        {
            get => _recSelect;
            set
            { _recSelect = value; }
        }
        public string VisibilityBoton
        {
            get => visibilityBoton;
            set
            {
                visibilityBoton = value;
            }
        }
        public decimal CostoReceta
        {
            get => _CostoReceta;
            set
            { _CostoReceta = value; }
        }
        public bool IsOpenDialogSubMesa
        {
            get => _isOpenDialogSubMesa;
            set
            {
                _isOpenDialogSubMesa = value;
            }
        }
        public bool _SubReceta
        {
            get => _subreceta;
            set
            {
                _subreceta = value;
            }
        }
        public int InsumoSeleccionado
        {
            get => _InsumoSeleccionado;
            set
            {
                _InsumoSeleccionado = value;
            }
        }

        public bool IsEnabledCombo
        {
            get => _IsEnabledCombo;
            set
            {
                _IsEnabledCombo = value;
            }
        }
        public bool EnabledTextSR
        {
            get => _EnabledTextSR;
            set
            {
                _EnabledTextSR = value;
            }
        }
        public string NombreSubReceta 
        {
            get => _NombreSubReceta;
            set 
            {
                _NombreSubReceta = value;
            }
        }
        #endregion
        #region Seleccion
        public Recetas SRSelected
        {
            get => _SRSelected;
            set
            {
                if (value != null)
                {
                    DataDetalleSubRecetaInsumo = srstructure.GetDetalleSubRecetaInsumo(((Recetas)value).ID);
                }
                _SRSelected = value;
            }
        }
        public Insumos InsumoSelected
        {
            get => _InsumoSelected;
            set
            {
                if (value != null)
                {
                    InsumoSeleccionado = ((Insumos)value).idins;
                }
                _InsumoSelected = value;
            }
        }

        public Detalle_SubReceta_Insumo InsumosEnter
        {
            get => _InsumosEnter;
            set
            {
                if (value != null)
                {
                    //DataInsumos = srstructure.GetInsumosxSR((Detalle_SubReceta_Insumo)value.DSI_ID_INS).ToList();
                }
                _InsumosEnter = value;
            }
        }
        #endregion
        #region Listas
        public List<SCategoria> ComboSuperCat { get; set; }
        public List<Categoria> ComboCat { get; set; }
        public List<Grupo> ComboGrupo { get; set; }
        public List<Platos> ComboPlato { get; set; }
        public List<Recetas> DataReceta { get; set; }
        public List<Recetas> DataSubReceta { get; set; }
        public List<Insumos> DataInsumos { get; set; }
        public List<Detalle_SubReceta_Insumo> DetInsxSubReceta { get; set; }
        public List<Detalle_SubReceta_Insumo> DataDetalleSubRecetaInsumo { get; set; }
        public ObservableCollection<Detalle_SubReceta_Insumo> _DataDetalleSubRecetaInsumo { get; set; }
        public ObservableCollection<Insumos> DataInsumosSubReceta { get; set; }
        public ObservableCollection<Insumos> InsumoList { get; set; }
        #endregion
        #region Commands
        public ICommand AgregarInsumoCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand CancelarEdit { get; set; }
        public ICommand SubRecetaCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarSubRecetaCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ObtenerInsumosCommand { get; set; }
        public ICommand EditarSubRecetaCommand { get; set; }
        public ICommand EliminarSubRecetaCommand { get; set; }
        #endregion

        public SubRecetaViewModel()
        {
            this.Operacion = "Lista";
            this.AccionBoton = "Agregar";
            this._SubReceta = false;
            this.GuardarSubRecetaCommand = new RelayCommand(new Action(GuardarSubReceta));
            this.AgregarInsumoCommand = new ParamCommand(new Action<object>(AgregarInsumo));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.EditarCommand = new ParamCommand(new Action<object>(EditarInsumo));
            this.CancelarEdit = new RelayCommand(new Action(CancelEdit));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.ObtenerInsumosCommand = new ParamCommand(new Action<object>(ObtenerInsumos));
            this.EditarSubRecetaCommand = new ParamCommand(new Action<object>(EditarSubReceta));
            this.EliminarSubRecetaCommand = new ParamCommand(new Action<object>(EliminarSubReceta));
            this.Recetas = new Recetas();
            this.DataInsumosSubReceta = new ObservableCollection<Insumos>();
            this._DataDetalleSubRecetaInsumo = new ObservableCollection<Detalle_SubReceta_Insumo>();
            this.Insumos = new Insumos();
            this.SubRecetas = new Recetas();

        }
        public void GuardarSubReceta()
        {
            if (this.Operacion == "Nuevo")
            {
                if (Recetas.SR_DESCR is null)
                {
                    var view = new MessageDialog { Mensaje = { Text = "Debes ingresar el nombre de la Sub Receta" } };
                    DialogHost.Show(view, "RootDialog");
                    return;
                }
                if (DataInsumosSubReceta.Count <= 0) 
                {
                    var view = new MessageDialog { Mensaje = { Text = "Debes ingresar insumos a la sub receta" } };
                    DialogHost.Show(view, "RootDialog");
                    return;
                }
                var _id = 0;
                string _mensaje = "";
                Recetas.RE_SUB_RECETA = true;
                DataTable dt = new DataTable();
                dt.Columns.Add("idins");
                dt.Columns.Add("INS_CANTIDAD");
                dt.Columns.Add("DSI_SUBTOTAL");
                foreach (var item in DataInsumosSubReceta)
                {
                    DataRow row = dt.NewRow();
                    row["idins"] = item.idins;
                    row["INS_CANTIDAD"] = item.INS_CANTIDAD;
                    row["DSI_SUBTOTAL"] = item.INS_PRECIO * item.INS_CANTIDAD;
                    dt.Rows.Add(row);
                }
                bool res = negReceta.SetSubReceta((_id == 0 ? 1 : 2), Recetas, dt, ref _mensaje);
                this.Operacion = "Lista";
                this.Recetas = new Recetas();
                this.Insumos = new Insumos();
                this.DataInsumosSubReceta = new ObservableCollection<Insumos>();
                if (res) 
                {
                    this.Operacion = "Lista";
                }
            }
            else if (this.Operacion == "Editar")
            {
                if (_DataDetalleSubRecetaInsumo.Count <= 0)
                {
                    var view = new MessageDialog { Mensaje = { Text = "No puedes dejar la sub receta sin insumos" } };
                    DialogHost.Show(view, "RootDialog");
                    return;
                }
                var _id = SubRecetas.SR_ID;
                Recetas.RE_SUB_RECETA = true;
                DataTable dt = new DataTable();
                string _mensaje = "";
                dt.Columns.Add("idins");
                dt.Columns.Add("INS_CANTIDAD");
                dt.Columns.Add("DSI_SUBTOTAL");
                foreach (var item in _DataDetalleSubRecetaInsumo)
                {
                    DataRow row = dt.NewRow();
                    row["idins"] = item.DSI_ID_INS;
                    row["INS_CANTIDAD"] = item.DSI_CANT_INS;
                    row["DSI_SUBTOTAL"] = item.DSI_COSTO_INS * item.DSI_CANT_INS;
                    dt.Rows.Add(row);
                }
                bool res = negReceta.SetSubReceta((_id == 0 ? 1 : 2), SubRecetas, dt, ref _mensaje);
                if (res)
                {
                    this.Operacion = "Lista";
                }
            }
        }
        public void AgregarInsumo(object idInsumo)
        {
           
            if (this.Operacion == "Nuevo")
            {
                this.InsumoList = new ObservableCollection<Insumos>();
                Insumos ins = new Insumos();
                var cant = this.CantidadInsumo;
                var insu = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(idInsumo)).FirstOrDefault();
                var cantgrilla = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(idInsumo)).FirstOrDefault().INS_CANTIDAD;
                insu.INS_CANTIDAD = cant;
                InsumoList.Add(insu);
                string nomins = "";
                nomins = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(idInsumo)).FirstOrDefault().nomins;
                ins = InsumoList.ToList().FirstOrDefault();

                if (_DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault() != null)
                {
                    cant = this._DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault().DSI_CANT_INS;
                    nomins = this._DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault().INS_NOM;
                    this._DataDetalleSubRecetaInsumo.Remove(_DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault());
                    if (AccionBoton == "Agregar")
                    {
                        ins.INS_CANTIDAD = cantgrilla + cant;
                    }
                    else if (AccionBoton == "Actualizar")
                    {
                        ins.INS_CANTIDAD = cant;
                    }
                }
                this.DataInsumosSubReceta.Add(ins);
                this.Insumos = new Insumos();
                this.AccionBoton = "Agregar";
                this.IsEnabledCombo = true;
                this.VisibilityBoton = "Hidden";
                this.CantidadInsumo = 0;
            }
            else if (this.Operacion == "Editar")
            {
                this.InsumoList = new ObservableCollection<Insumos>();
                Insumos ins = new Insumos();
                var cant = CantidadInsumo;
                var insu = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(idInsumo)).FirstOrDefault();
                var cantgrilla = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(idInsumo)).FirstOrDefault().INS_CANTIDAD;
                insu.INS_CANTIDAD = cant;
                InsumoList.Add(insu);
                ins = InsumoList.ToList().FirstOrDefault();
                decimal canti = 0;
                string nomins = "";
                nomins = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(idInsumo)).FirstOrDefault().nomins;
                if (_DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault() != null)
                {
                    canti = this._DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault().DSI_CANT_INS;
                    nomins = this._DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault().INS_NOM;
                    this._DataDetalleSubRecetaInsumo.Remove(_DataDetalleSubRecetaInsumo.Where(w => w.DSI_ID_INS == Convert.ToInt32(idInsumo)).FirstOrDefault());
                    if (AccionBoton == "Agregar")
                    {
                        ins.INS_CANTIDAD = cantgrilla + cant;
                    }
                    else if (AccionBoton == "Actualizar")
                    {
                        ins.INS_CANTIDAD = cant;
                    }
                }
                Detalle_SubReceta_Insumo det = new Detalle_SubReceta_Insumo();
                det.INS_NOM = nomins;
                det.DSI_ID_INS = ins.idins;
                det.DSI_CANT_INS = ins.INS_CANTIDAD;
                this._DataDetalleSubRecetaInsumo.Add(det);
                this.Insumos = new Insumos();
                this.AccionBoton = "Agregar";
                this.IsEnabledCombo = true;
                this.VisibilityBoton = "Hidden";
                this.CantidadInsumo = 0;
            }
        }
        private void EditarInsumo(object id)
        {
            this.VisibilityBoton = "Visible";
            this.AccionBoton = "Actualizar";
            this.IsEnabledCombo = false;
            if (this.Operacion == "Nuevo")
            {
                var h = this.DataInsumosSubReceta.Where(w => w.idins == Convert.ToInt32(id)).FirstOrDefault().INS_CANTIDAD;
                int idIns = Convert.ToInt32(id);
                this.Insumos = this.DataInsumos.Where(w => w.idins == idIns).FirstOrDefault();
                this.CantidadInsumo = this.DataInsumos.Where(w => w.idins == Convert.ToInt32(id)).FirstOrDefault().INS_CANTIDAD;
                Insumos.idins = Convert.ToInt32(id);
                Insumos.INS_CANTIDAD = CantidadInsumo;
            }
            else if (this.Operacion == "Editar")
            {
                var y = this._DataDetalleSubRecetaInsumo.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault().DSI_CANT_INS;
                var IDINS = this._DataDetalleSubRecetaInsumo.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault().DSI_ID_INS;
                int idIns = Convert.ToInt32(id);
                this.Insumos = this.DataInsumos.Where(w => w.idins == IDINS).FirstOrDefault();
                this.CantidadInsumo = this._DataDetalleSubRecetaInsumo.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault().DSI_CANT_INS;
                
                Insumos.idins = Convert.ToInt32(IDINS);
                Insumos.INS_CANTIDAD = y;
            }
        }
        private void EditarSubReceta(object id)
        {
            this.Operacion = "Editar";
            int _id = Convert.ToInt32(id);
            this.DataDetalleSubRecetaInsumo = srstructure.GetInsumosxSR(_id);
            this.NombreSubReceta = srstructure.GetNombreSR(_id);
            //Application.Current.Properties["Hola"] = 

            ObservableCollection<Detalle_SubReceta_Insumo> h = new ObservableCollection<Detalle_SubReceta_Insumo>(DataDetalleSubRecetaInsumo);
            this._DataDetalleSubRecetaInsumo = h;

            this.EnabledTextSR = false;
            this.SubRecetas.SR_ID = Convert.ToInt32(id);
        }

        private async void EliminarSubReceta(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar la Sub Receta?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Recetas.ID = (int)id;
            DataTable dt = new DataTable();
            llenardt(dt);
            bool result = negReceta.SetReceta(3, Recetas, dt, ref _mensaje);
            if (result)
            {
                GetLista();
            }
        }

        private void CancelEdit()
        {
            this.VisibilityBoton = "Hidden";
            this.AccionBoton = "Agregar";
            this.IsEnabledCombo = true;
            this.Insumos = new Insumos();
            this.Recetas = new Recetas();
            this.CantidadInsumo = 0;
        }

        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar el insumo?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            SubRecetas.SR_ID = (int)id;
            DataTable dt = new DataTable();
            llenardt(dt);
            bool result = negReceta.SetSubReceta(4, SubRecetas, dt, ref _mensaje);
            if (result)
            {
                this.DataDetalleSubRecetaInsumo = srstructure.GetInsumosxSR(Recetas.SR_ID);
                ObservableCollection<Detalle_SubReceta_Insumo> h = new ObservableCollection<Detalle_SubReceta_Insumo>(DataDetalleSubRecetaInsumo);
                this._DataDetalleSubRecetaInsumo = h;
            }
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.EnabledTextSR = true;
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
            this.DataInsumosSubReceta = new ObservableCollection<Insumos>();
        }
        private void ObtenerInsumos(object id)
        {
            int _id = Convert.ToInt32(id);
            DetInsxSubReceta = srstructure.GetInsumosxSR(Recetas.ID);

        }
        public void llenardt(DataTable dt)
        {
            dt.Columns.Add("idins");
            dt.Columns.Add("INS_CANTIDAD");
            dt.Columns.Add("DSI_SUBTOTAL");
            DataRow row = dt.NewRow();
            row["idins"] = 0;
            row["INS_CANTIDAD"] = 0;
            row["DSI_SUBTOTAL"] = 0;
            dt.Rows.Add(row);
        }
        public void GetLista()
        {
            DataSubReceta = srstructure.GetSubRecetas();
            DataInsumos = srstructure.GetInsumoySubReceta();
            this.visibilityBoton = "Hidden";
            this.RecSelect = 0;
            IsEnabledCombo = true;
        }
    }
}
