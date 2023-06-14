using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models;
using Capa_Negocio.Acceso;
using Capa_Negocio.Principal;

namespace Capa_Presentacion_WPF
{
    public class MenuStructureTotal
    {
        Neg_Principal neg = new Neg_Principal();
        Neg_Login negLog = new Neg_Login();
        static List<MenuItem> menuItems2 = new List<MenuItem>();
        static List<Ent_Usuario> UsuItems = new List<Ent_Usuario>();

        public MenuStructureTotal()
        {
            menuItems2 = neg.GetMenu(0);
        }
        public List<MenuItem> GetLogicalDrives()
        {
            //var x = menuItems.Select(s => new { s.nombre, s.id }).ToList();
            return menuItems2.Where(w => w.idPadre == -1).ToList();
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
                items = menuItems2.Where(w => w.idPadre == idPadre).ToList();
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
                name = menuItems2.Where(w => w.id == _id).Select(s => s.nombre).FirstOrDefault().ToString();
            }
            catch { }
            return name;
        }
        public static int GetNumChildrens(int _id)
        {
            var numChildrens = 0;
            try
            {
                numChildrens = menuItems2.Where(w => w.idPadre == _id).Count();
            }
            catch { }
            return numChildrens;
        }

        public static string GetIconItem(int _id)
        {
            var icon = "FolderOutline";
            try
            {
                icon = menuItems2.Where(w => w.id == _id).Select(s => s.icono).FirstOrDefault().ToString();
            }
            catch { }
            return icon == "" ? (GetNumChildrens(_id) > 0 ? "FolderOutline" : "PaperOutline") : icon;
        }
        internal static string GetVista(int _id)
        {
            var vista = "";
            try
            {
                vista = menuItems2.Where(w => w.id == _id).Select(s => s.vista).FirstOrDefault().ToString();
            }
            catch { }
            return vista;
        }
        #endregion
    }
}
