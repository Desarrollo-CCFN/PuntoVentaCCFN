using Capa_Datos.ReciboProducto;
using Capa_Negocio.ReciboProducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
 

namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para EntradaRecibo.xaml
    /// </summary>
    /// 
    

    public partial class EntradaRecibo : System.Windows.Controls.UserControl
    {

        //objeto_CD_ProcesarRecibo.CD_ProcesarSolTraslado 
        readonly CN_SolTraslado objeto_CN_SolTraslado = new CN_SolTraslado();
        readonly CD_ProcesarRecibo objeto_CD_ProcesarRecibo = new CD_ProcesarRecibo();
        
      //  readonly CD_ProcesarRecibo objeto_CD_ProcesarRecibo = new CD_ProcesarRecibo();

        private bool nonNumberEntered = false;
        public DataTable dt = null;
        public string sMensaje = null;

        public EntradaRecibo()
        {
            InitializeComponent();
        }

        private void btn_Consultar_Click(object sender, RoutedEventArgs e)
        {
            RefrescarGrid();
        }

        private void RefrescarGrid()
        {

            int iNoTst = 0;

            

            if (this.tb_tsr.Text == "")
            {
                System.Windows.MessageBox.Show("Se debe de ingresar Número de transferencia para la busqueda !!!");

            }

            else
            {
                tb_FechaDoc.Text = "";
                
                try
                {
                    iNoTst = Convert.ToInt32(tb_tsr.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Invalido Numero de TSR\n" + ex.Message);
                    return;
                }

                dt = objeto_CN_SolTraslado.CargarSolTraslado(iNoTst);

               

                GridSt.ItemsSource = dt.DefaultView; // objeto_CN_SolTraslado.CargarSolTraslado(iNoTst).DefaultView;

                if (dt.Rows.Count == 0 )
                {
                    System.Windows.MessageBox.Show("Numero de TSR Inexistente");
                    return;
                }

                tb_FechaDoc.Text = string.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(dt.Rows[0][16]));
                tb_Comentarios.Text = "";

                for (int i = 0; GridSt.Columns.Count > i; i++)
                {
                    GridSt.Columns[i].IsReadOnly = true;
                }


                //  GridSt.RowStyleSelector =  SingleLine;

                //GridSt.Columns[0].Header = "Linea";
                //GridSt.Columns[1].Header = "Estatus";
                //   GridSt.Columns[2].Header = "Codigo";
                //   GridSt.Columns[3].Header = "Descripción";
                //   GridSt.Columns[4].Header = "Cantidad";
                //   GridSt.Columns[5].Header = "UmTsr";
                //   GridSt.Columns[6].Header = "Costo";
                //   GridSt.Columns[7].Header = "Total";
                //   GridSt.Columns[8].Header = "Moneda";
                //   GridSt.Columns[9].Header = "Saldo";

                //   GridSt.Columns[10].Header = "Cant.Recibo";
                //   GridSt.CellEditEnding += GridSt_CellEditEnding;
                GridSt.Columns[10].IsReadOnly = false;
             //   GridSt.Columns[10].CellStyle.Equals
               //  GridSt.Columns[10]. Columns[10]. = false;
                ////  -- GridSt.Columns[10].header

                //   GridSt.Columns[11].Header = "Base";
                //   GridSt.Columns[12].Header = "Umi";
                //   GridSt.Columns[13].Header = "Almacén";

                //   GridSt.Columns[14].Visibility = 0; // DocEntry
                //   GridSt.Columns[15].Visibility = 0; // UmEntry;

                //string svalue = GridSt.Items[0][1].ToString();


            }
        }

        private void GridSt_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
           
           
        }

        private void tb_tsr_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            

        }

        private void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (dt.Columns.Count == 17)
            {
                // Agrego la columna de comentarios
                DataColumn Col = dt.Columns.Add("Comentarios", System.Type.GetType("System.String"));
                Col.DefaultValue = tb_Comentarios.Text; 
            }
*/

            if (objeto_CD_ProcesarRecibo.CD_ProcesarSolTraslado(ref dt, ref sMensaje, tb_Comentarios.Text))
            {
                System.Windows.MessageBox.Show("Proceso exitoso !!!");
                RefrescarGrid();
            }
            else
            {
                System.Windows.MessageBox.Show(sMensaje);
            }
        }

        private void tb_FechaDoc_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void CantidadRecibo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Expresión regular para permitir solo números decimales positivos (ej. 1.23, 0.56, 100)
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            e.Handled = !regex.IsMatch(((System.Windows.Controls.TextBox)sender).Text.Insert(((System.Windows.Controls.TextBox)sender).SelectionStart, e.Text));
        }


      /*  private void GridSt_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            // Verifica si la columna es "Cant. Recibo"
            if (e.Column.Header.ToString() == "Cant. Recibo")
            {
                // Obtiene la fila que está siendo editada
                var row = e.Row.Item as YourDataType; // Reemplaza 'YourDataType' por el tipo de datos de las filas

                if (row != null && row.LineStatus == "C") // Verifica si el valor de "Status" es "C"
                {
                    // Cancela la edición si el status es "C"
                    System.Windows.MessageBox.Show("No se puede editar 'Cant. Recibo' cuando el estado es 'C'.", "Edición no permitida", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                }
            }
        }
      */

    }
}
