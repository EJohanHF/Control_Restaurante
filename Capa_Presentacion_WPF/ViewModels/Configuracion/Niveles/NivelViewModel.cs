using Capa_Entidades.Models.Nivel;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Negocio.Nivel;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Capa_Presentacion_WPF.ViewModels.Dialogs;
using Capa_Entidades.Models.Carta;
using System.ComponentModel;
using System.Windows.Data;
using System.Drawing;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Carta;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Niveles
{
    public class NivelViewModel : IGeneric
    {
        NivelStructure nstructure = new NivelStructure();
        #region Negocio
        Neg_Nivel negNivel = new Neg_Nivel();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Platos negPlatos = new Neg_Platos();
        #endregion
        #region Objetos
        private string _oper;
        private string _tipo_nivel;
        public string tooltipDeleteEnabled { get; set; }
        public string TextoDialog { get; set; }
        public string iconoDeleteEnabled { get; set; }
        public string NombreNivelSeleccionado { get; set; }
        public string TipoDialog { get; set; }
        public int NumeroSelected { get; set; }

        private Ent_Nivel _NivelSelected;
        private Ent_Nivel _Ent_Nivel;
        public string TituloDialog { get; set; }
        public bool EnabledButtonNuevo { get; set; }
        public bool EnabledButtonAgregarOpcion { get; set; }
        public bool EnabledButtonAgregarGrupoPlatos { get; set; }
        public bool EnabledButtonAgregarPlatosGrupo { get; set; }
        public bool EnabledButtonEditar { get; set; }
        public bool EnabledButtonProbarNivel { get; set; }
        public object DialogObject { get; set; }
        public bool openDialog { get; set; }
        public string Estilo { get; set; }
        public object Logo { get; set; }
        public object Logo2 { get; set; }
        private string _VisibilityMenuSubNivel;
        public string NombreNivelNuevo { get; set; }
        public string NombreNivel { get; set; }
        public int _TipoSeleccionNivel;
        public string TEXTO_NIVEL { get; set; }
        public string VisibleBotonEliminarNivel { get; set; }
        public string VisibilityMenuSubNivel
        {
            get => _VisibilityMenuSubNivel;
            set 
            {
                _VisibilityMenuSubNivel = value;
            }
        }

        #endregion
        #region GetSetObjetos
        public bool enabledradio { get; set; } = false;
        public string Operacion
        {
            get => _oper;
            set
            {
                if (value == "Lista") { getLista(1); }
                _oper = value;
            }
        }
        public string tipo_nivel
        {
            get => _tipo_nivel;
            set
            {
                _tipo_nivel = value;
            }
        }
        public int TipoSeleccionNivel
        {
            get => _TipoSeleccionNivel;
            set
            {
                if (value == 4)
                {
                    if (DataSubNiveles.Count() > 0)
                    {
                        validar(1);
                    }
                    else 
                    {
                        EnabledButtonAgregarGrupoPlatos = true;
                        EnabledButtonAgregarOpcion = false;
                        DataSubNiveles = new ObservableCollection<Ent_Nivel>();
                    }
                }
                else
                {
                    if (DataPlatosNivel.Count() > 0)
                    {
                        validar(2);
                    }
                    else 
                    {
                        EnabledButtonAgregarGrupoPlatos = false;
                        EnabledButtonAgregarOpcion = true;
                        DataPlatosNivel = new ObservableCollection<Platos>();
                        ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
                        ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
                    }
                }
                _TipoSeleccionNivel = value;
            }
        }
        public Ent_Nivel Ent_Nivel
        {
            get => _Ent_Nivel;
            set 
            {
                _Ent_Nivel = value;
            }
        }
        #endregion
        #region ObjetosSeleccionados
        public Ent_Nivel NivelSelected
        {
            get => _NivelSelected;
            set 
            {
                if (value != null)
                {
                    this.Operacion = "Editar";
                    Ent_Nivel.ID = ((Ent_Nivel)value).ID;
                    EnabledButtonProbarNivel = true; 
                    CargarDatosDeNivel(((Ent_Nivel)value).ID, ((Ent_Nivel)value).N_NOM);
                    TipoSeleccionNivel = ((Ent_Nivel)value).N_TIPO_SELEC;
                }
                _NivelSelected = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<Ent_Nivel> DataNiveles { get; set; }
        public ObservableCollection<Ent_Nivel> DataSubNiveles { get; set; }
        public ObservableCollection<Ent_Nivel> DataSeleccionNiveles { get; set; }
        static public ObservableCollection<Platos> DataPlatosNivel { get; set; }
        public ICollectionView ContractsListCollectionView { get; set; }
        public ObservableCollection<Platos> DataGruposNivel { get; set; }
        public ObservableCollection<Platos> GRUPO { get; set; }
        public ObservableCollection<int> LongListToTestComboVirtualization { get; set; }
        public ObservableCollection<Platos> DataBotones { get; set; }
        #endregion
        #region Commands
        public ICommand NuevoCommand { get; set; }
        public ICommand AceptarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand CancelarRegistroCommand { get; set; }
        public ICommand AgregarOpcionCommand { get; set; }
        public ICommand GuardarNivelCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand CambiarTextoSubNivelCommand { get; set; }
        public ICommand EliminarSubNivelCommand { get; set; }
        public ICommand EliminarNivelCommand { get; set; }
        public ICommand EliminarGrupoNivelCommand { get; set; }
        public ICommand CambiarColorSubNivelCommand { get; set; }
        public ICommand CambiarImagenSubNivelCommand { get; set; }
        public ICommand LlamarDialogNivelCommand { get; set; }
        public ICommand AgregarGrupoPlatosCommand { get; set; }
        public ICommand EditarNombreNivelCommand { get; set; }
        public ICommand CheckTipoSeleccionNivelCommand { get; set; }
        public ICommand AgregarBoton { get; set; }
        public ICommand AgregarGrupoCommand { get; set; }
        #endregion
        public NivelViewModel()
        {
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.AceptarCommand = new RelayCommand(new Action(Aceptar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.CancelarRegistroCommand = new RelayCommand(new Action(CancelarRegistro));
            this.AgregarOpcionCommand = new RelayCommand(new Action(AgregarOpcion));
            this.GuardarNivelCommand = new RelayCommand(new Action(GuardarNivel));
            this.EditarCommand = new RelayCommand(new Action(Editar));
            this.CambiarTextoSubNivelCommand = new ParamCommand(new Action<object>(CambiarTextoSubNivel));
            this.EliminarSubNivelCommand = new ParamCommand(new Action<object>(EliminarSubNivel));
            this.EliminarNivelCommand = new RelayCommand(new Action(EliminarNivel));
            this.EliminarGrupoNivelCommand = new ParamCommand(new Action<object>(EliminarGrupoNivel));
            this.CambiarColorSubNivelCommand = new ParamCommand(new Action<object>(CambiarColorSubNivel));
            this.CambiarImagenSubNivelCommand = new ParamCommand(new Action<object>(CambiarImagenSubNivel));
            this.LlamarDialogNivelCommand = new RelayCommand(new Action(LlamarDialogNivel));
            this.AgregarGrupoPlatosCommand = new RelayCommand(new Action(AgregarGrupoPlatos));
            this.EditarNombreNivelCommand = new RelayCommand(new Action(EditarNombreNivel));
            this.CheckTipoSeleccionNivelCommand = new ParamCommand(new Action<object>(CheckTipoSeleccionNivel));
            this.AgregarBoton = new RelayCommand(new Action(AgregarBoto));
            this.AgregarGrupoCommand = new RelayCommand(new Action(AgregarGrupo));
            if (Application.Current.Properties["TituloDialog"] != null) 
            {
                this.TituloDialog = Application.Current.Properties["TituloDialog"].ToString();
                this.TEXTO_NIVEL = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
            }
            enabledradio = false;
            this.Ent_Nivel = new Ent_Nivel();
            this.DataSubNiveles = new ObservableCollection<Ent_Nivel>();
            VisibilityMenuSubNivel = "Hidden";
            DataPlatosNivel = new ObservableCollection<Platos>();
            this.DataGruposNivel = new ObservableCollection<Platos>();
            this.DataBotones = new ObservableCollection<Platos>();

            EnabledButtonAgregarOpcion = false;
            EnabledButtonAgregarGrupoPlatos = false;
            EnabledButtonAgregarPlatosGrupo = false;
            EnabledButtonProbarNivel = false;
            Operacion = "Lista";
        }
        private async void AgregarGrupo()
        {
            //string[] id_grupos_seleccionados = DataGridAssist
            var openpago = new NivelGrupoDialog { };
            var x = await DialogHost.Show(openpago, "RootDialog");
            if (Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"].ToString() != "True")
            {
                Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "";
                return;
            }
            //
            TipoSeleccionNivel = 5;
            
            Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "";
            DataPlatosNivel = new ObservableCollection<Platos>();
            string nombregrupo = "";
            foreach (Grupo p in NivelGrupoDialogViewModel.TextoPlatos)
            {
                Platos pl = new Platos();
                pl.idgrup = p.idgrup.ToString();
                pl.nomplato = p.nomgrup;
                //pl.nomgrup = p.nomgrup;
                pl.ischeck = p.ischeck;
                DataPlatosNivel.Add(pl);

                nombregrupo = DataPlatosNivel.Where(w => w.idgrup == p.idgrup.ToString()).FirstOrDefault().nomgrup;

                Platos pp = new Platos();

                int c = DataGruposNivel.Where(w => w.idgrup == p.idgrup.ToString()).Count();
                if (c == 0)
                {
                    pp.idgrup = p.idgrup.ToString();
                    pp.nomplato = p.nomgrup;
                    pp.nomgrup = nombregrupo;
                    this.DataGruposNivel.Add(pp);
                }
            }
            LongListToTestComboVirtualization = new ObservableCollection<int>(Enumerable.Range(0, 51));
            this.tipo_nivel = "Carta";

            ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("nomgrup"));
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
            DataSeleccionNiveles.Where(w => w.ID_SS == 4).FirstOrDefault().selec_ischeck = true;
            this.EnabledButtonAgregarOpcion = false;
            this.EnabledButtonAgregarGrupoPlatos = true;
            this.EnabledButtonAgregarPlatosGrupo = false;

            if (Ent_Nivel.ID != 0)
            {
                this.Operacion = "Editar";
            }
        }
        private async void validar(int op)
        {
            if (tipo_nivel == "Carta" && DataPlatosNivel.Count() >= 0 && op == 1)
            {
                return;
            }
            else if (tipo_nivel == "Opcion" && DataSubNiveles.Count() >= 0 && op == 2)
            {
                this.Operacion = "Editar";
                return;
            }
            else if (Ent_Nivel.N_TIPO_SELEC != 4 && DataSubNiveles.Count() > 0 && op == 1) 
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "Si selecciona este tipo de selección los \n datos que se agregaron se eliminaran \n ¿Desea realizar esta accion?" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                {
                    if (op == 1)
                    {
                        TipoSeleccionNivel = 1;
                        DataSeleccionNiveles.Where(w => w.ID_SS == 1).FirstOrDefault().selec_ischeck = true;
                    }
                    else if (op == 2)
                    {
                        TipoSeleccionNivel = 4;
                        DataSeleccionNiveles.Where(w => w.ID_SS == 4).FirstOrDefault().selec_ischeck = true;
                    }
                    return;
                }
                else
                {
                    if (op == 1)
                    {
                        EnabledButtonAgregarGrupoPlatos = true;
                        EnabledButtonAgregarOpcion = false;
                        DataSubNiveles = new ObservableCollection<Ent_Nivel>();
                    }
                    else if (op == 2)
                    {
                        EnabledButtonAgregarGrupoPlatos = false;
                        EnabledButtonAgregarOpcion = true;
                        DataPlatosNivel = new ObservableCollection<Platos>();
                        ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
                        ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
                    }
                }
            }
        }
        private void CargarDatosDeNivel(int ID,string NOMBRE)
        {
            try
            {
                List<Ent_Nivel> n = new List<Ent_Nivel>();
                n = nstructure.GetSubNivelesxNivel(ID, 1);
                ObservableCollection<Ent_Nivel> _n = new ObservableCollection<Ent_Nivel>(n);
                DataSubNiveles = _n;
                bool N_ACT = DataNiveles.Where(w => w.ID == ID).FirstOrDefault().N_ACT;
                string N_NOM = NOMBRE;
                int N_TIPO_SELEC = DataNiveles.Where(w => w.ID == ID).FirstOrDefault().N_TIPO_SELEC;
                DataSeleccionNiveles.Where(w => w.ID_SS == N_TIPO_SELEC).FirstOrDefault().selec_ischeck = true;
                if (N_ACT == true)
                {
                    iconoDeleteEnabled = "WindowClose";
                    tooltipDeleteEnabled = "Desactivar";
                }
                else
                {
                    iconoDeleteEnabled = "CheckboxMarkedCircleOutline";
                    tooltipDeleteEnabled = "Activar";
                }
                enabledradio = true;
                DataPlatosNivel = new ObservableCollection<Platos>();
                ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
                ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
                NombreNivel = N_NOM;

                Ent_Nivel.N_TIPO_SELEC = N_TIPO_SELEC;
                if (N_TIPO_SELEC == 4) {
                    this.tipo_nivel = "Carta";
                }
                if (DataSubNiveles.Where(w => w.SN_ID_GRUPO == 0).Count() == 0)
                {
                    this.tipo_nivel = "Carta";
                    if (N_TIPO_SELEC != 5)
                    {
                        DataSeleccionNiveles.Where(w => w.ID_SS == N_TIPO_SELEC).FirstOrDefault().selec_ischeck = true;
                        foreach (Ent_Nivel s in DataSubNiveles)
                        {
                            Platos p = new Platos();
                            p.idgrup = s.SN_ID_GRUPO.ToString();
                            p.idplato = s.SN_ID_CARTA;
                            p.nomplato = s.CAR_NOM;
                            p.nomgrup = s.GR_NOM;
                            DataPlatosNivel.Add(p);
                        }
                        ListCollectionView _lcv = new ListCollectionView(DataPlatosNivel);
                        _lcv.GroupDescriptions.Add(new PropertyGroupDescription("nomgrup"));
                        ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(_lcv);
                        this.EnabledButtonAgregarOpcion = false;
                        this.EnabledButtonAgregarGrupoPlatos = true;
                    }
                    else
                    {
                        int[] idGrupos = null;
                        idGrupos = DataSubNiveles.Select(s => s.SN_ID_GRUPO).ToArray();
                        ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
                        platos = negPlatos.GetPlatos();
                        List<Platos> DataPlatos = new List<Platos>();
                        DataPlatos = platos.Where(w => idGrupos.Contains(Convert.ToInt32(w.idgrup)) && w.estadoplato == 1).ToList();

                        foreach (Platos s in DataPlatos)
                        {
                            Platos p = new Platos();
                            p.idgrup = s.idgrup;
                            p.idplato = s.idplato;
                            p.nomplato = s.nomplato;
                            p.nomgrup = s.nomgrup;
                            DataPlatosNivel.Add(p);
                        }
                        ListCollectionView _lcv = new ListCollectionView(DataPlatosNivel);
                        _lcv.GroupDescriptions.Add(new PropertyGroupDescription("nomgrup"));
                        ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(_lcv);
                        this.EnabledButtonAgregarOpcion = false;
                        this.EnabledButtonAgregarGrupoPlatos = true;
                    }
                }
                else
                {
                    this.tipo_nivel = "Opcion";
                    this.EnabledButtonAgregarOpcion = true;
                    this.EnabledButtonAgregarGrupoPlatos = false;
                }
                VisibilityMenuSubNivel = "Hidden";
                this.EnabledButtonEditar = true;
                this.Ent_Nivel.ID = ID;
                this.EnabledButtonProbarNivel = true;
                VisibleBotonEliminarNivel = "Visible";
            }
            catch (Exception ex )
            {
            }
        }
        public void AgregarBoto()
        {
            Platos pl = new Platos();
            pl.nomplato = "1";
            DataBotones.Add(pl);
        }
        public void CheckTipoSeleccionNivel(object ID)
        {
            if (DataSubNiveles.Count() > 0)
            {
                this.Operacion = "Editar";
            }
            int id = Convert.ToInt32(ID);
            TipoSeleccionNivel = id;
            Ent_Nivel.N_TIPO_SELEC = TipoSeleccionNivel;
        }
        public void EditarNombreNivel() 
        {
            this.TipoDialog = "CambiarTextoNivel";
            TextoDialog = NombreNivel;
            this.openDialog = true;
            DialogObject = new Views.Dialogs.DialogNuevoNivel();
        }
        public async void LlamarDialogNivel()
        {
            if (Ent_Nivel.ID == 0)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Seleccione Nivel" }
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            Application.Current.Properties["ID_NIVEL_LDRP"] = Ent_Nivel.ID;
            var openview = new NivelDialog { };
            var x = await DialogHost.Show(openview, "RootDialog");
            this.TEXTO_NIVEL = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
        }
        public async void AgregarGrupoPlatos()
        {
            var openpago = new NivelCartaDialog { };
            var x = await DialogHost.Show(openpago, "RootDialog");
            if (Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"].ToString() != "True")
            {
                Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "";
                return;
            }
            enabledradio = false;
            TipoSeleccionNivel = 4;
            Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "";
            string nombregrupo="";
            foreach (Platos p in NivelCartaDialogViewModel.TextoPlatos)
            {
                Platos pl = new Platos();
                pl.idplato = p.idplato;
                pl.idgrup = p.idgrup;
                pl.nomplato = p.nomplato;
                pl.nomgrup = p.nomgrup;
                pl.ischeck = p.ischeck;
                DataPlatosNivel.Add(pl);

                nombregrupo = DataPlatosNivel.Where(w => w.idgrup == p.idgrup).FirstOrDefault().nomgrup;
                Platos pp = new Platos();

                int c = DataGruposNivel.Where(w => w.idgrup == p.idgrup).Count();
                if (c == 0) 
                {
                    pp.idgrup = p.idgrup;
                    pp.nomgrup = nombregrupo;
                    this.DataGruposNivel.Add(pp);
                }
            }
            LongListToTestComboVirtualization = new ObservableCollection<int>(Enumerable.Range(0, 51));
            this.tipo_nivel = "Carta";

            ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("nomgrup"));
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
            DataSeleccionNiveles.Where(w => w.ID_SS == 4).FirstOrDefault().selec_ischeck = true;
            this.EnabledButtonAgregarOpcion = false;
            this.EnabledButtonAgregarGrupoPlatos = false;
        }
        public void CambiarImagenSubNivel(object ID) 
        {
            int id = Convert.ToInt32(ID);
            OpenFileDialog getimg = new OpenFileDialog();
            getimg.InitialDirectory = "C:\\";
            getimg.Filter = "Archivos de imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png";
            byte[] imagen = null;
            if (getimg.ShowDialog() == true)
            {
                try
                {
                    string RutaArchivo;
                    RutaArchivo = getimg.FileName.ToString();
                    this.Logo = RutaArchivo;

                    imagen = File.ReadAllBytes(RutaArchivo);
                    DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().SN_IMAGEN = imagen;
                    Editar();
                }
                catch (Exception ex)
                {
                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error: No se puede carga la imagen " + ex.Message, 3);
                }
            }
        }
        public void CambiarColorSubNivel(object ID) 
        {
            //Editar();
        }
        public void EliminarSubNivel(object ID) 
        {
            int id = Convert.ToInt32(ID);
            if (tipo_nivel == "Carta")
            {
                DataPlatosNivel.Remove(DataPlatosNivel.Where(w => w.idplato == id).FirstOrDefault());
                DataPlatosNivel.Remove(DataPlatosNivel.Where(w => w.idgrup == id.ToString()).FirstOrDefault());
            }
            else if (tipo_nivel == "Opcion")
            {
                DataSubNiveles.Remove(DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault());
            }
            Editar();
        }
        public async void EliminarNivel() 
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Desea eliminar el nivel?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
            { return; }

            int id = Convert.ToInt32(this.Ent_Nivel.ID);
            string _mensaje = "";
            DataTable dt = new DataTable();
            DataColumn _ID = new DataColumn("ID");
            _ID.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(_ID);
            DataColumn SN_NOM = new DataColumn("SN_NOM");
            SN_NOM.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(SN_NOM);
            DataColumn SN_ACT = new DataColumn("SN_ACT");
            SN_ACT.DataType = System.Type.GetType(" System.Boolean");
            dt.Columns.Add(SN_ACT);
            DataColumn SN_IMAGEN = new DataColumn("SN_IMAGEN");
            SN_IMAGEN.DataType = System.Type.GetType("System.Byte[]");
            dt.Columns.Add(SN_IMAGEN);
            DataColumn SN_ID_GRUP = new DataColumn("SN_ID_GRUP");
            SN_ID_GRUP.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(SN_ID_GRUP);
            DataColumn SN_ID_CARTA = new DataColumn("SN_ID_CARTA");
            SN_ID_CARTA.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(SN_ID_CARTA);

            byte[] imagen = null;
            //imagen = File.ReadAllBytes(@"~\..\..\..\Resources\Images\logo-sf-blanco.png");
            DataRow row = dt.NewRow();
            row["ID"] = 0;
            row["SN_NOM"] = "";
            row["SN_ACT"] = 0;
            row["SN_IMAGEN"] = imagen;
            row["SN_ID_GRUP"] = 0;
            row["SN_ID_CARTA"] = 0;
            dt.Rows.Add(row);
            bool res = negNivel.SetFacturaCompra(3, Ent_Nivel, dt, ref _mensaje);
            if (!res)
            {
                var view = new MessageDialog { Mensaje = { Text = "No se pudo eliminar el registo" } };
                DialogHost.Show(view, "RootDialog");
            }
            getLista(1);
            Cancelar();
        }
        public void EliminarGrupoNivel(object ID)
        {
            int idgrupo = Convert.ToInt32(ID);
            if (tipo_nivel == "Carta")
            {
                DataPlatosNivel.Remove(DataPlatosNivel.Where(w => w.idgrup == idgrupo.ToString()).FirstOrDefault());
            }
            Editar();
        }
        public void CambiarTextoSubNivel(object ID) 
        {
            this.TipoDialog = "CambiarTextoSubNivel";
            if (Convert.ToInt32(ID) == 0)
            {
                this.TextoDialog = "";
            }
            else 
            {
                this.Ent_Nivel.ID_SN = Convert.ToInt32(ID);
                this.TextoDialog = DataSubNiveles.Where(w => w.ID_SN == Convert.ToInt32(ID)).FirstOrDefault().SN_NOM;
            }
            this.openDialog = true;
            DialogObject = new Views.Dialogs.DialogNuevoNivel();
        }
        public void Editar()
        {
            this.Operacion = "Editar";
            this.EnabledButtonNuevo = false;
            this.EnabledButtonEditar = false;
            this.EnabledButtonAgregarOpcion = true;
            this.EnabledButtonAgregarGrupoPlatos = false;
            this.VisibilityMenuSubNivel = "Visible";
        }
        public void GuardarNivel() 
        {
            try
            {
                if (TipoSeleccionNivel.ToString() == null || TipoSeleccionNivel == 0)
                {
                    TipoSeleccionNivel = 1;
                }
                if (DataSubNiveles.Count() == 0 && DataPlatosNivel.Count() == 0)
                {
                    return;
                }
                string _mensaje = "";
                int _id = Ent_Nivel.ID;
                Ent_Nivel.N_NOM = NombreNivel;

                DataTable dt = new DataTable();

                DataColumn ID = new DataColumn("ID");
                ID.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(ID);

                DataColumn SN_NOM = new DataColumn("SN_NOM");
                SN_NOM.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(SN_NOM);

                DataColumn SN_ACT = new DataColumn("SN_ACT");
                SN_ACT.DataType = System.Type.GetType(" System.Boolean");
                dt.Columns.Add(SN_ACT);

                DataColumn SN_IMAGEN = new DataColumn("SN_IMAGEN");
                SN_IMAGEN.DataType = System.Type.GetType("System.Byte[]");
                dt.Columns.Add(SN_IMAGEN);

                DataColumn SN_ID_GRUP = new DataColumn("SN_ID_GRUP");
                SN_ID_GRUP.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(SN_ID_GRUP);

                DataColumn SN_ID_CARTA = new DataColumn("SN_ID_CARTA");
                SN_ID_CARTA.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(SN_ID_CARTA);

                byte[] imagen = null;
                //imagen = File.ReadAllBytes(@"~\..\..\..\Resources\Images\logo-sf-blanco.png");

                if (tipo_nivel == "Opcion")
                {
                    foreach (var i in DataSubNiveles)
                    {
                        DataRow row = dt.NewRow();
                        row["ID"] = 0;
                        row["SN_NOM"] = i.SN_NOM;
                        row["SN_ACT"] = i.SN_ACT;
                        row["SN_IMAGEN"] = i.SN_IMAGEN;
                        row["SN_ID_GRUP"] = 0;
                        row["SN_ID_CARTA"] = 0;
                        dt.Rows.Add(row);
                    }
                }
                else if (tipo_nivel == "Carta")
                {
                    foreach (var i in DataPlatosNivel)
                    {
                        DataRow row = dt.NewRow();
                        row["ID"] = 0;
                        row["SN_NOM"] = i.nomplato;
                        row["SN_ACT"] = 1;
                        row["SN_IMAGEN"] = imagen;
                        row["SN_ID_GRUP"] = i.idgrup;
                        row["SN_ID_CARTA"] = i.idplato;
                        dt.Rows.Add(row);
                    }
                    if (TipoSeleccionNivel != 5)
                    {
                        Ent_Nivel.N_TIPO_SELEC = 4;
                    }
                    else 
                    {
                        Ent_Nivel.N_TIPO_SELEC = 5;
                    }
                    
                }
                Ent_Nivel.N_NOM = NombreNivel;
                bool res = negNivel.SetFacturaCompra((_id == 0 ? 1 : 2), Ent_Nivel, dt, ref _mensaje);
                getLista(1);
                this.Operacion = "Lista";
                TextoDialog = "";
                List<Ent_Nivel> n = new List<Ent_Nivel>();
                n = nstructure.GetNiveles(0);
                ObservableCollection<Ent_Nivel> o = new ObservableCollection<Ent_Nivel>(n);
                DataNiveles = o;
                DataSubNiveles = new ObservableCollection<Ent_Nivel>();
                DataPlatosNivel = new ObservableCollection<Platos>();
                DataPlatosNivel = new ObservableCollection<Platos>();
                ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
                ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
                NombreNivel = "";
                EnabledButtonProbarNivel = false;
                enabledradio = false;

                Ent_Nivel = new Ent_Nivel();
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error: " + ex.Message, 3);
            }
        }
        public void AgregarOpcion()
        {
            TituloDialog = "Ingrese el nombre de la opcion";
            TipoDialog = "NuevaOpcion";
            this.openDialog = true;
            DialogObject = new Views.Dialogs.DialogNuevoNivel();
            TextoDialog = "";
            this.Estilo = "{StaticResource MaterialDesignFloatingActionAccentButton}";
            this.tipo_nivel = "Opcion";

            if (TipoSeleccionNivel == 4) {
                DataSeleccionNiveles.Select(ss => { ss.selec_ischeck = false; return ss; }).ToList();
                DataSeleccionNiveles.Where(w => w.ID_SS == 1).FirstOrDefault().selec_ischeck = true;
            }
        }
        public void CancelarRegistro() 
        {
            this.Operacion = "Lista";
            this.DataSubNiveles = new ObservableCollection<Ent_Nivel>();
            //this.tipo_nivel = "Opcion";
            TextoDialog = "";
            NombreNivelNuevo = "";
            NombreNivelSeleccionado = "";
            NombreNivel = "";
            enabledradio = false;
            //ContractsListCollectionView = 
            EnabledButtonProbarNivel = false;
            DataPlatosNivel = new ObservableCollection<Platos>();
            ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);  
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
        }
        public void Cancelar() 
        {

            if (Ent_Nivel.ID == 0)
            {
                this.Operacion = "Lista";
            }
            else {
                this.Operacion = "Editar";
            }

            if (Operacion == "Lista")
            {
                enabledradio = false;
            }
            else {
                enabledradio = true;
            }
            TipoDialog = "Cancelar";
            DialogHost.CloseDialogCommand.Execute(null, null);
            TextoDialog = "";
            DataPlatosNivel = new ObservableCollection<Platos>();
            ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
            DataSeleccionNiveles.Where(w => w.ID_SS == 1).FirstOrDefault().selec_ischeck = true;
        }
        public void Aceptar() 
        {
            try
            {
                if (TextoDialog == null || TextoDialog == "" || TextoDialog.TrimStart() == "")
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Rellene el campo requerido por favor", 2);
                    return;
                }
                if (TipoDialog == "NuevoNivel")
                {
                    this.Operacion = "Nuevo";
                    this.NombreNivel = TextoDialog.ToUpper();
                    EnabledButtonNuevo = false;
                    EnabledButtonAgregarOpcion = true;
                    EnabledButtonAgregarGrupoPlatos = true;
                    EnabledButtonAgregarPlatosGrupo = true;
                    EnabledButtonProbarNivel = false;
                    enabledradio = true;
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else if (TipoDialog == "NuevaOpcion")
                {
                    if (Ent_Nivel.ID != 0)
                    {
                        this.Operacion = "Editar";
                    }
                    DataTable dt = new DataTable();
                    dt.Columns.Add("NOMBRE");
                    DataRow row = dt.NewRow();
                    row["NOMBRE"] = TextoDialog;
                    dt.Rows.Add(row);

                    OpenFileDialog getimg = new OpenFileDialog();
                    getimg.InitialDirectory = "C:\\";
                    getimg.Filter = "Archivos de imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png";
                    byte[] imagen = null;
                    this.Logo = imagen;
                    this.Ent_Nivel.SN_IMAGEN = imagen;
                    if (getimg.ShowDialog() == true)
                    {
                        try
                        {
                            string RutaArchivo;
                            RutaArchivo = getimg.FileName.ToString();
                            this.Logo = RutaArchivo;

                            imagen = File.ReadAllBytes(RutaArchivo);
                            this.Ent_Nivel.SN_IMAGEN = imagen;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    ObservableCollection<Ent_Nivel> fact = new ObservableCollection<Ent_Nivel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Ent_Nivel n = new Ent_Nivel();
                        n.SN_NOM = dt.Rows[i]["NOMBRE"].ToString();
                        n.SN_IMAGEN = this.Ent_Nivel.SN_IMAGEN;
                        fact.Add(n);
                    }
                    var h = fact.FirstOrDefault();
                    DataSubNiveles.Add(h);

                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else if (this.TipoDialog == "CambiarTextoNivel") {
                    NombreNivel = TextoDialog.ToUpper();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else if (this.TipoDialog == "CambiarTextoSubNivel")
                {
                    int id = Ent_Nivel.ID_SN;
                    DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().SN_NOM = TextoDialog;
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD", "Error: " + ex.Message, 3);
            }
            //if (TextoDialog == null || TextoDialog == "" || TextoDialog.TrimStart() == "")
            //{
            //    return;
            //}
            //if (TipoDialog == "NuevoNivel")
            //{

            //    this.Operacion = "Nuevo";
            //    this.NombreNivel = TextoDialog.ToUpper();
            //    this.EnabledButtonNuevo = false;
            //    this.EnabledButtonEditar = false;
            //    this.EnabledButtonProbarNivel = false;
            //    this.DataSubNiveles = new ObservableCollection<Ent_Nivel>();
            //    this.EnabledButtonAgregarOpcion = true;
            //    this.EnabledButtonAgregarGrupoPlatos = true;
            //    enabledradio = true;
            //}
            //else if (TipoDialog == "NuevaOpcion")
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("NOMBRE");

            //    DataRow row = dt.NewRow();
            //    row["NOMBRE"] = TextoDialog;
            //    dt.Rows.Add(row);

            //    OpenFileDialog getimg = new OpenFileDialog();
            //    getimg.InitialDirectory = "C:\\";
            //    getimg.Filter = "Archivos de imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png";
            //    byte[] imagen = null;
            //    this.Logo = imagen;
            //    this.Ent_Nivel.SN_IMAGEN = imagen;
            //    if (getimg.ShowDialog() == true)
            //    {
            //        try
            //        {
            //            string RutaArchivo;
            //            RutaArchivo = getimg.FileName.ToString();
            //            this.Logo = RutaArchivo;

            //            imagen = File.ReadAllBytes(RutaArchivo);
            //            this.Ent_Nivel.SN_IMAGEN = imagen;
            //        }
            //        catch (Exception ex)
            //        {
            //            //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error: No se puede carga la imagen " + ex.Message, 3);
            //        }
            //    }
            //    ObservableCollection<Ent_Nivel> fact = new ObservableCollection<Ent_Nivel>();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        Ent_Nivel n = new Ent_Nivel();
            //        n.SN_NOM = dt.Rows[i]["NOMBRE"].ToString();
            //        n.SN_IMAGEN = this.Ent_Nivel.SN_IMAGEN;
            //        fact.Add(n);
            //    }
            //    var h = fact.FirstOrDefault();
            //    DataSubNiveles.Add(h);
            //    this.EnabledButtonAgregarGrupoPlatos = false;
            //    this.Operacion = "Editar";
            //}
            //else if (this.TipoDialog == "CambiarTextoSubNivel")
            //{
            //    int id = Ent_Nivel.ID_SN;
            //    DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().SN_NOM = TextoDialog;
            //    Editar();
            //}else if (this.TipoDialog == "CambiarTextoNivel")
            //{      
            //    Editar();
            //    NombreNivelSeleccionado = TextoDialog;
            //}
            //DialogHost.CloseDialogCommand.Execute(null,null);
        }
        public void Nuevo() 
        {
            TextoDialog = "";
            VisibleBotonEliminarNivel = "Collapsed";
            DataSubNiveles = new ObservableCollection<Ent_Nivel>();
            DataPlatosNivel = new ObservableCollection<Platos>();
            List<Ent_Nivel> n = new List<Ent_Nivel>();
            n = nstructure.GetNiveles(0);
            ObservableCollection<Ent_Nivel> o = new ObservableCollection<Ent_Nivel>(n);
            DataNiveles = o;

            NombreNivel = "";
            TituloDialog = "Ingrese el nombre del nuevo nivel";
            TipoDialog = "NuevoNivel";

            this.openDialog = true;
            EnabledButtonAgregarOpcion = false;
            EnabledButtonAgregarGrupoPlatos = false;

            DataSeleccionNiveles.Where(w => w.ID_SS == 1).FirstOrDefault().selec_ischeck = true;
            DataPlatosNivel = new ObservableCollection<Platos>();
            ListCollectionView lcv = new ListCollectionView(DataPlatosNivel);
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
            Ent_Nivel.ID = 0;
        }
        public void getLista( int op) 
        {
            try
            {
                List<Ent_Nivel> n = new List<Ent_Nivel>();
                n = nstructure.GetNiveles(op);
                ObservableCollection<Ent_Nivel> o = new ObservableCollection<Ent_Nivel>(n);
                DataNiveles = o;

                List<Ent_Nivel> s = new List<Ent_Nivel>();
                s = nstructure.GetSeleccionNiveles();
                ObservableCollection<Ent_Nivel> _s = new ObservableCollection<Ent_Nivel>(s);
                DataSeleccionNiveles = _s;
                EstadoBotones();

                DataSeleccionNiveles.Select(ss => { ss.selec_ischeck = false; return ss; }).ToList();
                DataSeleccionNiveles.Where(w => w.ID_SS == 1).FirstOrDefault().selec_ischeck = true;



                if (op == 1) { Ent_Nivel.ID = DataNiveles.FirstOrDefault().ID; }
                //argarDatosDeNivel(Ent_Nivel.ID,NombreNivelSeleccionado);
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
        }
        public void EstadoBotones() 
        {
            this.EnabledButtonNuevo = true;
            this.EnabledButtonAgregarOpcion = false;
            this.EnabledButtonAgregarGrupoPlatos = false;
            this.EnabledButtonEditar = false;
            VisibilityMenuSubNivel = "Hidden";
            VisibleBotonEliminarNivel = "Hidden";
        }
    }
}
