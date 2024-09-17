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
//using DllTimbraCFNN40;
using System.Diagnostics;
using System.ComponentModel;
using Capa_Datos.ReciboProducto;
using Capa_Datos.Venta;
using System.IO;
using System.Reflection;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Datos;

namespace Capa_Presentacion.SCS.Boxes
{
    /// <summary>
    /// Lógica de interacción para modalFacturacion.xaml
    /// </summary>
    public partial class modalFacturacion : Window
    {
        readonly CN_Facturacion cN_Facturacion = new CN_Facturacion();
        public bool isFacturado = false;
        private readonly CD_Conexion conn = new CD_Conexion();
        //  readonly CD_ProcesarRecibo objeto_CD_ProcesarRecibo = new CD_ProcesarRecibo();
        readonly CD_GeneraFactura objeto_CD_GeneraFactura = new CD_GeneraFactura();
        public int idTickNumb;

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

            string sMensaje = "";
            string sCardCode = tbCodigoCliente.Text;
            string sUsoCfdi = cbUsos.SelectedValue.ToString();
            int iDocEntry;

            if (idTickNumb == 0)
            {
                // Me traigo el numero de ticket

                MySqlCommand cmd = new MySqlCommand("SP_V_QryTickets", conn.AbrirConexion());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@_IdHeader", MySqlDbType.Int32).Value = 0; ;
                cmd.Parameters.Add("@_NumTck", MySqlDbType.VarChar).Value = tbTicket.Text.ToUpper();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                idTickNumb = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            

            // public int GenerarFactura(int _IdHeader, string _CardCode, string _UsoCfdi, ref string _Mensaje)
            iDocEntry = objeto_CD_GeneraFactura.GenerarFactura(idTickNumb, sCardCode, sUsoCfdi,  ref sMensaje);
            if (iDocEntry == -1)
            {
                System.Windows.MessageBox.Show(sMensaje);
                return;
            }

            string sRutaAplicacion = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string sRutaTimbrado = @sRutaAplicacion + @"\Timbrado\TimbradoCCFN40.exe";
            string sRutaXmlFiles = @sRutaAplicacion + @"\Timbrado\XmlFiles\";
            string sXmlFiles = sCardCode + "_" + iDocEntry.ToString(); // @"documento";


            if (!Directory.Exists(sRutaXmlFiles))
            {
                Directory.CreateDirectory(sRutaXmlFiles);
            }

            var process = new Process();
            string sAplTimbrar = sRutaTimbrado; //  @"C:\Users\subdirector.ti\source\repos\TimbradoCCFN40\TimbradoCCFN40\bin\Debug\TimbradoCCFN40.exe";
            // string sAplTimbrar = @"C:\Users\subdirector.ti\source\repos\TimbradoCCFN40\TimbradoCCFN40\bin\Debug\TimbradoCCFN40.exe";
            ProcessStartInfo StartInfo = new ProcessStartInfo(sAplTimbrar);
            // StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            StartInfo.Arguments = iDocEntry.ToString() + " " + sRutaXmlFiles + " " + sXmlFiles; 
            process.StartInfo = StartInfo;
            try
            {
                process.Start();
                process.WaitForExit();


                // Reviso si ya esta generado el archivo xml con sus datos

                isFacturado = true;
                this.Close();
            }
            catch (Win32Exception ex)
            {
              System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                process.Dispose();
            }
            
        }
    }
}
