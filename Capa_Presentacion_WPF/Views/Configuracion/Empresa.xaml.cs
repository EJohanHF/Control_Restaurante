using Capa_Presentacion_WPF.ViewModels.Configuracion.Empleados;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
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
namespace Capa_Presentacion_WPF.Views.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Empresa.xaml
    /// </summary>
    public partial class Empresa : UserControl
    {
        public Empresa()
        {
            InitializeComponent();
            if (cboDepa.SelectedIndex > -1)
            {
                cboProv.Text = "";
                cboDist.Text = "";
            }
            this.DataContext = new EmpresaViewModel();
            //imagen();

            
            //listabox.Items.Add(txtcorreo.Text);

        }
        //private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    var currentChip = (Chip)sender;
        //    listabox.Items.Remove(currentChip);
        //}
        //private void BindUsers()
        //{
        //    for (var i = 0; i < 10; i++)
        //    {
        //        var chip = new Chip { IsDeletable = true };
        //        chip.DeleteClick += Chip_DeleteClick;
        //        chip.Content = txtcorreo.Text + i;
        //        chip.Icon = i;
        //        listabox.Items.Add(chip);
        //    }
        //}
        #region botones
        private void BtnCargarLogo_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog getimg = new OpenFileDialog();
            //getimg.InitialDirectory= "C:\\";
            //getimg.Filter = "Archivos de imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";

            //if (getimg.ShowDialog() == true)
            //{
            //    try
            //    {
            //        //string NombreArchivo;
            //        string RutaArchivo;
            //        RutaArchivo = getimg.FileName.ToString();
            //        //NombreArchivo = System.IO.Path.GetFileName(RutaArchivo);
            //        txtDirectorio.Text = RutaArchivo;
            //        //gbimagen = System.IO.Directory.GetCurrentDirectory();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: No se puede carga la imagen " + ex.Message);
            //    }
            //}
        }

        //public void imagen()
        //{
        //    string valor;
        //    valor = Convert.ToString(txtDirectorio.Text.Trim());
        //    BitmapImage bitmapImage = new BitmapImage(new Uri(valor));

        //}

        private void Txtruc_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtruc.MaxLength = 11;
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }



        private void Txttelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            txttelefono.MaxLength = 9;
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void CboDepa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cboDepa.SelectedIndex > -1)
            //{
            //    cboProv.Text = "";
            //    cboDist.Text = "";
            //}
        }

        private void Txtcorreo_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((int)e.Key == (int)Key.Enter)
            //{
            //    if (this.txtcorreo.Text != "")
            //    {

            //        listabox.Items.Add(this.txtcorreo.Text);
            //        this.txtcorreo.Focus();
            //        this.txtcorreo.Clear();

            //    }
            //}
        }

       
        private void CboTiCorp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblnomcorp.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CboPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblpais.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CboDepa_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            lbldep.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CboProv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblprov.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CboDist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbldist.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtruc_KeyUp(object sender, KeyEventArgs e)
        {
            lblruc.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtnomemp_KeyUp(object sender, KeyEventArgs e)
        {
            lblnomemp.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtnomcomer_KeyUp(object sender, KeyEventArgs e)
        {
            lblnomcomer.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtruc.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtnomemp.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtnomcomer.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txttelefono.Text))
                        {
                            if (cboTiCorp.SelectedItem != null)
                            {
                                if (cboPais.SelectedItem != null)
                                {
                                    if (cboDepa.SelectedItem != null)
                                    {
                                        if (cboProv.SelectedItem != null)
                                        {
                                            if (cboDist.SelectedItem != null)
                                            {
                                                if (!string.IsNullOrWhiteSpace(txturb.Text))
                                                {
                                                    //if (!string.IsNullOrWhiteSpace(txtcorreo.Text))
                                                    //{
                                                        if (imglogo.Source != null)
                                                        {
                                                            return;
                                                        }
                                                        else
                                                            lbllogo.Visibility = System.Windows.Visibility.Visible;
                                                    //}
                                                    //else
                                                    //    lblcor.Visibility = System.Windows.Visibility.Visible;
                                                }
                                                else
                                                    lblurb.Visibility = System.Windows.Visibility.Visible;
                                            }
                                            else
                                                lbldist.Visibility = System.Windows.Visibility.Visible;
                                        }
                                        else
                                            lblprov.Visibility = System.Windows.Visibility.Visible;
                                    }
                                    else
                                        lbldep.Visibility = System.Windows.Visibility.Visible;
                                }
                                else
                                    lblpais.Visibility = System.Windows.Visibility.Visible;
                            }
                            else lblnomcorp.Visibility = System.Windows.Visibility.Visible;
                        }
                        else lbltel.Visibility = System.Windows.Visibility.Visible;
                    }
                    else lblnomcomer.Visibility = System.Windows.Visibility.Visible;
                }
                else lblnomemp.Visibility = System.Windows.Visibility.Visible;
            }
            else lblruc.Visibility = System.Windows.Visibility.Visible;

        }

        private void txturb_KeyUp(object sender, KeyEventArgs e)
        {
            lblurb.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtcorreo_KeyUp(object sender, KeyEventArgs e)
        {
            lblcor.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnCargarLogo_Click_1(object sender, RoutedEventArgs e)
        {
            lbllogo.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txttelefono_KeyUp(object sender, KeyEventArgs e)
        {
            lbltel.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    string value = "";

        //    for (int i = 0; i < listabox.Items.Count; i++)
        //    {
        //        if (value != "")
        //        {
        //            value += ",";
        //        }
        //        value += listabox.Items[i].ToString();
        //    }
            
        //    string[] arr = value.Split(',');

        //}
        private void MainWindow(object sender, RoutedEventArgs e)
        {
            //BindUsers();
        }

        private void ButtonsDemoChip_OnDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonsDemoChip_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
