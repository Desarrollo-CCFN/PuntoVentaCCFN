using Capa_Negocio.Productos;
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

namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalProductos.xaml
    /// </summary>
    public partial class modalProductos : Window
    {
        readonly CN_Productos objeto_CN_Productos = new CN_Productos();
        public modalProductos()
        {
            InitializeComponent();
            CargarProductos();
        }

        void CargarProductos()
        {
            GridProductos.ItemsSource = objeto_CN_Productos.CargarProductos().DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridProductos.ItemsSource = objeto_CN_Productos.BusquedaProducto(tbSearchbox.Text).DefaultView;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
