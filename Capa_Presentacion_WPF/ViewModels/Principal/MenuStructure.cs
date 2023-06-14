using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models;
using Capa_Negocio.Acceso;
using Capa_Negocio.Principal;
using System.Collections.ObjectModel;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using System.Windows;

namespace Capa_Presentacion_WPF
{
    public class MenuStructure
    {
        Neg_Principal neg = new Neg_Principal();
        Neg_Login negLog = new Neg_Login();
        Neg_Empresa negEmpr = new Neg_Empresa();
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        static List<MenuItem> menuItems = new List<MenuItem>();
        static List<Ent_Usuario> UsuItems = new List<Ent_Usuario>();

        public MenuStructure()
        {
            if (Application.Current.Properties["IdEmpleadoUsuario"] != null)
            {
                menuItems = neg.GetMenu(int.Parse(System.Windows.Application.Current.Properties["IdRol"].ToString()));
                empresa = negEmpr.GetEmpresa();
            }
            else
            {
                empresa = negEmpr.GetEmpresa();
            }
        }
        public String ObtenerToken()
        {
            var TokenEmpresa_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
            String TokenEmpresa = TokenEmpresa_.token;
            return TokenEmpresa;
        }
        public String ObtenerCodigo()
        {
            var CodigoEmpresa_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
            String CodigoEmpresa = CodigoEmpresa_.codigo;
            return CodigoEmpresa;
        }
        public String ObtenerRuc()
        {
            var RucEmpresa_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
            String RucEmpresa = RucEmpresa_.empr_ruc;
            return RucEmpresa;
        }
        public void MenuStructureTotal()
        {
            menuItems = neg.GetMenu(0);
        }
        public List<MenuItem> GetLogicalDrives()
        {
            //var x = menuItems.Select(s => new { s.nombre, s.id }).ToList();
            return menuItems.Where(w => w.idPadre == -1).ToList();
        }

        public List<Ent_Usuario> GetUsuario()
        {
            //var x = menuItems.Select(s => new { s.nombre, s.id }).ToList();
            return UsuItems.ToList();
        }
        public static List<MenuItem> GetDirectoryContents(int idPadre)
        {
            var items = new List<MenuItem>();
            try
            {
                items = menuItems.Where(w => w.idPadre == idPadre).ToList();
            }
            catch { }
            return items;
            #region old
            //// Create empty list
            //var items = new List<DirectoryItem>();

            //#region Get Folders

            //// Try and get directories from the folder
            //// ignoring any issues doing so
            //try
            //{
            //    var dirs = Directory.GetDirectories(fullPath);

            //    if (dirs.Length > 0)
            //        items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            //}
            //catch { }

            //#endregion

            //#region Get Files

            //// Try and get files from the folder
            //// ignoring any issues doing so
            //try
            //{
            //    var fs = Directory.GetFiles(fullPath);

            //    if (fs.Length > 0)
            //        items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            //}
            //catch { }

            //#endregion

            //return items;
            #endregion
        }
        #region Helpers
        public static string GetFileFolderName(int _id)
        {
            var name = "";
            try
            {
                name = menuItems.Where(w => w.id == _id).Select(s => s.nombre).FirstOrDefault().ToString();
            }
            catch { }
            return name;
        }
        public static int GetNumChildrens(int _id)
        {
            var numChildrens = 0;
            try
            {
                numChildrens = menuItems.Where(w => w.idPadre == _id).Count();
            }
            catch { }
            return numChildrens;
        }
        public static string GetColorIconMenuItem(int _id)
        {
            var name = "";
            try
            {
                name = menuItems.Where(w => w.id == _id).Select(s => s.colorIcon).FirstOrDefault().ToString();
            }
            catch { }
            return name;
        }
        public static string GetIconItem(int _id)
        {
            var icon = "FolderOutline";
            try
            {
                icon = menuItems.Where(w => w.id == _id).Select(s => s.icono).FirstOrDefault().ToString();
            }
            catch { }
            return icon == "" ? (GetNumChildrens(_id) > 0 ? "FolderOutline" : "PaperOutline") : icon;
        }
        internal static string GetVista(int _id)
        {
            var vista = "";
            try
            {
                vista = menuItems.Where(w => w.id == _id).Select(s => s.vista).FirstOrDefault().ToString();
            }
            catch { }
            return vista;
        }
        #endregion
    }
}
