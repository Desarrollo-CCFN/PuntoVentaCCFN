using PuntoVentaCCFN.Views;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using Capa_Presentacion;
using Capa_Presentacion.SCS.Boxes;
using Capa_Presentacion.Reportes;
using System.Windows.Media.Animation;
using Capa_Presentacion.Views;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using static Capa_Presentacion.Views.LoginView;



  

namespace PuntoVentaCCFN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);



        public static class Retiro_Control
        {
            public static int Retiro_Acceso  { get; set; }
       
        }



        public MainWindow()
        {
            

            InitializeComponent();
            LoadJson();
            if (AppConfig.Sections["App_Preferences"] is null)
            {   
                AppConfig.Sections.Add("App_Preferences", new App_Preferences());
                AppConfig.Save();
            }

            // Realizar el casting adecuado a App_Preferences
            //var preferences = AppConfig.Sections[11] as App_Preferences;
            string Empresa = Nom_Cajera.Nom_Sucursal;     //preferences.CompanyName;     //Nombre de la empresa
            txtSucursal.Text = "Sucursal: " + Empresa+" Bienvenida/o: "+ Nom_Cajera.Nome_Cajera;

           

        }

         

        public void LoadJson()
        {
            try
            {
                if (File.Exists("C:\\PuntoVenta\\config.json"))
                {
                    using (StreamReader r = new StreamReader("C:\\PuntoVenta\\config.json"))
                    {

                        string json = r.ReadToEnd();
                        JObject jsons = JObject.Parse(json);

                        AppConfig1.IP = jsons["IP"].ToString();
                        AppConfig1.Sucursal = jsons["Sucursal"].ToString();
                        AppConfig1.Puerto = jsons["Puerto"].ToString();
                        AppConfig1.Caja = jsons["Caja"].ToString();
                        AppConfig1.Copia = jsons["Copia"].ToString();


                    }
                }
                else
                {
                    if (!Directory.Exists("C:\\PuntoVenta"))
                    {
                        Directory.CreateDirectory("C:\\PuntoVenta");
                    }
                    var _data = new { IP = "192.168.0.0", Sucursal = "Ensenada Mayoreo", Puerto = "12000", Caja = "1", Copia = "1" };



                    string json = JsonConvert.SerializeObject(_data);
                    File.WriteAllText(@"C:\\PuntoVenta\\config.json", json);

                    // System.Windows.Forms.MessageBox.Show("Se ha creado un archivo de configuracion en el disco local, C:\\PuntoVenta ", "Configuracion");
                    System.Windows.Forms.MessageBox.Show("Se creo la configuracion, salir y volver entrar ", "Configuracion");
                    Cerrar(this, new RoutedEventArgs());

                }
            }
            catch (Exception EX)
            {
                System.Windows.Forms.MessageBox.Show("Error en ejecucion");
            }
        }



        public static class AppConfig1
        {
            public static string IP { get; set; }
            public static string Sucursal { get; set; }
            public static string Puerto { get; set; }

            public static string Caja { get; set; }

            public static string Copia { get; set; }

        }



        private void TBShow(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 0.5;
        }

        private void TBHide(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 1;
        }

        private void PreviewMouseLeftButtonDownBG(object sender, MouseButtonEventArgs e)
        {
            //BtnShowHide.IsChecked = false;
        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            //Close();
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                var applicationConfiguration = ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);
                var section = applicationConfiguration.GetSection("App_Preferences");

                if(section != null)
                {
                    applicationConfiguration.Sections.Remove("App_Preferences");
                    section.SectionInformation.ForceSave = true;
                    applicationConfiguration.Save(ConfigurationSaveMode.Full);
                }

                //ConfigurationManager.RefreshSection("App_Preferences");

                window.Close();
            }


        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Usuarios();
        }

        private void Pos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new POS();
        }

       private void Productos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Productos();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnCuenta_Click(object sender, RoutedEventArgs e)
        {
           // int valueToSend = 1;
           // var Acceso = new Acceso(valueToSend);   // Activa el Password de acceso
            var Acceso = new Acceso(1);
            Acceso.Show();

            //  var configuracionWindow = new Capa_Presentacion.SCS.Boxes.configuracionApp();
            // configuracionWindow.Show();


            // 1 = Confinguracion Pantalla Principal
            // 2= Cancelar la Factura

            //  var configuracion = new configuracionApp();
            //configuracion.Show();



        }

        private void EntradaClick(object sender, RoutedEventArgs e)
        {
            DataContext = new EntradaRecibo();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                        (Duration)TimeSpan.FromSeconds(1));
            this.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {

            var AcercaDe = new AcercaDe();   // Activa el Password de acceso
            AcercaDe.Show();

        }

        private void BtnSoporte_Click(object sender, RoutedEventArgs e)
        {


            var WPFMessageReceiver = new WPFMessageReceiver();   // Activa el Password de acceso
            WPFMessageReceiver.Show();




        }

        private void Reportes_Click(object sender, RoutedEventArgs e)
        {

            //System.Windows.MessageBox.Show("Este Modulo se encuentra en costrucción", "AVISO", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            var MainReportes = new MainReportes();   // Activa el Password de acceso
            MainReportes.Show();

        }

        private void BtnLogOff_Click(object sender, RoutedEventArgs e)
        {
            // Muestra el mensaje de información
           // System.Windows.MessageBox.Show("LogOff", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);



            // Cierra todas las ventanas abiertas excepto la actual
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
            
            // Almacena la nueva ventana que quieres abrir
            LoginView loginView = new LoginView();
            // Muestra la nueva ventana
            loginView.Show();
            this.Close();


        }

        private void Factturacion(object sender, RoutedEventArgs e)
        {
            var clientes = new modalClientes();
            clientes.ShowDialog();
            if (clientes.codigoCliente != null)
            {
                var mFacturacion = new modalFacturacion();
                mFacturacion.tbCodigoCliente.Text = clientes.codigoCliente.ToString();
                mFacturacion.tbNombreCliente.Text = clientes.nombreCliente.ToString();
                mFacturacion.ShowDialog();
            }

        }

        #region apertura y cerrado inicial
        private void Caja_Click(object sender, RoutedEventArgs e)
        {

            var Acceso = new Acceso(3);
            //  Acceso.Show();
            bool? dialogResult = Acceso.ShowDialog();    // comtinua en otra pantalla el proceso
                                                         // 1 = Confinguracion Pantalla Principal
                                                         // 2= Cancelar la Factura
                                                         // 3= abre venta de rendicion de caja

            if (Acceso.ReturnValue >= 3)
            {
 
              //  System.Windows.MessageBox.Show(Nom_Cajera.Cod_Cajera, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                Retiro_Control.Retiro_Acceso = 1;

                    GlobalVariables.CodSuper = Acceso.ReturnValue;
                var acdialog = new modalAperturaSalida();
                acdialog.Show();
            }

        }
        #endregion
    }
}



