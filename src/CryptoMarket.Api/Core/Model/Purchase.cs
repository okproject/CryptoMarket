using System;

namespace CryptoMarket.Api.Core.Model
{
    public class Purchase:IEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string PriceUnit { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}