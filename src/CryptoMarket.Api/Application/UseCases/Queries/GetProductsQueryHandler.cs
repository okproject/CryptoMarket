using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;

namespace CryptoMarket.Api.Application.UseCases.Queries
{
    public class GetProductsQueryHandler:IQueryHandler<GetProductsQuery,ProductViewModel>
    {
        private IProductGateway _productGateway;

        public GetProductsQueryHandler(IProductGateway productGateway)
        {
            _productGateway = productGateway;
        }

        public async Task<ProductViewModel> Handle(GetProductsQuery query, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}