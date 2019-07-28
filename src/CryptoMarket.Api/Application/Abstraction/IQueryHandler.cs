using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarket.Api.Application.Abstraction
{
    public interface IQueryHandler<TQuery,TView>
    {
        Task<TView> Handle(TQuery query, CancellationToken token);
    }
}