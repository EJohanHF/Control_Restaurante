using Capa_Entidades.Models.Carta;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
    public class DocumentosViewModel : IGeneric
    {
        PlatosStructure directoryStructure = new PlatosStructure();

        #region Listas
        public ObservableCollection<Documentos> DataDocumentos { get; set; }
        public ObservableCollection<Documentos> Impresoras { get; set; }
        public ObservableCollection<PlatosItemViewModel> checkItems { get; set; }
        #endregion
        #region Negocio
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Platos negPlatos = new Neg_Platos();
        #endregion
        #region Entidad
        Documentos documentos = new Documentos();
        private Documentos _documentos;
        public Documentos Documentos
        {
            get => _documentos;
            set
            {
                _documentos = value;
            }
        }
        #endregion
        #region Objetos
        private string _operacion;
        public string Operacion
        {
            get => _operacion;
            set
            {
                if (value == "Lista")
                {
                    GetLista();
                }
                _operacion = value;
            }
        }
        #endregion
        #region Commands
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand TraercheckImpreCommand { get; set; }
        #endregion
        public DocumentosViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.TraercheckImpreCommand = new ParamCommand(new Action<object>(traercheck));
            this.Documentos = new Documentos();
        }
        private void traercheck(object idchek)
        {
            if (this.Impresoras.Where(w => w.DI_ID_IMP == (int)idchek).FirstOrDefault() == null)
            {
                this.Impresoras.Add(new Documentos() { DI_ID_IMP = (int)idchek });
            }
            else
            {
                this.Impresoras.Remove(this.Impresoras.Where(w => w.DI_ID_IMP == (int)idchek).FirstOrDefault());
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Documentos = this.DataDocumentos.Where(w => w.ID == (int)id).FirstOrDefault();

            this.Impresoras = negPlatos.GetImpresoraxDocumento_2((int)id);
            var children = directoryStructure.GetLogicalDrives();
            foreach (var item in children)
            {
                item.valor1 = false;
            }
            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                         children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, (bool)drive.valor1)));
            bool vacio = true;
            foreach (var imp in this.Impresoras)
            {
                vacio = false;
                Boolean yapaso = false;
                foreach (var item in children)
                {
                    if (imp.DI_ID_IMP == item.idchek && yapaso == false)
                    {
                        item.valor1 = true;
                        yapaso = true;
                    }
                    else
                    {

                    }
                }
            }
            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                     children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, (bool)drive.valor1)));
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private async void Guardar()
        {
            try
            {
                Documentos documentos = this.Documentos;
                var _id = documentos.ID;
                documentos.impresoras = (this.Impresoras == null ? "" : String.Join(",", this.Impresoras.Select(s => s.DI_ID_IMP).ToArray()));
                int result = negPlatos.SetDocumentos((_id == 0 ? 1 : 2), documentos);
                if (result != 0)
                {
                    this.Operacion = "Lista";
                }
                return;
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        private void GetLista()
        {
            DataDocumentos = negPlatos.GetDocumentos();
        }
    }
}
