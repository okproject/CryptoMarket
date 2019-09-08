using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CryptoMarket.Api;
using CryptoMarket.Api.Application.UseCases.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.Net.Http;
using Newtonsoft.Json;
using Xunit;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace CryptoMarket.IntegrationTests
{
    [Category("")]
    public class UseCaseIntegarationTests
    {
        HttpClient client;

        public UseCaseIntegarationTests()
        {
            var host = new WebHostBuilder()
                .UseContentRoot(@"../../../../../src/CryptoMarket.Api")
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            var server = new TestServer(host);

            client = server.CreateClient();
        }

        [Fact,Category("CustomCategory")]
        public async Task TestGetproductsUseCase()
        {
            var response = await client.GetAsync("/api/Market/products");
            response.EnsureSuccessStatusCode();
            var resultStr = await response.Content.ReadAsStringAsync();

            Assert.Contains("priceunit", resultStr.ToLower());
        }

        [Fact]
        public async Task TestPurchaseUseCase()
        {
            var command = new PurchaseProductCommand
            {
                Amount = 1,
                ProductId = 1
            };
            var content = JsonConvert.SerializeObject(command);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Market/purchase", httpContent);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created,response.StatusCode);

        }
    }
}