using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MySql.Data.MySqlClient;

namespace Capa_Presentacion.Views
{
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

            // Cadena de conexión
            string connectionString = "Server=10.101.1.130;uid=root; pwd=root.2024;database=db_s12;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                // Abrir la conexión
                conn.Open();

                // Obtener la IP del servidor de la conexión MySQL
                string serverIP = GetServerIP(conn);

                // Mostrar la IP en un Label
                IP.Text = "Conectado al servidor con IP: " + serverIP;
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

        private string GetServerIP(MySqlConnection connection)
        {
            try
            {
                // Usar la propiedad de Host para obtener la IP del servidor MySQL
                IPAddress[] addresses = Dns.GetHostAddresses(connection.DataSource);
                foreach (IPAddress address in addresses)
                {
                    // Devolver la primera dirección IPv4 que encuentre
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                return "No se encontró una IP válida";
            }
            catch (Exception ex)
            {
                return "Error al obtener IP: " + ex.Message;
            }
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

