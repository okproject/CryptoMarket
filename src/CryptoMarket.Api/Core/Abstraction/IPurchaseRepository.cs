using System.Threading.Tasks;
using CryptoMarket.Api.Core.Model;

namespace CryptoMarket.Api.Core
{
    public interface IPurchaseRepository:IRepository<Purchase>
    {
        new Task Add(Purchase entity);
        new Task Remove(Purchase entity);

    }
}