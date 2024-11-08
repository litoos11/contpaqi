using System.Xml.Serialization;

namespace Contpaqi.Entities;

[XmlRoot("Comprobante")]
public class Comprobante
{

  [XmlAttribute("Version")]
  public required string Version { get; set; }
  [XmlAttribute("LugarExpedicion")]
  public required string LugarExpedicion { get; set; }
  [XmlAttribute("MetodoPago")]
  public required string MetodoPago { get; set; }
  [XmlAttribute("TipoDeComprobante")]
  public required string TipoDeComprobante { get; set; }
  [XmlAttribute("FormaPago")]
  public required string FormaPago { get; set; }
  [XmlAttribute("Folio")]
  public required string Folio { get; set; }
  [XmlAttribute("Moneda")]
  public required string Moneda { get; set; }
  [XmlAttribute("Serie")]
  public required string Serie { get; set; }
  [XmlAttribute("UUID")]
  public required string UUID { get; set; }
  [XmlAttribute("Fecha")]
  public required DateTime Fecha { get; set; }
  [XmlAttribute("Total")]
  public required decimal Total { get; set; }
  [XmlAttribute("SubTotal")]
  public required decimal SubTotal { get; set; }

  [XmlElement("Emisor")]
  public required Contribuyente Emisor { get; set; }
  [XmlElement("Receptor")]
  public required Contribuyente Receptor { get; set; }
  [XmlArray("Conceptos")]
  [XmlArrayItem("Concepto")]
  public required List<Concepto> Conceptos { get; set; }
  [XmlElement("Impuestos")]
  public Impuestos? Impuestos { get; set; }
}
