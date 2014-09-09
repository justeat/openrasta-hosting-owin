using System.Runtime.Serialization;

namespace OpenRastaAPIProject
{
    [DataContract]
    public class SomeResponse
    {
        [DataMember]
        public string Value { get; set; }
    }
}