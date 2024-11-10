using System.Xml.Serialization;

namespace Contpaqi.Entities;

public class Impuestos
{
  [XmlAttribute("TotalImpuestosRetenidos")]
  public decimal TotalImpuestosRetenidos { get; set; }
  [XmlAttribute("TotalImpuestosTrasladados")]
  public decimal TotalImpuestosTrasladados { get; set; }
}
