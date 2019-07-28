using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;

namespace CryptoMarket.Api.Infrastructure.Service
{
    public class ProductGateway:IProductGateway
    {
        public  async Task<List<ProductViewModel>> GetProductsQuery(int? start, int? size)
        {
            throw new System.NotImplementedException();
        }
    }
}