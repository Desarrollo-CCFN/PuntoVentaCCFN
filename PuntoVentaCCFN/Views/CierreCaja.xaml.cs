using Capa_Entidad.OperacionesCaja;
using Capa_Negocio;
using Capa_Negocio.OperacionesCaja;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using static Capa_Presentacion.Views.LoginView;
using static PuntoVentaCCFN.MainWindow;

namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para CierreCaja.xaml
    /// </summary>
    public partial class CierreCaja : System.Windows.Controls.UserControl
    {
        readonly CN_CierreCaja obC = new CN_CierreCaja();
        readonly
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        readonly CN_VentaCaja objCNVentaCaja = new CN_VentaCaja();
        CE_VentaCaja infoCaja = new CE_VentaCaja();
        
        public int UserSupervisor;

        
         //public CierreCaja(int iSupervisor)
        public CierreCaja()
        {
            
            InitializeComponent();
            CargarHeader();

            tb_Qty_Debito.Text   = "0";
            tb_qty_Credito.Text  = "0";
            tb_totalDebito.Text  = "0";
            tb_TotalCredito.Text = "0";

        }

        public void CargarHeader()
        {
            var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
            string nombreCajaString = MainWindow.AppConfig1.Caja;
            string SucursalString = SettingSection.Filler;
            string NombreCompany = SettingSection.CompanyName;
            int nombreCajaInt = int.Parse(nombreCajaString);

            infoCaja = objCNVentaCaja.infoCaja(Nom_Cajera.Num_Cajera, SucursalString.Trim(), nombreCajaInt);

            if (infoCaja != null)
            {
                GridHeader.ItemsSource = obC.CargaHeader(infoCaja.IdCash).DefaultView;
                GriDDetalle.ItemsSource = obC.CargarDetalle(infoCaja.IdCash).DefaultView;
            }
            else
            {
                tb_Qty_Debito.IsEnabled = false;
                tb_qty_Credito.IsEnabled = false;
                tb_totalDebito.IsEnabled = false;
                tb_TotalCredito.IsEnabled = false;
                btn_Preview.IsEnabled = false;
                btn_Procesar.IsEnabled = false;

                System.Windows.MessageBox.Show("No existe caja abierta");

            }
        }

        private void btn_Preview_Click(object sender, RoutedEventArgs e)
        {
            CerrarCaja(true);
        }

        public void CerrarCaja(bool Preview)
        {
            
            Int32  iQtyDebito = 0;
            double totalDebito = 0;
            Int32  iQtyCredito = 0;
            double TotalCredito = 0;

            try
            {
                iQtyDebito = Convert.ToInt32(tb_Qty_Debito.Text);
                iQtyCredito = Convert.ToInt32(tb_qty_Credito.Text);
                totalDebito = Convert.ToDouble(tb_totalDebito.Text);
                TotalCredito = Convert.ToDouble(tb_TotalCredito.Text);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
                string nombreCajaString = MainWindow.AppConfig1.Caja;
                string SucursalString = SettingSection.Filler;
                string NombreCompany = SettingSection.CompanyName;
                int nombreCajaInt = int.Parse(nombreCajaString);

                infoCaja = objCNVentaCaja.infoCaja(Nom_Cajera.Num_Cajera, SucursalString.Trim(), nombreCajaInt);

                string CerrarCaja = "Y";
                if (Preview == true)
                {
                     CerrarCaja = "N";
                }
                

                if (infoCaja != null)
                {
                    string sMensaje = "";

                    if (obC.CierraCaja(infoCaja.IdCash, totalDebito, TotalCredito, iQtyDebito, iQtyCredito, UserSupervisor, CerrarCaja, ref sMensaje) == false)
                    {
                        System.Windows.MessageBox.Show(sMensaje);
                    }
                    else
                    {
                        if (CerrarCaja == "Y")
                        {
                            System.Windows.MessageBox.Show("Proceso Exitoso !!!");
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No existe caja abierta");
                }
            
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }

        }

        private void btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

        }

        private void btn_Procesar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Esta seguro de cerrar la caja", "Cierre de Caja", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            
            if (dialogResult == MessageBoxResult.Yes)
            {
                CerrarCaja(false);
            }
           
        }
    }
}
