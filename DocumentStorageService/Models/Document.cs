using System.Xml.Serialization;
using MessagePack;

namespace DocumentStorageService.Models
{
    [MessagePackObject]
    public class Document
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public List<string> Tags { get; set; }

        [Key(2)]
        [XmlIgnore]
        public Dictionary<string, object> Data { get; set; }
        
        [IgnoreMember]
        [XmlElement("Data")]
        public SerializableDictionary SerializableData
        {
            get => new SerializableDictionary(Data);
            set => Data = value.ToDictionary();
        }
    }
}
