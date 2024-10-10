using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Capa_Presentacion.Reportes
{
    /// <summary>
    /// Lógica de interacción para Cerrar.xaml
    /// </summary>
    public partial class Cerrar : Window
    {
        private int valueTo_;
        public Cerrar(int Value)
        {
            valueTo_ = Value;

            InitializeComponent();

            if (valueTo_ == 1)
            {
                 Label1.Content = "Numero de TCK Venta:";
                this.Title = "Tck Venta"; 
            }
            else if (valueTo_ == 2)
            {
                Label1.Content = "Numero de Retiro:";
                this.Title = "Retiro";
            }
            else if (valueTo_ == 3)
            {
                  Label1.Content = "Número Ciere de Caja:";
                  this.Title = "Cerrar Caja";
            }


            // Label1.Content = "Caja";
            // this.Title = "TCK";



        }

        private void Clieck_cerrar(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void Click_Buscar(object sender, RoutedEventArgs e)
        {



            if (valueTo_ == 1)
            {

             //   System.Windows.MessageBox.Show("Opcion1");

                int Param = 1;
                int IdTra = int.Parse(txtPrenume.Text);

                // Crear una instancia de ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja

                // startInfo.Arguments = $"{IdTra}";
                startInfo.Arguments = $"{IdTra} {Param}";

                // Ejecutar el programa externo
                Process.Start(startInfo);

                this.Close();
                 

            }
            else if (valueTo_ == 2)
            {

              //   System.Windows.MessageBox.Show("Opcion2");


               int Param = 2;
                int IdTra = int.Parse(txtPrenume.Text);

                // Crear una instancia de ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja

                // startInfo.Arguments = $"{IdTra}";
                startInfo.Arguments = $"{IdTra} {Param}";

                // Ejecutar el programa externo
                Process.Start(startInfo);
                this.Close();
            

            }
            else if (valueTo_ == 3)
            {

                int IdTra = int.Parse(txtPrenume.Text);      //51;   //este podria ser el para metro de prueba
                                                             // Crear una instancia de ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\PuntoVenta\cierre\CierreCaja.exe"; // Ruta completa al ejecutable de RetirosCaja

            startInfo.Arguments = $"{IdTra}";
            // startInfo.Arguments = $"{IdTra} {Param}";

            // Ejecutar el programa externo
            Process.Start(startInfo);
                this.Close();
            }

        }

        private void txtPrenume_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab) // Verificar si la tecla presionada es Enter
            {
                Click_Buscar(sender, e); // Llamar al método  Click_Buscar con los argumentos correctos
            }
        }
    }
}
