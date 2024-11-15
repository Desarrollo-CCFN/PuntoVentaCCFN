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
                    this.Title = "Rangos de Fecha Concentrado de Ventas";
                    break;
                case 6:
                 //   Label1.Content = "Número de Retiro:";
                    this.Title = "Rangos de Fecha Detallado de Ventas";
                    break;
            }

            }

        private void Click_Buscar(object sender, RoutedEventArgs e)
        {
            int IdTra = 0;
            int Param = valueTo_;

            // Validar que el texto no esté vacío y sea numérico
            /*  if (string.IsNullOrWhiteSpace(txtPrenume.Text) || !int.TryParse(txtPrenume.Text, out int IdTra))
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
              }*/

            EjecutarProcesoExterno(IdTra, Param);

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


        private void Click_cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
