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
            CRUDUsuarios ventana = new CRUDUsuarios();
            FrameUsuarios.Content = ventana;
        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((System.Windows.Controls.Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.IdUsuario = id;
            ventana.Consutlar();
            FrameUsuarios.Content = ventana;
        }
    }
}
