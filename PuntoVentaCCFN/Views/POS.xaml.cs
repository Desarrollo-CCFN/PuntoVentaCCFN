﻿using Capa_Negocio;
using Capa_Negocio.Productos;
using Capa_Negocio.Venta;
using Capa_Presentacion.SCS.Boxes;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Diagnostics;
using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Capa_Entidad.Venta;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Configuration;
using Capa_Presentacion.Views;
using System.Reflection;
using System.ComponentModel;
using static PuntoVentaCCFN.MainWindow;
using static Capa_Presentacion.Views.LoginView;
using Capa_Entidad.OperacionesCaja;
using Capa_Negocio.OperacionesCaja;
using Capa_Entidad;
using System.Windows.Threading;
using Org.BouncyCastle.Crypto;
using System.Text.RegularExpressions;

namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para POS.xaml
    /// </summary>
    /// 

    public static class GlobalVariables
    {
        public static int CodSuper { get; set; }
    }


    public partial class POS : System.Windows.Controls.UserControl
    {
        readonly CN_Denominacion objeto_CN_Denominacion = new CN_Denominacion();
        readonly CN_Clientes objeto_CN_Clientes = new CN_Clientes();
        readonly CN_TipoCambio objeto_CN_TipoCambio = new CN_TipoCambio();
        readonly CN_ListaPrecios objeto_CN_ListaPrecios = new CN_ListaPrecios();
        readonly CN_Productos objeto_CN_Productos = new CN_Productos();
        CE_VentaCaja infoCaja = new CE_VentaCaja();
        CE_MovCaja objMovCaja = new CE_MovCaja();
        readonly CN_VentaCaja objCNVentaCaja = new CN_VentaCaja();
        readonly CN_Venta venta = new CN_Venta();
        CE_VentaHeader ventaI = new CE_VentaHeader();
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        BasePrinter printer;
        List<GridList> lista = new List<GridList>();

        public string codigoClienteFactura;
        public string nombreClienteFactura;

        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public int listPrecios;
        public string cardCode;
        public string whsCode;
        public string nombreCaja;
        public decimal tipoCambio;
        public string sMensaje = null;
        public bool pagoUSD = false;
        private string nombreCajaString;
        private int nombreCajaInt;
        private string sCodigoBarra;
        private bool bGs11128 = false;


        public POS()
        {
            InitializeComponent();
            this.tbCodigoProducto.Focus();
            IniciarConfiguracion();
            VerificarVenta();
        }

        void IniciarConfiguracion()
        {
            var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
            try
            {
                printer = new SerialPrinter(portName: "COM8", baudRate: 9600);
            }
            catch (Exception ex) { }

            listPrecios = SettingSection.DefListNum;
            cardCode = SettingSection.DefCardCode;
            nombreCajaString = MainWindow.AppConfig1.Caja;
            nombreCajaInt = int.Parse(nombreCajaString);
            whsCode = SettingSection.Filler;
            tipoCambio = SettingSection.DefRateRetail;
            tbMoneda.Text = SettingSection.DefCurrency;

            if(tbMoneda.Text == "USD")
            {
                btnE.IsEnabled = false;
                btnD.IsEnabled = false;
                btnC.IsEnabled = false;
               // btnEU.IsEnabled = false;
            }

            nombreCaja = MainWindow.AppConfig1.Caja;  //"1";
           
        }

        #region verificar venta activa
        public void VerificarVenta()
        {
            CE_VentaHeader ventaActiva = venta.VentaActiva(whsCode, nombreCajaInt);


            if (ventaActiva.Id != -1)
            {
                ventaI.Id = ventaActiva.Id;
                System.Windows.MessageBox.Show("Se encontro una venta sin terminar. Recuperando..", "Venta Activa", MessageBoxButton.OK, MessageBoxImage.Information);


                List<CE_VentaDetalle> listaProductos = venta.GetVentaDetalle(ventaActiva.Id);


                foreach (CE_VentaDetalle item in listaProductos)
                {
                    GridList l = new GridList();
                    l.ItemCode = item.ItemCode;
                    l.ItemName = item.Dscription;
                    l.CodeBar = item.CodeBars;
                    l.Precio_Base = item.Price2;
                    l.Unidad = item.Unidad;
                    l.UomEntry = item.UomEntry;

                    if (ventaActiva.DocCur == "USD")        //if (tbMoneda.Text == "USD")
                    {
                        l.Total = item.TotalFrgn;
                        l.Precio_Base_FC = item.PriceList;
                    }
                    else 
                    {
                        l.Total = item.LineTotal;
                        
                    }
                    l.Impuesto_FC = item.VatSumFrgn;
                    l.Impuesto = item.VatSum;

                    if (item.Currency == "USD")
                    {
                        l.Precio_Base_FC = item.PriceList;
                    }else
                    {

                        decimal dBase0 = 0;
                        l.Precio_Base_FC = dBase0;
                    }
                     
                        l.LineNum = item.LineNum;
                    l.Cantidad = item.Cantidad;
                    lista.Add(l);

                }

                GridDatos.ItemsSource = null;
                GridDatos.ItemsSource = lista;
                for (int i = 0; GridDatos.Columns.Count > i; i++)
                {
                    GridDatos.Columns[i].IsReadOnly = true;
                }
                GridDatos.Columns[7].IsReadOnly = false;
                pagado = venta.GetVentaActivaPagado(ventaActiva.Id);
                tbMoneda.Text = ventaActiva.DocCur;
                Dispatcher.InvokeAsync(() => { saldo(); },
                DispatcherPriority.ApplicationIdle);


                if (tbMoneda.Text == "USD")
                {
                    btnE.IsEnabled = false;
                    btnD.IsEnabled = false;
                    btnC.IsEnabled = false;
                     btnEU.IsEnabled = true;
                }else
                {
                    btnE.IsEnabled = true;
                    btnD.IsEnabled = true;
                    btnC.IsEnabled = true;
                    btnEU.IsEnabled = false;

                }



            }
        }
        #endregion

        #region regex para validacion de input numerico
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        #endregion

        #region consulta del tipo de cambio
        public void ConsultarTC()
        {

            //var tipoCambio = objeto_CN_TipoCambio.Consulta();
            tbTipoCambio.Text = tipoCambio.ToString("F4");
        }
        #endregion

        #region consulta inicial del cliente default
        public void ConsultarCliente()
        {
            var clienteDatos = objeto_CN_Clientes.Consulta(cardCode);
            tbCodigoCliente.Text = clienteDatos.CardCode.ToString();
            tbNombreCliente.Text = clienteDatos.CardName.ToString();
        }
        #endregion

        #region consulta inicial de lista de precio default
        public void ConsultarListaPrecio()
        {
            var listaPrecio = objeto_CN_ListaPrecios.Consulta(listPrecios);
            tbListaPrecio.Text = listaPrecio.ListName.ToString();
        }
        #endregion

        #region busqueda e inserción header y detalle primera vez
        private void tbCodigoProducto_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if(tbCodigoProducto.Text.Length < 5) { tbCodigoProducto.Text = "";  return; }
           // int iCadenaLong = tbCodigoProducto.Text.Length;

            if (e.Key == Key.Enter)
            {

                if (tbCodigoProducto.Text == "") return;

                if (bGs11128 == true) 
                {
                    tbCodigoProducto.Text = sCodigoBarra;
                }
                operacionBusquedaInsercio();

                tbCodigoProducto.Text = "";
                sCodigoBarra = "";
                bGs11128 = false;
            }
            else
            {

                // Get the character representation of the key pressed
                char keyChar = (char)KeyInterop.VirtualKeyFromKey(e.Key);
                int asci = Convert.ToInt32(keyChar);

                if (asci > 31 && asci < 128) // numeric and chars only
                {
                    sCodigoBarra += keyChar;
                }
                else
                {
                    if ((asci == 29) || (asci == 162))
                    {
                        // GS1 Separator
                        sCodigoBarra += "@"; // GS1 Seperator    
                        bGs11128 = true;
                    }
                }


                /*
                //  e.InputSource
                int asci = Convert.ToInt32(e.Key);
                if (asci > 31 && asci < 128) // numeric and chars only
                    sCodigoBarra += Convert.ToChar(asci); //Convert.ToChar((int)(e.Key & 0xffff));
                else
                {
                    if (asci == 29)
                    {
                        //rawbcode += "<GS>"; // GS1 Seperator    
                        sCodigoBarra += "@"; // GS1 Seperator    
                    }
                }
                */
            }
        }

        private void operacionBusquedaInsercio()
        {
            CN_Carrito carrito = new CN_Carrito();

            if (ventaI.Id.Equals(0))
            {
                string numCajera = Nom_Cajera.Num_Cajera;
                CE_Denominacion c = objeto_CN_Denominacion.GetIdCash(nombreCajaInt, whsCode, numCajera, ref sMensaje);
                ventaI = venta.insertarVenta(whsCode, nombreCaja, tbCodigoCliente.Text.ToString(), c.IdCash, nombreCajaInt, tbMoneda.Text); //TODO obtener id cash actual y tomar el vendedor(default vendedor estandar)
            }

            if (ventaI.Id.Equals(-1))
            {
                MessageBox.Show(ventaI.Comments);
                return;
            }

            DataTable dt;
            sMensaje = "";
            dt = carrito.buscarProducto(tbCodigoProducto.Text, listPrecios, tbMoneda.Text, whsCode, ventaI.Id, ref sMensaje);

            if ((dt == null) || (sMensaje != ""))
            {
                MessageBox.Show(sMensaje);
                return;
            }

            DataRow row = dt.Rows[0];
            CE_VentaDetalle ce_Detalle = new CE_VentaDetalle();
            GridList l = new GridList();

            ce_Detalle.IdHeader = ventaI.Id;
            ce_Detalle.ItemCode = Convert.ToString(row[0]);
            ce_Detalle.Cantidad = Convert.ToDecimal(row[12]);
            ce_Detalle.Currency = tbMoneda.Text;

            if (ce_Detalle.Currency == "USD")
            {
                ce_Detalle.Monto = Convert.ToDecimal(row[13]);
            }
            else 
            {
                ce_Detalle.Monto = Convert.ToDecimal(row[6]);
            }

            
            
            ce_Detalle.WhsCode = whsCode;
            ce_Detalle.CodeBars = tbCodigoProducto.Text;
            ce_Detalle.PriceList = listPrecios;
            ce_Detalle.UomEntry = Convert.ToInt32(row[5]);
            ce_Detalle.LineNum = Convert.ToInt32(row[11]);

            if (!venta.insertarDetalleLive(ce_Detalle, ref sMensaje))
            {
                MessageBox.Show(sMensaje);
                return;
            }

            l.ItemCode = Convert.ToString(row[0]);
            l.ItemName = Convert.ToString(row[1]);
            l.CodeBar = Convert.ToString(row[2]);
            l.Precio_Base = Convert.ToDecimal(row[3]);
            l.Unidad = Convert.ToString(row[4]);
            l.UomEntry = Convert.ToInt32(row[5]);

            if (tbMoneda.Text == "USD")
            {
                l.Total = Convert.ToDecimal(row[13]);
            }
            else
            {
                l.Total = Convert.ToDecimal(row[6]);
            }

            
            l.Impuesto_FC = Convert.ToDecimal(row[10]);
            l.Impuesto = Convert.ToDecimal(row[7]);
            l.Precio_Base_FC = Convert.ToDecimal(row[9]);
            l.LineNum = Convert.ToInt32(row[11]);
            l.Cantidad = Convert.ToDecimal(row[12]);
            lista.Add(l);

            GridDatos.ItemsSource = null;
            GridDatos.ItemsSource = lista;
            //GridDatos.Items.Add(dt.DefaultView);

            for (int i = 0; GridDatos.Columns.Count > i; i++)
            {
                GridDatos.Columns[i].IsReadOnly = true;
            }
            GridDatos.Columns[7].IsReadOnly = false;  // cantidad


            saldo();

        }
        #endregion

        #region calculo del saldo en cada insersion de producto
        decimal total, cambio, pagado, subTotal, totalUSD, qty;
        private void saldo()
        {
            Nom_Cajera.logoff = 0;
            total = 0;
            totalUSD = 0;
            subTotal = 0;
            qty = 0;
            int iCount = 0;
            for (int i = 0; i < GridDatos.Items.Count; i++)
            {
                decimal precioTotal;
                decimal precioSubtotal;
                decimal quan;
                int j = 8;
                DataGridCell celda = GetCelda(i, j);
                TextBlock tb = celda.Content as TextBlock;
                
                precioTotal = decimal.Parse(tb.Text);
                
                total += precioTotal;

                if (tbMoneda.Text == "MXN")
                {
                    totalUSD += precioTotal / Convert.ToDecimal(tbTipoCambio.Text);
                }
                else
                {
                    totalUSD += precioTotal;
                }

                int m = 7;
                DataGridCell celda2 = GetCelda(i, m);
                TextBlock tb2 = celda2.Content as TextBlock;
                quan = decimal.Parse(tb2.Text);
                qty = quan;

                int k = 3;   // mxn
                if (tbMoneda.Text == "USD")
                {
                    k = 5;
                }
                
                DataGridCell celda1 = GetCelda(i, k);
                TextBlock tb1 = celda1.Content as TextBlock;
                precioSubtotal = decimal.Parse(tb1.Text) * qty;
                subTotal += precioSubtotal;
                Nom_Cajera.logoff = 1;
                iCount++;
            }

            cambio = pagado - total;

            tbImporte.Text = tbMoneda.Text + " $" + total.ToString("N2");

            if (tbMoneda.Text == "MXN")
            {
                lblTotal.Text = "Total USD";
                tbImporteUSD.Text = "$" + totalUSD.ToString("N2");
                lblSaldo.Text = "Saldo USD";
                decimal dSaldo = totalUSD - (pagado / Convert.ToDecimal(tbTipoCambio.Text));
                if (dSaldo < 0) 
                {
                    dSaldo = 0;
                }
                tbSaldoUSD.Text = "$" + dSaldo.ToString("N2");
            }
            else
            {
                lblTotal.Text = "Total MXN";
                decimal  dImpNacional = totalUSD * Convert.ToDecimal(tbTipoCambio.Text);
                tbImporteUSD.Text = "$" + dImpNacional.ToString("N2");
                lblSaldo.Text = "Saldo MXN";
                decimal dSaldo = totalUSD - pagado;
                
                if (dSaldo < 0)
                {
                    dSaldo = 0; 
                }

                dSaldo = dSaldo * Convert.ToDecimal(tbTipoCambio.Text);

                tbSaldoUSD.Text = "$" + dSaldo.ToString("N2");
            }
            

            tbSubtotal.Text = tbMoneda.Text + " $" + subTotal.ToString("N2");
            tbPagado.Text = tbMoneda.Text + " $" + pagado.ToString("###,###.00");
            tbCambio.Text = tbMoneda.Text + " $" + cambio.ToString("N2");

            lblTotalPartidas.Text = "Cant. Productos: " + iCount ;

        }
        #endregion

        #region formas de pago de la venta
        private void Efectivo(object sender, RoutedEventArgs e)
        {
            if (GridDatos.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Debes ingresar productos!!");
                return;
            }
                loadEMXN();
        }

        public void loadEMXN()
        {
            var ingresar = new Ingresar();
            ingresar.ShowDialog();

            if (ingresar.Efectivo == 0)
            {
                return;
            }

            if (ingresar.Efectivo > 0)
            {
                Decimal TipoCambo = Convert.ToDecimal(tbTipoCambio.Text);
                decimal dImporte = 0;
                if (tbMoneda.Text == "USD")
               {
                    dImporte = (ingresar.Efectivo / TipoCambo);
                }
               else
               {
                     dImporte = ingresar.Efectivo;
                }

                pagado += Math.Round(dImporte,2);

                saldo();

                CE_VentaPagos ventaPago = new CE_VentaPagos();
                ventaPago.Payform = "EF";
                ventaPago.Currency = "MXN";
                // ventaPago.Rate = 1;
                ventaPago.Rate = Convert.ToDecimal(tbTipoCambio.Text);
                ventaPago.AmountPay = ingresar.Efectivo;
                ventaPago.VoucherNum = "";
                if (cambio < 0)
                {
                    ventaPago.BalAmout = cambio;
                }
                else
                {
                    ventaPago.BalAmout = 0;
                }

                ventaPago.IdHeader = ventaI.Id;
                string sMensaje = "";
                venta.insertarVentaPago(ventaPago, ref sMensaje);

                if (sMensaje != "")
                {
                    // pagado -= ingresar.Efectivo;
                    pagado -=  Math.Round(dImporte, 2);
                    saldo();
                    MessageBox.Show(sMensaje);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ingresa una cantidad valida!!");
            }
        }

        private void EfectivoUSD(object sender, RoutedEventArgs e)
        {
            if (GridDatos.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Debes ingresar productos!!");
                return;
            }
            loadEUSD();
        }

        public void loadEUSD()
        {
            var ingresar = new Ingresar();
            ingresar.ShowDialog();

            if (ingresar.Efectivo == 0)
            {
                return;
            }

            if (ingresar.Efectivo > 0)
            {
                decimal dImporte = 0;
                if (tbMoneda.Text == "USD")
                {
                    dImporte = ingresar.Efectivo;
                    
                }
                else
                {
                    dImporte = ingresar.Efectivo * Convert.ToDecimal(tbTipoCambio.Text);
                }

                pagado += Math.Round(dImporte,2);

                saldo();
                CE_VentaPagos ventaPago = new CE_VentaPagos();
                ventaPago.Payform = "EFU";
                ventaPago.Currency = "USD";
                ventaPago.Rate = Convert.ToDecimal(tbTipoCambio.Text);
                ventaPago.AmountPay = ingresar.Efectivo;
                ventaPago.VoucherNum = "";
                if (cambio < 0)
                {
                    ventaPago.BalAmout = cambio;
                }
                else
                {
                    ventaPago.BalAmout = 0;
                }

                ventaPago.IdHeader = ventaI.Id;
                string sMensaje = "";
                venta.insertarVentaPago(ventaPago, ref sMensaje);

                if (sMensaje != "")
                {
                    //    pagado -= ingresar.Efectivo * Convert.ToDecimal(tbTipoCambio.Text);
                    pagado -= Math.Round(dImporte, 2);

                    saldo();
                    MessageBox.Show(sMensaje);
                    return;
                }

                pagoUSD = true;

            }
            else
            {
                MessageBox.Show("Ingresa una cantidad valida!!");
            }
        }
        private void Tarjeta(object sender, RoutedEventArgs e)
        {
            if (GridDatos.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Debes ingresar productos!!");
                return;
            }
            loadDebit();
        }

        public void loadDebit()
        {
            var ingresar = new ingresarT();
            ingresar.ShowDialog();

            if (ingresar.Cantidad == 0)
            {
                return;
            }

            if (ingresar.Cantidad > 0)
            {

                Decimal TipoCambo = Convert.ToDecimal(tbTipoCambio.Text);
                decimal dImporte = 0;  
                if (tbMoneda.Text == "USD")
                {
                    dImporte = (ingresar.Cantidad / TipoCambo);
                }
                else
                {
                    dImporte = ingresar.Cantidad;
                }

                CE_VentaPagos ventaPago = new CE_VentaPagos();
                ventaPago.Payform = "TD";
                ventaPago.Currency = "MXN";
                // ventaPago.Rate = 1;
                ventaPago.Rate = Convert.ToDecimal(tbTipoCambio.Text);
                ventaPago.AmountPay = ingresar.Cantidad;
                ventaPago.VoucherNum = ingresar.Voucher;
                if (cambio < 0)
                {
                    ventaPago.BalAmout = Math.Abs(cambio);
                }
                else
                {
                    ventaPago.BalAmout = 0;
                }
                ventaPago.IdHeader = ventaI.Id;
                string sMensaje = "";
                venta.insertarVentaPago(ventaPago, ref sMensaje);

                if (sMensaje != "")
                {
                    // pagado -= ingresar.Cantidad;
                    //pagado -= Math.Round(dImporte, 2);

                    //saldo();
                    MessageBox.Show(sMensaje);
                    return;
                }
                else
                {



                    pagado += Math.Round(dImporte, 2);
                    /// pagado = dImporte;

                    saldo();
                }

            }
            else
            {
                MessageBox.Show("Ingresa una cantidad valida!!");
            }
        }

        private void TarjetaC(object sender, RoutedEventArgs e)
        {
            if (GridDatos.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Debes ingresar productos!!");
                return;
            }

            loadCredit();
        }

        public void loadCredit()
        {
            var ingresar = new ingresarT();
            ingresar.ShowDialog();

            if (ingresar.Cantidad == 0)
            {
                return;
            }

            if (ingresar.Cantidad > 0)
            {
                Decimal TipoCambo = Convert.ToDecimal(tbTipoCambio.Text);
                decimal dImporte = 0;
                if (tbMoneda.Text == "USD")
                {
                    dImporte = (ingresar.Cantidad / TipoCambo);
                }
                else
                {
                    dImporte = ingresar.Cantidad;
                }

              //  pagado += Math.Round(dImporte,2);

             //   saldo();
                CE_VentaPagos ventaPago = new CE_VentaPagos();
                ventaPago.Payform = "TC";
                ventaPago.Currency = "MXN";
                //   ventaPago.Rate = 1;
                ventaPago.Rate = Convert.ToDecimal(tbTipoCambio.Text);
                ventaPago.AmountPay = ingresar.Cantidad;
                ventaPago.VoucherNum = ingresar.Voucher;
               
                if (cambio < 0)
                {
                    ventaPago.BalAmout = Math.Abs(cambio);
                }
                else
                {
                    ventaPago.BalAmout = 0;
                }
                ventaPago.IdHeader = ventaI.Id;

                string sMensaje = "";
                venta.insertarVentaPago(ventaPago, ref sMensaje);
                
                if (sMensaje != "")
                {
                    //    pagado -= ingresar.Cantidad;
                   // pagado -= Math.Round(dImporte, 2);

                  //  saldo();
                    MessageBox.Show(sMensaje);
                    return;
                }
                else
                {
                    // pagado -= Math.Round(dImporte, 2);
                    pagado += Math.Round(dImporte, 2);
                    saldo();



                }
            }
            else
            {
                MessageBox.Show("Ingresa una cantidad valida!!");
            }
        }
        #endregion

        #region busqueda de cliente
        private void buscarClBtn_Click(object sender, RoutedEventArgs e)
        {
            var busquedaCliente = new modalClientes();
            busquedaCliente.ShowDialog();

            if (busquedaCliente.codigoCliente != null)
            {
                tbCodigoCliente.Text = busquedaCliente.codigoCliente.ToString();
                tbNombreCliente.Text = busquedaCliente.nombreCliente.ToString();
                tbListaPrecio.Text = busquedaCliente.listaPrecio.ToString();
                listPrecios = int.Parse(busquedaCliente.numLista);
            }
            this.tbCodigoProducto.Focus();
        }
        #endregion

        #region busqueda de producto
        private void buscarProd_Click(object sender, RoutedEventArgs e)
        {
            loadModal();
            tbCodigoProducto.Focus();
        }

        public void loadModal()
        {
            var busquedaProducto = new modalProductos();
            busquedaProducto.ShowDialog();

            if (busquedaProducto.cadenaBusqueda != null)
            {
                tbCodigoProducto.Text = busquedaProducto.cadenaBusqueda;
                operacionBusquedaInsercio();
                tbCodigoProducto.Text = "";
            }

        }
        #endregion

        #region logica editar cantidad
        private void GridDatos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            DataGridRow row = e.Row;
            GridList item = row.Item as GridList;
            var obj = lista.FirstOrDefault(x => x.LineNum == item.LineNum);
            var Acceso = new Acceso(2);
            Acceso.ShowDialog();

            if (Acceso.ReturnValue == 1)
            {

                if (obj != null)
                {
                    var t = (e.EditingElement as System.Windows.Controls.TextBox).Text;

                    if(!IsTextAllowed(t))
                    {
                        System.Windows.MessageBox.Show("Debes ingresar solo datos numericos!!");
                        GridDatos.ItemsSource = null;
                        GridDatos.ItemsSource = lista;
                        return;
                    }

                    if(Convert.ToDecimal(t) <= 0)
                    {
                        System.Windows.MessageBox.Show("Debes ingresar una cantidad valida");
                        GridDatos.ItemsSource = null;
                        GridDatos.ItemsSource = lista;
                        return;
                    }


                   // System.Windows.MessageBox.Show(Nom_Supervisor.U_Name);
                    string sMensaje = "";
                    if (!objeto_CN_Productos.AnulacionProducto(ventaI.Id, item.LineNum, Convert.ToDecimal(t), ref sMensaje))
                    {
                        MessageBox.Show("Ocurrio un error al actualizar cantidad de producto!!" + "\n" + sMensaje);
                        GridDatos.ItemsSource = null;
                        GridDatos.ItemsSource = lista;
                        return;
                    }
                    else
                    {
                        if (tbMoneda.Text == "USD")
                        {
                            obj.Cantidad = Convert.ToDecimal(t);

                            // Impuesto
                            // Impuesto_FC


                            obj.Total = Convert.ToDecimal(t) * (obj.Precio_Base_FC+ obj.Impuesto_FC);
                        }
                        else
                        {
                            obj.Cantidad = Convert.ToDecimal(t);
                            obj.Total = Convert.ToDecimal(t) * (obj.Precio_Base + obj.Impuesto);

                        }
                    }

                }

                GridDatos.ItemsSource = null;
                GridDatos.ItemsSource = lista;
                saldo();

            }
            else
            {
                MessageBox.Show("No tienes acceso a cambiar cantidad!!");
                GridDatos.ItemsSource = null;
                GridDatos.ItemsSource = lista; saldo();

            }
        }
        #endregion

        #region teclas rapidas
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // MessageBox.Show($"Key pressed: {e.Key}");

            if (e.Key == Key.F1)
            {
                System.Windows.MessageBox.Show("Tecla Recalculo");
            }

            if (e.Key == Key.F2)
            {
                if (GridDatos.Items.Count >= 1)
                {
                    if (pagado <= 0)
                    {
                        System.Windows.MessageBox.Show("Ingresa una forma de pago!!.");
                        return;
                    }
                    ventaCambio();

                }
                else
                {
                    System.Windows.MessageBox.Show("No se han agregado productos!");
                }
            }

            if (e.Key == Key.F3)
            {
                loadAnularVenta();
            }

            if (e.Key == Key.F4)
            {
                loadAnularProducto();
            }

            if (e.Key == Key.F5)
            {
                loadAnularFPago();
            }

            if (e.Key == Key.F6)
            {
                loadModal();

            }

            if (e.Key == Key.F7)
            {
                if(tbMoneda.Text == "USD")
                {
                    System.Windows.MessageBox.Show("Forma de pago no valido para venta en USD!!");
                    return;
                }
                loadEMXN();
            }

            if (e.Key == Key.F8)
            {
                if (tbMoneda.Text == "MXN")
                {
                    System.Windows.MessageBox.Show("Forma de pago no valido para venta en Pesos!!");
                    return;
                }

                loadEUSD();
            }

            if (e.Key == Key.F9)
            {
                if (tbMoneda.Text == "USD")
                {
                    System.Windows.MessageBox.Show("Forma de pago no valido para venta en USD!!");
                    return;
                }
                loadDebit();
            }

            if (e.Key == Key.F10 || e.SystemKey == Key.F10)
            {
                if (tbMoneda.Text == "USD")
                {
                    System.Windows.MessageBox.Show("Forma de pago no valido para venta en USD!!");
                    return;
                }
                loadCredit();
            }

            if (e.Key == Key.F11)
            {
                loadRetiros();
            }



        }
        #endregion

        #region inserción header y detalle en un solo click al final

        private void Pagar(object sender, RoutedEventArgs e)
        {
            if (GridDatos.Items.Count >= 1)
            {
                Venta();
                pagado = 0;
                saldo();
            }
            else
            {
                System.Windows.MessageBox.Show("No se han agregado productos!");
            }
        }

        CN_Venta cN_Venta;
        void Venta()
        {

            string numTck = "F-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "S23" + "CA666";
            string usuario = "AnthonyA";
            DateTime fecha = DateTime.Now;
            cN_Venta = new CN_Venta();

            if (cambio >= 0)
            {
                cN_Venta.insertarHeader(numTck, fecha, total, usuario);
                Venta_Detalle(numTck);
                GridDatos.Items.Clear();
            }
            else
            {
                System.Windows.MessageBox.Show("Ingresa un pago mayor o igual a la venta!");
            }

        }

        void Venta_Detalle(string numTck)
        {
            cN_Venta = new CN_Venta();

            for (int i = 0; i < GridDatos.Items.Count; i++)
            {
                string itemCode;
                decimal cantidad, monto;

                int j = 0;
                DataGridCell cell = GetCelda(i, j);
                TextBlock tb = cell.Content as TextBlock;
                itemCode = tb.Text;

                int k = 4;
                DataGridCell cell2 = GetCelda(i, k);
                TextBlock tb2 = cell2.Content as TextBlock;
                cantidad = Decimal.Parse(tb2.Text);

                int l = 5;
                DataGridCell cell3 = GetCelda(i, l);
                TextBlock tb3 = cell3.Content as TextBlock;
                monto = Decimal.Parse(tb3.Text);

                cN_Venta.insertarDetalle(numTck, itemCode, cantidad, monto);
            }

            //  Imprimir(numTck);
            Imprimir(ventaI.Id);
        }
        #endregion

        #region inserción venta final(actualizacion de header final)
        private void PagoFinal(object sender, RoutedEventArgs e)
        {
            //printer.Dispose();
            if (GridDatos.Items.Count >= 1)
            {
                if (pagado <= 0)
                {
                    System.Windows.MessageBox.Show("Ingresa una forma de pago!!.");
                         this.tbCodigoProducto.Focus(); 
                    return;
                }

                if(cambio < 0)
                {
                    System.Windows.MessageBox.Show("Ingresa una cantidad de pago mayor o igual a la venta!!.");
                    this.tbCodigoProducto.Focus();
                    return;
                }
                ventaCambio();
                this.tbCodigoProducto.Focus();

            }
            else
            {
                System.Windows.MessageBox.Show("No se han agregado productos!");
                this.tbCodigoProducto.Focus();
            }

        }

        void ventaCambio()
        {
            //printer.Dispose();


            modalCambio modalc = new modalCambio(Convert.ToDecimal(tbTipoCambio.Text));
            modalc.tbCambioN.Text = cambio.ToString("0.00");
            modalc.tbCambioR.Text = cambio.ToString("0.00");
            modalc.tbCambioNU.Text = (cambio / Convert.ToDecimal(tbTipoCambio.Text)).ToString("0.00");

            //if (!pagoUSD)
            //{
            modalc.tbCambioU.Visibility = Visibility.Collapsed;
            modalc.lblU.Visibility = Visibility.Collapsed;
            modalc.lblConvert.Visibility = Visibility.Collapsed;
            modalc.lblcamu.Visibility = Visibility.Collapsed;
            modalc.tbCambioNU.Visibility = Visibility.Collapsed;
            //}
            modalc.ShowDialog();

            cN_Venta = new CN_Venta();
            //if (cambio < 0)
            //{
            CE_VentaHeader vf = new CE_VentaHeader();
            vf.Id = ventaI.Id;
            if (!cN_Venta.insertarHeaderFinal(vf, 1, ref sMensaje))
            {
                MessageBox.Show(sMensaje);
                return;
            }
            //   Imprimir(ventaI.NumTck);
            Imprimir(ventaI.Id);
            FacturarVenta();


            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Ingresa un pago mayor o igual a la venta!");
            //}

        }

        public void FacturarVenta()
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Desea facturar la venta?", "Facturacion de Venta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                var clientes = new modalClientes();
                clientes.ShowDialog();
                if (clientes.codigoCliente != null)
                {
                    codigoClienteFactura = clientes.codigoCliente.ToString();
                    nombreClienteFactura = clientes.nombreCliente.ToString();

                    var mFacturacion = new modalFacturacion();
                    mFacturacion.tbTicket.Visibility = Visibility.Collapsed;
                    mFacturacion.tbCodigoCliente.Text = codigoClienteFactura;
                    mFacturacion.tbNombreCliente.Text = nombreClienteFactura;
                    mFacturacion.idTickNumb = ventaI.Id;
                    mFacturacion.ShowDialog();

                    if (mFacturacion.isFacturado)
                    {
                        int Param = 3;
                        // Crear una instancia de ProcessStartInfo
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable para imprimira Facutura por TCK

                        // startInfo.Arguments = $"{IdTra}";
                        startInfo.Arguments = $"{ventaI.Id} {Param}";

                        // Ejecutar el programa externo
                        Process.Start(startInfo);

                        MessageBox.Show("Facturado con exito!!");

                    }
                    else
                    {
                        FacturarVenta();
                    }


                }
            }

            GridDatos.ItemsSource = null;
            lista.Clear();
            ventaI.Id = 0;
            pagado = 0;
            saldo();
            pagoUSD = false;
            this.tbCodigoProducto.Focus();
            
            // VERIFICA SI EXISTE TIPO DE CAMBIO NUEVO AL FNALIZAR LA VENTA EN CURSO

            CN_BusquedaReporte objConsulta = new CN_BusquedaReporte();
            CE_BusquedaReporte objCe;
            decimal tipoCambio = 0;
            decimal.TryParse(tbTipoCambio.Text, out tipoCambio);

            try
            {
                objCe = objConsulta.ConsultaTipoCambio(tipoCambio);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error al consultar la base de datos: {ex.Message}");
                return;
            }

            if (objCe.Tp_ > 0)
            {
                                
                tbTipoCambio.Text = objCe.Tp_.ToString("F4"); ;  
                tbTipoCambio.UpdateLayout();

            }

        }

        public void ventaFinal()
        {
            //printer.Dispose();
            cN_Venta = new CN_Venta();
            if (cambio < 0.01m)
            {
                CE_VentaHeader vf = new CE_VentaHeader();
                vf.Id = ventaI.Id;
                if (!cN_Venta.insertarHeaderFinal(vf, 1, ref sMensaje))
                {
                    MessageBox.Show(sMensaje);
                    return;
                }

                // Imprimir(ventaI.NumTck);
                Imprimir(ventaI.Id);
                GridDatos.Items.Clear();
                ventaI.Id = 0;
                pagado = 0;
                saldo();

            }
            else
            {
                System.Windows.MessageBox.Show("Ingresa un pago mayor o igual a la venta!");
            }
        }
        #endregion

        #region impresion ticket de venta
        void Imprimir(int numTck)
        {
            //var SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
            //printer = new SerialPrinter(portName: SettingSection.Puerto, baudRate: 9600);


            int Param = 2;
            // Crear una instancia de ProcessStartInfo
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja

            // startInfo.Arguments = $"{IdTra}";
            // startInfo.Arguments = $"{ventaI.NumTck} {Param}";
            startInfo.Arguments = $"{numTck} {Param}";

            // Ejecutar el programa externo
            try
            {
                Process.Start(startInfo);
                System.Windows.MessageBox.Show("Venta realizada con exito! " + ventaI.NumTck + " - "+ numTck);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error al imprimir ticket de venta " + ventaI.NumTck + " - " + numTck + "\n" + ex.Message);
            }
            
            
            Nom_Cajera.logoff = 0;

            //  =====================  se pone el nuevo tipo de cambio =================
           /*     var   SettingSection = AppConfig.GetSection("App_Preferences") as Capa_Presentacion.App_Preferences;
                    tbTipoCambio.IsEnabled = true;
                    tipoCambio = SettingSection.DefRateRetail;   //SettingSection.DefRateCash;
                    tbTipoCambio.Text = tipoCambio.ToString();
                    tbTipoCambio.IsEnabled = false; 
                  */  
            // ===============================================================================

            //var e = new EPSON();

            //try
            //{
            //    printer.Write( // or, if using and immediate printer, use await printer.WriteAsync

            //          ByteSplicer.Combine(
            //            e.CenterAlign(),
            //            e.PrintLine(""),
            //            //e.SetBarcodeHeightInDots(360),
            //            //e.SetBarWidth(BarWidth.Default),
            //            //e.SetBarLabelPosition(BarLabelPrintPosition.None),
            //            //e.PrintBarcode(BarcodeType.ITF, "0123456789"),
            //            e.PrintLine(""),
            //            e.PrintLine("COMERCIAL DE CARNES FRIAS DEL NORTE"),
            //            e.PrintLine("Cto. Brasil, Alamitos, 21210 Mexicali, B.C."),
            //            e.PrintLine("Mexicali, Baja California"),
            //            e.PrintLine("(686) 554 1535"),
            //            e.SetStyles(PrintStyle.Underline),
            //            e.PrintLine("www.ccfn.com"),
            //            e.SetStyles(PrintStyle.None),
            //            e.PrintLine(""),
            //            e.LeftAlign(),
            //            e.PrintLine("No: " + numTck + "        Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + " "),
            //            e.PrintLine(""),
            //            e.PrintLine(""),
            //            e.SetStyles(PrintStyle.FontB),
            //            e.PrintLine("Cant.     " + "Articulo               " + "        Precio           " + "Total")));


            //    for (int i = 0; i < GridDatos.Items.Count; i++)
            //    {
            //        string nombre;
            //        decimal cantidad, preciounitario, totalarticulo;

            //        int j = 1;
            //        DataGridCell cell1 = GetCelda(i, j);
            //        TextBlock tb1 = cell1.Content as TextBlock;
            //        nombre = tb1.Text;

            //        int k = 4;
            //        DataGridCell cell2 = GetCelda(i, k);
            //        TextBlock tb12 = cell2.Content as TextBlock;
            //        cantidad = Decimal.Parse(tb12.Text);

            //        int l = 6;
            //        DataGridCell cell23 = GetCelda(i, l);
            //        TextBlock tb13 = cell23.Content as TextBlock;
            //        totalarticulo = Decimal.Parse(tb13.Text);

            //        int m = 3;
            //        DataGridCell cell4 = GetCelda(i, m);
            //        TextBlock tb14 = cell4.Content as TextBlock;
            //        preciounitario = Decimal.Parse(tb14.Text);

            //        //test.Add(e.PrintLine(cantidad + "" + nombre + "" + preciounitario + "" + totalarticulo));
            //        printer.Write(e.PrintLine(cantidad + " " + nombre + "            " + preciounitario + "          " + totalarticulo));
            //    };


            //    printer.Write(
            //        ByteSplicer.Combine(
            //             e.PrintLine("----------------------------------------------------------------"),
            //                    e.RightAlign(),
            //                    e.PrintLine("Total:                 $" + total),
            //                    e.PrintLine("Pagado:                $" + pagado),
            //                    e.PrintLine("Cambio:                $" + cambio),
            //                    e.PrintLine(""),
            //                    e.PrintLine("----------------------------------------------------------------")
            //            //e.LeftAlign(),
            //            //e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
            //            //e.PrintLine("SOLD TO:                        SHIP TO:"),
            //            //e.SetStyles(PrintStyle.FontB),
            //            //e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
            //            //e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
            //            //e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
            //            //e.PrintLine("  (123)456-7890                   (123)456-7890"),
            //            //e.PrintLine("  CUST: 87654321"),
            //            //e.PrintLine(""),
            //            //e.PrintLine("")
            //            ));

            //    // e.PrintLine("1   TRITON LOW-NOISE IN-LINE MICROPHONE PREAMP"),
            //    //e.PrintLine("    TRFETHEAD/FETHEAD                        89.95         89.95"),


            //    System.Windows.MessageBox.Show("Venta realizada con exito!");
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show("Error al imprimir ticket \n" + ex.Message);
            //}

        }
        #endregion

        #region logica para anular venta
        private void AnularVenta(object sender, RoutedEventArgs e)
        {
            loadAnularVenta();
            this.tbCodigoProducto.Focus();
        }

        public void loadAnularVenta()
        {
            var Acceso = new Acceso(4);
            Acceso.ShowDialog();

            if (Acceso.ReturnValue == 1)
            {

                CN_Venta cn_Venta = new CN_Venta();

                if (!cn_Venta.anularVenta(ventaI.Id))
                {
                    MessageBox.Show("Error al anular venta!!");
                }
                else
                {
                    GridDatos.ItemsSource = null;
                    lista.Clear();
                    ventaI.Id = 0;
                    pagado = 0;
                    saldo();
                    pagoUSD = false;
                    MessageBox.Show("Se anulo la venta con exito!!");
                }
            }
        }
        #endregion

        #region logica anular formas de pago
        private void AnularFPago(object sender, RoutedEventArgs e)
        {
            loadAnularFPago();
            this.tbCodigoProducto.Focus();
        }

        public void loadAnularFPago()
        {

            if(pagado == 0)
            {
                System.Windows.MessageBox.Show("No hay formas de pago para anular!!");
                return;
            }

            cN_Venta = new CN_Venta();

            if (cN_Venta.AnularFpago(ventaI.Id))
            {
                System.Windows.MessageBox.Show("Formas de pago anuladas correctamente!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                pagado = 0;
                saldo();
            }
            else
            {
                System.Windows.MessageBox.Show("Error al anular Formas de pago!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region apertura y cerrado inicial
        private void btnAperturaCerrado_Click(object sender, RoutedEventArgs e)
        {
            if(ventaI.Id == 0)
            {
                loadRetiros();
            } else
            {
                System.Windows.MessageBox.Show("Venta en curso!!");
                return;
            }

        }

        public void loadRetiros()
        {
            var Acceso = new Acceso(3);
            Acceso.ShowDialog();

            if (Acceso.ReturnValue >= 2)
            {

                Retiro_Control.Retiro_Acceso = 2;
                GlobalVariables.CodSuper = Acceso.ReturnValue;

                infoCaja = objCNVentaCaja.infoCaja(Nom_Cajera.Num_Cajera, whsCode, nombreCajaInt);
                objMovCaja = objCNVentaCaja.validateRetiro(infoCaja.IdCash);
                if (objMovCaja.IdCash == -1)
                {

                    var acdialog = new modalAperturaSalida(2);
                    acdialog.Show();
                }
                else
                {
                    var acdialog = new modalAperturaSalida(3);
                    acdialog.Show();
                }


            }
        }
        #endregion

        #region al cerrar ventana venta
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            return;
            printer.Dispose();
        }


        #endregion

        #region logica al abrir venta
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            tbCodigoProducto.Focus();
            ConsultarCliente();
            ConsultarListaPrecio();
            ConsultarTC();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string sRutaAplicacion = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string sRutaTimbrado = sRutaAplicacion + @"\Timbrado\TimbradoCCFN40.exe";
            string sRutaXmlFiles = sRutaAplicacion + @"\Timbrado\XmlFiles\";
            string sXmlFiles = @"documento";


            if (!Directory.Exists(sRutaXmlFiles))
            {
                Directory.CreateDirectory(sRutaXmlFiles);
            }

            MessageBox.Show(sRutaAplicacion);

            var process = new Process();
            // string sAplTimbrar = @"C:\Users\subdirector.ti\source\repos\TimbradoCCFN40\TimbradoCCFN40\bin\Debug\TimbradoCCFN40.exe";
            string sAplTimbrar = sRutaTimbrado; //  @"C:\Users\subdirector.ti\source\repos\TimbradoCCFN40\TimbradoCCFN40\bin\Debug\TimbradoCCFN40.exe";
            ProcessStartInfo StartInfo = new ProcessStartInfo(sAplTimbrar);
            // StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            StartInfo.Arguments = "13 " + sRutaXmlFiles + " " + sXmlFiles;
            process.StartInfo = StartInfo;

            try
            {
                process.Start();
                process.WaitForExit();

            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                process.Dispose();
            }
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            //IniciarConfiguracion();
        }
        #endregion

        #region logica para eliminar producto de la venta
        private void ElminarProducto(object sender, RoutedEventArgs e)
        {
            loadAnularProducto();
            this.tbCodigoProducto.Focus();
        }



        //private void GridDatos_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    if (Key.Delete == e.Key) e.Handled = false;
        //}

        public void loadAnularProducto()
        {
            var seleccionado = GridDatos.SelectedItem as GridList;


            if (seleccionado != null)
            {
                var Acceso = new Acceso(2);
                Acceso.ShowDialog();
                if (Acceso.ReturnValue == 1)
                {
                    if (!objeto_CN_Productos.AnularProducto(ventaI.Id, seleccionado.LineNum))
                    {
                        MessageBox.Show("Error al anular producto!!");
                        return;
                    }

                    lista.Remove(seleccionado);
                    GridDatos.ItemsSource = null;
                    GridDatos.ItemsSource = lista;
                    //GridDatos.Items.Remove(seleccio/*nado);*/
                    if (GridDatos.Items.Count < 1)
                    {
                        pagado = 0;
                    }

                    saldo();
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un producto!!");
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if(!ventaI.Id.Equals(0))
            {
                System.Windows.MessageBox.Show(
                "No se puede cambiar la moneda en una venta en curso. ¡Debe cancelar la venta en curso primero!",
                  "Alerta Importante",
                   MessageBoxButton.OK,
               MessageBoxImage.Warning);
                tbCodigoProducto.Focus();
                return;
            }

            if(GridDatos.Items.Count > 0)
            {
                //System.Windows.MessageBox.Show("No se puede cambiar moneda en venta en curso debe cancelar antes la venta en curso !!");
                System.Windows.MessageBox.Show(
                "No se puede cambiar la moneda en una venta en curso. ¡Debe cancelar la venta en curso primero!",
               "Alerta Importante",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                tbCodigoProducto.Focus();
                return;
            }

            if(tbMoneda.Text == "MXN")
            {
                tbMoneda.Text = "USD";
                btnC.IsEnabled = false;
                btnD.IsEnabled = false;
                btnE.IsEnabled = false;
                btnEU.IsEnabled = true;
                tbSubtotal.Text = tbMoneda.Text + " $" + subTotal.ToString("0.00");
                tbPagado.Text = tbMoneda.Text + " $" + pagado.ToString("###,###.00");
                tbCambio.Text = tbMoneda.Text + " $" + cambio.ToString("0.00");
                this.tbCodigoProducto.Focus();
                return;
            }
             
            if (tbMoneda.Text == "USD")
            {
                btnEU.IsEnabled = false;
                tbMoneda.Text = "MXN";
                btnC.IsEnabled = true;
                btnD.IsEnabled = true;
                btnE.IsEnabled = true;
                tbSubtotal.Text = tbMoneda.Text + " $" + subTotal.ToString("0.00");
                tbPagado.Text = tbMoneda.Text + " $" + pagado.ToString("###,###.00");
                tbCambio.Text = tbMoneda.Text + " $" + cambio.ToString("0.00");
                return;
            }
        }

        private void tbCodigoProducto_GotFocus(object sender, RoutedEventArgs e)
        {
            sCodigoBarra = "";
        }

        #endregion

        #region logica para obtener datos del grid table
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T hijo = default;
            int num = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < num; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                hijo = v as T;
                if (hijo == null)
                {
                    hijo = GetVisualChild<T>(v);
                }
                if (hijo != null)
                {
                    break;
                }
            }
            return hijo;
        }
        public DataGridRow Getfila(int index)
        {
            DataGridRow fila = (DataGridRow)GridDatos.ItemContainerGenerator.ContainerFromIndex(index);

            if (fila == null)
            {
                GridDatos.UpdateLayout();
                GridDatos.ScrollIntoView(GridDatos.Items[index]);
                fila = (DataGridRow)GridDatos.ItemContainerGenerator.ContainerFromIndex((int)index);
            }
            GridDatos.UpdateLayout();
            return fila;
        }

        public DataGridCell GetCelda(int fila, int columna)
        {
            DataGridRow filaCon = Getfila(fila);
            if (filaCon != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(filaCon);
                DataGridCell celda = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromItem(columna);

                if (celda == null)
                {
                    GridDatos.UpdateLayout();
                    GridDatos.ScrollIntoView(filaCon, GridDatos.Columns[columna]);
                    celda = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columna);
                }
                GridDatos.UpdateLayout();
                return celda;
            }
            return null;
        }
        #endregion



        #region entidad para row de datagrid
        public class GridList
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string CodeBar { get; set; }
            public string Unidad { get; set; }
            public decimal Precio_Base { get; set; }
            public int UomEntry { get; set; }
            public decimal Cantidad { get; set; }
            public decimal Impuesto { get; set; }
            public decimal Precio_Base_FC { get; set; }
            public decimal Impuesto_FC { get; set; }
            public decimal Total { get; set; }
            public int LineNum { get; set; }
        }
        #endregion

    }
}
