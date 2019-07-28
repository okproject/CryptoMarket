using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.UseCases.Queries;
using CryptoMarket.Api.Controllers;
using CryptoMarket.Api.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CryptoMarket.Api.UnitTests
{
    public class MarketControllerTests
    {
        private MarketController _ctrl { get; set; }

        public MarketControllerTests()
        {
            var prdGatewayMock = new Mock<IProductGateway>();
            var purchaseRepoMock = new Mock<IPurchaseRepository>();
//   
            _ctrl = new MarketController(prdGatewayMock.Object, purchaseRepoMock.Object);
        }

        [Fact]
        public async Task Products_Ctrl_Should_Return_Product_Product_ViewModel()
        {
            var query = new Mock<GetProductsQuery>();
            var result = await _ctrl.Products(query.Object);
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}