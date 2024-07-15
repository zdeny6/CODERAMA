using MessagePack;

using System.Xml.Serialization;

/// <summary>
/// Class added to solve problem with serialization of Dictionary<string, object> in XML
/// </summary>
[MessagePackObject]
[Serializable]
public class SerializableDictionary : IXmlSerializable
{
    [Key(0)]
    private Dictionary<string, object> _dictionary = new();

    public SerializableDictionary() { }

    public SerializableDictionary(Dictionary<string, object> dictionary)
    {
        _dictionary = dictionary;
    }

    public Dictionary<string, object> ToDictionary() => _dictionary;

    public System.Xml.Schema.XmlSchema GetSchema() => null;

    public void ReadXml(System.Xml.XmlReader reader)
    {
        reader.ReadStartElement();
        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
        {
            string key = reader.Name;
            string value = reader.ReadInnerXml();
            _dictionary[key] = value; // Handle other types as necessary
            reader.MoveToContent();
        }
        reader.ReadEndElement();
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
        if(_dictionary != null)
        {
            foreach (var kvp in _dictionary)
            {
                writer.WriteStartElement(kvp.Key);
                writer.WriteValue(kvp.Value?.ToString());
                writer.WriteEndElement();
            }
        }
    }
}