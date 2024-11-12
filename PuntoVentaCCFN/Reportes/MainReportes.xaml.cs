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
                Cerrar.ShowDialog();
                


        }

        private void bitacora_Click(object sender, RoutedEventArgs e)
        {

            var Cerrar = new Cerrar(2);
            Cerrar.ShowDialog();


        }

        private void retiros_Click(object sender, RoutedEventArgs e)
        {
            var Cerrar = new Cerrar(4);
            Cerrar.ShowDialog();
        }

        private void entrega_Click(object sender, RoutedEventArgs e)
        {

            var Cerrar = new Cerrar(1);
            Cerrar.ShowDialog();

        }

        private void QuerysC_click(object sender, RoutedEventArgs e)
        {
            var Query = new Query();
            Query.ShowDialog();
        }
    }
}
