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
        return BadRequest(e.Message);
      }

      string result = Encoding.UTF8.GetString(bytes);
      string pathXsd = Environment.CurrentDirectory + "/Resources/comprobante.xsd";
      if (!System.IO.File.Exists(pathXsd))
      {
        return BadRequest("No se encontro el archivo xsd");
      }
      _xmlServices.ValidateXml(result, pathXsd);

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
