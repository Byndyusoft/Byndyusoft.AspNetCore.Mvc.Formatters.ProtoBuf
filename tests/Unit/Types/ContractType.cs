using ProtoBuf;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Unit.Types
{
    [ProtoContract]
    public class ContractType
    {
        [ProtoMember(1)] public int Property { get; set; }
    }
}