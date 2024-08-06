
using System.Windows;


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
            } else
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
    }
}
