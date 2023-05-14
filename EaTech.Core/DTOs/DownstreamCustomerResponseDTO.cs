using EaTech.Core.Models;

namespace EaTech.Core.DTOs
{
    public class DownstreamCustomerResponseDTO
    {
        public int TotalCustomers { get; set; }
        public IEnumerable<NodeCustomers> Nodes { get; set; }
    }
}
