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
using static PuntoVentaCCFN.MainWindow;
using System.Windows.Media.Media3D;
using static Capa_Presentacion.Views.LoginView;
using Capa_Entidad.OperacionesCaja;
using Capa_Negocio.OperacionesCaja;
using Org.BouncyCastle.Crypto;
using System.Diagnostics;




namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalAperturaSalida.xaml
    /// </summary>
    public partial class modalAperturaSalida : Window
    {
         
        readonly CN_Denominacion objeto_CN_Denominacion = new CN_Denominacion();
        readonly CE_Denominacion objeto_CE_Denominacion = new CE_Denominacion();
        readonly CN_VentaCaja   objCNVentaCaja = new CN_VentaCaja();
        public CE_VentaCaja cev = new CE_VentaCaja();
        CE_VentaCaja infoCaja = new CE_VentaCaja();
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        private string SucursalString;
        private string NombreCompany;
        private string nombreCajaString;
        private  int nombreCajaInt;
        private int CodSuper;
        public int OpcValue = 0;
        public string sMensaje = null;
        public bool status = true;


        public modalAperturaSalida(int opcValue)
        {
            CodSuper = GlobalVariables.CodSuper;
            OpcValue = opcValue;
            nombreCajaString = MainWindow.AppConfig1.Caja;
            var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
            SucursalString = SettingSection.Filler;
            NombreCompany = SettingSection.CompanyName;
            nombreCajaInt = int.Parse(nombreCajaString);
            InitializeComponent();

            if (opcValue == 1)
            {
                CargaInicial();
            } else if(opcValue == 2)
            {
                CargaInicialF();
            } else
            {
                CargaIncialR();
            }
            
            
        }

        public void CargaInicial()
        {

            bool _status;

            _status = objeto_CN_Denominacion.VerificarCaja(nombreCajaInt, SucursalString, ref sMensaje);

            if(_status)
            {
                System.Windows.MessageBox.Show("La caja ya esta abierta!!");
                status = false;
                return;
            }

            if (SucursalString.Trim() == Nom_Cajera.Cod_Sucursal.Trim())
            {

                lblCajera.Content = Nom_Cajera.Nome_Cajera;
                lblTitulo.Content = "Apertura de Caja";
                lblMov.Content = "Apertura";
                cbMoneda.Items.Add("Pesos");
                cbMoneda.Items.Add("Dólares");
                cbMoneda.SelectionChanged += CbMoneda_SelectionChanged;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Caja configurada para la sucursal: " + NombreCompany + " la Cajera/o esta asignado en la sucursal: " + Nom_Cajera.Nom_Sucursal + " Se debe de reasignar", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                status = false;
                return;
            }

        }

        public void CargaInicialF()
        {
            if (SucursalString.Trim() == Nom_Cajera.Cod_Sucursal.Trim())
            {

                lblCajera.Content = Nom_Cajera.Nome_Cajera;
                lblTitulo.Content = "Retiro Fondo Caja";
                lblMov.Content = "Retiro de Fondo Caja";
                cbMoneda.Items.Add("Pesos");
                cbMoneda.Items.Add("Dólares");
                cbMoneda.SelectionChanged += CbMoneda_SelectionChanged;

                infoCaja = objCNVentaCaja.infoCaja(Nom_Cajera.Num_Cajera, SucursalString.Trim(), nombreCajaInt);
                lblP.Content = "Monto Pesos: " + infoCaja.BegAmount.ToString();
                lblD.Content = "Monto Dolares: " + infoCaja.BegAmountFC.ToString();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Caja configurada para la sucursal: " + NombreCompany + " la Cajera/o esta asignado en la sucursal: " + Nom_Cajera.Nom_Sucursal + " Se debe de reasignar", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }


        }

        public void CargaIncialR()
        {
            if (SucursalString.Trim() == Nom_Cajera.Cod_Sucursal.Trim())
            {

                lblCajera.Content = Nom_Cajera.Nome_Cajera;
                lblTitulo.Content = "Retiro De Efectivo";
                lblMov.Content = "Retiro De Efectivo";
                cbMoneda.Items.Add("Pesos");
                cbMoneda.Items.Add("Dólares");
                cbMoneda.SelectionChanged += CbMoneda_SelectionChanged;
                
                infoCaja = objCNVentaCaja.infoCaja(Nom_Cajera.Num_Cajera, SucursalString.Trim(), nombreCajaInt);

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Caja configurada para la sucursal: " + NombreCompany + " la Cajera/o esta asignado en la sucursal: " + Nom_Cajera.Nom_Sucursal + " Se debe de reasignar", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
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
           if(OpcValue == 1)
            {
                AperturaCaja();
            } else if (OpcValue == 2)
            {
                RetiroFondoCaja();
            } else
            {
                RetiroEfectivo();
            }

        }

        public void AperturaCaja()
        {
            string selectCajera = Nom_Cajera.Num_Cajera;

            decimal beginAmount = 0;
            decimal beginAmountFC = 0;

            string WhsCode = Nom_Cajera.Cod_Sucursal;
            string User = CodSuper.ToString();
            int StationId = nombreCajaInt;
            int IdTra = 0;
            int Error = 0;


            //string Status =   cbTipo.SelectedItem.ToString();

            foreach (GridItem item in GridDatos.Items)
            {
                if (item.PayForm.Equals("EF"))
                {
                    beginAmount += item.Total;
                }
                else
                {
                    beginAmountFC += item.Total;
                }
            }

            CE_VentaCaja objVc = new CE_VentaCaja
            {
                User = User,  // User,
                WhsCode = WhsCode,
                BegAmount = beginAmount,
                BegAmountFC = beginAmountFC,
                Cashier = selectCajera, // selectCajera,
                StationId = StationId,
            };


            cev = objCNVentaCaja.insertVentaCaja(objVc);

            if (cev.IdCash < 0)
            {
                System.Windows.MessageBox.Show("Hubo un error al crear apertura de caja!!");
                return;
            }

            foreach (GridItem item in GridDatos.Items)
            {
                CE_Denominacion objCed = new CE_Denominacion
                {
                    IdCash = cev.IdCash,
                    PayForm = item.PayForm,
                    IdCDenom = item.IdCDenom,
                    Quantity = item.Cantidad,
                    TotAmount = item.Total,
                    Usuario = CodSuper,
                    Idtrans = IdTra
                };

                IdTra = objCNVentaCaja.insertCajaDenom(objCed);

                

            }


            if (beginAmount > 0)
            {
                CE_MovCaja objMovC = new CE_MovCaja
                {
                    IdCash = cev.IdCash,
                    Type = "AC",
                    Currency = "MXN",
                    Amount = beginAmount,
                    Comments = "APERTURA DE CAJA",
                    User = Nom_Cajera.Num_Cajera,
                    Supervisor = CodSuper.ToString(),
                    Sucursal = Nom_Cajera.Cod_Sucursal,
                      Idtrans = IdTra
                };

                Error = objCNVentaCaja.insertMovCaja(objMovC);
            }

            if (beginAmountFC > 0)
            {
                CE_MovCaja objMovC = new CE_MovCaja
                {
                    IdCash = cev.IdCash,
                    Type = "AC",
                    Currency = "USD",
                    Amount = beginAmountFC,
                    Comments = "APERTURA DE CAJA",
                    User = Nom_Cajera.Num_Cajera,
                    Supervisor = CodSuper.ToString(),
                    Sucursal = Nom_Cajera.Cod_Sucursal,
                    Idtrans = IdTra
                };

                Error = objCNVentaCaja.insertMovCaja(objMovC);
            }

            if (Error == 1)
            {

                // Crear una instancia de ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja

                startInfo.Arguments = $"{IdTra}";
                //  startInfo.Arguments = $"{idCash} {Param}";

                // Ejecutar el programa externo
                Process.Start(startInfo);


                System.Windows.MessageBox.Show("Apertura Creada Con Exito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Apertura No Creada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
            this.Close();
        }

        public void RetiroFondoCaja()
        {

            decimal beginAmount = 0;
            decimal beginAmountFC = 0;
            int IdTra = 0;
            int Error = 0;

            //string Status =   cbTipo.SelectedItem.ToString();

            foreach (GridItem item in GridDatos.Items)
            {
                if (item.PayForm.Equals("EF"))
                {
                    beginAmount += item.Total;
                }
                else
                {
                    beginAmountFC += item.Total;
                }
            }

            if(infoCaja.BegAmount != 0)
            {
                if(infoCaja.BegAmount != beginAmount)
                {
                    System.Windows.MessageBox.Show("Debes retirar la cantidad igual a apartura de caja en MXN");
                    return;
                }
            }

            if (infoCaja.BegAmountFC != 0)
            {
                if (infoCaja.BegAmountFC != beginAmountFC)
                {
                    System.Windows.MessageBox.Show("Debes retirar la cantidad igual a apartura de caja en USD");
                    return;
                }
            }

            foreach (GridItem item in GridDatos.Items)
            {
                CE_Denominacion objCed = new CE_Denominacion
                {
                    IdCash = infoCaja.IdCash,
                    PayForm = item.PayForm,
                    IdCDenom = item.IdCDenom,
                    Quantity = item.Cantidad,
                    TotAmount = item.Total,
                    Usuario = CodSuper,
                    Idtrans = IdTra
                };

                IdTra = objCNVentaCaja.insertCajaDenom(objCed);
               
            }

            if (beginAmount > 0)
            {
                CE_MovCaja objMovC = new CE_MovCaja
                {
                    IdCash = infoCaja.IdCash,
                    Type = "RF",
                    Currency = "MXN",
                    Amount = beginAmount,
                    Comments = "RETIRO DE FONDO",
                    User = Nom_Cajera.Num_Cajera,
                    Supervisor = CodSuper.ToString(),
                    Sucursal = Nom_Cajera.Cod_Sucursal,
                    Idtrans = IdTra
                };

               Error = objCNVentaCaja.insertMovCaja(objMovC);
            }

            if (beginAmountFC > 0)
            {
                CE_MovCaja objMovC = new CE_MovCaja
                {
                    IdCash = infoCaja.IdCash,
                    Type = "RF",
                    Currency = "USD",
                    Amount = beginAmountFC,
                    Comments = "RETIRO DE FONDO",
                    User = Nom_Cajera.Num_Cajera,
                    Supervisor = CodSuper.ToString(),
                    Sucursal = Nom_Cajera.Cod_Sucursal,
                    Idtrans = IdTra
                };

               Error = objCNVentaCaja.insertMovCaja(objMovC);
            }

           // System.Windows.MessageBox.Show("Retiro de Fondo Exitoso!!");


            if (Error == 1)
            {

                // Crear una instancia de ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja
                                                                                      
                startInfo.Arguments = $"{IdTra}";
                //  startInfo.Arguments = $"{idCash} {Param}";

                // Ejecutar el programa externo
                Process.Start(startInfo);

                //System.Windows.MessageBox.Show("Apertura Creada Con Exito");
                System.Windows.MessageBox.Show("Retiro de Fondo Exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Retiro de Fondo No Creada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            this.Close();

        }

        public void RetiroEfectivo()
        {

            decimal beginAmount = 0;
            decimal beginAmountFC = 0;
            int IdTra = 0;

            foreach (GridItem item in GridDatos.Items)
            {
                if (item.PayForm.Equals("EF"))
                {
                    beginAmount += item.Total;
                }
                else
                {
                    beginAmountFC += item.Total;
                }
            }

            foreach (GridItem item in GridDatos.Items)
            {
                CE_Denominacion objCed = new CE_Denominacion
                {
                    IdCash = infoCaja.IdCash,
                    PayForm = item.PayForm,
                    IdCDenom = item.IdCDenom,
                    Quantity = item.Cantidad,
                    TotAmount = item.Total,
                    Usuario = CodSuper,
                    Idtrans = IdTra

                };

                IdTra = objCNVentaCaja.insertCajaDenom(objCed);
               // IdTra = IdTra + 1;
            }

            if (beginAmount > 0)
            {
                CE_MovCaja objMovC = new CE_MovCaja
                {
                    IdCash = infoCaja.IdCash,
                    Type = "RE",
                    Currency = "MXN",
                    Amount = beginAmount,
                    Comments = "RETIRO DE EFECTIVO",
                    User = Nom_Cajera.Num_Cajera,
                    Supervisor = CodSuper.ToString(),
                    Sucursal = Nom_Cajera.Cod_Sucursal,
                    Idtrans = IdTra
                };

                objCNVentaCaja.insertMovCaja(objMovC);
            }

            if (beginAmountFC > 0)
            {
                CE_MovCaja objMovC = new CE_MovCaja
                {
                    IdCash = infoCaja.IdCash,
                    Type = "RE",
                    Currency = "USD",
                    Amount = beginAmountFC,
                    Comments = "RETIRO DE EFECTIVO",
                    User = Nom_Cajera.Num_Cajera,
                    Supervisor = CodSuper.ToString(),
                    Sucursal = Nom_Cajera.Cod_Sucursal,
                    Idtrans = IdTra
                };

                objCNVentaCaja.insertMovCaja(objMovC);
            }

            // Crear una instancia de ProcessStartInfo
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja

            startInfo.Arguments = $"{IdTra}";
            //  startInfo.Arguments = $"{idCash} {Param}";

            // Ejecutar el programa externo
            Process.Start(startInfo);

            //System.Windows.MessageBox.Show("Apertura Creada Con Exito");


            System.Windows.MessageBox.Show("Retiro de Efectivo Exitoso!!");
            this.Close();
        
        
        
        
        
        
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
                        IdCDenom = selectedDenominacion.IdCDenom,
                        Denominacion = selectedDenominacion.Descrip,
                        PayForm = selectedDenominacion.PayForm,
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
            public int IdCDenom {  get; set; }
            public string Denominacion { get; set; }
            public string PayForm { get; set; }
            public int Cantidad { get; set; }
            public decimal Total { get; set; }

            public Decimal AmountValue  { get; set; }
        }

        
    }
}
