using System.Runtime.Serialization;

namespace OpenRastaAPIProject
{
    [DataContract]
    public class SomeRequest
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Value { get; set; } 
    }
}