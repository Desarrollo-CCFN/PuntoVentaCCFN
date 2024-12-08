﻿using Capa_Entidad.Devoluciones;
using Capa_Negocio;
using PuntoVentaCCFN;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Capa_Datos.CD_Devoluciones;

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
        List<PayForm> ListPagos = new List<PayForm>();
        public string sMensaje = null;
        string voucher = "";
        string Pago = "";
        public Devoluciones()
        {
            InitializeComponent();
        }

        private void CargarHeader(object sender, RoutedEventArgs e)
        {
            if(tbNumTicket.Text == "") {
                System.Windows.MessageBox.Show("Debes ingresar un numero de recibo!!");
                return;
            }

             _oDevolucionHeader = _oDevoluciones.CargarHeader(tbNumTicket.Text);

            if(_oDevolucionHeader.Id == -1)
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
            lblRate.Text = "$" + _oDevolucionHeader.DocRate.ToString();
            lblMXN.Text = "$" +  _oDevolucionHeader.DocTotal.ToString();
            lblUSD.Text = "$" +  _oDevolucionHeader.DocTotalFC.ToString();

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

        public void getSelectedItems()
        {
            int idHeader = 0;

            //if(GridDatos.SelectedItems.Count == 0) {
            //    System.Windows.MessageBox.Show("Debes seleccionar productos a devolver!!");
            //    return;
            //}
            voucher = textVoucher.Text;
            if (cbPago.SelectedValue.ToString() != "EF")
            {
                if(textVoucher.Text == "")
                {
                    System.Windows.MessageBox.Show("Debes Ingresar Numero De Voucher");
                    return;
                }
            }
            
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

            System.Windows.MessageBox.Show("Exito!!");
            GridDatos.ItemsSource = null;
            tbNumTicket.Text = "";
            lblRecibo.Text = "";
            lblFecha.Text = "";
            lblMoneda.Text = "";
            lblRate.Text = "";
            lblMXN.Text = "";
            lblUSD.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getSelectedItems();
        }

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

        double total = 0;
        public void saldo()
        {
            
            for (int i = 0; i < GridDatos.SelectedItems.Count; i++)
            {
                DataRowView dataRow = (DataRowView)GridDatos.SelectedItems[i];
                total += Convert.ToDouble(dataRow.Row[17].ToString());
                lblTotal.Text = "Total: $" + total.ToString("0.00") + " " + dataRow.Row[8].ToString();
            }
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

            if (item.Row[8].ToString() == "MXN") 
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
            string content = cbPago.SelectedValue.ToString();
            
            if (content == "EF")
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
    }
}
