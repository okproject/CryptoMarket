using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Application.UseCases.Commands;
using CryptoMarket.Api.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoMarket.Api.UnitTests.Presentation.Application.UseCases
{
    public class PurchaseProductCommandHandlerTests
    {
        public PurchaseProductCommandHandlerTests()
        {
            
        }

        [Fact]
        public async Task Shoud_Calculate_Total_And_Call_Gateway()
        {
            var prdGtwy = new Mock<IProductGateway>();
            var purRepo = new Mock<IPurchaseRepository>();
            
            var price = 0.22m;
            var amount = 0.522; //0,11484
            var command = new PurchaseProductCommand()
            {
                Amount = amount,
                ProductId = 1
            };

            prdGtwy.Setup(x => x.GetProductPriceById(It.IsAny<CancellationToken>(), It.IsAny<int>()))
                .Returns(Task.FromResult(price));

            var handler = new PurchaseProductCommandHandler(prdGtwy.Object, purRepo.Object);

            await handler.Handle(command, new CancellationToken());
            prdGtwy.Verify(x=>x.GetProductPriceById(It.IsAny<CancellationToken>(),It.IsAny<int>()),Times.Once);

            var purchaseModelResult = await handler.CreateAsync(command);
            var calculationResult= handler.CalculateTotal(command.Amount, price);
            purchaseModelResult.TotalPrice.Should().Equals(calculationResult);
            prdGtwy.Verify(x=>x.GetProductPriceById(It.IsAny<CancellationToken>(),It.IsAny<int>()),Times.AtLeast(2));

        }
    }
}