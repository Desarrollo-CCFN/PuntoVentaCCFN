using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Capa_Datos.TimbraCFDI40;
[Serializable]
[DebuggerStepThrough]
[XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
[XmlRoot(Namespace = "http://www.sat.gob.mx/cfd/4", IsNullable = false)]

public class Comprobante
{
    [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public string xsiSchemaLocation = "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd";

    [XmlIgnore]
    public bool DonativoSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool DivisasSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/divisas http://www.sat.gob.mx/sitio_internet/cfd/divisas/divisas.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/divisas http://www.sat.gob.mx/sitio_internet/cfd/divisas/divisas.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/divisas http://www.sat.gob.mx/sitio_internet/cfd/divisas/divisas.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ImpLocalSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool LeyendasFiscalesSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/leyendasFiscales http://www.sat.gob.mx/sitio_internet/cfd/leyendasFiscales/leyendasFisc.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/leyendasFiscales http://www.sat.gob.mx/sitio_internet/cfd/leyendasFiscales/leyendasFisc.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/leyendasFiscales http://www.sat.gob.mx/sitio_internet/cfd/leyendasFiscales/leyendasFisc.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool PFICSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/pfic http://www.sat.gob.mx/sitio_internet/cfd/pfic/pfic.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/pfic http://www.sat.gob.mx/sitio_internet/cfd/pfic/pfic.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/pfic http://www.sat.gob.mx/sitio_internet/cfd/pfic/pfic.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool TuristaPasajeroExtranjeroSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/TuristaPasajeroExtranjero http://www.sat.gob.mx/sitio_internet/cfd/TuristaPasajeroExtranjero/TuristaPasajeroExtranjero.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/TuristaPasajeroExtranjero http://www.sat.gob.mx/sitio_internet/cfd/TuristaPasajeroExtranjero/TuristaPasajeroExtranjero.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/TuristaPasajeroExtranjero http://www.sat.gob.mx/sitio_internet/cfd/TuristaPasajeroExtranjero/TuristaPasajeroExtranjero.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool Nomina12Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace("http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool CFDIRegistroFiscalSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/registrofiscal http://www.sat.gob.mx/sitio_internet/cfd/cfdiregistrofiscal/cfdiregistrofiscal.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/registrofiscal http://www.sat.gob.mx/sitio_internet/cfd/cfdiregistrofiscal/cfdiregistrofiscal.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/registrofiscal http://www.sat.gob.mx/sitio_internet/cfd/cfdiregistrofiscal/cfdiregistrofiscal.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool PagoEnEspecieSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/pagoenespecie http://www.sat.gob.mx/sitio_internet/cfd/pagoenespecie/pagoenespecie.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/pagoenespecie http://www.sat.gob.mx/sitio_internet/cfd/pagoenespecie/pagoenespecie.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/pagoenespecie http://www.sat.gob.mx/sitio_internet/cfd/pagoenespecie/pagoenespecie.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool AerolineasSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/aerolineas http://www.sat.gob.mx/sitio_internet/cfd/aerolineas/aerolineas.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/aerolineas http://www.sat.gob.mx/sitio_internet/cfd/aerolineas/aerolineas.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/aerolineas http://www.sat.gob.mx/sitio_internet/cfd/aerolineas/aerolineas.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ValesDeDespensaSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/valesdedespensa http://www.sat.gob.mx/sitio_internet/cfd/valesdedespensa/valesdedespensa.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/valesdedespensa http://www.sat.gob.mx/sitio_internet/cfd/valesdedespensa/valesdedespensa.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/valesdedespensa http://www.sat.gob.mx/sitio_internet/cfd/valesdedespensa/valesdedespensa.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool NotariosSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/notariospublicos http://www.sat.gob.mx/sitio_internet/cfd/notariospublicos/notariospublicos.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/notariospublicos http://www.sat.gob.mx/sitio_internet/cfd/notariospublicos/notariospublicos.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/notariospublicos http://www.sat.gob.mx/sitio_internet/cfd/notariospublicos/notariospublicos.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool VehiculoUsadoSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/vehiculousado http://www.sat.gob.mx/sitio_internet/cfd/vehiculousado/vehiculousado.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/vehiculousado http://www.sat.gob.mx/sitio_internet/cfd/vehiculousado/vehiculousado.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/vehiculousado http://www.sat.gob.mx/sitio_internet/cfd/vehiculousado/vehiculousado.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ServicioParcialConstruccionSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/servicioparcialconstruccion http://www.sat.gob.mx/sitio_internet/cfd/servicioparcialconstruccion/servicioparcialconstruccion.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/servicioparcialconstruccion http://www.sat.gob.mx/sitio_internet/cfd/servicioparcialconstruccion/servicioparcialconstruccion.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/servicioparcialconstruccion http://www.sat.gob.mx/sitio_internet/cfd/servicioparcialconstruccion/servicioparcialconstruccion.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool RenovacionYSustitucionDeVehiculosSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/renovacionysustitucionvehiculos http://www.sat.gob.mx/sitio_internet/cfd/renovacionysustitucionvehiculos/renovacionysustitucionvehiculos.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/renovacionysustitucionvehiculos http://www.sat.gob.mx/sitio_internet/cfd/renovacionysustitucionvehiculos/renovacionysustitucionvehiculos.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/renovacionysustitucionvehiculos http://www.sat.gob.mx/sitio_internet/cfd/renovacionysustitucionvehiculos/renovacionysustitucionvehiculos.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool CertificadoDeDestruccionSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/certificadodestruccion http://www.sat.gob.mx/sitio_internet/cfd/certificadodestruccion/certificadodedestruccion.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/certificadodestruccion http://www.sat.gob.mx/sitio_internet/cfd/certificadodestruccion/certificadodedestruccion.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/certificadodestruccion http://www.sat.gob.mx/sitio_internet/cfd/certificadodestruccion/certificadodedestruccion.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ObrasArteSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/arteantiguedades http://www.sat.gob.mx/sitio_internet/cfd/arteantiguedades/obrasarteantiguedades.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/arteantiguedades http://www.sat.gob.mx/sitio_internet/cfd/arteantiguedades/obrasarteantiguedades.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/arteantiguedades http://www.sat.gob.mx/sitio_internet/cfd/arteantiguedades/obrasarteantiguedades.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ComercioExterior11Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/ComercioExterior11 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior11/ComercioExterior11.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/ComercioExterior11 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior11/ComercioExterior11.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace("http://www.sat.gob.mx/ComercioExterior11 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior11/ComercioExterior11.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool INE11Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/ine http://www.sat.gob.mx/sitio_internet/cfd/ine/ine11.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/ine http://www.sat.gob.mx/sitio_internet/cfd/ine/ine11.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/ine http://www.sat.gob.mx/sitio_internet/cfd/ine/ine11.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool IeudSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/iedu http://www.sat.gob.mx/sitio_internet/cfd/iedu/iedu.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/iedu http://www.sat.gob.mx/sitio_internet/cfd/iedu/iedu.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/iedu http://www.sat.gob.mx/sitio_internet/cfd/iedu/iedu.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool VentaVehiculosSpecified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/ventavehiculos http://www.sat.gob.mx/sitio_internet/cfd/ventavehiculos/ventavehiculos11.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/ventavehiculos http://www.sat.gob.mx/sitio_internet/cfd/ventavehiculos/ventavehiculos11.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/ventavehiculos http://www.sat.gob.mx/sitio_internet/cfd/ventavehiculos/ventavehiculos11.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool DetallistaSpecified
    {
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/detallista http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/detallista http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool EstadoDeCuentaCombustibles12Specified
    {
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/EstadoDeCuentaCombustible12 http://www.sat.gob.mx/sitio_internet/cfd/EstadoDeCuentaCombustible/ecc12.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/EstadoDeCuentaCombustible12 http://www.sat.gob.mx/sitio_internet/cfd/EstadoDeCuentaCombustible/ecc12.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ConsumoDeCombustibles11Specified
    {
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/ConsumoDeCombustibles11 http://www.sat.gob.mx/sitio_internet/cfd/ConsumoDeCombustibles/consumodeCombustibles11.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/ConsumoDeCombustibles11 http://www.sat.gob.mx/sitio_internet/cfd/ConsumoDeCombustibles/consumodeCombustibles11.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool GastosHidrocarburos10Specified
    {
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/GastosHidrocarburos10 http://www.sat.gob.mx/sitio_internet/cfd/GastosHidrocarburos10/GastosHidrocarburos10.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/GastosHidrocarburos10 http://www.sat.gob.mx/sitio_internet/cfd/GastosHidrocarburos10/GastosHidrocarburos10.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool IngresosHidrocarburos10Specified
    {
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/IngresosHidrocarburos10 http://www.sat.gob.mx/sitio_internet/cfd/IngresosHidrocarburos10/IngresosHidrocarburos.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/IngresosHidrocarburos10 http://www.sat.gob.mx/sitio_internet/cfd/IngresosHidrocarburos10/IngresosHidrocarburos.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool CartaPorte20Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/CartaPorte20 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte20.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/CartaPorte20 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte20.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/CartaPorte20 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte20.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool CartaPorte30Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/CartaPorte30 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte30.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/CartaPorte30 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte30.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/CartaPorte30 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte30.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool CartaPorte31Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/CartaPorte31 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte31.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/CartaPorte31 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte31.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/CartaPorte31 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte31.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool Pagos20Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public bool ComercioExterior20Specified
    {
        get
        {
            return xsiSchemaLocation.Contains("http://www.sat.gob.mx/ComercioExterior20 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior20/ComercioExterior20.xsd");
        }
        set
        {
            if (value)
            {
                xsiSchemaLocation += " http://www.sat.gob.mx/ComercioExterior20 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior20/ComercioExterior20.xsd";
            }
            else
            {
                xsiSchemaLocation = xsiSchemaLocation.Replace(" http://www.sat.gob.mx/ComercioExterior20 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior20/ComercioExterior20.xsd", "");
            }
        }
    }

    [XmlIgnore]
    public string AddendaEsquema
    {
        get
        {
            return xsiSchemaLocation;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                xsiSchemaLocation = xsiSchemaLocation + " " + value;
            }
        }
    }

    [XmlIgnore]
    public string ComplementoEsquema
    {
        get
        {
            return xsiSchemaLocation;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                xsiSchemaLocation = xsiSchemaLocation + " " + value;
            }
        }
    }

    [XmlElement(Order = 1)]
    public ComprobanteInformacionGlobal InformacionGlobal { get; set; }

    [XmlElement(Order = 2)]
    public ComprobanteCfdiRelacionados[] CfdiRelacionados { get; set; }

    [XmlElement(Order = 3)]
    public ComprobanteEmisor Emisor { get; set; }

    [XmlElement(Order = 4)]
    public ComprobanteReceptor Receptor { get; set; }

    [XmlArray(Order = 5)]
    [XmlArrayItem("Concepto", IsNullable = false)]
    public ComprobanteConcepto[] Conceptos { get; set; }

    [XmlElement(Order = 6)]
    public ComprobanteImpuestos Impuestos { get; set; }

    [XmlElement(Order = 7)]
    public ComprobanteComplemento Complemento { get; set; }

    [XmlElement(Order = 8)]
    public ComprobanteAddenda Addenda { get; set; }

    [XmlAttribute]
    public string Version { get; set; }

    [XmlAttribute]
    public string Serie { get; set; }

    [XmlAttribute]
    public string Folio { get; set; }

    [XmlIgnore]
    public DateTime Fecha { get; set; }

    [XmlAttribute("Fecha")]
    public string XmlDateTime
    {
        get
        {
            return Fecha.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        set
        {
            Fecha = DateTime.Parse(value);
        }
    }

    [XmlAttribute]
    public string Sello { get; set; }

    [XmlAttribute]
    public string FormaPago { get; set; }

    [XmlIgnore]
    public bool FormaPagoSpecified { get; set; }

    [XmlAttribute]
    public string NoCertificado { get; set; }

    [XmlAttribute]
    public string Certificado { get; set; }

    [XmlAttribute]
    public string CondicionesDePago { get; set; }

    [XmlAttribute]
    public decimal SubTotal { get; set; }

    [XmlAttribute]
    public decimal Descuento { get; set; }

    [XmlIgnore]
    public bool DescuentoSpecified { get; set; }

    [XmlAttribute]
    public string Moneda { get; set; }

    [XmlAttribute]
    public decimal TipoCambio { get; set; }

    [XmlIgnore]
    public bool TipoCambioSpecified { get; set; }

    [XmlAttribute]
    public decimal Total { get; set; }

    [XmlAttribute]
    public string TipoDeComprobante { get; set; }

    [XmlAttribute]
    public string Exportacion { get; set; }

    [XmlAttribute]
    public string MetodoPago { get; set; }

    [XmlIgnore]
    public bool MetodoPagoSpecified { get; set; }

    [XmlAttribute]
    public string LugarExpedicion { get; set; }

    [XmlAttribute]
    public string Confirmacion { get; set; }

    public Comprobante()
    {
        Version = "4.0";
    }
}
#if false // Decompilation log
'11' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
------------------
Resolve: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Xml.dll'
------------------
Resolve: 'System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.dll'
------------------
Resolve: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Xml.Linq.dll'
------------------
Resolve: 'System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Core.dll'
------------------
Resolve: 'System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Could not find by name: 'System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
#endif
