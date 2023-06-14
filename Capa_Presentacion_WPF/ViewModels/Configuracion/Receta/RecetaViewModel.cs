using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Receta;
using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
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
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Receta
{
    public class RecetaViewModel : IGeneric
    {
        RecetaStructure rstructure = new RecetaStructure();
        #region Negocio
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Receta negReceta = new Neg_Receta();
        #endregion
        #region Entidad
        private SCategoria _SCatSelected;
        Funcion_Global globales = new Funcion_Global();
        private Categoria _CatSelected;
        private Grupo _GrupSelected;
        private Platos _PlatoSelected;
        private Insumos _InsumoSelected;
        private Recetas recetas;
        private Recetas _subrecetas;
        private Platos platos;
        private Insumos insumos;
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
        private bool _EnabledCantidad;
        private bool _EnabledEdit;
        public bool RE_INS_ACT_ { get; set; }
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
        public bool EnabledCantidad 
        {
            get => _EnabledCantidad;
            set 
            {
                _EnabledCantidad = value;
            }
        }
        public bool EnabledEdit
        {
            get => _EnabledEdit;
            set 
            {
                _EnabledEdit = value;
            }
        }
        public string TextoSelected { get; set; }
        #endregion
        #region SeleccionCombos
        public SCategoria SCatSelected
        {
            get => _SCatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboCat = new List<Categoria>();
                    ComboCat = rstructure.GetCategoria(((SCategoria)value).idscat);
                    this.ComboGrupo = new List<Grupo>();
                    this.ComboPlato = new List<Platos>();
                    limpiarEdit();
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
                    limpiarEdit();
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
                    limpiarEdit();
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
                    DataReceta = rstructure.GetReceta(((Platos)value).idplato);
                    this.Recetas.RE_ID_CARTA = ((Platos)value).idplato;
                    this.CostoReceta = rstructure.GetCostoReceta(((Platos)value).idplato);
                    limpiarEdit();
                }
                _PlatoSelected = value;
            }
        }
        public Insumos InsumoSelected
        {
            get => _InsumoSelected;
            set
            {
                if (value != null)
                {
                    if (((Insumos)value).SUBRECETA)
                    {
                        this.EnabledCantidad = false;
                        this.Recetas.SR_ID = ((Insumos)value).idins;
                        this.recetas.RE_SUB_RECETA = true;
                        
                    }
                    else 
                    {
                        this.EnabledCantidad = true;
                        this.recetas.RE_SUB_RECETA = false;
                    }
                    this.TextoSelected = ((Insumos)value).nomins;

                }
                _InsumoSelected = value;
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
        #endregion
        #region Commands
        public ICommand AgregarInsumoCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand CancelarEdit { get; set; }
        public ICommand SubRecetaCommand { get; set; }
        #endregion
        public RecetaViewModel()
        {
            this.Operacion = "Lista";
            this.AccionBoton = "Agregar";
            this._SubReceta = false;
            this.AgregarInsumoCommand = new RelayCommand(new Action(AgregarInsumo));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarEdit = new RelayCommand(new Action(CancelEdit));
            this.SubRecetaCommand = new RelayCommand(new Action(SubReceta));
            this.Recetas = new Recetas();
            this.insumos = new Insumos();
        }

        public void AgregarInsumo()
        {
            if (Recetas.RE_CANT_INS == null && Recetas.RE_SUB_RECETA == false)
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Llene correctamente todos los campos.", 2);
                return;
            }
            if (Recetas.RE_CANT_INS == 0 && Recetas.RE_SUB_RECETA == false)
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Llene correctamente todos los campos.", 2);
                return;
            }
            if (Recetas.RE_CANT_INS <= 0 && Recetas.RE_SUB_RECETA == false) {
                globales.Mensaje("SOS-FOOD - Informacion", "La cantidad no puede ser menor o igual a 0", 2);
                return;
            }
            if (Recetas.RE_ID_INS == null || Recetas.RE_ID_INS == 0) {
                globales.Mensaje("SOS-FOOD - Informacion", "Seleccione un insumo", 2);
                return;
            }
            int IdCarta;
            if (AccionBoton == "Agregar")
            {
                var _id = 0;
                string _mensaje = "";
                DataTable dt = new DataTable();
                llenardt(dt);
                Recetas.RE_INS_ACT = RE_INS_ACT_;
                bool res = negReceta.SetReceta((_id == 0 ? 1 : 2), Recetas, dt, ref _mensaje);
                DataReceta = rstructure.GetReceta(Recetas.RE_ID_CARTA);
                IdCarta = Recetas.RE_ID_CARTA;
                this.Recetas = new Recetas();
                Recetas.RE_ID_CARTA = IdCarta;
                this.CostoReceta = rstructure.GetCostoReceta(Recetas.RE_ID_CARTA);
            }
            else if (AccionBoton == "Actualizar")
            {
                var _id = this.Recetas.ID;
                var cant = this.Recetas.RE_CANT_INS;
                this.Recetas = this.DataReceta.Where(w => w.ID == _id).FirstOrDefault();
                this.Recetas.RE_CANT_INS = cant;
                string _mensaje = "";
                DataTable dt = new DataTable();
                llenardt(dt);
                Recetas.RE_INS_ACT = RE_INS_ACT_;
                bool res = negReceta.SetReceta((_id == 0 ? 1 : 2), Recetas, dt, ref _mensaje);
                DataReceta = rstructure.GetReceta(Recetas.RE_ID_CARTA);
                this.VisibilityBoton = "Hidden";
                this.AccionBoton = "Agregar";
                IdCarta = Recetas.RE_ID_CARTA;
                this.Recetas = new Recetas();
                Recetas.RE_ID_CARTA = IdCarta;
                this.CostoReceta = rstructure.GetCostoReceta(Recetas.RE_ID_CARTA);
            }
        }
        private void limpiarEdit() 
        {
            this.VisibilityBoton = "Hidden";
            this.AccionBoton = "Agregar";
            Recetas.RE_CANT_INS = 0;
            //this.DataInsumos = new List<Insumos>();

        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.VisibilityBoton = "Visible";
            this.AccionBoton = "Actualizar";

            var insRec = this.DataReceta.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault();
            int idIns = insRec.RE_ID_INS;
            if (idIns == 0)
            {
                idIns = insRec.SR_ID;
            }

            this.Insumos = this.DataInsumos.Where(w => w.idins == idIns).FirstOrDefault();
            this.Recetas = this.DataReceta.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault();
            this.CantidadInsumo = rstructure.CantidadInsumo(Convert.ToInt32(id));
            this.RecSelect = insRec.ID;
            Recetas.ID = Convert.ToInt32(id);
            RE_INS_ACT_ = Recetas.RE_INS_ACT;
            DataReceta = rstructure.GetReceta(this.Recetas.RE_ID_CARTA);
            this.CostoReceta = rstructure.GetCostoReceta(this.Recetas.RE_ID_CARTA);
        }

        private void CancelEdit()
        {
            this.VisibilityBoton = "Hidden";
            this.AccionBoton = "Agregar";
            this.Recetas = new Recetas();

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
            Recetas.ID = (int)id;
            DataTable dt = new DataTable();
            llenardt(dt);
            bool result = negReceta.SetReceta(3, Recetas, dt, ref _mensaje);
            if (result)
            {
                DataReceta = rstructure.GetReceta(Recetas.RE_ID_CARTA);
                this.CostoReceta = rstructure.GetCostoReceta(Recetas.RE_ID_CARTA);
            }
        }

        public void SubReceta()
        {
            IsOpenDialogSubMesa = true;
            this.SubRecetas = new Recetas();
            this._SubReceta = true;
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
            ComboSuperCat = rstructure.GetSuperCategoria();
            DataInsumos = rstructure.GetInsumosYSubRecetas();
            this.visibilityBoton = "Hidden";
            this.RecSelect = 0;
        }
    }
}
