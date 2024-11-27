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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using PuntoVentaCCFN;
using Capa_Entidad;
using Capa_Negocio;
using PuntoVentaCCFN.Views;
using Capa_Presentacion;
using Org.BouncyCastle.Crypto;







namespace Capa_Presentacion.Reportes
{
    /// <summary>
    /// Lógica de interacción para MainReportes.xaml
    /// </summary>
    public partial class MainReportes : Window
    {
        public MainReportes()
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
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove(); // Permite mover la ventana al hacer clic y arrastrar
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apertura_Click(object sender, RoutedEventArgs e)
        {
            var Cerrar = new Cerrar(3);
            Cerrar.Owner = this; // Establece MainReportes como el propietario
            Cerrar.ShowDialog();
                


        }

        private void bitacora_Click(object sender, RoutedEventArgs e)
        {

            var Cerrar = new Cerrar(2);
            Cerrar.Owner = this; // Establece MainReportes como el propietario
            Cerrar.ShowDialog();


        }

        private void retiros_Click(object sender, RoutedEventArgs e)
        {
            var Cerrar = new Cerrar(4);
            Cerrar.Owner = this; // Establece MainReportes como el propietario
            Cerrar.ShowDialog();
        }

        private void entrega_Click(object sender, RoutedEventArgs e)
        {

            // var Cerrar = new Cerrar(1);
            // Cerrar.ShowDialog();
            // Asigna el UserControl Cerrar a ContentArea en lugar de abrirlo como ventana modal
           // ContentArea.Content = new Cerrar(1); // Muestra el UserControl Cerrar en el área de contenido
          
             Cerrar Cerrar = new Cerrar(1);
            Cerrar.Owner = this; // Establece MainReportes como el propietario
            Cerrar.ShowDialog();  // Muestra la ventana como modal

        }

        private void QuerysC_click(object sender, RoutedEventArgs e)
        {
            var Query = new Query();
            Query.Owner = this; // Establece MainReportes como el propietario
            Query.ShowDialog();
        }

        private void ConsVenta_click(object sender, RoutedEventArgs e)
        {

            var Rangos = new Rangos(5);
            Rangos.Owner = this; // Establece MainReportes como el propietario
            Rangos.ShowDialog();
        }

        private void DetaVenta_click(object sender, RoutedEventArgs e)
        {

            var Rangos = new Rangos(6);
            Rangos.Owner = this; // Establece MainReportes como el propietario
            Rangos.ShowDialog();
        }

        private void VentaXart_click(object sender, RoutedEventArgs e)
        {
            var Rangos = new Rangos(7);
            Rangos.Owner = this; // Establece MainReportes como el propietario
            Rangos.ShowDialog();
        }
    }
}
