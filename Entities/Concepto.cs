namespace Contpaqi.Entities;

public class Concepto
{
  public required string NoIdentificacion { get; set; }
  public required uint ClaveProdServ { get; set; }
  public required string Descripcion { get; set; }
  public required string ClaveUnidad { get; set; }
  public required decimal ValorUnitario { get; set; }
  public required byte Cantidad { get; set; }
  public required decimal Importe { get; set; }
  public string? ObjetoImp { get; set; }
}
