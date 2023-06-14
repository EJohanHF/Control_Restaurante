using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Capa_Entidades.Acceso;
using Capa_Negocio.Acceso;

namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para FilaMenus.xaml
    /// </summary>
    public partial class FilaMenus : UserControl
    {
        public FilaMenus()
        {
            InitializeComponent();
        }

        Neg_Modulos objNegMod = new Neg_Modulos();

        private void ListMenu_Loaded(object sender, RoutedEventArgs e)
        {
            listar_Submenu();
        }



        public void listar_Submenu()
        {
            
            DataTable dt = objNegMod.N_Menus();

            int cant_sub = 0;

            for (int f = 0; f < dt.Rows.Count; f++)
            {
                cant_sub = 0;
                //MENUS
                MenuItem newMenuItem1 = new MenuItem();
                newMenuItem1.Header = dt.Rows[f]["descripcion"].ToString();
                if (dt.Rows[f]["icon"].ToString() != "")
                {
                    Image imageMenu = new Image { Source = new BitmapImage(new Uri("Images/" + dt.Rows[f]["icon"].ToString(), UriKind.Relative)) };
                    newMenuItem1.Icon = imageMenu;
                }
                this.ListMenu.Items.Add(newMenuItem1);

                DataTable dti = objNegMod.N_SubMenus(f + 1);
                cant_sub = dti.Rows.Count;

                if (cant_sub > 0)
                {
                    for (int i = 0; i < cant_sub; i++)
                    {
                        //SUBMENUS
                        MenuItem newMenuItem2 = new MenuItem();
                        MenuItem newExistMenuItem = (MenuItem)this.ListMenu.Items[f];
                        newMenuItem2.Header = dti.Rows[i]["nombre"].ToString();
                        if (dti.Rows[i]["icon"].ToString() != "")
                        {
                            Image imageSubmenu = new Image { Source = new BitmapImage(new Uri("Images/" + dti.Rows[i]["icon"].ToString(), UriKind.Relative)) };
                            newMenuItem2.Icon = imageSubmenu;
                            imageSubmenu.Width = 15;
                            imageSubmenu.Height = 15;
                        }
                        newExistMenuItem.Items.Add(newMenuItem2);
                    }
                }
            }

        }

    }
}
