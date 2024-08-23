using Capa_Entidad.Facturacion;
using Capa_Negocio.Facturacion;
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

namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalFacturacion.xaml
    /// </summary>
    public partial class modalFacturacion : Window
    {
        readonly CN_Facturacion cN_Facturacion = new CN_Facturacion();
        public bool isFacturado = false;
        public modalFacturacion()
        {
            InitializeComponent();
            CargarUsos();
        }

        public void CargarUsos()
        {
            List<CE_SatUso> listaUso = cN_Facturacion.BusquedaUsos();
            cbUsos.ItemsSource = listaUso;
            cbUsos.DisplayMemberPath = "DescripUso";
            cbUsos.SelectedValuePath = "CveUso";
            cbUsos.SelectedIndex = 0;

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            isFacturado = true;
            this.Close();
        }
    }
}
