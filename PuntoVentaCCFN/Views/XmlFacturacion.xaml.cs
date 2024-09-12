using Capa_Datos;
using Capa_Datos.TimbraCFDI40;
using Mysqlx.Cursor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
using System.Xml;
using System.Xml.Serialization;


namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Interaction logic for XmlFacturacion.xaml
    /// </summary>
    public partial class XmlFacturacion : System.Windows.Controls.UserControl
    {
        readonly CD_FacturaCFDI objeto_CD_FacturaCFDI = new CD_FacturaCFDI();
        public DataTable dt = null;
        public XmlFacturacion()
        {
            InitializeComponent();
        }

        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            int iDocNUm = 0;

            try
            {
                iDocNUm = Convert.ToInt32(TbDocNum.Text);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Invalido Numero de Factura\n" + ex.Message);
                return;
            }

            dt = objeto_CD_FacturaCFDI.ConsultaDatosFactura(iDocNUm);

            GridXmlItems.ItemsSource = dt.DefaultView; // objeto_CN_SolTraslado.CargarSolTraslado(iNoTst).DefaultView;

            for (int i = 0; GridXmlItems.Columns.Count > i; i++)
            {
                GridXmlItems.Columns[i].IsReadOnly = true;
            }


            /*

            GridSt.Columns[0].Header = "Linea";
            GridSt.Columns[1].Header = "Estatus";
            GridSt.Columns[2].Header = "Codigo";
            GridSt.Columns[3].Header = "Descripción";
            GridSt.Columns[4].Header = "Cantidad";
            GridSt.Columns[5].Header = "UmTsr";
            GridSt.Columns[6].Header = "Costo";
            GridSt.Columns[7].Header = "Total";
            GridSt.Columns[8].Header = "Moneda";
            GridSt.Columns[9].Header = "Saldo";

            GridSt.Columns[10].Header = "Cant.Recibo";
            GridSt.CellEditEnding += GridSt_CellEditEnding;
            GridSt.Columns[10].IsReadOnly = false;
            //  -- GridSt.Columns[10].header

            GridSt.Columns[11].Header = "Base";
            GridSt.Columns[12].Header = "Umi";
            GridSt.Columns[13].Header = "Almacén";

            GridSt.Columns[14].Visibility = 0; // DocEntry
            GridSt.Columns[15].Visibility = 0; // UmEntry;
            */

        }

        private void BtnCreaXml_Click(object sender, RoutedEventArgs e)
        {
            //$this->xmlfile = new DOMDocument('1.0', 'utf-8');

            XmlDocument xml = new XmlDocument();
           

            XmlNode root = xml.CreateElement("cfdi:Comprobante");
            xml.AppendChild(root);

            #region Encabezado

            XmlAttribute xmlnscfdi = xml.CreateAttribute("xmlns:cfdi");
            xmlnscfdi.Value = "http://www.sat.gob.mx/cfd/4";
            root.Attributes.Append(xmlnscfdi);

            XmlAttribute xmlnsxsi = xml.CreateAttribute("xmlns:xsi");
            xmlnsxsi.Value = "http://www.w3.org/2001/XMLSchema-instance";
            root.Attributes.Append(xmlnsxsi);

            XmlAttribute xsischemaLocation = xml.CreateAttribute("xsi:schemaLocation");
            xsischemaLocation.Value = "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd";
            root.Attributes.Append(xsischemaLocation);
            #endregion

            #region DatosDocumento
            XmlAttribute Version = xml.CreateAttribute("Version");
            Version.Value = "4.0";
            root.Attributes.Append(Version);

            XmlAttribute Folio = xml.CreateAttribute("Folio");
            Folio.Value = "148";
            root.Attributes.Append(Folio);

            XmlAttribute Serie = xml.CreateAttribute("Serie");
            Serie.Value = "E";
            root.Attributes.Append(Serie);

            XmlAttribute Fecha = xml.CreateAttribute("Fecha");
            Fecha.Value = "2023-12-27T17:17:27";
            root.Attributes.Append(Fecha);

            XmlAttribute FormaPago = xml.CreateAttribute("FormaPago");
            FormaPago.Value = "03";
            root.Attributes.Append(FormaPago);

            XmlAttribute SubTotal = xml.CreateAttribute("SubTotal");
            SubTotal.Value = "2050.00";
            root.Attributes.Append(SubTotal);

            XmlAttribute Moneda = xml.CreateAttribute("SubTotal");
            Moneda.Value = "MXN";
            root.Attributes.Append(Moneda);

            XmlAttribute TipoCambio = xml.CreateAttribute("TipoCambio");
            TipoCambio.Value = "1";
            root.Attributes.Append(TipoCambio);

            XmlAttribute Total = xml.CreateAttribute("Total");
            Total.Value = "2378.00";
            root.Attributes.Append(Total);
                         
            XmlAttribute TipoDeComprobante = xml.CreateAttribute("TipoDeComprobante");
            TipoDeComprobante.Value = "I";
            root.Attributes.Append(TipoDeComprobante);

            XmlAttribute MetodoPago = xml.CreateAttribute("MetodoPago");
            MetodoPago.Value = "PUE";
            root.Attributes.Append(MetodoPago);

            XmlAttribute LugarExpedicion = xml.CreateAttribute("LugarExpedicion");
            LugarExpedicion.Value = "42501";
            root.Attributes.Append(LugarExpedicion);

            XmlAttribute Exportacion = xml.CreateAttribute("Exportacion");
            Exportacion.Value = "01";
            root.Attributes.Append(Exportacion);
            #endregion

            #region NodoEmisor
            XmlNode NodoEmisor = xml.CreateElement("Emisor");
            root.AppendChild(NodoEmisor);

            XmlAttribute Rfc = xml.CreateAttribute("Rfc");
            Rfc.Value = "EKU9003173C9";
            NodoEmisor.Attributes.Append(Rfc);

            XmlAttribute RegimenFiscal = xml.CreateAttribute("RegimenFiscal");
            RegimenFiscal.Value = "601";
            NodoEmisor.Attributes.Append(RegimenFiscal);

            XmlAttribute NombreEmisor = xml.CreateAttribute("Nombre");
            NombreEmisor.Value = "ESCUELA KEMPER URGATE";
            NodoEmisor.Attributes.Append(NombreEmisor);
            #endregion

            #region NodoReceptor
            XmlNode NodoReceptor = xml.CreateElement("Receptor");
            root.AppendChild(NodoReceptor);

            XmlAttribute RfcReceptor = xml.CreateAttribute("Rfc");
            RfcReceptor.Value = "EKU9003173C9";
            NodoReceptor.Attributes.Append(RfcReceptor);

            XmlAttribute RegimenFiscalReceptor = xml.CreateAttribute("RegimenFiscalReceptor");
            RegimenFiscalReceptor.Value = "601";
            NodoReceptor.Attributes.Append(RegimenFiscalReceptor);

            XmlAttribute NombreReceptor = xml.CreateAttribute("Nombre");
            NombreReceptor.Value = "RUIZ RENTERIA";
            NodoReceptor.Attributes.Append(NombreReceptor);

            XmlAttribute DomicilioFiscalReceptor = xml.CreateAttribute("DomicilioFiscalReceptor");
            DomicilioFiscalReceptor.Value = "22840";
            NodoReceptor.Attributes.Append(DomicilioFiscalReceptor);

            XmlAttribute UsoCFDI = xml.CreateAttribute("UsoCFDI");
            UsoCFDI.Value = "G01";
            NodoReceptor.Attributes.Append(UsoCFDI);
            #endregion



            XmlNode NodoConceptos = xml.CreateElement("cfdi:Conceptos");
            root.AppendChild(NodoConceptos);
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode NodoConcepto = xml.CreateElement("cfdi:Concepto");

                XmlAttribute ClaveProdServ = xml.CreateAttribute("ClaveProdServ");
                ClaveProdServ.Value = dt.Rows[i]["ItemCode"].ToString();
                NodoConcepto.Attributes.Append(ClaveProdServ);

                XmlAttribute Cantidad = xml.CreateAttribute("Cantidad");
                Cantidad.Value = dt.Rows[i]["Quantity"].ToString();
                NodoConcepto.Attributes.Append(Cantidad);

                XmlAttribute ClaveUnidad = xml.CreateAttribute("ClaveUnidad");
                ClaveUnidad.Value = dt.Rows[i]["UomCode"].ToString();
                NodoConcepto.Attributes.Append(ClaveUnidad);

                XmlAttribute Descripcion = xml.CreateAttribute("Descripcion");
                Descripcion.Value = dt.Rows[i]["Dscription"].ToString();
                NodoConcepto.Attributes.Append(Descripcion);

                XmlAttribute ValorUnitario = xml.CreateAttribute("ValorUnitario");
                ValorUnitario.Value = dt.Rows[i]["Price"].ToString();
                NodoConcepto.Attributes.Append(ValorUnitario);

                XmlAttribute Importe = xml.CreateAttribute("Importe");
                Importe.Value = dt.Rows[i]["LineTotalAmount"].ToString();
                NodoConcepto.Attributes.Append(Importe);

                XmlAttribute ObjetoImp = xml.CreateAttribute("Importe");
                ObjetoImp.Value = "02"; // dt.Rows[i]["LineTotalAmount"].ToString();
                NodoConcepto.Attributes.Append(ObjetoImp);

                XmlNode NodoImpuestos = xml.CreateElement("cfdi:Impuestos");

                XmlNode NodoTraslados = xml.CreateElement("cfdi:Traslados");

                XmlNode NodoTraslado = xml.CreateElement("cfdi:Traslado");

                XmlAttribute NTImporte = xml.CreateAttribute("Importe");
                NTImporte.Value = "240.00"; 
                NodoTraslado.Attributes.Append(NTImporte);

                XmlAttribute NTTipoFactor = xml.CreateAttribute("TipoFactor");
                NTTipoFactor.Value = "Tasa";
                NodoTraslado.Attributes.Append(NTTipoFactor);

                XmlAttribute NTTasaOCuota = xml.CreateAttribute("TasaOCuota");
                NTTasaOCuota.Value = "0.160000";
                NodoTraslado.Attributes.Append(NTTasaOCuota);

                XmlAttribute NTImpuesto = xml.CreateAttribute("Impuesto");
                NTImpuesto.Value = "002";
                NodoTraslado.Attributes.Append(NTImpuesto);

                XmlAttribute NTBase = xml.CreateAttribute("Base");
                NTBase.Value = "1500.00";
                NodoTraslado.Attributes.Append(NTBase);

                NodoTraslados.AppendChild(NodoTraslado);
                NodoImpuestos.AppendChild(NodoTraslados);
                NodoConcepto.AppendChild(NodoImpuestos);


                NodoConceptos.AppendChild(NodoConcepto);
               // xml.AppendChild(NodoConcepto);
            }

            //XmlNode rootConceptos = xml.CreateElement("cfdi:Conceptos");
           // xml.AppendChild(rootConceptos);


            XmlNode cfdiNodoImpuestos = xml.CreateElement("cfdi:Impuestos");
           // xml.AppendChild(rootImpuestos);
            root.AppendChild(cfdiNodoImpuestos);

            XmlAttribute TotalImpuestosTrasladados = xml.CreateAttribute("TotalImpuestosTrasladados");
            TotalImpuestosTrasladados.Value = "328.00";
            cfdiNodoImpuestos.Attributes.Append(TotalImpuestosTrasladados);

            XmlNode cfdiTraslados = xml.CreateElement("cfdi:Traslados");

            XmlNode cfdiTraslado = xml.CreateElement("cfdi:Traslado");

            XmlAttribute itBase = xml.CreateAttribute("Base");
            itBase.Value = "208.00";
            cfdiTraslado.Attributes.Append(itBase);

            XmlAttribute itImpuesto = xml.CreateAttribute("Impuesto");
            itImpuesto.Value = "002";
            cfdiTraslado.Attributes.Append(itImpuesto);

            XmlAttribute itTipoFactor = xml.CreateAttribute("TipoFactor");
            itTipoFactor.Value = "Tasa";
            cfdiTraslado.Attributes.Append(itTipoFactor);

            XmlAttribute itTasaOCuota = xml.CreateAttribute("TasaOCuota");
            itTasaOCuota.Value = "0.160000";
            cfdiTraslado.Attributes.Append(itTasaOCuota);

            XmlAttribute itImporte = xml.CreateAttribute("Importe");
            itImporte.Value = "328.00";
            cfdiTraslado.Attributes.Append(itImporte);

            cfdiTraslados.AppendChild(cfdiTraslado);
            cfdiNodoImpuestos.AppendChild(cfdiTraslados);
            
            xml.Save("XmlEjemplo.xml");
        }

        private void GeneraXml40()
        {
            //Ejemplo de timbrado de CFDI v4.0 haciendo uso de las clases serializables para el llenado de la información

            //Inicializamos el conector, el parámetro indica el ambiente que deseamos utilizar
          //  Conector conector = new Conector(ambienteProductivo);

            //Establecemos las credenciales para el permiso de conexión
         // **   conector.EstableceCredenciales(usuarioIntegrador);

            //Creamos un objeto comprobante por medio de la entidad Comprobante 
            Comprobante comprobante = new Comprobante();

            //A continuación se muestran diferentes escenarios de timbrado de CFDI v4.0, puedes comentar/descomentar el código según el escenario que deseas probar

            //Por lo anterior se generaria el primer ejemplo que no esta comentado "Comprobante Ingreso 4.0"
           // #region Comprobante Ingreso 4.0

            comprobante.Serie = "CFDI4I";
            comprobante.Folio = "1";
            comprobante.Version = "4.0";
            comprobante.Fecha = DateTime.Now;
            comprobante.TipoDeComprobante = "I";
            comprobante.Descuento = 1.00m;
            comprobante.DescuentoSpecified = true;
            comprobante.Total = 198.95m;
            comprobante.SubTotal = 200.00m;
            comprobante.LugarExpedicion = "20000";
            comprobante.Moneda = "AMD";
            comprobante.FormaPago = "99";
            comprobante.FormaPagoSpecified = true;
            comprobante.TipoCambio = 1.00m;
            comprobante.TipoCambioSpecified = true;
            comprobante.MetodoPago = "PPD";
            comprobante.MetodoPagoSpecified = true;
            comprobante.Exportacion = "01";

            ////Llenamos datos del emisor
            comprobante.Emisor = new Capa_Datos.TimbraCFDI40.ComprobanteEmisor();
            comprobante.Emisor.Rfc = "IIA040805DZ4";
            comprobante.Emisor.Nombre = "INDISTRIA ILUMINADORA DE ALMACENES";
            comprobante.Emisor.RegimenFiscal = "601";


            ////Llena datos del receptor
            comprobante.Receptor = new Capa_Datos.TimbraCFDI40.ComprobanteReceptor();
            comprobante.Receptor.Rfc = "PFE140312IW8";
            comprobante.Receptor.Nombre = "PROVEEDORES DE FACTURACION ELECTRONICA Y SOFTWARE";
            comprobante.Receptor.DomicilioFiscalReceptor = "53100";
            comprobante.Receptor.RegimenFiscalReceptor = "601";
            comprobante.Receptor.UsoCFDI = "G01";

            ////Llenamos los conceptos
            comprobante.Conceptos = new Capa_Datos.TimbraCFDI40.ComprobanteConcepto[1];

            ////Concepto 1
            comprobante.Conceptos[0] = new Capa_Datos.TimbraCFDI40.ComprobanteConcepto();
            comprobante.Conceptos[0].Cantidad = 1;
            comprobante.Conceptos[0].Unidad = "Pieza";
            comprobante.Conceptos[0].ClaveUnidad = "H87";
            comprobante.Conceptos[0].Descripcion = "Cigarros";
            comprobante.Conceptos[0].ValorUnitario = 200.00m;
            comprobante.Conceptos[0].Importe = 200.00m;
            comprobante.Conceptos[0].Descuento = 1.00m;
            comprobante.Conceptos[0].DescuentoSpecified = true;
            comprobante.Conceptos[0].ClaveProdServ = "50211503";
            comprobante.Conceptos[0].ObjetoImp = "02";

            ////Llenamos los impuestos del concepto
            comprobante.Conceptos[0].Impuestos = new Capa_Datos.TimbraCFDI40.ComprobanteConceptoImpuestos();
            comprobante.Conceptos[0].Impuestos.Traslados = new Capa_Datos.TimbraCFDI40.ComprobanteConceptoImpuestosTraslado[1];
            comprobante.Conceptos[0].Impuestos.Traslados[0] = new Capa_Datos.TimbraCFDI40.ComprobanteConceptoImpuestosTraslado();
            comprobante.Conceptos[0].Impuestos.Traslados[0].Base = 1.00m;
            comprobante.Conceptos[0].Impuestos.Traslados[0].Impuesto = "002";
            comprobante.Conceptos[0].Impuestos.Traslados[0].TipoFactor = "Tasa";
            comprobante.Conceptos[0].Impuestos.Traslados[0].Importe = 0.16m;
            comprobante.Conceptos[0].Impuestos.Traslados[0].ImporteSpecified = true;
            comprobante.Conceptos[0].Impuestos.Traslados[0].TasaOCuota = "0.160000";
            comprobante.Conceptos[0].Impuestos.Traslados[0].TasaOCuotaSpecified = true;

            comprobante.Conceptos[0].Impuestos.Retenciones = new Capa_Datos.TimbraCFDI40.ComprobanteConceptoImpuestosRetencion[2];
            comprobante.Conceptos[0].Impuestos.Retenciones[0] = new Capa_Datos.TimbraCFDI40.ComprobanteConceptoImpuestosRetencion();
            comprobante.Conceptos[0].Impuestos.Retenciones[0].Base = 1.00m;
            comprobante.Conceptos[0].Impuestos.Retenciones[0].Impuesto = "001";
            comprobante.Conceptos[0].Impuestos.Retenciones[0].TipoFactor = "Tasa";
            comprobante.Conceptos[0].Impuestos.Retenciones[0].Importe = 0.10m;
            comprobante.Conceptos[0].Impuestos.Retenciones[0].TasaOCuota = 0.100000m;

            comprobante.Conceptos[0].Impuestos.Retenciones[1] = new Capa_Datos.TimbraCFDI40.ComprobanteConceptoImpuestosRetencion();
            comprobante.Conceptos[0].Impuestos.Retenciones[1].Base = 1.00m;
            comprobante.Conceptos[0].Impuestos.Retenciones[1].Impuesto = "002";
            comprobante.Conceptos[0].Impuestos.Retenciones[1].TipoFactor = "Tasa";
            comprobante.Conceptos[0].Impuestos.Retenciones[1].Importe = 0.11m;
            comprobante.Conceptos[0].Impuestos.Retenciones[1].TasaOCuota = 0.106666m;

            ////LLenamos el nodo impuestos globales
            comprobante.Impuestos = new Capa_Datos.TimbraCFDI40.ComprobanteImpuestos();

            ////Impuesto 2
            comprobante.Impuestos.Retenciones = new Capa_Datos.TimbraCFDI40.ComprobanteImpuestosRetencion[2];
            comprobante.Impuestos.Retenciones[0] = new Capa_Datos.TimbraCFDI40.ComprobanteImpuestosRetencion();
            comprobante.Impuestos.Retenciones[0].Impuesto = "001";
            comprobante.Impuestos.Retenciones[0].Importe = 0.10m;

            comprobante.Impuestos.Retenciones[1] = new Capa_Datos.TimbraCFDI40.ComprobanteImpuestosRetencion();
            comprobante.Impuestos.Retenciones[1].Impuesto = "002";
            comprobante.Impuestos.Retenciones[1].Importe = 0.11m;

            ////Impuesto 1
            comprobante.Impuestos.Traslados = new Capa_Datos.TimbraCFDI40.ComprobanteImpuestosTraslado[1];
            comprobante.Impuestos.Traslados[0] = new Capa_Datos.TimbraCFDI40.ComprobanteImpuestosTraslado();
            comprobante.Impuestos.Traslados[0].Base = 1.00m;
            comprobante.Impuestos.Traslados[0].Impuesto = "002";
            comprobante.Impuestos.Traslados[0].TipoFactor = "Tasa";
            comprobante.Impuestos.Traslados[0].TasaOCuota = "0.160000";
            comprobante.Impuestos.Traslados[0].TasaOCuotaSpecified = true;
            comprobante.Impuestos.Traslados[0].Importe = 0.16m;
            comprobante.Impuestos.Traslados[0].ImporteSpecified = true;

            comprobante.Impuestos.TotalImpuestosRetenidos = 0.21m;
            comprobante.Impuestos.TotalImpuestosTrasladados = 0.16m;
            comprobante.Impuestos.TotalImpuestosRetenidosSpecified = true;
            comprobante.Impuestos.TotalImpuestosTrasladadosSpecified = true;


            //Si deseas obtener el XML generado por las clases para efectos de revisión puedes utilizar la variable xmlEncode
            string xmlEncode;
            using (StringWriter sw = new Utf8StringWriter())
            {
                var xs = new XmlSerializer(typeof(Comprobante));
                xs.Serialize(sw, comprobante);
                xmlEncode = sw.ToString();
                sw.Close();

            }


        }

    }
}


