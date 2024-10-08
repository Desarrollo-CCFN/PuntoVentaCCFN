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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Capa_Negocio.Productos;
using System.Data;
namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : System.Windows.Controls.UserControl
    {
        readonly CN_Productos objeto_CN_Productos = new CN_Productos();
        public Productos()
        {
            InitializeComponent();
            CargarDatos();
        }

        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Productos.CargarProductos().DefaultView;   
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            GridDatos.UnselectAll();
            GridDatos.ItemsSource = objeto_CN_Productos.BusquedaProducto(tbNombre.Text).DefaultView;
        }
    }
}
