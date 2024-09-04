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
public class ComprobanteConcepto
{
    public ComprobanteConceptoImpuestos Impuestos { get; set; }

    public ComprobanteConceptoACuentaTerceros ACuentaTerceros { get; set; }

    [XmlElement("InformacionAduanera")]
    public ComprobanteConceptoInformacionAduanera[] InformacionAduanera { get; set; }

    [XmlElement("CuentaPredial")]
    public ComprobanteConceptoCuentaPredial[] CuentaPredial { get; set; }

    public ComprobanteConceptoComplementoConcepto ComplementoConcepto { get; set; }

    [XmlElement("Parte")]
    public ComprobanteConceptoParte[] Parte { get; set; }

    [XmlAttribute]
    public string ClaveProdServ { get; set; }

    [XmlAttribute]
    public string NoIdentificacion { get; set; }

    [XmlAttribute]
    public decimal Cantidad { get; set; }

    [XmlAttribute]
    public string ClaveUnidad { get; set; }

    [XmlAttribute]
    public string Unidad { get; set; }

    [XmlAttribute]
    public string Descripcion { get; set; }

    [XmlAttribute]
    public decimal ValorUnitario { get; set; }

    [XmlAttribute]
    public decimal Importe { get; set; }

    [XmlAttribute]
    public decimal Descuento { get; set; }

    [XmlIgnore]
    public bool DescuentoSpecified { get; set; }

    [XmlAttribute]
    public string ObjetoImp { get; set; }
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
