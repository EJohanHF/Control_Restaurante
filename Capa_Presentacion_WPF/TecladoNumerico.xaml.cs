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
    public partial class TecladoNumerico : UserControl
    {
        public static TextBox txtbox = new TextBox();
        public static PasswordBox txtpass = new PasswordBox();

        public static bool punto = true;

        public TecladoNumerico()
        {
            InitializeComponent();
        }
        public static Control _focusedControl;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            
            Int32 selectStart = txtbox.SelectionStart;
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
                        txtbox.Text = txtbox.Text.Insert(selectStart, bt.Content.ToString());
                        txtbox.SelectionStart = (selectStart + 1);
                    }
                }

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (punto)
            {
                btnPunto.Visibility = Visibility.Visible;
            }
            else
            {
                btnPunto.Visibility = Visibility.Hidden;
            }
        }
    }
}
