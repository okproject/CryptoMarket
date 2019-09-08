using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Application.UseCases.Queries;
using CryptoMarket.Api.Infrastructure.Service;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoMarket.Api.UnitTests.Presentation.Application.UseCases
{
    public class GetProductsQueryHandlerTests
    {
        private Mock<IProductGateway> _productGateway = new Mock<IProductGateway>();
        private int _start = 1;
        private int _limit = 10;
        private GetProductsQuery _query;

        public GetProductsQueryHandlerTests()
        {
            _query = new GetProductsQuery() 
            {
                Size = _limit,
                Start = _start
            };
        }

        [Fact]
        public async Task Should_Match_Base_ResponseType_And_Call_Gateway()
        {var marketCapViewModel = new ProductCoinMarketCapViewModel();
            
            _productGateway.Setup(x =>  x.GetProducts(It.IsAny<CancellationToken>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(marketCapViewModel));
            
            var handler = new GetProductsQueryHandler(_productGateway.Object);
            var sut=await handler.Handle(_query, new CancellationToken());

            sut.Should().BeOfType<List<ProductViewModel>>();
            sut.Should().BeEmpty();
            _productGateway.Verify(x=>x.GetProducts(It.IsAny<CancellationToken>(),It.IsAny<int>(),It.IsAny<int>()),Times.Once);
            
        }
    }
}