using Capa_Presentacion.Views;
using System.Configuration;
using System.Windows;


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
         //   ((Capa_Presentacion.App_Preferences)SettingSection).NombreCaja.ToString();
        //    System.Windows.Forms.MessageBox.Show(((Capa_Presentacion.App_Preferences)SettingSection).NombreCaja.ToString());
            

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.Save();
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
