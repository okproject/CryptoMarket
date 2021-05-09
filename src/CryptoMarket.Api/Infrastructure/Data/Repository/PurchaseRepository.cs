using System.Linq;
using System.Threading.Tasks;
using CryptoMarket.Api.Core;
using CryptoMarket.Api.Core.Model;
using CryptoMarket.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CryptoMarket.Api.Data.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Save(Purchase entity)
        {
            await _dbContext.Purchases.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}