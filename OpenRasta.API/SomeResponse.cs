using System.Runtime.Serialization;

namespace OpenRasta.API
{
    [DataContract]
    public class SomeResponse
    {
        [DataMember]
        public string Value { get; set; }
    }
}