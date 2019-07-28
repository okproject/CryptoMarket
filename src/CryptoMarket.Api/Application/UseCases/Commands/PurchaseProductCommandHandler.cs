using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Core;

namespace CryptoMarket.Api.Application.UseCases.Commands
{
    public class PurchaseProductCommandHandler:ICommandHandler<PurchaseProductCommand>
    {
        private IProductGateway _productGateway;
        private IPurchaseRepository _purchaseRepository;

        public PurchaseProductCommandHandler(IProductGateway productGateway, IPurchaseRepository purchaseRepository)
        {
            _productGateway = productGateway;
            _purchaseRepository = purchaseRepository;
        }

        public Task Handle(PurchaseProductCommand command, CancellationToken token)
        {
            /*
             * Get product's latest price
             * Set product up do date price
             * Create purchase
             * Save purchase
             */
            
            throw new System.NotImplementedException();
        }
    }
}