using System.Runtime.Serialization;

namespace OpenRasta.API
{
    [DataContract]
    public class SomeRequest
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Description { get; set; } 
    }
}