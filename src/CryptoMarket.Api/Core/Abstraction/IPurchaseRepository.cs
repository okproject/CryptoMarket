using System.Threading.Tasks;
using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Core
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task Add(Purchase entity);
        Task Remove(Purchase entity);
    }
}