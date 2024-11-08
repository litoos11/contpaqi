using System.Xml;
using System.Xml.Schema;

namespace Contpaqi.Services;

public class XmlServices : IXmlServices
{
  public void ValidateXml(string xml, string pathXsd)
  {
    XmlSchemaSet schema = new();
    schema.Add("", pathXsd);

    XmlReaderSettings settings = new();
    settings.Schemas.Add(schema);
    settings.ValidationType = ValidationType.Schema;
    settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler!);
    using StringReader stringReader = new(xml);
    using XmlReader xmlReader = XmlReader.Create(stringReader, settings);
    while (xmlReader.Read()) { }
  }

  private void ValidationEventHandler(object sender, ValidationEventArgs e)
  {
    switch (e.Severity)
    {
      case XmlSeverityType.Error:
        throw new Exception(e.Message);
      case XmlSeverityType.Warning:
        throw new Exception(e.Message);
    }
  }
}
