using EaTech.Core.DTOs;
using EaTech.Core.Models;
using EaTech.Core.Services.Interfaces;
namespace EaTech.Core.Services
{
    public class NetworkService : INetworkService
    {
        public DownstreamCustomerResponseDTO GetDownstreamCustomers(DownstreamCustomerRequestDTO request)
        {
            var downstreamNodes = GetDownstreamNodes(request.SelectedNode, request.Network.Branches);

            var customerNodes = request.Network.Customers
                .Where(c => downstreamNodes.Contains(c.Node))
                .ToList();

            var totalCustomers = customerNodes.Sum(n => n.NumberOfCustomers);

            return new DownstreamCustomerResponseDTO
            {
                TotalCustomers = totalCustomers,
                Nodes = customerNodes
            };
        }

        private HashSet<int> GetDownstreamNodes(int startNode, IEnumerable<Branch> branches) 
        {
            var visitedNodes = new HashSet<int>();

            var stack = new Stack<int>();
            stack.Push(startNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                visitedNodes.Add(node);

                var children = branches
                    .Where(b => b.StartNode == node)
                    .Select(b => b.EndNode)
                    .ToList();

                foreach (var child in children)
                {
                    stack.Push(child);
                }
            }

            return visitedNodes;
        }
    
    }
}
