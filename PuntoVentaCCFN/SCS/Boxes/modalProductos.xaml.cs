using Capa_Entidad.Productos;
using Capa_Negocio.Productos;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalProductos.xaml
    /// </summary>
    public partial class modalProductos : Window
    {
        readonly CN_Productos objeto_CN_Productos = new CN_Productos();
        public string cadenaBusqueda;
        public string itemCode;
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
            GridProductos.UnselectAll();
            GridProductos.ItemsSource = objeto_CN_Productos.BusquedaProducto(tbSearchbox.Text).DefaultView;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if(itemCode == null || itemCode == "")
            {
                System.Windows.MessageBox.Show("Debes Seleccionar un producto!!");
                return;
            }

            cadenaBusqueda = "*" + itemCode + "*" + cbUnidades.Text;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void searchProductUoms(object sender, SelectionChangedEventArgs e)
        {
            if (GridProductos.SelectedItem == null) return;

            DataRowView row = GridProductos.SelectedItem as DataRowView;
            itemCode = row.Row.ItemArray[0].ToString();
            List<CE_ProductUm> lista = objeto_CN_Productos.BusquedaUm(row.Row.ItemArray[0].ToString());

            cbUnidades.ItemsSource = lista;
            cbUnidades.DisplayMemberPath = "UomCode";
            cbUnidades.SelectedValuePath = "UomEntry";
            cbUnidades.SelectedIndex = 0;
        }
    }

    public class ProductRow
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }
}
