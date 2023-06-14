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
using System.Windows.Shapes;
using System.Data;
using Capa_Entidades.Acceso;
using Capa_Negocio.Acceso;

namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para Corporacion.xaml
    /// </summary>
    /// 
    public partial class Corporacion : Window
    {

        Neg_Corporacion objNegCor = new Neg_Corporacion();
        Ent_Corporacion objEntCor = new Ent_Corporacion();

        private bool Editar = false;

        public Corporacion()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Neg_Corporacion neg_corp = new Neg_Corporacion();
            String mensaje = "";
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                    Ent_Corporacion ent_corp = neg_corp.Registrar(txtNombre.Text, ref mensaje);
                    if (mensaje != "")
                    {
                        MessageBox.Show(mensaje, "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Se registro '" + txtNombre.Text + " 'Correctamente", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimpiarForm();
                        ListarCorporacion();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex);
                }
            }

            //ELIMINAR
            if (Editar == true)
            {
                DataRowView rowview = DGCorporacion.SelectedItem as DataRowView;
                int _id = Convert.ToInt32(rowview.Row[0]);
                try
                {
                    Ent_Corporacion ent_corp = neg_corp.Actualizar(txtNombre.Text, _id, ref mensaje);
                    if (mensaje != "")
                    {
                        MessageBox.Show(mensaje, "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Se Edito Correctamente", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimpiarForm();
                        ListarCorporacion();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex);
                }

                LimpiarForm();
                ListarCorporacion();
                Editar = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCorporacion();
           
        } 

        void ListarCorporacion()
        {
            DataTable dt = objNegCor.N_listado();
            DGCorporacion.ItemsSource = dt.DefaultView;
            DGCorporacion.FontSize = 25;
        }
        void LimpiarForm()
        {
            txtNombre.Text = "";
            txtNombre.Focus();
        }

        private void Button_Eliminar(object sender, RoutedEventArgs e)
        {
            Neg_Corporacion neg_corp = new Neg_Corporacion();
            String mensaje = "";
            try
            {

                if (DGCorporacion.SelectedItems.Count > 0)
                {
                    DataRowView rowview = DGCorporacion.SelectedItem as DataRowView;
                    int _id = Convert.ToInt32(rowview.Row[0]);
                    Ent_Corporacion ent_corp = neg_corp.Eliminar(_id, ref mensaje);
                    MessageBox.Show("Se Elimino Correctamente", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarForm();
                    ListarCorporacion();
                }
                else
                {
                    MessageBox.Show("Seleccione fila", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
        }

        private void Button_Actualizar(object sender, RoutedEventArgs e)
        {
            Neg_Corporacion neg_corp = new Neg_Corporacion();
            DataRowView rowview = DGCorporacion.SelectedItem as DataRowView;
            try
            {
                if (DGCorporacion.SelectedItems.Count > 0)
                {
                    Editar = true;
                    txtNombre.Text = Convert.ToString(rowview.Row[1]);
                }
                else
                {
                    MessageBox.Show("Seleccione fila", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
