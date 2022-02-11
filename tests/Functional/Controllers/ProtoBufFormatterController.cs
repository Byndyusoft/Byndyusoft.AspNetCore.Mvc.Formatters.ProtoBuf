using Byndyusoft.AspNetCore.Mvc.Formatters.Models;
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
        public SimpleModel Echo([FromBody] SimpleModel model)
        {
            return model;
        }
    }
}