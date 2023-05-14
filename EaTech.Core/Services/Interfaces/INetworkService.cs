using EaTech.Core.DTOs;

namespace EaTech.Core.Services.Interfaces
{
    public interface INetworkService
    {
        DownstreamCustomerResponseDTO GetDownstreamCustomers(DownstreamCustomerRequestDTO request);
    }
}
