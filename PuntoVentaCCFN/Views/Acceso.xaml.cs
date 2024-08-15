using Capa_Presentacion.SCS.Boxes;
using MySqlConnector;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
 

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para Acceso.xaml
    /// </summary>
    public partial class Acceso : Window
    {
        public int ReturnValue { get; set; } // Propiedad para devolver el valor

        private MySqlConnectionStringBuilder objBuidel = new MySqlConnectionStringBuilder();
        private int valueToSend_;
        public Acceso(int valueToSend)
        {
            valueToSend_ = valueToSend;
            ReturnValue = 0; // Valor por defecto
          //  System.Windows.MessageBox.Show("Password para Acceso: " + valueToSend_, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            InitializeComponent();
        }
       
        

            private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            objBuidel.Server = "54.177.203.25";
            objBuidel.Database = "CCFNPROD";
            objBuidel.UserID = "apisap";
            objBuidel.Password = "34sg!MaXN**5c%tG";

            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(objBuidel.ConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    string password = txtPassword.Password;
                    string email = txtUsuario.Text;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://192.168.0.32:8886");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var loginData = new { Email = email, Password = password };

                        HttpResponseMessage response = await client.PostAsJsonAsync("/api/Account/Login", loginData);

                        if (response.IsSuccessStatusCode)
                        {
                            Mouse.OverrideCursor = null;
                            

                            if (valueToSend_ == 1)     // Acceso Confinguracion Pantalla Principal
                            {
                                 var configuracionWindow = new Capa_Presentacion.SCS.Boxes.configuracionApp();
                                 configuracionWindow.Show();
                              
                            }

                            if (valueToSend_ == 2)  // Acceso Cancelar la Factura
                            {
                                
                                ReturnValue = 1;   // regresa 1 que puede proseguir con acceso

                            }

                            this.Close();

                            // var configuracionWindow = new Capa_Presentacion.SCS.Boxes.configuracionApp();
                            // configuracionWindow.Show();

                            //   var configuracion = new configuracionApp();
                            //   configuracion.Show();

                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Error Favor de Verificar Usuario/Password", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                            connection.Close();
                            txtPassword.Password = "";
                            txtUsuario.Text = "";
                            Mouse.OverrideCursor = null;
                        }
                    }
                }
            }

        }
    }
}
