using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ExampleController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var testList = new List<string>();
            testList.Add("apple");
            testList.Add("orange");
            return Ok(await Task.FromResult(testList.ToList()));
        }
    }
}