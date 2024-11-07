using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Contpaqi.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
  public async Task<IActionResult> Post([FromBody] XmlRequest xml)
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

      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(result);
      string jsonText = JsonConvert.SerializeXmlNode(xmlDocument);

      Comprobante? comprobante = JsonConvert.DeserializeObject<Comprobante>(jsonText);

      return Ok(comprobante);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

  }
}
