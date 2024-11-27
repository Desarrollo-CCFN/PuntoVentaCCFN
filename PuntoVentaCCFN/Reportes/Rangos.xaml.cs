using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using Org.BouncyCastle.Crypto;
using PuntoVentaCCFN;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion.Reportes
{
    /// <summary>
    /// Lógica de interacción para Rangos.xaml
    /// </summary>
    public partial class Rangos : Window
    {
        private int valueTo_;
        public Rangos(int Value)
        {
            valueTo_ = Value;
            InitializeComponent();

            switch (valueTo_)
            {
                case 5:
                 //   Label1.Content = "Número de TCK Venta:";
                    this.Title = "Rangos de Fecha Concentrado Ventas dd/mm/yyyy";
                    Art.Visibility = Visibility.Collapsed;
                   labelArt.Visibility = Visibility.Collapsed;

                    Tit2.Height = 200; // Altura como un valor numérico, no una cadena
                    Buscar.Margin = new Thickness(80, 114, 0, 0); // Margen como un objeto Thickness
                    cerrar.Margin = new Thickness(205, 114, 0, 0);


                    break;
                case 6:
                 //   Label1.Content = "Número de Retiro:";
                    this.Title = "Rangos de Fecha Detallado Ventas   dd/mm/yyyy";
                    Art.Visibility = Visibility.Collapsed;
                   labelArt.Visibility = Visibility.Collapsed;
                    Tit2.Height = 200; // Altura como un valor numérico, no una cadena
                    Buscar.Margin = new Thickness(80, 114, 0, 0); // Margen como un objeto Thickness
                    cerrar.Margin = new Thickness(205, 114, 0, 0);
                    break;

                case 7:
                    //   Label1.Content = "Número de Retiro:";
                    this.Title = "Ventas Artículos  dd/mm/yyyy";
                   
                    Tit2.Height = 250;
                    Buscar.Margin = new Thickness(80, 152, 0, 0);
                    cerrar.Margin = new Thickness(205, 152, 0, 0);
                    break;
            }

         }


        private void FechaIn_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ValidarFechas();
        }

        private void FechaOut_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ValidarFechas();
        }

        private void ValidarFechas()
        {
            if (FechaIn.SelectedDate.HasValue && FechaOut.SelectedDate.HasValue)
            {
                DateTime fechaInicio = FechaIn.SelectedDate.Value;
                DateTime fechaFinal = FechaOut.SelectedDate.Value;

                if (fechaInicio > fechaFinal)
                {
                    System.Windows.MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.", "Error de Validación",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Opcional: Reiniciar las fechas si son inválidas
                    FechaIn.SelectedDate = null;
                    FechaOut.SelectedDate = null;
                }
            }
        }



        private void Click_Buscar(object sender, RoutedEventArgs e)
        {
            string IdTra_1 = FechaIn.Text+ FechaOut.Text;
            int Param = valueTo_;
            int IdTra = 0;
            string Art = this.Art.Text;



            if ((valueTo_ == 7 && string.IsNullOrEmpty(this.Art.Text)) || valueTo_ == 5 || valueTo_ == 6)
            {
                if (valueTo_ == 7 && string.IsNullOrEmpty(this.Art.Text))
                {
                    System.Windows.MessageBox.Show("No se agregó código de producto. Se realizará una búsqueda total.",
                                                    "Aviso de Búsqueda Total",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Warning);
                }

                Art = "*";
            }


            EjecutarProcesoExterno(IdTra, Param, IdTra_1, Art);

            this.Close();

        }

        private void EjecutarProcesoExterno(int IdTra, int Param, string IdTra_1, string Art)
        {
            // Crear una instancia de ProcessStartInfo
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\PuntoVenta\reportes\WindowsTesoreria.exe", // Ruta completa al ejecutable
                Arguments = $"{IdTra} {Param} {IdTra_1} {Art}" // Argumentos para el ejecutable
            };

            try
            {
                // Ejecutar el programa externo
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error al ejecutar el proceso externo: {ex.Message}");
            }
        }


        private void Click_cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
