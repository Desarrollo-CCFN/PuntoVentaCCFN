using MySqlConnector;
using System;
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

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private MySqlConnectionStringBuilder objBuidel = new MySqlConnectionStringBuilder();
        public LoginView()
        {
            InitializeComponent();
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


        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            InitializeComponent();
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            //Mouse.OverrideCursor = Cursors.WaitCursor;
            //objBuidel.Server = "192.168.101.7";
            //objBuidel.Database = "ccfn_desarrollo";
            //objBuidel.UserID = "desarrollo2";
            //objBuidel.Password = "Chivas.2024";

            objBuidel.Server = "54.177.203.25";
            objBuidel.Database = "CCFNPROD";
            objBuidel.UserID = "apisap";
            objBuidel.Password = "34sg!MaXN**5c%tG";

            MySqlConnection connection = null;

            // using (var connection = new MySqlConnection(objBuidel.ConnectionString))
            // {
            try
            {
                connection = new MySqlConnection(objBuidel.ConnectionString);
                connection.Open();
               // System.Windows.MessageBox.Show("Conexión exitosa.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
               // MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // }
            finally
            {
                // Cerrar la conexión manualmente si está abierta
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {


                    string password = this.txtPass.Password;

                    string email = this.txtUser.Text;
                    //  MessageBox.Show(password);
                    //  MessageBox.Show(email);


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://192.168.0.32:8886");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var loginData = new
                        {
                            Email = email,
                            Password = password
                        };

                        HttpResponseMessage response = await client.PostAsJsonAsync("/api/Account/Login", loginData);

                        if (response.IsSuccessStatusCode)
                        {
                          // System.Windows.MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            Mouse.OverrideCursor = null;
                          
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();

                            // Cerrar la ventana LoginView.xaml
                            this.Close();



                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Error Favor de Verificar Usuario/Password", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                            connection.Close();
                            // MessageBox.Show("Cerro");
                            this.txtPass.Password = "";
                            this.txtUser.Text = "";
                            Mouse.OverrideCursor = null;
                        }
                    }


                }
            }



        }



    }
}
