using Microsoft.AspNetCore.Mvc;
using Section6.dattingapp.API.Helpers;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]

    public class BaseApiController : ControllerBase
    {
        
    }
}