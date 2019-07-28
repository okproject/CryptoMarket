using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.UseCases.Commands;
using CryptoMarket.Api.Application.UseCases.Queries;
using CryptoMarket.Api.Core;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.Api.Controllers
{
    [ApiController] 
    [Route("api/[Controller]")]
    public class MarketController:ControllerBase
    {
        private readonly IProductGateway _productGateway;
        private readonly IPurchaseRepository _purchaseRepository;

        public MarketController(IProductGateway productGateway, IPurchaseRepository purchaseRepository)
        {
            _productGateway = productGateway;
            _purchaseRepository = purchaseRepository;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> Products([FromQuery]GetProductsQuery query)
        {
            var productQueryHandler = new GetProductsQueryHandler(_productGateway);
            var result = await productQueryHandler.Handle(query, new CancellationToken());
            return Ok(result);
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseProduct([FromBody] PurchaseProductCommand command)
        {
            var handler=new PurchaseProductCommandHandler(_productGateway,_purchaseRepository);
            await handler.Handle(command,new CancellationToken());
            return StatusCode(201);
        }
    }
}