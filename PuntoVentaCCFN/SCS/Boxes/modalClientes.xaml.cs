﻿using Capa_Negocio.Clientes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalClientes.xaml
    /// </summary>
    public partial class modalClientes : Window
    {

        readonly CN_ConsultaModal objeto_CN_ConsultaModal = new CN_ConsultaModal();
        public modalClientes()
        {
            InitializeComponent();
            CargarClientes();
            tbSearchbox.Focus();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void CargarClientes()
        {
            GridClientes.ItemsSource = objeto_CN_ConsultaModal.ConsultaClientesModal().DefaultView;
        }

        private void buscarCliente(object sender, RoutedEventArgs e)
        {
            GridClientes.ItemsSource = objeto_CN_ConsultaModal.ConsultarCliente(tbSearchbox.Text).DefaultView;
        }

        private void GridClientes_Selected(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(GridClientes.SelectedItem.ToString());
        }

        public string codigoCliente {  get; set; }
        public string nombreCliente { get; set; }
        public string listaPrecio {  get; set; }
        public string numLista {  get; set; }

        private void seleccionarCliente(object sender, RoutedEventArgs e)
        {
            var seleccionado = GridClientes.SelectedItem;

            if(seleccionado != null)
            {
                var celda = GridClientes.SelectedCells[0];
                var celda1 = GridClientes.SelectedCells[1];
                var celda2 = GridClientes.SelectedCells[2];
                var celda3 = GridClientes.SelectedCells[3];
                codigoCliente = (celda.Column.GetCellContent(celda.Item) as TextBlock).Text;
                nombreCliente = (celda1.Column.GetCellContent(celda1.Item) as TextBlock).Text;
                listaPrecio = (celda2.Column.GetCellContent(celda2.Item) as TextBlock).Text;
                numLista = (celda3.Column.GetCellContent(celda3.Item) as TextBlock).Text;
                this.Close();
            }
        }

        private void tbSearchbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                buscarCliente(sender, e);  
            }

        }
    }
}
