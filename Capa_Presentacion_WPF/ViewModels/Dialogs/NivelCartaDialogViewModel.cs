using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Nivel;
using Capa_Negocio.Nivel;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Niveles;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class NivelCartaDialogViewModel : IGeneric
    {
        NivelStructure nstructure = new NivelStructure();
        #region Negocio
        Neg_Nivel negNivel = new Neg_Nivel();

        #endregion
        #region Objetos
        private string _oper;
        public string TEXTO_MUESTRA { get; set; }
        public Ent_Nivel _Ent_Nivel;
        public Grupo _GrupoSelected;
        public int idgrupo { get; set; }
        public bool IsCheckedPlato { get; set; }
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
        #region SELECCION DE OBJETOS
        public Grupo GrupoSelected
        {
            get => _GrupoSelected;
            set 
            {
                if (value != null)
                {
                    List<Platos> _p = new List<Platos>();
                    _p = nstructure.GetPlatosxGrupo(((Grupo)value).idgrup);
                    ObservableCollection<Platos> p = new ObservableCollection<Platos>(_p);
                    this.DataPlatos = p;
                    if (DataPlatosSeleccionados != null) 
                    {
                        foreach (Platos _pla in NivelViewModel.DataPlatosNivel) 
                        {
                            if (DataPlatos.Where(w => w.idplato == _pla.idplato).Count() != 0) 
                            {
                                DataPlatos.Where(w => w.idplato == _pla.idplato).First().ischeck = _pla.ischeck;
                            }
                        }
                    }
                    
                    //TextoPlatos = new ObservableCollection<Platos>();
                    //this.TEXTO_MUESTRA = "";
                    idgrupo = ((Grupo)value).idgrup;
                }
                _GrupoSelected = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<Ent_Nivel> DataNiveles { get; set; }
        public ObservableCollection<Ent_Nivel> DataSubNiveles { get; set; }
        public ObservableCollection<Grupo> DataGrupos { get; set; }
        public ObservableCollection<Platos> DataPlatos { get; set; }
        static public ObservableCollection<Platos> TextoPlatos { get; set; }
        public static ObservableCollection<Platos> DataPlatosSeleccionados { get; set; }
        #endregion
        #region Command
        public ICommand AceptarDialogCommand { get; set; }
        public ICommand TraercheckCommand { get; set; }
        public ICommand BorrarTextoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion
        public NivelCartaDialogViewModel()
        {
            this.AceptarDialogCommand = new RelayCommand(new Action(Aceptar));
            this.TraercheckCommand = new ParamCommand(new Action<object>(Traercheck));
            this.BorrarTextoCommand = new RelayCommand(new Action(BorrarTexto));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            TextoPlatos = new ObservableCollection<Platos>();
            this.Operacion = "Lista";
        }
        public void Cancelar() 
        {
            TextoPlatos = new ObservableCollection<Platos>();
            Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "False";
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public void Traercheck(object id) 
        {
            int _id = (int)id;
            bool ischeck = this.DataPlatos.Where(w => w.idplato == _id).FirstOrDefault().ischeck;
            string idGrupo = this.DataPlatos.Where(w => w.idplato == _id).FirstOrDefault().idgrup;
            if (ischeck == false)
            {
                this.DataPlatos.Where(w => w.idplato == _id).FirstOrDefault().ischeck = false;
                IsCheckedPlato = false;
                TextoPlatos.Remove(TextoPlatos.Where(w => w.idplato == _id).FirstOrDefault());
                NivelViewModel.DataPlatosNivel.Remove(NivelViewModel.DataPlatosNivel.Where(w => w.idplato == _id).FirstOrDefault());
                //TEXTO_MUESTRA = "";
                //foreach (Platos pl in TextoPlatos)
                //{
                //    TEXTO_MUESTRA = TEXTO_MUESTRA + ", " + pl.nomplato;
                //}
            }
            else {
                this.DataPlatos.Where(w => w.idplato == _id).FirstOrDefault().ischeck = true;
                string nombre_plato = this.DataPlatos.Where(w => w.idplato == _id).FirstOrDefault().nomplato;
                string nombre_grupo = this.DataPlatos.Where(w => w.idgrup == idgrupo.ToString()).FirstOrDefault().nomgrup;
                Platos p = new Platos();
                p.idplato = _id;
                p.nomplato = nombre_plato;
                p.idgrup = idGrupo;
                p.nomgrup = nombre_grupo;
                p.ischeck = ischeck;
                TextoPlatos.Add(p);
                //this.TEXTO_MUESTRA = "";
                //foreach (Platos pl in TextoPlatos)
                //{
                //    this.TEXTO_MUESTRA = TEXTO_MUESTRA + ", " + pl.nomplato;
                //}
            }
        }

        public void BorrarTexto() 
        {
            TextoPlatos = new ObservableCollection<Platos>();
            this.DataPlatos = new ObservableCollection<Platos>();
            this.TEXTO_MUESTRA = "";
        }
        public void Aceptar()
        {
            DataPlatosSeleccionados = TextoPlatos;
            Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "True";
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public void getLista()
        {
            List<Grupo> _g = new List<Grupo>();
            _g = nstructure.GetGrupos();
            ObservableCollection<Grupo> g = new ObservableCollection<Grupo>(_g);
            DataGrupos = g;
        }
    }
}
