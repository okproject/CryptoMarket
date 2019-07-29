using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CryptoMarket.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.Net.Http;
using Xunit;

namespace CryptoMarket.IntegrationTests
{
    public class GetProductsIntegrationTest
    {

        [Fact]
        public async Task TestHost()
        {
            var host = new WebHostBuilder()
                .UseContentRoot(@"../../../../../src/CryptoMarket.Api")
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            var server = new TestServer(host);

            var client = server.CreateClient();

            var response = await client.GetAsync("/api/Market/products");
            response.EnsureSuccessStatusCode();
            var resultStr = await response.Content.ReadAsStringAsync();

            Assert.Contains("priceunit", resultStr.ToLower());
        }
    }
}