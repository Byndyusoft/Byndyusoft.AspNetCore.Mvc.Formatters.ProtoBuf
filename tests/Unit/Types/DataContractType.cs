using System.Runtime.Serialization;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Unit.Types
{
    [DataContract]
    public class DataContractType
    {
        [DataMember] public int Property { get; set; }
    }
}