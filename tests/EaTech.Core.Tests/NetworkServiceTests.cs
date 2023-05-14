using EaTech.Core.DTOs;
using EaTech.Core.Models;
using EaTech.Core.Services;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace EaTech.Core.Tests
{
    public class NetworkServiceTests
    {
        private NetworkService sut;
        private DownstreamCustomerRequestDTO input;

        public NetworkServiceTests()
        {
            sut = new NetworkService();
            input = new DownstreamCustomerRequestDTO
            {
                Network = new Network
                {
                    Branches = new List<Branch>()
                    {
                        new Branch { StartNode = 10, EndNode = 20 },
                        new Branch { StartNode = 20, EndNode = 30 },
                        new Branch { StartNode = 30, EndNode = 50 },
                        new Branch { StartNode = 50, EndNode = 60 },
                        new Branch { StartNode = 50, EndNode = 90 },
                        new Branch { StartNode = 60, EndNode = 40 },
                        new Branch { StartNode = 70, EndNode = 80 },
                    },
                    Customers = new List<NodeCustomers>
                    {
                        new NodeCustomers { Node = 30, NumberOfCustomers = 8 },
                        new NodeCustomers { Node = 40, NumberOfCustomers = 3 },
                        new NodeCustomers { Node = 60, NumberOfCustomers = 2 },
                        new NodeCustomers { Node = 70, NumberOfCustomers = 1 },
                        new NodeCustomers { Node = 80, NumberOfCustomers = 3 },
                        new NodeCustomers { Node = 90, NumberOfCustomers = 5 },
                    }
                },
                SelectedNode = 50
            };
        }

        [Fact]
        public void GetDownstreamCustomers_CalculatesTotalCustomersCorrectly()
        {
            var expectedResponse = new DownstreamCustomerResponseDTO
            {
                TotalCustomers = 10,
                Nodes = new List<NodeCustomers>
                {
                    new NodeCustomers { Node = 60, NumberOfCustomers = 2 },
                    new NodeCustomers { Node = 40, NumberOfCustomers = 3 },
                    new NodeCustomers { Node = 90, NumberOfCustomers = 5 },
                }
            };

            var response = sut.GetDownstreamCustomers(input);

            response.Should().BeEquivalentTo(expectedResponse);
            Assert.Equal(10, response.TotalCustomers);
        }

        [Fact]
        public void GetDownstreamCustomers_ShouldIncludeStartNode()
        {
            input.SelectedNode = 70;
            var response = sut.GetDownstreamCustomers(input);

            Assert.Equal(4, response.TotalCustomers);
        }

        [Fact]
        public void GetDownstreamCustomers_WhenNoCustomerNodesListed_NumberOfCustomersIsZero()
        {
            input.Network.Customers = new List<NodeCustomers>
            {
               new NodeCustomers { Node = 30, NumberOfCustomers = 8 },
            };

            var expectedResponse = new DownstreamCustomerResponseDTO
            {
                TotalCustomers = 0,
                Nodes = new List<NodeCustomers>()
            };

            var response = sut.GetDownstreamCustomers(input);

            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void GetDownstreamCustomers_WhenSelectedNodeDoesNotExist_NumberOfCustomersIsZero()
        {
            input.SelectedNode = 100;

            var expectedResponse = new DownstreamCustomerResponseDTO
            {
                TotalCustomers = 0,
                Nodes = new List<NodeCustomers>()
            };

            var response = sut.GetDownstreamCustomers(input);

            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}