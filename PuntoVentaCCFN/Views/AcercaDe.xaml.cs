using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
 

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para AcercaDe.xaml
    /// </summary>
    public partial class AcercaDe : Window
    {
        public AcercaDe()
        {
            InitializeComponent();

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(2)) // Duración de 2 segundos
            };

            // Aplicar la animación al cargar la ventana
            this.BeginAnimation(Window.OpacityProperty, fadeInAnimation);


            // Crear una conexión a MySQL
            //string connectionString = "Server=localhost;Database=mi_bd;Uid=usuario;Pwd=contraseña;";
            //  string connectionString = "Server=192.168.101.7;uid=desarrollo2; pwd=Chivas.2024;database=ccfn_desarrollo;";
            //  string connectionString = "Server=192.168.101.34;uid=root; pwd=root.2024;database=db_s12;";
               string connectionString = "Server=localhost;uid=root; pwd=root.2024;database=db_s12;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                // Abrir la conexión
                conn.Open();

                // Obtener la IP asociada a "localhost"
                string ipAddress = GetLocalhostIP();

                // Mostrar la IP en un Label
                IP.Text = "Conectado al servidor con IP: " + ipAddress;
            }
            catch (Exception ex)
            {
                IP.Text = "Error: " + ex.Message;
            }
            finally
            {
                // Cerrar la conexión
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
             

        }
        private string GetLocalhostIP()
        {
            // Resolver el nombre "localhost" para obtener su IP
            IPAddress[] addresses = Dns.GetHostAddresses("localhost");
            foreach (IPAddress address in addresses)
            {
                // Devolver la primera dirección IPv4 que encuentre
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }

            return "No se encontró una IP";
        }




        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove(); // Permite mover la ventana al hacer clic y arrastrar
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
