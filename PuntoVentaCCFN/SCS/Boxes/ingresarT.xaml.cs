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
            tbVoucher.Focus();
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


        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (tbCantidad.Text == null || tbCantidad.Text == "")
                {
                    System.Windows.MessageBox.Show("Debes ingresar una cantidad!!");

                    return;
                } else if (tbVoucher.Text == null || tbVoucher.Text == "")
                {
                    System.Windows.MessageBox.Show("Debes ingresar un voucher valido!!");

                    return;
                }
                else
                {
                 
                        Cantidad = Convert.ToDecimal(tbCantidad.Text);
                        Voucher = tbVoucher.Text;
                        this.Close();
                    
                }
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            if(tbVoucher.Text == "")
            {
                tbVoucher.Focus();
            } else
            {
                tbCantidad.Focus();
            }
            
            
        }
    }
}
