using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Core;
using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Application.UseCases.Commands
{
    public class PurchaseProductCommandHandler : ICommandHandler<PurchaseProductCommand>
    {
        private readonly IProductGateway _productGateway;
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseProductCommandHandler(IProductGateway productGateway, IPurchaseRepository purchaseRepository)
        {
            _productGateway = productGateway;
            _purchaseRepository = purchaseRepository;
        }

        public async Task Handle(PurchaseProductCommand command, CancellationToken token)
        {
            /*
             * Get product's latest price
             * Set product up do date price
             * Create purchase
             * Save purchase
             */
            var purchaseModel = await CreateAsync(command);
                
            await _purchaseRepository.Save(purchaseModel);
        }

        public async Task<Purchase> CreateAsync(PurchaseProductCommand command)
        {
            var productPrice = await _productGateway.GetProductPriceById(new CancellationToken(), command.ProductId);
            var purchaseModel = new Purchase
            {
                Amount = command.Amount,
                Price = productPrice,
                CustomerId = 1,
                PriceUnit = PriceUnits.USD.ToString(),
                ProductId = command.ProductId,
                CreateDateTime = DateTime.UtcNow,
                TotalPrice = CalculateTotal(command.Amount,productPrice)
            };
            return purchaseModel;
        }

        public decimal CalculateTotal(double amount, decimal price)
        {
            var amountStr = amount.ToString();
            var decimalValue = decimal.Parse(amountStr);
            var result = decimal.Multiply(price, decimalValue);
            return result;
        }
    }
}