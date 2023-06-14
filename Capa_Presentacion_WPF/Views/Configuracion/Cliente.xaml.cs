using Capa_Presentacion_WPF.ViewModels.Configuracion.Clientes;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Runtime;
using System.Runtime.InteropServices;
namespace Capa_Presentacion_WPF.Views.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : UserControl
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        public Cliente()
        {
            InitializeComponent();
            this.DataContext = new ClienteViewModel();
            //bloqueo(false);
            cbxEstado.IsChecked = true;
            //btnComprobarDoc.IsEnabled = false;
        }

        //private async void BtnComprobarDoc_Click(object sender, RoutedEventArgs e)
        //{
        //    int Desc;
        //    string verificar = InternetGetConnectedState(out Desc, 0).ToString();
        //    if ((Convert.ToBoolean(verificar)== true))
        //    {
        //        lblestado.Visibility = System.Windows.Visibility.Collapsed;

        //        string nombrecompleto;
        //        ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();
        //       var cadena = txtndoc.Text;
        //        if (cboTipoDoc.SelectedValue.ToString() == "1")
        //        {
        //            if (cadena != "" && cadena.Length == 8)
        //            {
        //                try
        //                {
        //                    nombrecompleto = vf.ConsultaDNI(txtndoc.Text);

        //                    string[] partes = nombrecompleto.Split(' ');
        //                    string result = partes[0] + ' ' + partes[1];
        //                    string result1 = partes[2] + ' ' + partes[3];
        //                    //txtapecli.Text = result;
        //                    txtnomcli.Text = result1;
        //                }
        //                catch (Exception ex)
        //                {
        //                    var SiNo = new SiNoMessageDialog
        //                    {
        //                        Mensaje = { Text = "Problemas al cargar: " + ex.Message.ToString() + "" }
        //                    };
        //                    var x = await DialogHost.Show(SiNo, "RootDialog");
        //                    if (!(bool)x)
        //                        txtndoc.Clear();
        //                    txtndoc.Focus();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                txtndoc.Focus();
        //                return;
        //            }
        //        }
        //        else if (cboTipoDoc.SelectedValue.ToString() == "2")
        //        {
        //            if (cadena != "" && cadena.Length == 11)
        //            {
        //                try
        //                {
        //                    string valor;
        //                    ServiceReference1.ClsClienteConsultaEN cn;
        //                    cn = vf.ConsultaRuc("0a682fbe-009d-4758-aad1-2ff1092ab7c2-838d6cd5-620a-4add-9632-a3b37c5ae216", txtndoc.Text);
        //                    txtnomcli.Text = cn.nombre_o_razon_social;
        //                    txtdir.Text = cn.direccion_completa;
        //                    valor = cn.estado_del_contribuyente;
        //                    if (valor.ToString() == "ACTIVO")
        //                    {
        //                        cbxEstado.IsChecked = true;
        //                    }
        //                    else
        //                    {
        //                        cbxEstado.IsChecked = false;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    var SiNo = new SiNoMessageDialog
        //                    {
        //                        Mensaje = { Text = "Problemas al cargar: " + ex.Message.ToString() + "" }
        //                    };
        //                    var x = await DialogHost.Show(SiNo, "RootDialog");
        //                    if (!(bool)x)
        //                        txtndoc.Clear();
        //                    txtndoc.Focus();
        //                    return;
        //                }

        //            }
        //            else
        //            {
        //                txtndoc.Focus();
        //                return;
        //            }
                        
                    
        //        }
        //    }
        //    else
        //    {
        //        lblestado.Visibility= System.Windows.Visibility.Visible;
        //    }


        //}
        private void bloqueo(bool t)
        {
            txtnomcli.IsEnabled = t;
            //txtapecli.IsEnabled = t;
            txtndoc.IsEnabled = t;
            txtcorreo.IsEnabled = t;
            cbxEstado.IsEnabled = t;
            txtdistr.IsEnabled = t;
            txtcalle.IsEnabled = t;
            txtref.IsEnabled = t;
            txttel.IsEnabled = t;
            txttel.IsEnabled = t;
        }
        private void ClearText()
        {
            txtnomcli.Clear();
            //txtapecli.Clear();
            txtcorreo.Clear();
            txtdistr.Clear();
            txtcalle.Clear();
            txtref.Clear();
            txtndoc.Clear();
            txttel.Clear();
        }
        //private void CboTipoDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cboTipoDoc.SelectedItem!=null)
        //    {
        //        lblndoc.Visibility = System.Windows.Visibility.Collapsed;
        //        cbxEstado.IsChecked = true;
        //        bloqueo(true);
        //        if (cboTipoDoc.SelectedValue==null)
        //        {
        //            cboTipoDoc.SelectedValue = 1;
        //        }
        //        if (cboTipoDoc.SelectedValue.ToString() == "1")
        //        {
        //            btnComprobarDoc.IsEnabled = true;
        //            //ClearText();
        //            txtndoc.Clear();
        //            txtndoc.IsEnabled = true;
        //            txtndoc.Focus();
        //            txtndoc.MaxLength = 8;
        //            lblestado.Visibility = System.Windows.Visibility.Collapsed;
        //        }
        //        if (cboTipoDoc.SelectedValue.ToString() == "2")
        //        {
        //            btnComprobarDoc.IsEnabled = true;
        //            //();
        //            txtndoc.Clear();
        //            txtndoc.IsEnabled = true;
        //            txtndoc.Focus();
        //            txtndoc.MaxLength = 11;
        //        }
        //        if (cboTipoDoc.SelectedValue.ToString() == "3")
        //        {
        //            btnComprobarDoc.IsEnabled = false;
        //            //ClearText();
        //            txtndoc.Clear();
        //            txtndoc.IsEnabled = true;
        //            txtndoc.Focus();
        //            txtndoc.MaxLength = 12;
        //            lblestado.Visibility = System.Windows.Visibility.Collapsed;
        //        }
        //        if (cboTipoDoc.SelectedValue.ToString() == "4")
        //        {
        //            btnComprobarDoc.IsEnabled = false;
        //            //ClearText();
        //            txtndoc.IsEnabled = true;
        //            txtndoc.Focus();
        //            txtndoc.Clear();
        //            txtndoc.MaxLength = 12;
        //            lblestado.Visibility = System.Windows.Visibility.Collapsed;
        //        }
        //        if (cboTipoDoc.SelectedValue.ToString() == "5")
        //        {
        //            btnComprobarDoc.IsEnabled = false;
        //            //ClearText();
        //            txtndoc.Clear();
        //            txtndoc.IsEnabled = true;
        //            txtndoc.Focus();
        //            txtndoc.MaxLength = 15;
        //            lblestado.Visibility = System.Windows.Visibility.Collapsed;
        //        }
        //    }
        //    return;
        //}
        private void Txtndoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (cboTipoDoc.SelectedItem !=null)
                {
                    if (cboTipoDoc.SelectedValue.ToString() == "1")
                    {
                        string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
                        ((TextBox)sender).Text = s;

                    }
                    if (cboTipoDoc.SelectedValue.ToString() == "2")
                    {
                        string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
                        ((TextBox)sender).Text = s;
                    }
                }
                return;
                
            }
            catch (Exception)
            {
                return;
            }
            
            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cboTipoDoc.SelectedItem != null)
            {
                if (!string.IsNullOrWhiteSpace(txtndoc.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtnomcli.Text))
                    {
                        return;
                    }
                    else
                        lblnom.Visibility = System.Windows.Visibility.Visible;

                }
                else
                    lblndoc.Visibility = System.Windows.Visibility.Visible;


            }
            else lbltdoc.Visibility = System.Windows.Visibility.Visible;
        }

        private void Txtndoc_KeyUp(object sender, KeyEventArgs e)
        {
            lblndoc.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtnomcli_KeyUp(object sender, KeyEventArgs e)
        {
            lblnom.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txttel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void btnComprobarDoc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
