using Capa_Datos.ReciboProducto;
using Capa_Negocio.ReciboProducto;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
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
using MySql.Data.MySqlClient;
using Capa_Datos;

namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para EntradaRecibo.xaml
    /// </summary>
    /// 


    public partial class EntradaManual : System.Windows.Controls.UserControl
    {
        readonly CN_SolTraslado objeto_CN_SolTraslado = new CN_SolTraslado();
        readonly CD_EntradaManual objeto_CD_EntradaManual = new CD_EntradaManual();
        private bool nonNumberEntered = false;
        public DataTable dt = null;
        public string sMensaje = null;

        public DataTable EMTable = new DataTable("EntradaManual");
        public DataColumn dtColumn;
        public DataRow myDataRow;
        public DataSet dtSet = new DataSet();
        public BindingSource bs = new BindingSource();
        private readonly CD_Conexion? conn = new CD_Conexion();


        public EntradaManual()
        {
            InitializeComponent();
            CreateEMTable();
        }

        public void CreateEMTable()
        {

            // Create id column 0
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "LineNum";
            dtColumn.Caption = "LineNum";
            dtColumn.ReadOnly = true;
            dtColumn.Unique = true;

            EMTable.Columns.Add(dtColumn);

            // Create id column 1
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "ItemCode";
            dtColumn.Caption = "ItemCode";
            //dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            // Create id column 2
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Dscription";
            dtColumn.Caption = "Dscription";
            //dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            // Create id column 3
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "Quantity";
            dtColumn.Caption = "Quantity";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            // Create id column 4
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "UmI";
            dtColumn.Caption = "UmI";
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);


            // Create id column 5
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "Price";
            dtColumn.Caption = "Price";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            // Create id column 6
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "LineTotal";
            dtColumn.Caption = "LineTotal";
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            // Create id column 7
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Currency";
            dtColumn.Caption = "Currency";
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            // Create id column 8
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "WhsCode";
            dtColumn.Caption = "WhsCode";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);


            // Create id column 8
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "AcctCode";
            dtColumn.Caption = "AcctCode";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            EMTable.Columns.Add(dtColumn);

            

            // Add custTable to the DataSet.
            dtSet.Tables.Add(EMTable);


        }

        private void btn_Consultar_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void RefrescarGridM()
        {
            int irows = EMTable.Rows.Count;

            if (irows > 0)
            {

                for (int i = irows - 1; i >= 0; i--)
                {
                    EMTable.Rows.RemoveAt(0);
                }
            }

            EMTable.AcceptChanges();

            tb_Almacen.Clear();
            tb_Cantidad.Clear();
            tb_CodigoId.Clear();
            tb_Comentarios.Clear();
            tb_Costo.Clear();
            tb_Cuenta.Clear();

            
        }

        private void GridSt_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {

            /*
            if (e.Column.DisplayIndex == 1) // Codigo de articulo
            {
                string siItemCode = e.EditingElement.ToString();

                MySqlDataAdapter da = new MySqlDataAdapter("SP_P_BusqProductoCodigo", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_ItemCode", MySqlDbType.VarChar).Value = siItemCode;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                conn.CerrarConexion();

                
                System.Windows.Forms.MessageBox.Show(siItemCode);

            }
            */


        }

        
    
        private void tb_tsr_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            

        }

        private void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            

            if (objeto_CD_EntradaManual.CD_ProcesarEntradaManual(ref EMTable, ref sMensaje, tb_Comentarios.Text))
            {
                System.Windows.MessageBox.Show("Proceso exitoso !!!");
               
                 RefrescarGridM();
            }
            else
            {
                System.Windows.MessageBox.Show(sMensaje);
            }
        }

        private void tb_FechaDoc_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void btn_Agregar_Click(object sender, RoutedEventArgs e)
        {
            // RefrescarGridM();

            double dCantidad = 0;
            double dCosto =0;
            if (this.tb_Cuenta.Text == "")
            {
                System.Windows.MessageBox.Show("Se debe de ingresar la cuenta contable  !!!");
                return;
            }

            if (this.tb_Cantidad.Text == "")
            {
                System.Windows.MessageBox.Show("Se debe de ingresar la cantidad de entrada  !!!");
                return;
            }

            try
            {
               dCantidad = Convert.ToDouble(tb_Cantidad.Text);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Invalido valor para cantidad\n" + ex.Message);
                return;
            }

            if (this.tb_Costo.Text == "")
            {
                System.Windows.MessageBox.Show("Se debe de ingresar el costo de unidad por UMI   !!!");
                return;
            }

            try
            {
                dCosto = Convert.ToDouble(tb_Costo.Text);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Invalido valor para costo\n" + ex.Message);
                return;
            }


            // Valido Almacen

            MySqlDataAdapter da = new MySqlDataAdapter("SP_P_BusqAlmacenCodigo", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("_WhsCode", MySqlDbType.VarChar).Value = tb_Almacen.Text;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No existe el almacén " + tb_Almacen.Text + " !!!");
                return;
            }

            if (Convert.ToString(dt.Rows[0]["U_TIPOALM"]) == "TR")
            {
                System.Windows.Forms.MessageBox.Show("No esta autorizado agregar inventario a almacén de tránsito [" + tb_Almacen.Text + "] !!!");
                return;
            }

            // Valido el articulo
            MySqlDataAdapter daItem = new MySqlDataAdapter("SP_P_BusqProductoCodigo", conn.AbrirConexion());
            daItem.SelectCommand.CommandType = CommandType.StoredProcedure;
            daItem.SelectCommand.Parameters.Add("_ItemCode", MySqlDbType.VarChar).Value = tb_CodigoId.Text;
            DataSet dsItem = new DataSet();
            dsItem.Clear();
            daItem.Fill(dsItem);
            DataTable dtItem = dsItem.Tables[0];

            if (dtItem.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No existe el Código de Producto " + tb_CodigoId.Text + " !!!");
                return;
            }

            string sItemName = Convert.ToString(dtItem.Rows[0]["ItemName"]);
            string sUmiCode  = Convert.ToString(dtItem.Rows[0]["InvntryUom"]);

            conn.CerrarConexion();


            bs.DataSource = dtSet.Tables["EntradaManual"];
            GridSt.ItemsSource = bs;

            int iLine = EMTable.Rows.Count + 1;
            try
            {
                myDataRow = EMTable.NewRow();
                myDataRow["LineNum"] = iLine;
                myDataRow["ItemCode"] = tb_CodigoId.Text;
                myDataRow["Dscription"] = sItemName;
                myDataRow["Quantity"] = dCantidad;
                myDataRow["Umi"] = sUmiCode;
                myDataRow["Price"] = dCosto;
                myDataRow["LineTotal"] = (dCantidad * dCosto);
                myDataRow["Currency"] = "MXN";
                myDataRow["WhsCode"] = tb_Almacen.Text;
                myDataRow["AcctCode"] = tb_Cuenta.Text;


                EMTable.Rows.Add(myDataRow);
            }
            catch (Exception ex) 
            {
              System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void tb_Cantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo dígitos (0-9)
            // e.Handled = !IsTextAllowed(e.Text);
            e.Handled = !IsTextAllowed(tb_Cantidad.Text, e.Text);
        }

        private static bool IsTextAllowed(string currentText, string newText)
        {
            // Verificar si el texto es un número positivo entero
            //return int.TryParse(text, out int _);
            // Concatenar el texto actual con el nuevo carácter ingresado
            string fullText = currentText + newText;

            // Verificar que sea un número decimal positivo con solo un punto decimal
            return decimal.TryParse(fullText, out decimal result) && result >= 0;
        }

        private void tb_Cantidad_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Prevenir pegado de texto
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }

        

        private void tb_Costo_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
             // Evitar que el usuario pegue texto
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }

        private void tb_Costo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verificar si el texto ingresado es válido (número decimal positivo)
            e.Handled = !IsTextAllowed_Costo(tb_Costo.Text, e.Text);
        }

        private static bool IsTextAllowed_Costo(string currentText, string newText)
        {
            // Verificar si el texto es un número positivo entero
            //return int.TryParse(text, out int _);
            // Concatenar el texto actual con el nuevo carácter ingresado
            string fullText = currentText + newText;

            // Verificar que sea un número decimal positivo con solo un punto decimal
            return decimal.TryParse(fullText, out decimal result) && result >= 0;
        }

        private void tb_Costo_LostFocus(object sender, RoutedEventArgs e)
        {
            // Formatea el número a dos decimales cuando pierde el foco
           /*  if (decimal.TryParse(tb_Costo.Text, out decimal value))
            {
                tb_Costo.Text = value.ToString("F4");  // Ajusta aquí el número de decimales, por ejemplo "F4" para cuatro decimales
            }
         */
        }
    }
}
