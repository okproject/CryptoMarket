using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Application.UseCases.Queries
{
    public class GetProductsQueryHandler:IQueryHandler<GetProductsQuery,List<ProductViewModel>>
    {
        private readonly IProductGateway _productGateway;

        public GetProductsQueryHandler(IProductGateway productGateway)
        {
            _productGateway = productGateway;
        }

        public async Task<List<ProductViewModel>> Handle(GetProductsQuery query, CancellationToken token)
        {
            var productList = await _productGateway.GetProducts(new CancellationToken(), query.Start, query.Size);

            var productViewModelList = productList?.data?.Select(x => new ProductViewModel()
            {
                Id = x.id,
                Name = x.name,
                Price = x.quote.Usd.price,
                PriceUnit = PriceUnits.USD.ToString()
            }).ToList();
            return productViewModelList;
        }
    }
}