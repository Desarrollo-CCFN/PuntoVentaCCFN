using Capa_Datos;
using Capa_Entidad.Devoluciones;
using Capa_Negocio;
using System.Data;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using static Capa_Datos.CD_Devoluciones;
using System.Windows.Input;
using MiControl = PuntoVentaCCFN.MainWindow.Control;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para Devoluciones.xaml
    /// </summary>
    public partial class Devoluciones : System.Windows.Controls.UserControl
    {
        readonly CN_Devoluciones _oDevoluciones = new CN_Devoluciones();

        CE_DevolucionHeader _oDevolucionHeader = new CE_DevolucionHeader();
        CE_DevolucionDetalle _oDevolucionDetalle = new CE_DevolucionDetalle();

        readonly CD_Devoluciones objeto_DevolVentaEjecuta = new CD_Devoluciones();

        // CE_DevolucionHeader _oDevolVentaEjecuta = new CE_DevolucionHeader();
        double total = 0;
       

        List<PayForm> ListPagos = new List<PayForm>();
        public string sMensaje = null;
        string voucher = "";
        string Pago = "";
        public Devoluciones()
        {
            InitializeComponent();
            tbNumTicket.Focus(); // Establece el foco en el TextBox
        }
 

        private void CargarHeader(object sender, RoutedEventArgs e)
        {
            if(tbNumTicket.Text == "") {
                System.Windows.MessageBox.Show("Debes ingresar un numero de recibo!!");
                return;
            }

             string smensaje = string.Empty;

             _oDevolucionHeader = _oDevoluciones.CargarHeader(tbNumTicket.Text, ref smensaje);

            if (smensaje != string.Empty )
            {
                System.Windows.MessageBox.Show(smensaje);
                return;
            }

            if (_oDevolucionHeader.Id == -1)
            {
                System.Windows.MessageBox.Show("Venta fuera de rango de fecha!!");
                return;
            }

            if (_oDevolucionHeader.Id == -2)
            {
                System.Windows.MessageBox.Show("Venta ya cuenta con devolucion!!!");
                return;
            }

            if (_oDevolucionHeader.Id == -3)
            {
                System.Windows.MessageBox.Show("Error al buscar venta [SP_V_DevoHeader]");
                return;
            }

            lblRecibo.Text = _oDevolucionHeader.NumTck;
            lblFecha.Text = _oDevolucionHeader.Fecha;
            lblMoneda.Text = _oDevolucionHeader.DocCur;
            lblRate.Text = "$" + _oDevolucionHeader.DocRate.ToString("N2");
            lblMXN.Text = "$" +  _oDevolucionHeader.DocTotal.ToString("N2");
            lblUSD.Text = "$" +  _oDevolucionHeader.DocTotalFC.ToString("N2");
            lblTotal.Text = "Total: $";
             total = 0;

            MiControl.nPase = 1;

            CargarDetalle();
            CargarFormasPago();
        }

        private void CargarDetalle()
        {
            GridDatos.ItemsSource = _oDevoluciones.CargarDetalle(tbNumTicket.Text).DefaultView;
            foreach(DataRowView dr in GridDatos.ItemsSource)
            {
                dr.Row[16] = 0;
                dr.Row[17] = 0;
            }

        }

        private void CargarFormasPago()
        {
            ListPagos = _oDevoluciones.GetPayForms(tbNumTicket.Text, ref sMensaje);
            cbPago.ItemsSource = ListPagos;
            cbPago.SelectedValuePath = "Name";
        }

        public void getSelectedItems(ref string smensaje)
        {
            // int idHeader = 0;

            //if(GridDatos.SelectedItems.Count == 0) {
            //    System.Windows.MessageBox.Show("Debes seleccionar productos a devolver!!");
            //    return;
            //}
            if (cbPago.SelectedValue ==  null)
            {
                System.Windows.MessageBox.Show("Seleccionar la forma de pago de la devolución");
                return;
            }

            voucher = textVoucher.Text;
            if (cbPago.SelectedValue.ToString() != "EF" && cbPago.SelectedValue.ToString() != "EFU")
            {
                if(textVoucher.Text == "")
                {
                    System.Windows.MessageBox.Show("Debes Ingresar Numero De Voucher");
                    return;
                }
            }
            /*
            if (!_oDevoluciones.DevoluacionHeader(_oDevolucionHeader.NumTck, Pago, voucher, ref sMensaje)) {
                System.Windows.MessageBox.Show(sMensaje);
                return;
            } else
            {
                idHeader = Convert.ToInt32(sMensaje);
                //id devolucion header
                foreach(DataRowView dr  in GridDatos.ItemsSource) 
                {
                    if (Convert.ToInt32(dr.Row[16]) != 0)
                    if (Convert.ToInt32(dr.Row[16]) != 0)
                    {
                        
                        if (!_oDevoluciones.DevolucionDetalle(_oDevolucionHeader.NumTck, Convert.ToInt32(dr.Row[4].ToString()), Convert.ToDouble(dr.Row[16].ToString()), idHeader, total, ref sMensaje))
                        {
                            System.Windows.MessageBox.Show(sMensaje);
                            break;
                        }
                    }
                }
            }

            if(!_oDevoluciones.DevolucionCierre(idHeader, 1, ref sMensaje))
            {
                System.Windows.MessageBox.Show(sMensaje);
                return;
            }
            */

            int iRows = 0;
                        
            string sJson = "";
            
           // try
            //{
                foreach (DataRowView dr in GridDatos.ItemsSource)
                {
                
                // System.Windows.MessageBox.Show(Convert.ToString(dr.Row[4]) + "\n" + Convert.ToString(dr.Row[16]));

                if (Convert.ToInt32(dr.Row[16]) != 0)
                    {
                        iRows++;

                    

                    if (sJson == "")
                        {
                            sJson = $@"[[""{Convert.ToString(dr.Row[4])}"", 
                                               {Convert.ToString(dr.Row[16])},
                                               ""{Convert.ToString(dr.Row[17])}""]";
                        }
                        else
                        {
                            sJson += $@",[""{Convert.ToString(dr.Row[4])}"", 
                                               { Convert.ToString(dr.Row[16])},
                                               ""{Convert.ToString(dr.Row[17])}""]";
                        }
                    }

                        
                }

                sJson += "]";

                if (iRows == 0)
                {
                    sMensaje = "No existe partidas por procesar !!!";
                System.Windows.MessageBox.Show(sMensaje);
                return ;
                }

                if (!_oDevoluciones.DevolVentaEjecuta(0, sJson, _oDevolucionHeader.NumTck, Pago, voucher, ref sMensaje))
                {
                    System.Windows.MessageBox.Show(sMensaje);
                    return;
                }
                else
                {
                   
                System.Windows.MessageBox.Show("Se realizó con éxito la devolución del TCK de Venta: " + _oDevolucionHeader.NumTck, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                GridDatos.ItemsSource = null;
                    tbNumTicket.Text = "";
                    lblRecibo.Text = "";
                    lblFecha.Text = "";
                    lblMoneda.Text = "";
                    lblRate.Text = "";
                    lblMXN.Text = "";
                    lblUSD.Text = "";
                textVoucher.Text = "";
                lblTotal.Text = "";
                //txtID.Text = String.Empty;
                cbPago.SelectedItem = null;
            }
        }

  

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string smensaje = string.Empty;

            getSelectedItems(ref smensaje);

            if (smensaje.Length > 0)
            {
                System.Windows.MessageBox.Show(sMensaje);
                return;
            }

        }

        #region teclas rapidas
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                Button_Click(sender, e);
                //CargarHeader(sender, e);
            }

        }
        #endregion

        public class GridList
        {
            public int CodigoError { get; set; }
            public string Mensaje { get; set; }
            public string ItemCode { get; set; }
            public string Unidad { get; set; }
            public int LineNum { get; set; }
            public string LineStatus { get; set; }
            public string Dscription { get; set; }
            public decimal Quantiy { get; set; }
            public string Currency {  get; set; }
            public double Rate { get; set; }
            public double LineTotal { get; set; }
            public double TotalFrgn { get; set; }
            public double VatPrcnt { get; set; }
            public double VatSum { get; set; }
            public double VatSumFrgn { get; set; }
            public int IdHeader { get; set; }
            public int QtyDevolver { get; set; }
            public double ImporteFinal { get; set; }
        }

        
        private void GridDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //saldo();
        }

       // double total = 0;
        public void saldo()
        {
            
            for (int i = 0; i < GridDatos.SelectedItems.Count; i++)
            {
                double priceUnit = 0;
                DataRowView dataRow = (DataRowView)GridDatos.SelectedItems[i];

                if(Convert.ToInt32(dataRow.Row[16]) == 0)
                {
                    total = 0;
                }

                if (_oDevolucionHeader.DocCur == "MXN")
                {
                    priceUnit = Convert.ToDouble(dataRow.Row[10]) / Convert.ToInt32(dataRow.Row[7]);
                    total += priceUnit * Convert.ToInt32(dataRow.Row[16]);
                } else
                {
                    priceUnit = Convert.ToDouble(dataRow.Row[11]) / Convert.ToInt32(dataRow.Row[7]);
                    total += priceUnit * Convert.ToInt32(dataRow.Row[16]);
                }
                
                lblTotal.Text = "Total: $" + total.ToString("0.00") + " " + _oDevolucionHeader.DocCur;
            }
            lblTotal.Text = "Total: $" + total.ToString("N2") + " " + lblMoneda.Text; // dataRow.Row[8].ToString();

        }

        private void GridDatos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            double priceUnit = 0;
            double LineTotalFinal = 0;
            DataGridRow row = e.Row;
            DataRowView item = row.Item as DataRowView;
            var t = (e.EditingElement as System.Windows.Controls.TextBox).Text;
            if (Convert.ToInt32(t) > Convert.ToInt32(item.Row[7]))
            {
                System.Windows.MessageBox.Show("Cantidad a devolver es mayor a la cantidad vendida!!");
                item.Row[16] = 0;
                return;
            }

            item.Row[16] = Convert.ToInt32(t);

            if (_oDevolucionHeader.DocCur == "MXN") 
            {
                priceUnit = Convert.ToDouble(item.Row[10]) / Convert.ToInt32(item.Row[7]);
                LineTotalFinal = priceUnit * Convert.ToInt32(item.Row[16]);
            
                item.Row[17] = LineTotalFinal;

            } else
            {
                priceUnit = Convert.ToDouble(item.Row[11]) / Convert.ToInt32(item.Row[7]);
                LineTotalFinal = priceUnit * Convert.ToInt32(item.Row[16]);
    
                item.Row[17] = LineTotalFinal;
            }

            saldo();

        }

        private void cbPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbPago.SelectedValue == null) return;

            string content = cbPago.SelectedValue.ToString();
            
            if (content == "EF" || content == "EFU")
            {
                Pago = "EF";
                voucher = "";
                textVoucher.IsEnabled = false;
            } else
            {
                
                Pago = content;
                textVoucher.IsEnabled = true;
                
            }
        }

        private void tbNumTicket_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                CargarHeader(sender, e);
            }
        }
    }
}
