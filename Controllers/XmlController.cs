using System.Text;
using System.Xml.Serialization;
using Contpaqi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Contpaqi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class XmlController : ControllerBase
{
  private readonly ILogger<XmlController> _logger;

  public XmlController(ILogger<XmlController> logger)
  {
    _logger = logger;
  }

  [HttpPost]
  public IActionResult Post([FromBody] XmlRequest xml)
  {
    try
    {
      byte[] bytes = [];
      try
      {
        bytes = Convert.FromBase64String(xml.Xml);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
      //TODO: Validar contra el xsd

      string result = Encoding.UTF8.GetString(bytes);
      XmlSerializer serializer = new(typeof(Comprobante));

      using StringReader reader = new(result);
      Comprobante comprobante = (Comprobante)serializer.Deserialize(reader)!;

      if (comprobante == null)
      {
        return BadRequest("No se pudo deserializar el xml");
      }

      return Ok(new { Comprobante = comprobante });
    }
    catch (Exception e)
    {
      //TODO: Armar mensaje de error 
      _logger.LogError(e.Message);
      return BadRequest(e.Message);
    }

  }
}
