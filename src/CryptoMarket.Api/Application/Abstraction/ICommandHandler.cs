using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarket.Api.Application.Abstraction
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command,CancellationToken token);
    }
}