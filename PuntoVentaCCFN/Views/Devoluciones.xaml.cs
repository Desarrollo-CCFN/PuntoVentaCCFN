using Capa_Entidad.Devoluciones;
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

        public string sMensaje = null;
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
            lblRate.Text = _oDevolucionHeader.DocRate.ToString();
            lblMXN.Text = _oDevolucionHeader.DocTotal.ToString();
            lblUSD.Text = _oDevolucionHeader.DocTotalFC.ToString();

            CargarDetalle();

        }

        private void CargarDetalle()
        {
            GridDatos.ItemsSource = _oDevoluciones.CargarDetalle(tbNumTicket.Text).DefaultView;

        }

        public void getSelectedItems()
        {
            int idHeader = 0;

            if(GridDatos.SelectedItems.Count == 0) {
                System.Windows.MessageBox.Show("Debes seleccionar productos a devolver!!");
                return;
            }

            if(!_oDevoluciones.DevoluacionHeader(_oDevolucionHeader.NumTck, ref sMensaje)) {
                System.Windows.MessageBox.Show(sMensaje);
                return;
            } else
            {
                idHeader = Convert.ToInt32(sMensaje);
                //id devolucion header
                for (int i = 0; i < GridDatos.SelectedItems.Count; i++)
                {
                    DataRowView dataRowView = (DataRowView)GridDatos.SelectedItems[i];
                    if(!_oDevoluciones.DevolucionDetalle(_oDevolucionHeader.NumTck, Convert.ToInt32(dataRowView.Row[4].ToString()), Convert.ToDouble(dataRowView.Row[7].ToString()), ref sMensaje))
                    {
                        System.Windows.MessageBox.Show(sMensaje);
                        break;
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
            public string ItemCode { get; set; }
            public string Dscription { get; set; }
            public string Unidad { get; set; }
            public decimal Quantiy { get; set; }
            public decimal LineTotal { get; set; }
            public decimal LineTotalFrgn { get; set; }
        }

        
        private void GridDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double total = 0;
            for (int i = 0; i < GridDatos.SelectedItems.Count; i++)
            {
                DataRowView dataRow = (DataRowView)GridDatos.SelectedItems[i];
                total += Convert.ToDouble(dataRow.Row[10].ToString());
                lblTotal.Text = "Total: $" + total.ToString();
            }
        }
    }
}
