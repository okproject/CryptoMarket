using System.Threading.Tasks;
using CryptoMarket.Api.Core;
using CryptoMarket.Api.Core.Model;
using CryptoMarket.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CryptoMarket.Api.Data.Repository
{
    public class PurchaseRepository:IPurchaseRepository
    {
        private CryptoMarketDbContext _dbContext;

        public PurchaseRepository(CryptoMarketDbContext dbContext)
        {
            
            _dbContext = dbContext;
            
        }

        public async Task Remove(Purchase entity)
        {
            
            throw new System.NotImplementedException();
        }

        public async Task Add(Purchase entity)
        {
            throw new System.NotImplementedException();
        }
    }
}