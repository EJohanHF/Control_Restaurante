using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Presentacion_WPF.Core;
using Capa_Presentacion_WPF.ViewModels;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Role;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF
{
    public class MenuItemViewModel : IGeneric
    {
        //#region Referencias
        //MenuStructure directoryStructure = new MenuStructure();
        //#endregion

        #region Public Properties
        //public ObservableCollection<MenuItemViewModel> Items { get; set; }   
        public int idPadre { get; set; } //id Padre
        public int id { get; set; } // id
        public bool value { get; set; } // value
        public string Name { get { return MenuStructure.GetFileFolderName(id); } } // nombre
        public string ColorIcon { get { return MenuStructure.GetColorIconMenuItem(id); } }
        public string Icon { set { return; } get { return MenuStructure.GetIconItem(id); } } //icon
        public string Vista { get { return MenuStructure.GetVista(id); } }
        public ObservableCollection<MenuItemViewModel> Children { get; set; }
        Neg_Roles negRoles = new Neg_Roles();
        public int idrol { get; set; }
        public bool IsExpanded
        {

            get
            {
                if (MenuStructure.GetNumChildrens(this.id) == 0)
                {
                    this.Children = null;
                }
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                if (value == true)
                {
                    Expand();

                }
                else
                {
                    this.ClearChildren();
                }
            }
        }
        public bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == true)
                    if (!string.IsNullOrEmpty(MenuStructure.GetVista(this.id)))
                        Selected();
                if (this.Vista == "Desbloquear Mesas")
                {
                    _isSelected = false;
                }
                else
                {
                    _isSelected = value;
                }
                
                return;
            }
        }
        #endregion

        #region Public Commands
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor
        //public MenuItemViewModel()
        //{
        //    var children = directoryStructure.GetLogicalDrives();
        //    this.Items = new ObservableCollection<MenuItemViewModel>(
        //        children.Select(drive => new MenuItemViewModel(drive.idPadre, drive.id)));
        //}
        public MenuItemViewModel(int idPadre, int id, bool value, int idrol)
        {
            //Messenger.Default.Register<string>(this, GetUserControl);

            this.ExpandCommand = new RelayCommand(Expand);
            this.idPadre = idPadre;
            this.id = id;
            this.value = value;
            this.idrol = idrol;
            this.ClearChildren();

        }
        #endregion

        #region Helper Methods
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<MenuItemViewModel>();
            this.Children.Add(null);

        }
        #endregion

        public List<Menu> Menu { get; set; }

        private void Expand()
        {

            var children = MenuStructure.GetDirectoryContents(this.id);
            foreach (var menu in children)
            {
                menu.value = false;
            }
            string _mensaje = "";
            this.Children = new ObservableCollection<MenuItemViewModel>(children.Select(content => new MenuItemViewModel(content.idPadre, content.id, (bool)content.value, (int)id)));
            this.Menu = negRoles.getMenuRol(idrol.ToString(), ref _mensaje);
            foreach (var menu in children)
            {


                Boolean yapaso = false;
                foreach (var menuBD in this.Menu)
                {
                    if (menu.id == menuBD.idmenu && yapaso == false)
                    {
                        menu.value = true;
                        yapaso = true;
                    }
                }
            }
            this.Children = new ObservableCollection<MenuItemViewModel>(children.Select(content => new MenuItemViewModel(content.idPadre, content.id, content.value, (int)id)));
            //new MenuStructureViewModel(this.Vista);
        }
        private void Selected()

        {
            if (this.Vista == "Desbloquear Mesas")
            {

            }
            else
            {
                Messenger.Default.Send(this.Vista);
            }
            

        }
    }
}
