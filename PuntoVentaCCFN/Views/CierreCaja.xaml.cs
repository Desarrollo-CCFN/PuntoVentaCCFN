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
        public CierreCaja()
        {
            InitializeComponent();
            CargarHeader();
        }

        public void CargarHeader()
        {
            var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
            string nombreCajaString = MainWindow.AppConfig1.Caja;
            string SucursalString = SettingSection.Filler;
            string NombreCompany = SettingSection.CompanyName;
            int nombreCajaInt = int.Parse(nombreCajaString);

            infoCaja = objCNVentaCaja.infoCaja(Nom_Cajera.Num_Cajera, SucursalString.Trim(), nombreCajaInt);

            //GridHeader.ItemsSource = obC.CargaHeader(infoCaja.IdCash).DefaultView;
            //GriDDetalle.ItemsSource = obC.CargarDetalle(infoCaja.IdCash).DefaultView;
        }

    }
}
