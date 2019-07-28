using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Application.UseCases.Queries;

namespace CryptoMarket.Api.Application.Abstraction
{
    public interface IProductGateway
    {
        Task<ProductCoinMarketCapViewModel> GetProductsQuery(CancellationToken cancellationToken,int? start=1, int? limit=20);

        Task<decimal> GetProductPriceById(CancellationToken cancellationToken,int id);
    }
}