
using System.Windows;
using System.Windows.Input;


namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para Ingresar.xaml
    /// </summary>
    public partial class Ingresar : Window
    {
        public Ingresar()
        {
            InitializeComponent();
            tbCantidad.Focus();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            bool esnumerico = decimal.TryParse(tbCantidad.Text, out _);

            if (esnumerico)
            {
                Total = decimal.Parse(tbCantidad.Text);
                Efectivo = decimal.Parse(tbCantidad.Text);
                this.Close();
            }
            else
            {
                this.Close();
            }

        }

        public decimal Total { get; set; }
        public decimal Efectivo { get; set; }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                if(tbCantidad.Text == null || tbCantidad.Text == "")
                {
                    MessageBoxResult r = System.Windows.MessageBox.Show("Debes ingresar una cantidad!!");
                    if(r == MessageBoxResult.OK) {
                        tbCantidad.Focus();
                        Keyboard.Focus(tbCantidad);
                    }
                    
                    
                } else
                {
                    bool esnumerico = decimal.TryParse(tbCantidad.Text, out _);

                    if (esnumerico)
                    {
                        Total = decimal.Parse(tbCantidad.Text);
                        Efectivo = decimal.Parse(tbCantidad.Text);
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }

                tbCantidad.Focus();
            }

            if(e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbCantidad.Focus();
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            tbCantidad.Focus();
            Keyboard.Focus(tbCantidad);
        }
    }
}
