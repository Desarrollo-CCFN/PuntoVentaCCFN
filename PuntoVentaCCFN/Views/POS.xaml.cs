using Capa_Negocio;
using Capa_Negocio.Productos;
using Capa_Negocio.Venta;
using Capa_Presentacion.SCS.Boxes;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using iTextSharp.text;
using iTextSharp.xmp;
using iTextSharp.text.pdf;
using System.IO;
using System.util;
using System.Windows.Forms.PropertyGridInternal;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Drawing.Printing;
using System.Diagnostics;
using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Capa_Negocio.Clientes;
using Capa_Entidad.Venta;
using System.Threading;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para POS.xaml
    /// </summary>
    public partial class POS : System.Windows.Controls.UserControl
    {
        readonly CN_Clientes objeto_CN_Clientes = new CN_Clientes();
        readonly CN_TipoCambio objeto_CN_TipoCambio = new CN_TipoCambio();
        readonly CN_ListaPrecios objeto_CN_ListaPrecios = new CN_ListaPrecios();
        readonly CN_Venta venta = new CN_Venta();
        CE_VentaHeader ventaI = new CE_VentaHeader();
        BasePrinter printer = new SerialPrinter(portName: "COM8", baudRate: 9600); //TODO obtener puerto de archivo de configuracion
        public int listPrecios = 13; //obtener lista de precios default de archivo de configuracion

        public POS()
        {
            InitializeComponent();
        }

        #region consulta del tipo de cambio
        public void ConsultarTC()
        {

            var tipoCambio = objeto_CN_TipoCambio.Consulta();
            tbTipoCambio.Text = tipoCambio.Rate.ToString();
        }
        #endregion

        #region consulta inicial del cliente default
        public void ConsultarCliente()
        {
            var clienteDatos = objeto_CN_Clientes.Consulta("C00000024"); //TODO obtener cliente default dependiendo la sucursal de archivo de configuracion
            tbCodigoCliente.Text = clienteDatos.CardCode.ToString();
            tbNombreCliente.Text = clienteDatos.CardName.ToString();
        }
        #endregion

        #region consulta inicial de lista de precio default
        public void ConsultarListaPrecio()
        {
            var listaPrecio = objeto_CN_ListaPrecios.Consulta(13); //TODO Enviar lista de precios defualt de archivo de configuracion
            tbListaPrecio.Text = listaPrecio.ListName.ToString();
        }
        #endregion

        #region busqueda e inserción header y detalle primera vez
        private void tbCodigoProducto_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
                if (tbCodigoProducto.Text == "") return;

                CN_Carrito carrito = new CN_Carrito();

                if (ventaI.Id.Equals(0))
                {
                    ventaI = venta.insertarVenta("S23", "C666", tbCodigoCliente.Text.ToString(), 1, 102); //TODO obtener id cash actual y tomar el vendedor(default vendedor estandar)
                }

                DataTable dt;
                dt = carrito.buscarProducto(tbCodigoProducto.Text, listPrecios);

                GridDatos.Items.Add(dt);
                saldo();
                DataRow row = dt.Rows[0];
                CE_VentaDetalle ce_Detalle = new CE_VentaDetalle();
                ce_Detalle.NumTck = ventaI.NumTck;
                ce_Detalle.IdHeader = ventaI.Id;
                ce_Detalle.ItemCode = Convert.ToString(row[0]);
                ce_Detalle.Cantidad = Convert.ToDecimal(row[7]);
                ce_Detalle.Currency = "MXN"; //TODO obtener moneda de documento
                ce_Detalle.Monto = Convert.ToDecimal(row[5]);
                ce_Detalle.WhsCode = "S23"; //TODO obtener sucursal de archivo de configuracion
                ce_Detalle.CodeBars = tbCodigoProducto.Text;
                ce_Detalle.PriceList = listPrecios;
                venta.insertarDetalleLive(ce_Detalle);
                tbCodigoProducto.Text = "";
            }
        }
        #endregion

        #region calculo del saldo en cada insersion de producto
        decimal total, cambio, pagado, subTotal;
        private void saldo()
        {
            total = 0;
            subTotal = 0;
            for (int i = 0; i < GridDatos.Items.Count; i++)
            {
                decimal precioTotal;
                decimal precioSubtotal;
                int j = 6;
                DataGridCell celda = GetCelda(i, j);
                TextBlock tb = celda.Content as TextBlock;
                precioTotal = decimal.Parse(tb.Text);
                total += precioTotal;

                int k = 3;
                DataGridCell celda1 = GetCelda(i, k);
                TextBlock tb1 = celda1.Content as TextBlock;
                precioSubtotal = decimal.Parse(tb1.Text);
                subTotal += precioSubtotal;
            }

            cambio = pagado - total;

            tbImporte.Text = "$" + total.ToString();
            tbSubtotal.Text = "$" + subTotal.ToString();
            tbPagado.Text = "$" + pagado.ToString("###,###.00");
            tbCambio.Text = "$" + cambio.ToString();

        }
        #endregion

        #region formas de pago de la venta
        private void Efectivo(object sender, RoutedEventArgs e)
        {
            var ingresar = new Ingresar();
            ingresar.ShowDialog();

            if (ingresar.Efectivo > 0)
            {
                pagado += ingresar.Efectivo;
                saldo();
                CE_VentaPagos ventaPago = new CE_VentaPagos();
                ventaPago.Payform = "EF";
                ventaPago.Currency = "MXN";
                ventaPago.Rate = 1;
                ventaPago.AmountPay = ingresar.Efectivo;
                if(cambio < 0)
                {
                    ventaPago.BalAmout = cambio;
                } else
                {
                    ventaPago.BalAmout = 0;
                }
                
                ventaPago.IdHeader = ventaI.Id;
                venta.insertarVentaPago(ventaPago);
            }

        }
        private void Tarjeta(object sender, RoutedEventArgs e)
        {
            var ingresar = new Ingresar();
            ingresar.ShowDialog();

            if (ingresar.Efectivo > 0)
            {
                pagado += ingresar.Efectivo;
                saldo();
                CE_VentaPagos ventaPago = new CE_VentaPagos();
                ventaPago.Payform = "TD";
                ventaPago.Currency = "MXN";
                ventaPago.Rate = 1;
                ventaPago.AmountPay = ingresar.Efectivo;
                if (cambio < 0)
                {
                    ventaPago.BalAmout = cambio;
                }
                else
                {
                    ventaPago.BalAmout = 0;
                }
                ventaPago.IdHeader = ventaI.Id;
                venta.insertarVentaPago(ventaPago);
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
        }
        #endregion

        #region teclas rapidas
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Windows.MessageBox.Show("Tecla Recalculo");
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

            Imprimir(numTck);
        }
        #endregion

        #region inserción venta final(actualizacion de header final)
        private void PagoFinal(object sender, RoutedEventArgs e)
        {
            if (GridDatos.Items.Count >= 1)
            {
                ventaFinal();
                pagado = 0;
                saldo();
            } else
            {
                System.Windows.MessageBox.Show("No se han agregado productos!");
            }

        }

        void ventaFinal()
        {
            cN_Venta = new CN_Venta();

            if(cambio >=0)
            {
                CE_VentaHeader vf = new CE_VentaHeader();
                vf.Id = ventaI.Id;
                vf.Usuario = "Cajero A"; //TODO obtener usuario de conf
                vf.DocCur = "MXN"; //TODO obtener currency de documento de lista de currencies
                vf.PriceList = listPrecios;
                vf.Filler = "S24"; //TODO Obtener filler de conf
                vf.Comments = "";
                cN_Venta.insertarHeaderFinal(vf);
                Imprimir(ventaI.NumTck);
                GridDatos.Items.Clear();
                ventaI.Id = 0;
            }
            else
            {
                System.Windows.MessageBox.Show("Ingresa un pago mayor o igual a la venta!");
            }
        }
        #endregion

        #region impresion ticket de venta
        void Imprimir(string numTck)
        {

            var e = new EPSON();


            printer.Write( // or, if using and immediate printer, use await printer.WriteAsync

                  ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(""),
                    //e.SetBarcodeHeightInDots(360),
                    //e.SetBarWidth(BarWidth.Default),
                    //e.SetBarLabelPosition(BarLabelPrintPosition.None),
                    //e.PrintBarcode(BarcodeType.ITF, "0123456789"),
                    e.PrintLine(""),
                    e.PrintLine("COMERCIAL DE CARNES FRIAS DEL NORTE"),
                    e.PrintLine("Cto. Brasil, Alamitos, 21210 Mexicali, B.C."),
                    e.PrintLine("Mexicali, Baja California"),
                    e.PrintLine("(686) 554 1535"),
                    e.SetStyles(PrintStyle.Underline),
                    e.PrintLine("www.ccfn.com"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(""),
                    e.LeftAlign(),
                    e.PrintLine("No: " + numTck + "        Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + " "),
                    e.PrintLine(""),
                    e.PrintLine(""),
                    e.SetStyles(PrintStyle.FontB),
                    e.PrintLine("Cant.     " + "Articulo               " + "        Precio           " + "Total")));


            for (int i = 0; i < GridDatos.Items.Count; i++)
            {
                string nombre;
                decimal cantidad, preciounitario, totalarticulo;

                int j = 1;
                DataGridCell cell1 = GetCelda(i, j);
                TextBlock tb1 = cell1.Content as TextBlock;
                nombre = tb1.Text;

                int k = 4;
                DataGridCell cell2 = GetCelda(i, k);
                TextBlock tb12 = cell2.Content as TextBlock;
                cantidad = Decimal.Parse(tb12.Text);

                int l = 6;
                DataGridCell cell23 = GetCelda(i, l);
                TextBlock tb13 = cell23.Content as TextBlock;
                totalarticulo = Decimal.Parse(tb13.Text);

                int m = 3;
                DataGridCell cell4 = GetCelda(i, m);
                TextBlock tb14 = cell4.Content as TextBlock;
                preciounitario = Decimal.Parse(tb14.Text);

                //test.Add(e.PrintLine(cantidad + "" + nombre + "" + preciounitario + "" + totalarticulo));
                printer.Write(e.PrintLine(cantidad + " " + nombre + "            " + preciounitario + "          " + totalarticulo));
            };


            printer.Write(
                ByteSplicer.Combine(
                     e.PrintLine("----------------------------------------------------------------"),
                            e.RightAlign(),
                            e.PrintLine("Total:                 $" + total),
                            e.PrintLine("Pagado:                $" + pagado),
                            e.PrintLine("Cambio:                $" + cambio),
                            e.PrintLine(""),
                            e.PrintLine("----------------------------------------------------------------")
                    //e.LeftAlign(),
                    //e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
                    //e.PrintLine("SOLD TO:                        SHIP TO:"),
                    //e.SetStyles(PrintStyle.FontB),
                    //e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
                    //e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
                    //e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
                    //e.PrintLine("  (123)456-7890                   (123)456-7890"),
                    //e.PrintLine("  CUST: 87654321"),
                    //e.PrintLine(""),
                    //e.PrintLine("")
                    ));

            // e.PrintLine("1   TRITON LOW-NOISE IN-LINE MICROPHONE PREAMP"),
            //e.PrintLine("    TRFETHEAD/FETHEAD                        89.95         89.95"),


            System.Windows.MessageBox.Show("Venta realizada con exito!");

        }
        #endregion

        #region al cerrar ventana venta
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
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

        #endregion

        #region logica para eliminar producto de la venta
        private void ElminarProducto(object sender, RoutedEventArgs e)
        {
            var seleccionado = GridDatos.SelectedItem;
            if (seleccionado != null)
            {
                GridDatos.Items.Remove(seleccionado);
                if (GridDatos.Items.Count < 1)
                {

                    pagado = 0;
                }
            }

            saldo();
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
                    GridDatos.ScrollIntoView(filaCon, GridDatos.Columns[columna]);
                    celda = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columna);
                }
                return celda;
            }
            return null;
        }
        #endregion


    }
}

