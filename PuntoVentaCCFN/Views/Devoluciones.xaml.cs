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

            if(_oDevolucionHeader == null)
            {
                System.Windows.MessageBox.Show("Venta no encontrada en el rango de fechas!!");
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
            if(GridDatos.SelectedItems.Count == 0) {
                System.Windows.MessageBox.Show("Debes seleccionar productos a devolver!!");
                return;
            }

            for(int i = 0; i < GridDatos.SelectedItems.Count; i++)
            {
                DataRowView dataRowView = (DataRowView)GridDatos.SelectedItems[i];

            }

            if(!_oDevoluciones.DevoluacionHeader(_oDevolucionHeader.Id, int.Parse(MainWindow.AppConfig1.Caja), ref sMensaje)) {
                System.Windows.MessageBox.Show(sMensaje);
                return;
            }

            System.Windows.MessageBox.Show("Exito!!");
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
    }
}
