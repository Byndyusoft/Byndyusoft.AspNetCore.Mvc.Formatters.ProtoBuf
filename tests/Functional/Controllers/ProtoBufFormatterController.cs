using Microsoft.AspNetCore.Mvc;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Functional.Controllers
{
    [Controller]
    [Route("protobuf-formatter")]
    public class ProtoBufFormatterController : ControllerBase
    {
        [HttpPost]
        [Route("echo")]
        [FormatFilter]
        public object Echo([FromBody] object model)
        {
            return model;
        }
    }
}