using Microsoft.AspNetCore.Mvc;

namespace EaTech.WebApi.Controllers
{
    [ApiController]
    public class GraphController: Controller
    {
        [HttpGet("downstreamCustomers")]
        public async Task<IActionResult> GetDownstreamCustomers()
        {

        }
    }
}
