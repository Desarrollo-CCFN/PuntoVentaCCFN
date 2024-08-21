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
    /// Lógica de interacción para modalAperturaSalida.xaml
    /// </summary>
    public partial class modalAperturaSalida : Window
    {
        public modalAperturaSalida()
        {
            InitializeComponent();
            CargaInicial();
        }

        public void CargaInicial()
        {
            cbTipo.Items.Add("Apertura");
            cbTipo.Items.Add("Cerrado");
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
