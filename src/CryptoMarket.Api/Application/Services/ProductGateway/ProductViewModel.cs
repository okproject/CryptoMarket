using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Application.Services.ProductGateway
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PriceUnits PriceUnit { get; set; }
        
    }
}