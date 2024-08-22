using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Capa_Negocio;
using Capa_Presentacion.Views;
using Capa_Entidad;
namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : System.Windows.Controls.UserControl
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        public Usuarios()
        {
            InitializeComponent();
            CargarDatos();
        }


        MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Usuarios.CargarUsuarios().DefaultView;
        }


        private void Agregar(object sender, RoutedEventArgs e)
        {
          //  CRUDUsuarios ventana = new Insert();
          //  FrameUsuarios.Content = ventana;

           // int id = (int)((System.Windows.Controls.Button)sender).CommandParameter;
            InsertUsuarios ventana = new InsertUsuarios();
         //   ventana.IdUsuario = id;
         //   ventana.Update();
            FrameUsuarios.Content = ventana;



        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((System.Windows.Controls.Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.IdUsuario = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
        }

        private void FrameUsuarios_Navigated(object sender, NavigationEventArgs e)
        {

        }

    
        private void Modificar(object sender, RoutedEventArgs e)
        {

            int id = (int)((System.Windows.Controls.Button)sender).CommandParameter;
            ModifUsuarios ventana = new ModifUsuarios();
            ventana.IdUsuario = id;
            ventana.Update();
            FrameUsuarios.Content = ventana;



        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {

            string usuario = tbUsuario.Text.Trim();
            if (!string.IsNullOrEmpty(usuario))
            {
                BuscarUsuario(usuario);
            }
            else
            {
                System.Windows.MessageBox.Show("Por favor, ingresa un nombre de usuario para buscar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CargarDatos();



            }

        }

        private void BuscarUsuario(string usuario)
        {
            DataTable dtResultado = objeto_CN_Usuarios.BuscarUsuario(usuario);
            
            if (dtResultado.Rows.Count > 0)
            {
                GridDatos.ItemsSource = dtResultado.DefaultView;
            }
            else
            {
                System.Windows.MessageBox.Show("No se encontraron usuarios con ese nombre.", "Resultado de la Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
                GridDatos.ItemsSource = null; // Limpiar los datos previos si no hay resultados
            }
        }

        private void tbUsuario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string usuario = tbUsuario.Text.Trim();
                if (!string.IsNullOrEmpty(usuario))
                {
                    BuscarUsuario(usuario);
                }
                else
                {
                    System.Windows.MessageBox.Show("Por favor, ingresa un nombre de usuario para buscar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CargarDatos();
                }

            }

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

            int id = (int)((System.Windows.Controls.Button)sender).CommandParameter;
            objeto_CN_Usuarios.Borrar(id);
            System.Windows.MessageBox.Show("Se borro de forma Exitosa", "Confirmación", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            GridDatos.ItemsSource = objeto_CN_Usuarios.CargarUsuarios().DefaultView;


        }

    /*    private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Content = new Usuarios();
            //throw new NotImplementedException();
            Window parentWindow = Window.GetWindow(this);
            
            if (parentWindow != null)
            {
                // Cerrar la ventana principal
                parentWindow.Close();
                
            }
        }*/
    }
}
