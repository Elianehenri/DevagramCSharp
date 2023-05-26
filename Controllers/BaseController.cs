using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {

    }
}
