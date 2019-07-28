using System;

namespace CryptoMarket.Api.Core
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}