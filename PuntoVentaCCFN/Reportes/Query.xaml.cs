using Capa_Datos;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using ClosedXML.Excel;
using System.IO;




namespace Capa_Presentacion.Reportes
{
    /// <summary>
    /// Lógica de interacción para Query.xaml
    /// </summary>
    public partial class Query : Window
    {
        public Query()
        {
            InitializeComponent();
        }

        private void Ejecutar_Click(object sender, RoutedEventArgs e)
        {
           // string connectionString = "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;";
            MySqlConnection conn = new MySqlConnection(Configuracion.CadenaConexion);
            string query = QueryTextBox.Text;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(Configuracion.CadenaConexion))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    string serverIP = GetServerIP(conn);

                   IP.Text = "Conectado al servidor con IP: " + serverIP;

                    ResultsDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error ejecutando el query:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetServerIP(MySqlConnection connection)
        {
            try
            {
                // Usar la propiedad de Host para obtener la IP del servidor MySQL
                IPAddress[] addresses = Dns.GetHostAddresses(connection.DataSource);
                foreach (IPAddress address in addresses)
                {
                    // Devolver la primera dirección IPv4 que encuentre
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                return "No se encontró una IP válida";
            }
            catch (Exception ex)
            {
                return "Error al obtener IP: " + ex.Message;
            }
        }

        private void ExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            var dataTable = ((DataView)ResultsDataGrid.ItemsSource).ToTable();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Resultados");
                worksheet.Cell(1, 1).InsertTable(dataTable);

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = "ResultadosQuery.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    System.Windows.MessageBox.Show("Datos exportados exitosamente a Excel.", "Exportación", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void CopiarPortapapeles_Click(object sender, RoutedEventArgs e)
        {
            var dataTable = ((DataView)ResultsDataGrid.ItemsSource).ToTable();
            StringWriter stringWriter = new StringWriter();

            // Añadir los nombres de las columnas
            foreach (DataColumn column in dataTable.Columns)
            {
                stringWriter.Write(column.ColumnName + "\t");
            }
            stringWriter.WriteLine();

            // Añadir los datos de cada fila
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var cell in row.ItemArray)
                {
                    stringWriter.Write(cell.ToString() + "\t");
                }
                stringWriter.WriteLine();
            }

            // Copiar al portapapeles
            System.Windows.Clipboard.SetText(stringWriter.ToString());
            System.Windows.MessageBox.Show("Datos copiados al portapapeles. Puedes pegarlos en Excel.", "Copiado al Portapapeles", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CerrarC(object sender, RoutedEventArgs e)
        {

            this.Close();

        }
    }
}
