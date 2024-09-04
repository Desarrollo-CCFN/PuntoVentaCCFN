using PuntoVentaCCFN.Views;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using Capa_Presentacion;
using Capa_Presentacion.SCS.Boxes;
using System.Windows.Media.Animation;
using Capa_Presentacion.Views;
using Capa_Presentacion.Reportes;


namespace PuntoVentaCCFN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public MainWindow()
        {
            InitializeComponent();

            if (AppConfig.Sections["App_Preferences"] is null)
            {
                AppConfig.Sections.Add("App_Preferences", new App_Preferences());
                AppConfig.Save();
            }
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
            System.Windows.MessageBox.Show("LogOff", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);



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

            // Cierra la ventana actual
            this.Close();

        }
    }
}