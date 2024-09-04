using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para WPFMessageReceiver.xaml
    /// </summary>
    public partial class WPFMessageReceiver : Window
    {
        private UdpClient udpClient;
          private bool isRunning = true;
          private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

   
        public WPFMessageReceiver()
        {
            InitializeComponent();
            udpClient = new UdpClient(11000); // Configura el puerto en el constructor
            ledIndicator.Fill = new SolidColorBrush(Colors.Red); // Inicializa el LED en rojo
            LoadJson();

            StartListening();
        }



        public void LoadJson()
        {
            try
            {
                if (File.Exists("C:\\PuntoVenta\\config.json"))
                {
                    using (StreamReader r = new StreamReader("C:\\PuntoVenta\\config.json"))
                    {
                        
                        string json = r.ReadToEnd();
                        JObject jsons = JObject.Parse(json);

                        AppConfig1.IP = jsons["IP"].ToString();
                        AppConfig1.Sucursal = jsons["Sucursal"].ToString();
                        AppConfig1.Puerto = jsons["Puerto"].ToString();
                       

                    }
                }
                else
                {
                    if (!Directory.Exists("C:\\PuntoVenta"))
                    {
                        Directory.CreateDirectory("C:\\PuntoVenta");
                    }
                    var _data = new { IP = "192.168.0.0", Sucursal = "Caja 1 Lazaro", Puerto = "12000" };



                    string json = JsonConvert.SerializeObject(_data);
                    File.WriteAllText(@"C:\\PuntoVenta\\config.json", json);

                    System.Windows.Forms.MessageBox.Show("Se ha creado un archivo de configuracion en el disco local, C:\\PuntoVenta ", "Configuracion");
                }
            }
            catch (Exception EX)
            {
                System.Windows.Forms.MessageBox.Show("Error en ejecucion");
            }
        }



        public static class AppConfig1
        {
            public static string IP { get; set; }
            public static string Sucursal { get; set; }
            public static string Puerto { get; set; }

        }

         


        private async void StartListening()
        {
            try
            {
                await Task.Run(() => ReceiveMessages());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al iniciar la escucha: " + ex.Message);
                 

            }
        }

        private void ReceiveMessages()
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while (isRunning)
                {
                    byte[] data = udpClient.Receive(ref remoteEndPoint);
                    string message = Encoding.UTF8.GetString(data);

                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    Dispatcher.Invoke(() =>
                    {
                        ledIndicator.Fill = new SolidColorBrush(Colors.Green);
                        lstReceivedMessages.Items.Add($"Soporte IT {timestamp}: {message}");
                    });
                }
            }
            catch (ObjectDisposedException)
            {
                Dispatcher.Invoke(() =>
                {
                    lstReceivedMessages.Items.Add("Socket cerrado.");
                    ledIndicator.Fill = new SolidColorBrush(Colors.Red);
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ledIndicator.Fill = new SolidColorBrush(Colors.Red);
                    if (ex.HResult == -2147467259)
                    {
                        System.Windows.Forms.MessageBox.Show("Se cerro la Conversación");
                    }
                    else
                    {

                        System.Windows.Forms.MessageBox.Show("Error recibiendo mensaje: " + ex.Message);
                    }

                    Close();
                });
            }
        }
         
        private async void btnReply_Click(object sender, RoutedEventArgs e)
        {

            string replyMessage = txtReply.Text;
            byte[] data = Encoding.UTF8.GetBytes(replyMessage);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
             //   await udpClient.SendAsync(data, data.Length, "192.168.101.20", 12000);
                await udpClient.SendAsync(data, data.Length, AppConfig1.IP, int.Parse(AppConfig1.Puerto));
                Dispatcher.Invoke(() =>
                {
                    ledIndicator.Fill = new SolidColorBrush(Colors.Green);
                    lstReceivedMessages.Items.Add($"Enviado {AppConfig1.Sucursal} {timestamp}: {replyMessage}");
                    txtReply.Clear();
                });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error enviando respuesta: " + ex.Message);
                ledIndicator.Fill = new SolidColorBrush(Colors.Red);
                Close();
            }

        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            ledIndicator.Fill = new SolidColorBrush(Colors.Red);
            isRunning = false;

            udpClient.Close();
            udpClient = new UdpClient(11000); // Reinicia la conexión
            lstReceivedMessages.Items.Clear();
            isRunning = true;

            StartListening();

            lstReceivedMessages.Items.Add("La conexión ha sido reiniciada.");

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ledIndicator.Fill = new SolidColorBrush(Colors.Red);
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            isRunning = false;
            udpClient.Close();
            base.OnClosed(e);
        }

        private void txtReply_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnReply_Click(sender, e);
                e.Handled = true;
            }
        }


    }
}
