using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using Org.BouncyCastle.Crypto;
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
                    break;
                case 6:
                 //   Label1.Content = "Número de Retiro:";
                    this.Title = "Rangos de Fecha Detallado Ventas   dd/mm/yyyy";
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
 

           EjecutarProcesoExterno(IdTra, Param, IdTra_1);

            this.Close();

        }

        private void EjecutarProcesoExterno(int IdTra, int Param, string IdTra_1)
        {
            // Crear una instancia de ProcessStartInfo
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\PuntoVenta\reportes\WindowsTesoreria.exe", // Ruta completa al ejecutable
                Arguments = $"{IdTra} {Param} {IdTra_1}" // Argumentos para el ejecutable
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
