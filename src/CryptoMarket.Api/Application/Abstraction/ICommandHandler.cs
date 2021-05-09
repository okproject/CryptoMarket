using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarket.Api.Application.Abstraction
{
    public interface ICommandHandler<in TCommand>
    {
        Task Handle(TCommand command,CancellationToken token);
    }
}