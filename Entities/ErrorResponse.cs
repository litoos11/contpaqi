namespace Contpaqi.Entities;

public class ErrorResponse
{
  public required int Status { get; set; }
  public required string Title { get; set; }
  public string? Detail { get; set; }
  public List<string>? Errors { get; set; } = [];
}
