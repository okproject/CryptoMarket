using System.Threading.Tasks;

namespace CryptoMarket.Api.Core
{
    public interface IRepository<T> where T : IEntity
    {
        Task Save(T entity);
        
    }
}