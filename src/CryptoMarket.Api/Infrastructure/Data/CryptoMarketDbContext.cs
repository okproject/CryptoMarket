using CryptoMarket.Api.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CryptoMarket.Api.Infrastructure
{
    public class CryptoMarketDbContext:DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
        
        
    }
}