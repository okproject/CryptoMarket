using System.Threading.Tasks;

namespace CryptoMarket.Api.Core
{
    public interface IRepository<in T> where T : IEntity
    {
        Task Save(T entity);
        
    }
}