using EaTech.Core.DTOs;
using EaTech.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EaTech.WebApi.Controllers
{
    [ApiController]
    public class NetworkController: Controller
    {
        private INetworkService _networkService;
        
        public NetworkController(INetworkService networkService)
        {
            _networkService = networkService;
        }

        [HttpPut("downstreamCustomers")]
        public IActionResult DownstreamCustomers(DownstreamCustomerRequestDTO request)
        {
            var response = _networkService.GetDownstreamCustomers(request);
            return Ok(response);    
        }
    }
}
