using System.Runtime.Serialization;

namespace OpenRastaAPIProject
{
    [DataContract]
    public class SomeResponse
    {
        [DataMember]
        public string value { get; set; }
    }
}