using Capa_Negocio.Funciones_Globales;
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

namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para TecladoNumerico.xaml
    /// </summary>
    public partial class Teclado : UserControl
    {
        public static TextBox txtbox = new TextBox();
        public static PasswordBox txtpass = new PasswordBox();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public static bool punto = true;

        public Teclado()
        {
            InitializeComponent();
        }
        public static Control _focusedControl;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;

            Int32 selectStart = txtbox.SelectionStart;
            try
            {
                if (selectStart == null)
                {
                    selectStart = 0;
                }
                //Int32 dd = txtpass.IsFocused

                //MessageBox.Show(Convert.ToString(selectStart));

                if (bt.Name == "btnBorrar")
                {

                    if (_focusedControl != null)
                    {
                        if (txtpass.Password.Length == 0) return;
                        txtpass.Password = txtpass.Password.Remove(txtpass.Password.Length - 1);

                        if (txtbox.Text.Length == 0) return;
                        txtbox.Text = txtbox.Text.Remove(txtbox.Text.Length - 1);
                    }
                    else
                    {
                        //borrar caracter
                        if (selectStart >= 1)
                        {
                            txtbox.Text = txtbox.Text.Remove(selectStart - 1, 1);
                            txtbox.SelectionStart = (selectStart - 1);
                            if (Application.Current.Properties["Length"] != null)
                            {
                                txtbox.SelectionStart = ((int)Application.Current.Properties["Length"] - 1);
                                Application.Current.Properties["Length"] = (int)Application.Current.Properties["Length"] - 1;
                                Application.Current.Properties["Texto"] = txtbox.Text;
                            }
                        }
                    }

                }
                else
                {
                    if (_focusedControl != null)
                    {
                        txtpass.Password = txtpass.Password + bt.Content.ToString();
                        txtbox.Text = txtbox.Text + bt.Content.ToString();
                    }
                    else
                    {
                        //escritura
                        if (selectStart >= 0)
                        {
                            if (Application.Current.Properties["Length"] != null)
                            {
                                txtbox.Text = txtbox.Text.Insert((int)Application.Current.Properties["Length"], bt.Content.ToString());
                                txtbox.SelectionStart = ((int)Application.Current.Properties["Length"] + 1);
                                Application.Current.Properties["Length"] = (int)Application.Current.Properties["Length"] + 1;
                                Application.Current.Properties["Texto"] = txtbox.Text;
                            }
                            else
                            {
                                txtbox.Text = txtbox.Text.Insert(selectStart, bt.Content.ToString());
                                txtbox.SelectionStart = (selectStart + 1);
                                Application.Current.Properties["Texto"] = txtbox.Text;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                selectStart = 0;
                Application.Current.Properties["Length"] = 0;
                txtbox.Focus();
            }
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (punto)
            {
                btnPunto2.Visibility = Visibility.Visible;
            }
            else
            {
                btnPunto2.Visibility = Visibility.Hidden;
            }
        }
    }
}
