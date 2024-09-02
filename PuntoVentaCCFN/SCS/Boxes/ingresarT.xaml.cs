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
    /// Lógica de interacción para ingresarT.xaml
    /// </summary>
    public partial class ingresarT : Window
    {
        public decimal Cantidad;
        public string Voucher;
        public ingresarT()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Cantidad = Convert.ToDecimal(tbCantidad.Text);
            Voucher = tbVoucher.Text;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cantidad = 0;
            this.Close();
        }
    }
}
