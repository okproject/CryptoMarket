using System.ComponentModel.DataAnnotations;
using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Application.UseCases.Commands
{
    public class PurchaseProductCommand
    {
        public int ProductId { get; set; }
        public double Amount { get; set; }
        
        
    }
}