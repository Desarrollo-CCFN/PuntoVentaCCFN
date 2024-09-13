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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using static Capa_Presentacion.Views.LoginView;
using Capa_Entidad;
using Capa_Negocio;




  

namespace PuntoVentaCCFN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        readonly CN_Denominacion objeto_CN_Denominacion = new CN_Denominacion();
        readonly CE_Denominacion objeto_CE_Denominacion = new CE_Denominacion();


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


            var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;

           string nombreCajaString = MainWindow.AppConfig1.Caja;   // ((Capa_Presentacion.App_Preferences)SettingSection).NombreCaja.ToString();

           string SucursalString = SettingSection.Filler;    //((Capa_Presentacion.App_Preferences)SettingSection).Sucursal.ToString();
           string  NombreCompany = SettingSection.CompanyName;
           int nombreCajaInt = int.Parse(nombreCajaString);
               


            string _status = objeto_CN_Denominacion.VerificarCaja(nombreCajaInt, SucursalString);

            if (!string.IsNullOrEmpty(_status) && _status.Length >= 4) // Asegurarse de que la cadena tenga al menos 4 caracteres
            {
                char firstChar = _status[0];  // Primer carácter
                char secondChar = _status[1]; // Segundo carácter
                char tersChar = _status[2]; // Cuarto carácter

                // Mostrar el segundo y cuarto carácter
              //  System.Windows.Forms.MessageBox.Show($"Caracter 2: {secondChar}, Caracter 4: {fourthChar}");

                if (firstChar == 'N' && tersChar == 'N')
                {
                    // Mostrar el mensaje si el primer carácter es 'N'
                    //System.Windows.Forms.MessageBox.Show("No se tiene Apartura de Caja se debe de abrir Caja");
                    System.Windows.MessageBox.Show("No se tiene Apartura de Caja se debe de abrir Caja", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);

                    InitializeComponent();
                }
                else if (firstChar == 'Y' || tersChar == 'Y')
                {

                    if (firstChar == 'N')
                    {
                        //System.Windows.Forms.MessageBox.Show(_status.Substring(7)+ " - Falta Apertura de Pesos");
                        System.Windows.MessageBox.Show(_status.Substring(7) + " - Falta Apertura de Pesos", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);


                    }
                    else if (tersChar == 'N')
                    {
                        //System.Windows.Forms.MessageBox.Show(_status.Substring(7) + " - Falta Apertura de Dolares");
                        System.Windows.MessageBox.Show(_status.Substring(7) + " - Falta Apertura de Dolares", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    string[] partes = _status.Substring(7).Split('-');
                    // Las partes relevantes están en los índices 1 y 3
                    string codigoCajero = partes[1].Trim(); // "623"
                    string nombreCajero = partes[3].Trim(); // "ERIKA LOPEZ"

                    if (codigoCajero == Nom_Cajera.Num_Cajera.Trim())
                    {
                        InitializeComponent();
                        // Continuar con la asignación de DataContext si es 'Y'
                        DataContext = new POS();
                    }
                    else
                    {
                       // System.Windows.Forms.MessageBox.Show("Error: la cajera que desea ingresar no coincide con la cajera que abrió caja: " + nombreCajero+ " Cod."+ codigoCajero);
                        System.Windows.MessageBox.Show("El cajera/o que desea ingresar no coincide con el que abrió caja: " + nombreCajero+ " Cod."+ codigoCajero, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        // Mostrar los resultados
                        //  System.Windows.Forms.MessageBox.Show($"Código Cajero: {codigoCajero}, Nombre Cajero: {nombreCajero}");
                    }
                }
            }
            else
            {
                InitializeComponent();
                // Manejo de casos en los que _status no tiene suficientes caracteres o es null
                System.Windows.Forms.MessageBox.Show("Error: El estado de la caja no es válido o la cadena es demasiado corta.");
            }


            // DataContext = new POS();


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

        private void Cierre_Caja_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Se encuentra en Construcción", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
 

        }
    }
}



