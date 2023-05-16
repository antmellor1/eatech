using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net.Http.Json;
using EaTech.Core.DTOs;
using EaTech.Core.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;

namespace EaTech.WebApi.Tests;

public class NetworkControllerTests
{
    private HttpClient client;

    public NetworkControllerTests()
    {
        var testWebApp = new CustomWebApplicationFactory();
        client = testWebApp.CreateClient();
    }

    [Fact]
    public async Task DownstreamCustomers_WhenModelIsIncomplete_ReturnsBadRequestResponse()
    {
        // missing Customers & SelectedNode properties
        var request = new DownstreamCustomerRequestDTO
        {
            Network = new Network
            {
                Branches = new List<Branch>
                {
                    new Branch { StartNode = 10, EndNode = 20 }
                }
            }
        };

        var response = await client.PutAsJsonAsync<DownstreamCustomerRequestDTO>("downstreamCustomers", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DownstreamCustomers_WhenModelIsComplete_ReturnsOkResponse()
    {
        var request = new DownstreamCustomerRequestDTO
        {
            Network = new Network
            {
                Branches = new List<Branch>
                {
                    new Branch { StartNode = 10, EndNode = 20 },
                    new Branch { StartNode = 20, EndNode = 30 }
                },
                Customers = new List<NodeCustomers>
                {
                    new NodeCustomers { Node = 10, NumberOfCustomers = 5 },
                    new NodeCustomers { Node = 20, NumberOfCustomers = 5 }
                }
            },
            SelectedNode = 10
        };

        var response = await client.PutAsJsonAsync<DownstreamCustomerRequestDTO>("downstreamCustomers", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }
}
