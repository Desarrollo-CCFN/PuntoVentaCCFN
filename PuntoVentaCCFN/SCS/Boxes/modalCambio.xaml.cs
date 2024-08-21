using PuntoVentaCCFN.Views;
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
    /// Lógica de interacción para modalCambio.xaml
    /// </summary>
    public partial class modalCambio : Window
    {
        public decimal conversion;
        public decimal tc;
        public decimal totalcambion;

        public modalCambio(decimal tipoCambio)
        {
            InitializeComponent();
            this.tc = tipoCambio;
        }

        private void calculateMXN(object sender, TextChangedEventArgs e)
        {
            if (tbCambioU.Text == null || tbCambioU.Text == "") {
                lblConvert.Text = "$0 MXN";
                tbCambioR.Text = null;
                return;
            }



            conversion = Convert.ToDecimal(tbCambioU.Text) * tc;

            lblConvert.Text = "$ " + conversion + " MXN";

            tbCambioR.Text = (Convert.ToDecimal(tbCambioN.Text) - conversion).ToString();
        }

        private void aceptarCambio(object sender, RoutedEventArgs e)
        {

            if (tbCambioR.Text == null || tbCambioR.Text == "")
            {
                System.Windows.MessageBox.Show("Ingresa cantidad a regresar!!");
                return;
            }

            totalcambion = conversion + Convert.ToDecimal(tbCambioR.Text);

            if (tbCambioU.Text != null || tbCambioU.Text != "")
            {
                if(totalcambion > Convert.ToDecimal(tbCambioN.Text))
                {
                    System.Windows.MessageBox.Show("El cambio ingresado excede el cambio neto!!");
                    return;
                }
            } else if(Convert.ToDecimal(tbCambioR.Text) > Convert.ToDecimal(tbCambioN.Text))
            {
                System.Windows.MessageBox.Show("El cambio ingresado excede el cambio neto!!");
                return;
            }

            if(tbCambioU.Text != null || tbCambioU.Text != "")
            {
                if(totalcambion != Convert.ToDecimal(tbCambioN.Text))
                {
                    System.Windows.MessageBox.Show("El cambio ingresado no coincide con el cambio neto!!");
                    return;
                }
            } else if (Convert.ToDecimal(tbCambioR.Text) != Convert.ToDecimal(tbCambioN.Text))
            {
                System.Windows.MessageBox.Show("El cambio ingresado no coincide con el cambio neto!!");
                return;
            }

            //var posObject = new POS();
            //posObject.ventaFinal();
            this.Close();
        }
    }
}
