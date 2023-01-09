using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class SiteBaseController : ControllerBase
    {
    }
}
