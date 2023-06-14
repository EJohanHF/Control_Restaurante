using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Nivel;
using Capa_Negocio.Nivel;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Niveles;
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
    public class NivelGrupoDialogViewModel : IGeneric
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
        static public ObservableCollection<Grupo> TextoPlatos { get; set; }
        public static ObservableCollection<Grupo> DataPlatosSeleccionados { get; set; }
        #endregion
        #region Command
        public ICommand AceptarDialogCommand { get; set; }
        public ICommand TraercheckCommand { get; set; }
        public ICommand BorrarTextoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion
        public NivelGrupoDialogViewModel()
        {
            this.AceptarDialogCommand = new RelayCommand(new Action(Aceptar));
            this.TraercheckCommand = new ParamCommand(new Action<object>(Traercheck));
            this.BorrarTextoCommand = new RelayCommand(new Action(BorrarTexto));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            //TextoPlatos = new ObservableCollection<Grupo>();
            if (TextoPlatos == null) {
                TextoPlatos = new ObservableCollection<Grupo>();
            }
            this.Operacion = "Lista";
            foreach (var f in TextoPlatos) {
                DataGrupos.Where(w => w.idgrup == f.idgrup).FirstOrDefault().ischeck = true;
            }
        }
        public void Cancelar()
        {
            //TextoPlatos = new ObservableCollection<Grupo>();
            Application.Current.Properties["Est_Aceptar_Nivel_Carta_Dialog"] = "False";
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public void Traercheck(object id)
        {
            if (TextoPlatos == null) {
                TextoPlatos = new ObservableCollection<Grupo>();
            }
            int _id = (int)id;
            bool ischeck = this.DataGrupos.Where(w => w.idgrup == _id).FirstOrDefault().ischeck;
            string idGrupo = this.DataGrupos.Where(w => w.idgrup == _id).FirstOrDefault().idgrup.ToString();
            if (ischeck == false)
            {
                this.DataGrupos.Where(w => w.idgrup == _id).FirstOrDefault().ischeck = false;
                IsCheckedPlato = false;
                TextoPlatos.Remove(TextoPlatos.Where(w => w.idgrup == _id).FirstOrDefault());;
                NivelViewModel.DataPlatosNivel.Remove(NivelViewModel.DataPlatosNivel.Where(w => w.idgrup == _id.ToString()).FirstOrDefault());
            }
            else
            {
                this.DataGrupos.Where(w => w.idgrup == _id).FirstOrDefault().ischeck = true;
                string nombre_plato = this.DataGrupos.Where(w => w.idgrup == _id).FirstOrDefault().nomgrup;
                string nombre_grupo = this.DataGrupos.Where(w => w.idgrup == Convert.ToInt32(idGrupo)).FirstOrDefault().nomgrup;
                Grupo g = new Grupo();
                g.idgrup = Convert.ToInt32(idGrupo);
                g.nomgrup = nombre_grupo;
                g.ischeck = ischeck;
               
                Platos p = new Platos();
                p.idplato = _id;
                p.nomplato = nombre_plato;
                p.idgrup = idGrupo;
                p.nomgrup = nombre_grupo;
                p.ischeck = ischeck;
                TextoPlatos.Add(g);
            }
        }

        public void BorrarTexto()
        {
            TextoPlatos = new ObservableCollection<Grupo>();
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
