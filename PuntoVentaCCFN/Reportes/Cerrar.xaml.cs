using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion.Reportes
{
    public partial class Cerrar : Window
    {
        private int valueTo_;

        public Cerrar(int Value)
        {
            valueTo_ = Value;
            InitializeComponent();

            // Configurar etiquetas y títulos según valueTo_
            switch (valueTo_)
            {
                case 1:
                    Label1.Content = "Número de TCK Venta:";
                     this.Title = "Retiro";
                    break;
                case 2:
                    Label1.Content = "Número de Retiro:";
                    this.Title = "Tck Venta";
                    break;
                case 3:
                    Label1.Content = "Número de Cierre de Caja:";
                    this.Title = "Cierre de Caja";
                    break;

                case 4:
                    Label1.Content = "Número de Factura:";
                    this.Title = "Reimpresión Factura TCK";
                    break;


            }
        }

        private void Click_cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_Buscar(object sender, RoutedEventArgs e)
        {
            // Validar que el texto no esté vacío y sea numérico
            if (string.IsNullOrWhiteSpace(txtPrenume.Text) || !int.TryParse(txtPrenume.Text, out int IdTra))
            {
                System.Windows.Forms.MessageBox.Show("Por favor, ingrese un número válido.");
                return;
            }

            int Param = valueTo_;
            CN_BusquedaReporte objConsulta = new CN_BusquedaReporte();
            CE_BusquedaReporte objCe;

            try
            {
                objCe = objConsulta.ConsultaDatos(IdTra, Param);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error al consultar la base de datos: {ex.Message}");
                return;
            }

            if (objCe != null && objCe.IdTr_ == 1)      // realiza la validacion 
            {
                EjecutarProcesoExterno(IdTra, Param);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No existe información que desea buscar. Intente con otro número de folio.");
            }

            this.Close();
        }

        private void EjecutarProcesoExterno(int IdTra, int Param)
        {
            // Crear una instancia de ProcessStartInfo
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\PuntoVenta\reportes\WindowsTesoreria.exe", // Ruta completa al ejecutable
                Arguments = $"{IdTra} {Param}" // Argumentos para el ejecutable
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

     
        private void txtPrenume_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Click_Buscar(sender,  e); // Llamar a Click_Buscar al presionar Enter o Tab
            }
        }

        private void txtPrenume_(object sender, TextCompositionEventArgs e)
        {
            // Solo permitir números
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void Clieck_cerrar(object sender, RoutedEventArgs e)
        {

            this.Close();

        }
    }
}
