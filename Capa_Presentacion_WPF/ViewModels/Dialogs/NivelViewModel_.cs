using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Nivel;
using Capa_Negocio.Carta;
using Capa_Negocio.Nivel;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Niveles;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class NivelViewModel_ : IGeneric
    {
        NivelStructure nstructure = new NivelStructure();
       
        #region Negocio
        Neg_Nivel negNivel = new Neg_Nivel();
        Neg_Platos negPlatos = new Neg_Platos();
        #endregion
        #region Objetos
        private int c = 0;
        private string _oper;
        public int ID_NIVEL { get; set; }
        public string NombreNivel { get; set; }
        public string tipo_nivel { get; set; }
        public string TEXTO_MUESTRA { get; set; }
        private Ent_Nivel _Ent_Nivel;

        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => _oper;
            set
            {
                if (value == "Lista") { getLista(); }
                _oper = value;
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
        #region Listas
        public ObservableCollection<Ent_Nivel> DataNiveles { get; set; }
        public ObservableCollection<Ent_Nivel> DataSubNiveles { get; set; }
        static public ObservableCollection<Ent_Nivel> TextoNivel { get; set; }
        public ObservableCollection<MenuItemViewModelTotal> Items { get; set; }
        public ICollectionView ContractsListCollectionView { get; set; }
        public ObservableCollection<Platos> DataPlatosNivel { get; set; }
        public ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
        public List<Platos> DataPlatos = new List<Platos>();

        #endregion
        #region Command
        public ICommand CerrarDialogCommand { get; set; }
        public ICommand ObtenerTextoCommand { get; set; }
        public ICommand BorrarTextoCommand { get; set; }
        public ICommand CerrarYBorrarDialogCommand { get; set; }
        #endregion
        public NivelViewModel_()
        {
            if (Application.Current.Properties["ID_NIVEL_LDRP"] != null)
            {
                this.CerrarDialogCommand = new RelayCommand(new Action(CerrarDialog));
                this.ObtenerTextoCommand = new ParamCommand(new Action<object>(ObtenerTexto));
                this.BorrarTextoCommand = new RelayCommand(new Action(BorrarTexto));
                this.CerrarYBorrarDialogCommand = new RelayCommand(new Action(CerrarYBorrar));

                this.ID_NIVEL = Convert.ToInt32(Application.Current.Properties["ID_NIVEL_LDRP"].ToString());
                this.Ent_Nivel = new Ent_Nivel();
                this.DataSubNiveles = new ObservableCollection<Ent_Nivel>();
                this.DataNiveles = new ObservableCollection<Ent_Nivel>();
                TextoNivel = new ObservableCollection<Ent_Nivel>();
                this.DataPlatosNivel = new ObservableCollection<Platos>();
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                this.Operacion = "Lista";
            }
        }
        public void CerrarYBorrar() {
            BorrarTexto();
            if (Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString() == "")
            {
                Application.Current.Properties["SEGUNDO_SOLO"] = true;
            }
            else
            {
                Application.Current.Properties["SEGUNDO_SOLO"] = false;
            }
            Application.Current.Properties["NO_PASAR_NIVEL"] = "NO";
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public void BorrarTexto()
        {
            try
            {
                c = 0;
                TextoNivel = new ObservableCollection<Ent_Nivel>();
                TEXTO_MUESTRA = "";
                DataSubNiveles.FirstOrDefault().cant = 0;
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                if (DataPlatosNivel == null || DataPlatosNivel == null)
                {
                    Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                    //return;
                }
                if (DataSubNiveles.Count == 0 || DataPlatosNivel.Count == 0)
                {
                    Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                    //return;
                }
                //DataPlatosNivel.Where(w => w.cant > 0).FirstOrDefault().cant = 0;
                foreach (Ent_Nivel j in DataSubNiveles)
                {
                    DataSubNiveles.Where(w => w.ID_SN == j.ID_SN).FirstOrDefault().cant = 0;
                }
                foreach (Platos p in DataPlatosNivel)
                {
                    DataPlatosNivel.Where(w => w.idplato == p.idplato).FirstOrDefault().cant = 0;
                }
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                //DataPlatosNivel = new ObservableCollection<Platos>(); 
            }
            catch (Exception ex)
            {
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
            }
        }
        public void ObtenerTexto(object ID) 
        {
            //
            int id = Convert.ToInt32(ID);
            string texto = "";
            int id_nivel = 0;
            int tipo_seleccion = DataNiveles.Where(w => w.ID == ID_NIVEL).FirstOrDefault().N_TIPO_SELEC;
            if (tipo_seleccion == 5)
            {
                texto = DataPlatos.Where(w => w.idplato == id).Select(s => s.nomplato).FirstOrDefault();
                id_nivel = ID_NIVEL;
            } else if (tipo_seleccion == 6)
            {
                texto = DataPlatos.Where(w => w.idplato == id).Select(s => s.nomplato).FirstOrDefault();
                id_nivel = ID_NIVEL;
            }
            else
            {
                texto = DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().SN_NOM;
                id_nivel = DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().SN_ID_NIVEL;

            }


            if (tipo_seleccion == 1) 
            {
                TEXTO_MUESTRA = texto;
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = TEXTO_MUESTRA;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else if (tipo_seleccion == 2)
            {
                c = c + 1;
                TEXTO_MUESTRA = c + " " + texto;
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = TEXTO_MUESTRA;
            }
            else if (tipo_seleccion == 3 || tipo_seleccion == 4)
            {
                int cantidad_inicial = DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().cant;
                DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().cant = cantidad_inicial + 1;
                int cantidad_final = DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().cant;
                TextoNivel = new ObservableCollection<Ent_Nivel>();
                foreach (Ent_Nivel niv in DataSubNiveles)
                {
                    int count = DataSubNiveles.Where(w => w.ID_SN == id).Count();
                    if (count > 0) 
                    {
                        int uu = niv.cant;
                        if (uu > 0)
                        {
                            int k = TextoNivel.Where(w => w.ID_SN == id).Count();
                            if (k > 0)
                            {
                                TextoNivel.Where(w => w.ID_SN == id).FirstOrDefault().cant = c + 1;
                            }
                            else 
                            {
                                Ent_Nivel n = new Ent_Nivel();
                                n.ID_SN = niv.SN_ID_NIVEL;
                                n.SN_NOM = niv.SN_NOM;
                                n.cant = niv.cant;
                                TextoNivel.Add(n);
                            }
                        }
                    }
                }
                
                string mm = "";
                foreach (Ent_Nivel j in TextoNivel)
                {
                    if (mm == "")
                    {
                        mm = j.cant + " " + j.SN_NOM;
                    }
                    else 
                    {
                        mm = mm + " / " + j.cant + " " + j.SN_NOM;
                        //mm = mm + " / " + j.cant + " " + j.SN_NOM;
                    }
                    
                }
                TEXTO_MUESTRA = mm;

                //TEXTO_MUESTRA = n + " " + nom;
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = TEXTO_MUESTRA;
                
            }
            else if (tipo_seleccion == 5)
            {
                int cantidad_inicial = DataPlatosNivel.Where(w => w.idplato == id).FirstOrDefault().cant;
                DataPlatosNivel.Where(w => w.idplato== id).FirstOrDefault().cant = cantidad_inicial + 1;
                int cantidad_final = DataPlatosNivel.Where(w => w.idplato== id).FirstOrDefault().cant;
                //TextoNivel = new ObservableCollection<Ent_Nivel>();
                foreach (Platos niv in DataPlatosNivel)
                {
                    int count = DataPlatosNivel.Where(w => w.idplato == id).Count();
                    if (count > 0)
                    {
                        int uu = 0;
                        if (niv.idplato == id) { 
                            uu = niv.cant;
                        }
                        
                        if (uu > 0)
                        {
                            int k = TextoNivel.Where(w => w.ID_SN == id).Count();
                            if (k > 0)
                            {
                                TextoNivel.Where(w => w.ID_SN == id).FirstOrDefault().cant = c + 1;
                            }
                            else
                            {
                                Ent_Nivel n = new Ent_Nivel();
                                n.ID_SN = niv.idplato;
                                n.SN_NOM = niv.nomplato;
                                n.cant = niv.cant;
                                n.combo_imprimir = true;
                                TextoNivel.Add(n);
                            }
                        }
                    }
                }
                string mm = "";
                foreach (Ent_Nivel j in TextoNivel)
                {
                    if (mm == "")
                    {
                        //mm = j.cant + " " + j.SN_NOM;
                        mm = j.SN_NOM;
                    }
                    else
                    {
                        //mm = mm + " / " + j.cant + " " + j.SN_NOM;
                        mm = mm + " / " + j.SN_NOM;
                    }

                }
                TEXTO_MUESTRA = mm;

                //TEXTO_MUESTRA = n + " " + nom;
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = TEXTO_MUESTRA;
            }
            else if (tipo_seleccion == 6)
            {
                int cantidad_inicial = DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().cant;
                DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().cant = cantidad_inicial + 1;
                int cantidad_final = DataSubNiveles.Where(w => w.ID_SN == id).FirstOrDefault().cant;
                TextoNivel = new ObservableCollection<Ent_Nivel>();
                foreach (Ent_Nivel niv in DataSubNiveles)
                {
                    int count = DataSubNiveles.Where(w => w.ID_SN == id).Count();
                    if (count > 0)
                    {
                        int uu = niv.cant;
                        if (uu > 0)
                        {
                            int k = TextoNivel.Where(w => w.ID_SN == id).Count();
                            if (k > 0)
                            {
                                TextoNivel.Where(w => w.ID_SN == id).FirstOrDefault().cant = c + 1;
                            }
                            else
                            {
                                Ent_Nivel n = new Ent_Nivel();
                                n.ID_SN = niv.SN_ID_NIVEL;
                                n.SN_NOM = niv.SN_NOM;
                                n.cant = niv.cant;
                                TextoNivel.Add(n);
                            }
                        }
                    }
                }

                string mm = "";
                foreach (Ent_Nivel j in TextoNivel)
                {
                    if (mm == "")
                    {
                        mm = j.SN_NOM;
                    }
                    else
                    {
                        mm = mm + " " + j.SN_NOM;
                    }

                }
                TEXTO_MUESTRA = mm;

                //TEXTO_MUESTRA = n + " " + nom;
                Application.Current.Properties["TEXTO_NIVEL_LDRP"] = TEXTO_MUESTRA;
            }
        }
        public void CerrarDialog()
        {
            if (Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString() == "")
            {
                Application.Current.Properties["SEGUNDO_SOLO"] = true;
            }
            else
            {
                Application.Current.Properties["SEGUNDO_SOLO"] = false;
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public void getLista()
        {
            tipo_nivel = "Opcion";
            List<Ent_Nivel> sn = new List<Ent_Nivel>();
            sn = nstructure.GetSubNivelesxNivel(ID_NIVEL, 0);
            ObservableCollection<Ent_Nivel> _sn = new ObservableCollection<Ent_Nivel>(sn);
            DataSubNiveles = _sn;
            
            List<Ent_Nivel> n = new List<Ent_Nivel>();
            n = nstructure.GetNiveles(0);
            ObservableCollection<Ent_Nivel> _n = new ObservableCollection<Ent_Nivel>(n);
            DataNiveles = _n;
            int tip = DataNiveles.Where(w=> w.ID == ID_NIVEL).Select(s => s.N_TIPO_SELEC).FirstOrDefault();
            if (DataSubNiveles.Where(w => w.SN_ID_GRUPO == 0).Count() == 0) 
            {
                tipo_nivel = "Carta";
                if (tip != 5)
                {
                    foreach (Ent_Nivel s in DataSubNiveles)
                    {
                        Platos p = new Platos();
                        p.idgrup = s.SN_ID_GRUPO.ToString();
                        p.idplato = s.SN_ID_CARTA;
                        p.nomplato = s.CAR_NOM;
                        p.nomgrup = s.GR_NOM;
                        p.idcat = s.ID_SN.ToString();
                        DataPlatosNivel.Add(p);
                    }
                    ListCollectionView _lcv = new ListCollectionView(DataPlatosNivel);
                    _lcv.GroupDescriptions.Add(new PropertyGroupDescription("nomgrup"));
                    ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(_lcv);
                }
                else
                {
                    int[] idGrupos = null;
                    idGrupos = DataSubNiveles.Select(s => s.SN_ID_GRUPO).ToArray();
                    
                    platos = negPlatos.GetPlatos();
                    
                    DataPlatos = platos.Where(w => idGrupos.Contains(Convert.ToInt32(w.idgrup)) && w.estadoplato == 1).ToList();

                    foreach (Platos s in DataPlatos)
                    {
                        Platos p = new Platos();
                        p.idgrup = s.idgrup;
                        p.idplato = s.idplato;
                        p.nomplato = s.nomplato;
                        p.nomgrup = s.nomgrup;
                        p.idcat = s.idplato.ToString();
                        DataPlatosNivel.Add(p);
                    }
                    ListCollectionView _lcv = new ListCollectionView(DataPlatosNivel);
                    _lcv.GroupDescriptions.Add(new PropertyGroupDescription("nomgrup"));
                    ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(_lcv);
                }
                
            }
            //if (Application.Current.Properties["OP_LDRP"].ToString() == "Carta")
            //{
                
            //}
            this.NombreNivel = nstructure.GetNombreNivelSelect(ID_NIVEL);
        }
    }
}
