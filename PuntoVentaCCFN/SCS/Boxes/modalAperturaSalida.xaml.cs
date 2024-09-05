using Capa_Negocio;
using PuntoVentaCCFN.Views;
using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;
using Capa_Presentacion.Views;
using Capa_Entidad;
using System.Windows.Navigation;
using System.Data;
using System.Configuration;
using PuntoVentaCCFN;




namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalAperturaSalida.xaml
    /// </summary>
    public partial class modalAperturaSalida : Window
    {
         
        readonly CN_Denominacion objeto_CN_Denominacion = new CN_Denominacion();
        readonly CE_Denominacion objeto_CE_Denominacion = new CE_Denominacion();
        MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
       // Decimal Monto = 0;

        private string SucursalString;
        private string nombreCajaString;
       private  int nombreCajaInt;
       private int CodSuper;
     

        
        public modalAperturaSalida()
        {
            CodSuper = GlobalVariables.CodSuper; ;

            InitializeComponent();
            CargaInicial();
        }

        public void CargaInicial()
        {
            var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
    
            // Accede a la propiedad Caja desde AppConfig1
            nombreCajaString = MainWindow.AppConfig1.Caja;   // ((Capa_Presentacion.App_Preferences)SettingSection).NombreCaja.ToString();

            SucursalString = SettingSection.Filler;    //((Capa_Presentacion.App_Preferences)SettingSection).Sucursal.ToString();
           
            nombreCajaInt = int.Parse(nombreCajaString);
            int Control1 = 0;
            int Control2 = 0;
            string _status;
         



            _status = objeto_CN_Denominacion.VerificarCaja(nombreCajaInt, SucursalString);

            if (_status.Substring(0, 1) == "O" )

            {
                System.Windows.MessageBox.Show("Caja Abierta");

                if ((_status.Substring(1, 1) == "Y") && (_status.Substring(3, 1) == "Y"))
                {  Control1 = 1;
                   
                }

                if ((_status.Substring(2, 1) == "Y") && (_status.Substring(4, 1) == "Y"))
                {  Control2 = 1;
                  
                }

                if (Control1 == 0 && Control2 == 0)
                {

                    cbTipo.Items.Add("1- Apertura de Caja");
                    cbTipo.Items.Add("2- Cerrado de Caja");
                    cbTipo.Items.Add("3- Retiros Fondo de Caja");
                }
                else if (Control1 == 1)
                {
                     cbTipo.Items.Add("2- Cerrado de Caja");
                     cbTipo.Items.Add("3- Retiros Fondo de Caja");

                }else if (Control1 == 2)
                {
                     cbTipo.Items.Add("1- Apertura de Caja");
                     cbTipo.Items.Add("3- Retiros Fondo de Caja");

                }

                    System.Windows.MessageBox.Show(_status);

            }
            else
            {


                cbTipo.Items.Add("1- Apertura de Caja");
                cbTipo.Items.Add("2- Cerrado de Caja");
                cbTipo.Items.Add("3- Retiros Fondo de Caja");


              
            }

            //   cbDenominacionesItems.Add

            // Agregar evento para manejar la selección

            cbMoneda.Items.Add("Pesos");
            cbMoneda.Items.Add("Dólares");

            cbMoneda.SelectionChanged += CbMoneda_SelectionChanged;

          

          List<CE_Denominacion> cajeras;

            //      cajeras  
            cajeras = objeto_CN_Denominacion.Cajeras(SucursalString);

            cbCajera.Items.Clear();        // Limpia los ítems actuales de cbCajera

            foreach (var Cajera in cajeras)
            {
               // cbCajera.Items.Add(Cajera.Name); // Agrega el objeto completo
                                                 // CodCajera = Cajera.INTERNAL_K;

                cbCajera.Items.Add($"{Cajera.INTERNAL_K}- {Cajera.Name}");

            }

 

        }



        private void CbMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbCantidad.Clear();
            string selectedMoneda = cbMoneda.SelectedItem.ToString();
     

             

            List<CE_Denominacion> denominaciones;
          

            if (selectedMoneda == "Pesos")
            {
                denominaciones = objeto_CN_Denominacion.Consulta("EF");
         


                /* string EF = "EF";
                 List<CE_Denominacion> denominaciones = objeto_CN_Denominacion.Consulta(EF);

                 cbDenominaciones.Items.Clear(); // Limpia los ítems actuales

                 foreach (var denominacion in denominaciones)
                 {
                     // cbDenominaciones.Items.Add($"{denominacion.Descrip} - {denominacion.AmountValue}");
                     cbDenominaciones.Items.Add($"{denominacion.Descrip}");

                     //  Monto = Convert.ToDecimal(cbDenominaciones.Items.Add(denominacion.AmountValue));


                 }*/

            }
            else if (selectedMoneda == "Dólares")
            {
                denominaciones = objeto_CN_Denominacion.Consulta("EFU");
            }
            else
            {
                return;
            }

            cbDenominaciones.Items.Clear(); // Limpia los ítems actuales
            

            foreach (var denominacion in denominaciones)
            {
                cbDenominaciones.Items.Add(denominacion); // Agrega el objeto completo
                

            }


        }



        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
       {
            string selectCajera = cbCajera.SelectedItem.ToString();
            string Status =   cbTipo.SelectedItem.ToString();
           

            //NomeCajera = selectCajera;
            // Separar el número del nombre
            string[] parts = selectCajera.Split(new[] { "- " }, StringSplitOptions.None);
            string[] parts1 = Status.Split(new[] { "- " }, StringSplitOptions.None);

            // Extraer y convertir el número en un int
            int CodUser = int.Parse(parts[0]);
            int StatusCaja = int.Parse(parts1[0]);

            // Extraer el nombre
            string NombreCajera = parts[1];



            if (GridDatos.Items == null || GridDatos.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No hay elementos que ingresar. Por favor, agregue datos antes de continuar.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return; // Salir de la función si no hay datos
            }

            string selectedMoneda = cbMoneda.SelectedItem.ToString();
            string _PayForm;
            List<string> _PayFormArray = new List<string>();
            List<decimal> _AmountValueArray = new List<decimal>();
            List<int> _QuantityArray = new List<int>();
            List<decimal> _TotAmountArray = new List<decimal>();

            if (selectedMoneda == "Pesos")
            {
                _PayForm = "EF";
            } 
            
            else
            {
                _PayForm = "EFU";
            }



            // Aquí recorremos los elementos en GridDatos y los mostramos
          //  foreach (GridItem item in GridDatos.Items)
            //{
              //  string message = $"Denominación: {item.Denominacion}, Cantidad: {item.Cantidad}, Total: {item.Total}";
                //System.Windows.Forms.MessageBox.Show(message);
            //}

            foreach (GridItem item in GridDatos.Items)
            {
                // Añadir valores a los arreglos
                _PayFormArray.Add(_PayForm);
                _AmountValueArray.Add(item.AmountValue);
                //  _IdCDenomArray.Add(item.); // Asume que IdCDenom es parte de GridItem
                _QuantityArray.Add(item.Cantidad);
                _TotAmountArray.Add(item.Total);

                // Mostrar cada línea en un MessageBox
                string message = $"Denominación: {item.Denominacion}, Cantidad: {item.Cantidad}, Total: {item.Total}";
            //    System.Windows.Forms.MessageBox.Show(message);    //muestra lo que hay linea por linea
            }
            // Convertir los arreglos a cadenas separadas por comas
      //      string _PayFormStr = string.Join(",", _PayFormArray);
            string _AmountValueStr = string.Join(",", _AmountValueArray);
            string _QuantityStr = string.Join(",", _QuantityArray);
            string _TotAmountStr = string.Join(",", _TotAmountArray);

            // Mostrar los arreglos como cadenas
            //    System.Windows.Forms.MessageBox.Show($"_PayForm: {_PayFormStr}");
            //  System.Windows.Forms.MessageBox.Show($"_AmountValue: {_AmountValueStr}");
            //   System.Windows.Forms.MessageBox.Show($"_Quantity: {_QuantityStr}");
            //   System.Windows.Forms.MessageBox.Show($"_TotAmount: {_TotAmountStr}");

          
            
            if (cbCajera.SelectedItem is CE_Denominacion selectedusuario)
            {
                int n = selectedusuario.INTERNAL_K;
            }



            objeto_CE_Denominacion.PayForm = _PayForm; //_PayFormStr;
            objeto_CE_Denominacion.IdCDenom = _AmountValueStr;
            objeto_CE_Denominacion.Quantity = _QuantityStr;
            objeto_CE_Denominacion.TotAmount = _TotAmountStr;
              objeto_CE_Denominacion.Usuario = CodUser;
            objeto_CE_Denominacion.Super = CodSuper;
            // objeto_CE_Denominacion.retiro = nombreCajaString;
            objeto_CE_Denominacion.Sucursal = SucursalString;
            objeto_CE_Denominacion.StationId = nombreCajaInt;
            objeto_CE_Denominacion.StatusCaja = StatusCaja;


            // Llamar a InsertarRetiro para insertar los valores en la base de datos
           string mensaje = objeto_CN_Denominacion.InsertarRetiro(objeto_CE_Denominacion);

            if (!string.IsNullOrEmpty(mensaje))
            {
                System.Windows.Forms.MessageBox.Show(mensaje);
            }
          

            tbCantidad.Clear();
            GridDatos.Items.Clear();

        }
         
    

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (cbDenominaciones.SelectedItem is CE_Denominacion selectedDenominacion)
            {
                if (int.TryParse(tbCantidad.Text, out int cantidad))
                {
                    decimal amountValue = selectedDenominacion.AmountValue;
                    decimal resultado = cantidad * amountValue;

                    // Crear una nueva instancia de GridItem y asignar los valores
                    GridItem newItem = new GridItem
                    {
                        Denominacion = selectedDenominacion.Descrip,
                        AmountValue = amountValue, // Cambiado a AmountValue
                        Cantidad = cantidad,
                        Total = resultado
                    };

                    // Agregar el nuevo item al DataGrid
                    GridDatos.Items.Add(newItem);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Por favor, ingresa una cantidad válida.");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se ha seleccionado ninguna denominación.");
            }


               tbCantidad.Clear();


        } 

        private void tbCantidad_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {


            // Verifica si la tecla presionada es un número (0-9) o una tecla de control permitida como Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) &&
                !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) &&
                e.Key != Key.Back)
            {

                // Mueve el foco al botón Agregar cuando se presionan Enter o Tab
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    //Agregar.Focus();
                    Agregar_Click(Agregar, new RoutedEventArgs());
                }
                else
                {


                    e.Handled = true; // Cancela la entrada de la tecla si no es válida
                    return;
                }
            }

           
        }
        private void Limpiar_Click(object sender, RoutedEventArgs e)
        {
            // Limpia todos los ítems del DataGrid
            GridDatos.Items.Clear();

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica si hay un ítem seleccionado en el DataGrid
            if (GridDatos.SelectedItem != null)
            {
                // Remueve el ítem seleccionado
                GridDatos.Items.Remove(GridDatos.SelectedItem);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se ha seleccionado ningún registro para eliminar.");
            }

        }


        public class GridItem
        {
            public string Denominacion { get; set; }
            public int Cantidad { get; set; }
            public decimal Total { get; set; }

            public Decimal AmountValue  { get; set; }
        }

        
    }
}
