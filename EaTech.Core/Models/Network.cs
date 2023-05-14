namespace EaTech.Core.Models
{
    public class Network
    {
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<NodeCustomers> Customers { get; set; }
    }
}
