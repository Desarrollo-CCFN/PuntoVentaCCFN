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

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para AcercaDe.xaml
    /// </summary>
    public partial class AcercaDe : Window
    {
        public AcercaDe()
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
