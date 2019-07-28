using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Core;
using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Application.UseCases.Commands
{
    public class PurchaseProductCommandHandler : ICommandHandler<PurchaseProductCommand>
    {
        private IProductGateway _productGateway;
        private IPurchaseRepository _purchaseRepository;

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
            var productPrice = await _productGateway.GetProductPriceById(new CancellationToken(), command.ProductId);

            var purchaseModel = new Purchase
            {
                Amount = command.Amount,
                Price = productPrice,
                CustomerId = 1,
                PriceUnit = PriceUnits.USD.ToString(),
                ProductId = command.ProductId,
                CreateDateTime = DateTime.UtcNow
            };
            await _purchaseRepository.Save(purchaseModel);
            
        }
    }
}