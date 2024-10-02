using Capa_Presentacion.Views;
using System.Configuration;
using System.Windows;
using Capa_Presentacion;
using Newtonsoft.Json.Linq;
using System.IO;
using static PuntoVentaCCFN.MainWindow;


namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para configuracionApp.xaml
    /// </summary>
    public partial class configuracionApp : Window
    {
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
       
        public configuracionApp()
        { 
            InitializeComponent();
            configurationData();
        }

        void configurationData()
        {
           

            var SettingSection = AppConfig.GetSection("App_Preferences");
            this.DataContext = SettingSection;

          

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            string filePath = "C:\\PuntoVenta\\company.json";
            string json = File.ReadAllText(filePath);
            JObject jsons = JObject.Parse(json);
            AppConfig.Save();

            jsons["CompanyName"] = tbCodigoC.Text;
            jsons["Filler"] = tbListaPrecio.Text;
            jsons["Bd"] = tbSucursal.Text;
            jsons["DefCardCode"] = tbPuerto.Text;
            jsons["DefRateCash"] = tbCaja.Text;
            jsons["DefRateCredit"] = TCCredito.Text;
            jsons["DefCurrency"] = Moneda.Text;
            jsons["DefListNum"] = ListaPre.Text;

            jsons["Sucursal"] = tbCodigoC.Text;

            // Guardar los cambios en el archivo JSON
            File.WriteAllText(filePath, jsons.ToString());

            // Actualizar la configuración en AppConfig1
            AppConfig1.CompanyName = jsons["CompanyName"].ToString();
            AppConfig1.Filler = jsons["Filler"].ToString();
            AppConfig1.bd = jsons["Bd"].ToString();
            AppConfig1.DefCardCode = jsons["DefCardCode"].ToString();
            AppConfig1.DefRateCash = jsons["DefRateCash"].ToString();

            AppConfig1.DefRateCredit = jsons["DefRateCredit"].ToString();
            AppConfig1.DefCurrency = jsons["DefCurrency"].ToString();
            AppConfig1.DefListNum = jsons["DefListNum"].ToString();

            AppConfig1.Sucursal = jsons["CompanyName"].ToString();

            this.Close();

             
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
