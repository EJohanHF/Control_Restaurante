using Capa_Entidades.Models.Carta;
using Capa_Negocio.Carta;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
   
   
    public class DialogDescripcionesViewModel : IGeneric
    {

        Neg_DetDescripciones negdetdesc = new Neg_DetDescripciones();

        Neg_Descripciones negdesc = new Neg_Descripciones();
        public ObservableCollection<DetDescripciones> DatasDetDescripcionesGeneral { get; set; }
        public ObservableCollection<DetDescripciones> DatasDetDescripcionesNoGeneral { get; set; }
        public ObservableCollection<DetDescripciones> DatasDetDescripcionesNoGeneral2 { get; set; }
        public ObservableCollection<Descripciones> DatasDescripciones { get; set; }
        public ObservableCollection<Descripciones> DatasDescripciones_title { get; set; }
        public ObservableCollection<DetDescripciones> DatasDetDescripciones { get; set; }
        public ICollectionView ContractsListCollectionView { get; private set; }
        public ICommand GuardarComentario { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Limpiar { get; set; }
        public string textoEscritura { get; set; }
        public ICommand clickup { get; set; }
        public ICommand clickup2 { get; set; }
        public ICommand clickup3 { get; set; }
        //Application.Current.Properties["Comentario"]
        public DialogDescripcionesViewModel() 
        {
            DatasDetDescripcionesNoGeneral = new ObservableCollection<DetDescripciones>();
            DatasDescripciones = negdesc.GetDes();
            DatasDetDescripciones = negdetdesc.GetDetDes();

            DatasDetDescripcionesNoGeneral = new ObservableCollection<DetDescripciones>(negdetdesc.GetDetDes().Where(d => d.DP_ID_DESC != 1));
            DatasDetDescripcionesGeneral = new ObservableCollection<DetDescripciones>(negdetdesc.GetDetDes().Where(d => d.DP_ID_DESC == 1));

            DatasDescripciones_title = new ObservableCollection<Descripciones>(negdesc.GetDes().Where(d => d.ID != 1));

            //List<DetDescripciones> h = DatasDetDescripciones.GroupBy(c => c.DP_ID_DESC)
            //       .Select(grp => grp.First()).Where(w => w.DP_ID_DESC != 1).ToList();
            //ObservableCollection<DetDescripciones> o = new ObservableCollection<DetDescripciones>(h);
            //DatasDetDescripcionesNoGeneral2 = o;

            ListCollectionView lcv = new ListCollectionView(DatasDetDescripcionesNoGeneral);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("TITLE_DESCRIPTION"));

            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);

            

            this.clickup = new ParamCommand(new Action<object>(eventos));
            this.clickup2 = new ParamCommand(new Action<object>(eventos2));
            this.clickup3 = new ParamCommand(new Action<object>(eventos3));
            this.GuardarComentario = new ParamCommand(new Action<object>(GrabarComentario));
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
            this.Limpiar = new RelayCommand(new Action(LimpiarTexto));
            if (Application.Current.Properties["Texto"] != null)
            {
                this.textoEscritura = Application.Current.Properties["Texto"].ToString();
            }
            else
            {
                this.textoEscritura = "";
            }
        }
        public void CerrarDialogo()
        {
            Application.Current.Properties["Comentario"] = null;
            Application.Current.Properties["Length"] = null;
            Application.Current.Properties["Texto"] = null;
            DialogHost.CloseDialogCommand.Execute(null,null);
        }
        public void LimpiarTexto()
        {
            textoEscritura = "";
            Application.Current.Properties["Length"] = textoEscritura.ToString().Length;
            Application.Current.Properties["Texto"] = textoEscritura;
        }
        public void GrabarComentario(object parameter)
        {
            var textBox = parameter as TextBox;
            var text = textBox.Text;
            Application.Current.Properties["Comentario"] = text;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void eventos(object id)
        {
          String descripciones = DatasDetDescripcionesGeneral.Where(w => w.ID == Convert.ToInt32(id)).First().DET_DESCRIPTION;
            if (textoEscritura == null || textoEscritura == "" || Application.Current.Properties["Texto"].ToString() == "")
            {
                textoEscritura = textoEscritura + " " + descripciones;
                Application.Current.Properties["Length"] = textoEscritura.ToString().Length;
                Application.Current.Properties["Texto"] = textoEscritura;
            }
            else if(Application.Current.Properties["Texto"].ToString() != "")
            {
                textoEscritura = Application.Current.Properties["Texto"].ToString() + " / " + descripciones;
                Application.Current.Properties["Length"] = textoEscritura.ToString().Length;
                Application.Current.Properties["Texto"] = textoEscritura;
            }
        }
       private void eventos2(object id)
        {
            if (Application.Current.Properties["Texto"] != null)
            {
                String descripciones = DatasDetDescripcionesNoGeneral.Where(w => w.ID == Convert.ToInt32(id)).First().DET_DESCRIPTION;
                textoEscritura = Application.Current.Properties["Texto"].ToString() + " " + descripciones;
                Application.Current.Properties["Texto"] = textoEscritura;
                Application.Current.Properties["Length"] = textoEscritura.ToString().Length;
            }
            else
            {
                String descripciones = DatasDetDescripcionesNoGeneral.Where(w => w.ID == Convert.ToInt32(id)).First().DET_DESCRIPTION;
                textoEscritura = textoEscritura + "  " + descripciones;
                Application.Current.Properties["Length"] = textoEscritura.ToString().Length;
                Application.Current.Properties["Texto"] = textoEscritura;
            }
           
        }
        private void eventos3(object id)
        {
            
            textoEscritura = "DE";
        }








    }





}
