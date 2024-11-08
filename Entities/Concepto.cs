using System.Xml.Serialization;

namespace Contpaqi.Entities;

public class Concepto
{
  [XmlAttribute("NoIdentificacion")]
  public required string NoIdentificacion { get; set; }
  [XmlAttribute("ClaveProdServ")]
  public required uint ClaveProdServ { get; set; }
  [XmlAttribute("Descripcion")]
  public required string Descripcion { get; set; }
  [XmlAttribute("ClaveUnidad")]
  public required string ClaveUnidad { get; set; }
  [XmlAttribute("ValorUnitario")]
  public required decimal ValorUnitario { get; set; }
  [XmlAttribute("Cantidad")]
  public required byte Cantidad { get; set; }
  [XmlAttribute("Importe")]
  public required decimal Importe { get; set; }
  [XmlAttribute("ObjetoImp")]
  public string? ObjetoImp { get; set; }
}
