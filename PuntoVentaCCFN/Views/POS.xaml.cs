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
        BasePrinter printer  = new SerialPrinter(portName: "COM8", baudRate: 9600);

        public int listPrecios = 13;

        public POS()
        {
            InitializeComponent();
            ConsultarCliente();
            ConsultarListaPrecio();
            ConsultarTC();
            
            tbCodigoProducto.Focus();
        }



        public void ConsultarTC()
        {
            //TODO obtener tipo de cambio de bd

            var tipoCambio = objeto_CN_TipoCambio.Consulta();
            tbTipoCambio.Text = tipoCambio.Rate.ToString();
        }

        public void ConsultarCliente()
        {
            var clienteDatos = objeto_CN_Clientes.Consulta("C00000024");
            tbCodigoCliente.Text = clienteDatos.CardCode.ToString();
            tbNombreCliente.Text = clienteDatos.CardName.ToString();
        }

        public void ConsultarListaPrecio()
        {
            var listaPrecio = objeto_CN_ListaPrecios.Consulta(13);
            tbListaPrecio.Text = listaPrecio.ListName.ToString();
        }


        private void buscarProducto(object sender, TextChangedEventArgs e)
        {
            
            if (tbCodigoProducto.Text == "") return;

            CN_Carrito carrito = new CN_Carrito();

            if (ventaI.Id.Equals(0))
            {
                ventaI = venta.insertarVenta("S23", "C666");
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
            ce_Detalle.Currency = "MXN";
            ce_Detalle.Monto = Convert.ToDecimal(row[5]);
            ce_Detalle.WhsCode = "S23";
            ce_Detalle.CodeBars = tbCodigoProducto.Text;
            ce_Detalle.PriceList = listPrecios;
            venta.insertarDetalleLive(ce_Detalle);
            tbCodigoProducto.Text = "";


        }

        decimal total, cambio, pagado, subTotal;
        private void saldo()
        {
            total = 0;
            subTotal = 0;
            for(int i = 0; i <GridDatos.Items.Count; i++) {
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

        #region filas, columnas y celdas
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T hijo = default;
            int num = VisualTreeHelper.GetChildrenCount(parent);
            for(int i = 0; i < num;i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                hijo = v as T;
                if(hijo == null)
                {
                    hijo = GetVisualChild<T>(v);
                }
                if(hijo != null)
                {
                    break;
                }
            }
            return hijo;
        }


        #endregion
        private void Efectivo(object sender, RoutedEventArgs e)
        {
            var ingresar = new Ingresar();
            ingresar.ShowDialog();

            if (ingresar.Efectivo > 0)
            {
                pagado += ingresar.Efectivo;
                saldo();
            }
            CE_VentaPagos ventaPago = new CE_VentaPagos();
            ventaPago.Payform = "EF";
            ventaPago.Currency = "MXN";
            ventaPago.Rate = 1;
            ventaPago.AmountPay = ingresar.Efectivo;
            ventaPago.BalAmout = cambio;
            ventaPago.IdHeader = ventaI.Id;
            venta.insertarVentaPago(ventaPago);
        }

        private void Tarjeta(object sender, RoutedEventArgs e)
        {
            var ingresar = new Ingresar();
            ingresar.ShowDialog();

            if (ingresar.Efectivo > 0)
            {
                pagado += ingresar.Efectivo;
                saldo();
            }

            CE_VentaPagos ventaPago = new CE_VentaPagos();
            ventaPago.Payform = "Deb";
            ventaPago.Currency = "MXN";
            ventaPago.Rate = 1;
            ventaPago.AmountPay = ingresar.Efectivo;
            ventaPago.BalAmout = cambio;
            ventaPago.IdHeader = ventaI.Id;
            venta.insertarVentaPago(ventaPago);

        }
        private void buscarClBtn_Click(object sender, RoutedEventArgs e)
        {
            var busquedaCliente = new modalClientes();
            busquedaCliente.ShowDialog();

            if(busquedaCliente.codigoCliente != null)
            {
                tbCodigoCliente.Text = busquedaCliente.codigoCliente.ToString();
                tbNombreCliente.Text = busquedaCliente.nombreCliente.ToString();
                tbListaPrecio.Text = busquedaCliente.listaPrecio.ToString();
                listPrecios = int.Parse(busquedaCliente.numLista);
            }
        }



        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Windows.MessageBox.Show("Tecla Recalculo");
            }
        }

        #region Pagar e Imprimir
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

            for(int i = 0; i < GridDatos.Items.Count; i++)
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

                int l = 5;
                DataGridCell cell23 = GetCelda(i, l);
                TextBlock tb13 = cell23.Content as TextBlock;
                totalarticulo = Decimal.Parse(tb13.Text);

                int m = 3;
                DataGridCell cell4 = GetCelda(i, m);
                TextBlock tb14 = cell4.Content as TextBlock;
                preciounitario = Decimal.Parse(tb14.Text);

                //test.Add(e.PrintLine(cantidad + "" + nombre + "" + preciounitario + "" + totalarticulo));
                printer.Write(e.PrintLine(cantidad + " " + nombre.Substring(0, 25) + "            " + preciounitario + "          " + totalarticulo));
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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            printer.Dispose();
        }

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

        public DataGridRow Getfila(int index)
        {
            DataGridRow fila = (DataGridRow)GridDatos.ItemContainerGenerator.ContainerFromIndex(index);

            if(fila == null)
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
            if(filaCon != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(filaCon);
                DataGridCell celda = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromItem(columna);

                if(celda == null)
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

