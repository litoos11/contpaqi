namespace Contpaqi.Entities;

public class Comprobante
{
  public required string Version { get; set; }
  public required string LugarExpedicion { get; set; }
  public required string MetodoPago { get; set; }
  public required string TipoDeComprobante { get; set; }
  public required string FormaPago { get; set; }
  public required string Folio { get; set; }
  public required string Moneda { get; set; }
  public required string Serie { get; set; }
  public required string UUID { get; set; }
  public required DateTime Fecha { get; set; }
  public required decimal Total { get; set; }
  public required decimal SubTotal { get; set; }

  public required Contribuyente Emisor { get; set; }
  public required Contribuyente Receptor { get; set; }
  public required List<Concepto> Conceptos { get; set; }
  public required Impusto Impuestos { get; set; }
}
