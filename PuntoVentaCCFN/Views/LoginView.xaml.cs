using MySqlConnector;
using MySql.Data.MySqlClient;
using System.Data;
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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PuntoVentaCCFN;
using System.Windows.Media.Animation;
using Capa_Entidad;
using Capa_Negocio;
using PuntoVentaCCFN.Views;
using Capa_Presentacion;



namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();
        //   private MySqlConnectionStringBuilder objBuidel = new MySqlConnectionStringBuilder();



        public static class Nom_Cajera
        {
            public static string Nome_Cajera { get; set; }
            public static string Num_Cajera { get; set; }

            public static string Cod_Cajera { get; set; }

            public static string Nom_Sucursal { get; set; }

            public static string Cod_Sucursal { get; set; }

            public static int logoff { get; set; }

        }


        public LoginView()
        {
            InitializeComponent();
            txtUser.Focus();

            // Crear la animación de opacidad
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(2)) // Duración de 2 segundos
            };

            // Aplicar la animación al cargar la ventana
            this.BeginAnimation(Window.OpacityProperty, fadeInAnimation);
 

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            InitializeComponent();

            Nom_Cajera.Nome_Cajera = "";
            Nom_Cajera.Num_Cajera = "";
            Nom_Cajera.Cod_Cajera = "";
            Nom_Cajera.Nom_Sucursal = "";
            Nom_Cajera.Cod_Sucursal = "";
            Nom_Cajera.logoff = 0;


            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                // Asignar los valores de la interfaz a la entidad
                objeto_CE_Usuarios.U_NAME = this.txtUser.Text;
                objeto_CE_Usuarios.PASSWORD4 = this.txtPass.Password;

                // Llamar al método de negocio para autenticar el usuario
                DataTable dtResultado = objeto_CN_Usuarios.AutUsuario(this.txtUser.Text, this.txtPass.Password);

                Mouse.OverrideCursor = null;

                if (dtResultado.Rows.Count > 0)
                {
                    string superUserFlag = dtResultado.Rows[0].ItemArray[5]?.ToString();

                    Nom_Cajera.Nome_Cajera = dtResultado.Rows[0].ItemArray[1]?.ToString();
                    Nom_Cajera.Num_Cajera = dtResultado.Rows[0].ItemArray[0]?.ToString();
                    Nom_Cajera.Cod_Cajera = dtResultado.Rows[0].ItemArray[2]?.ToString();
                    Nom_Cajera.Nom_Sucursal = dtResultado.Rows[0].ItemArray[7]?.ToString();
                    Nom_Cajera.Cod_Sucursal = dtResultado.Rows[0].ItemArray[3]?.ToString();
                   

                    if (superUserFlag == "Y" || superUserFlag == "N" )
                    {
                        MainWindow mainWindow = new MainWindow();

                        if (superUserFlag == "Y")
                        {
                            mainWindow.menuItemUsuarios.Visibility = Visibility.Visible;
                            mainWindow.menuItemalmacen.Visibility = Visibility.Visible;
                            mainWindow.menuItemdashboard.Visibility = Visibility.Visible;
                            mainWindow.menuItemReportes.Visibility = Visibility.Visible;
                          //  mainWindow.menuItemFacturacion.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            mainWindow.menuItemUsuarios.Visibility = Visibility.Collapsed;
                            mainWindow.menuItemalmacen.Visibility = Visibility.Collapsed;
                            mainWindow.menuItemdashboard.Visibility = Visibility.Collapsed;
                            mainWindow.menuItemReportes.Visibility = Visibility.Collapsed;
                           // mainWindow.menuItemFacturacion.Visibility = Visibility.Collapsed;
                        }

                        //  System.Windows.MessageBox.Show("Super usuario autenticado.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
 
                        mainWindow.Show();
                        this.Close();
                    }
                 
                }
                else
                {
                    System.Windows.MessageBox.Show("No se tiene registro Favor de Verificar Usuario y/o Password.", "No se tiene Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtUser.Clear();
                    txtPass.Clear();    
                    txtUser.Focus();


                }
           
        }

        private void txtUser_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

           /* if (this.txtUser.Text == "AJ" || this.txtUser.Text == "aj")
            {
                // System.Windows.MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Mouse.OverrideCursor = null;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Cerrar la ventana LoginView.xaml
                this.Close();

            }*/

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtPass.Focus();
            }
      
        
        }

        private void txtPass_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Tab) // Verificar si la tecla presionada es Enter
            {
                btnLogin_Click(sender, e); // Llamar al método btnLogin_Click con los argumentos correctos
            }
             

        }
    }
}
