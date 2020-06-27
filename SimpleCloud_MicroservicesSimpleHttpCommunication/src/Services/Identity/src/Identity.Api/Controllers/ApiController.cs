using Microsoft.AspNetCore.Mvc;

namespace SimpleCloudMonolithic.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
