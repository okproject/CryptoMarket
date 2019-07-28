using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Application.UseCases.Queries;

namespace CryptoMarket.Api.Application.Abstraction
{
    public interface IProductGateway
    {
        Task<List<ProductViewModel>> GetProductsQuery(int? start=1, int? size=20);
    }
}