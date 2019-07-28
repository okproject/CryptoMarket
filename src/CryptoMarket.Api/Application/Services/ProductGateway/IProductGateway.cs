using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarket.Api.Application.Services.ProductGateway
{
    public interface IProductGateway
    {
        Task<ProductCoinMarketCapViewModel> GetProductsQuery(CancellationToken cancellationToken,int? start=1, int? limit=20);

        Task<decimal> GetProductPriceById(CancellationToken cancellationToken,int id);
    }
}