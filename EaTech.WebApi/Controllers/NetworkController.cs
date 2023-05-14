using Microsoft.AspNetCore.Mvc;

namespace EaTech.WebApi.Controllers
{
    [ApiController]
    public class NetworkController: Controller
    {
        [HttpGet("downstreamCustomers")]
        public async Task<IActionResult> GetDownstreamCustomers()
        {

        }
    }
}
