using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoMarket.Api.Core.Model
{
    public class Purchase:IEntity
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string PriceUnit { get; set; }
        public double Amount { get; set; }
        
        public DateTime CreateDateTime { get; set; }

        public decimal TotalPrice { get; set; }
    }
}