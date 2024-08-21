using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            StartListening();
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
                await udpClient.SendAsync(data, data.Length, "192.168.101.20", 12000);
                Dispatcher.Invoke(() =>
                {
                    ledIndicator.Fill = new SolidColorBrush(Colors.Green);
                    lstReceivedMessages.Items.Add($"Enviado Caja1 Lazaro {timestamp}: {replyMessage}");
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
