using System.Net;
using System.Text;
using System.Xml.Serialization;
using Contpaqi.Entities;
using Contpaqi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contpaqi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class XmlController : ControllerBase
{
  private readonly ILogger<XmlController> _logger;
  private readonly IXmlServices _xmlServices;

  public XmlController(ILogger<XmlController> logger, IXmlServices xmlServices)
  {
    _logger = logger;
    _xmlServices = xmlServices;
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
        _logger.LogError(e.Message);
        ErrorResponse errorResponse = new()
        {
          Status = HttpStatusCode.BadRequest.GetHashCode(),
          Title = "Error al decodificar el xml",
          Detail = "El base64 proporcionado no es válido para su decodificación.",
        };
        return BadRequest(errorResponse);
      }

      string result = Encoding.UTF8.GetString(bytes);
      string pathXsd = Environment.CurrentDirectory + "/Resources/comprobante.xsd";
      if (!System.IO.File.Exists(pathXsd))
      {
        ErrorResponse errorResponse = new()
        {
          Status = HttpStatusCode.BadRequest.GetHashCode(),
          Title = "Error al validar el xml",
          Detail = "No se encontro el archivo xsd necesario para validar el Xml proporcionado.",
        };
        return BadRequest(errorResponse);
      }
      _xmlServices.ValidateXml(result, pathXsd);

      XmlSerializer serializer = new(typeof(Comprobante));

      using StringReader reader = new(result);
      Comprobante comprobante = (Comprobante)serializer.Deserialize(reader)!;

      if (comprobante == null)
      {
        ErrorResponse errorResponse = new()
        {
          Status = HttpStatusCode.BadRequest.GetHashCode(),
          Title = "Error al deserializar el xml",
          Detail = "No se pudo deserializar el xml proporcionado.",
        };
      }

      return Ok(new { Comprobante = comprobante });
    }
    catch (Exception e)
    {
      _logger.LogError(e.Message);
      ErrorResponse errorResponse = new()
      {
        Status = HttpStatusCode.BadRequest.GetHashCode(),
        Title = "Error al procesar el xml.",
        Detail = e.Message,
        Errors = [e.Message]
      };
      return BadRequest(errorResponse);
    }

  }
}
