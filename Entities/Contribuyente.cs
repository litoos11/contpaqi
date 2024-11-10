using System.Xml.Serialization;

namespace Contpaqi.Entities;

public class Contribuyente
{
  [XmlAttribute("Nombre")]
  public required string Nombre { get; set; }
  [XmlAttribute("Rfc")]
  public required string Rfc { get; set; }
}
