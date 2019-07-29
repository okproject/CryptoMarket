using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;
using Marvin.StreamExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoMarket.Api.Infrastructure.Service
{
    public class ProductGateway : IProductGateway
    {
        private readonly HttpClient _httpClient;

        public ProductGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ProductCoinMarketCapViewModel> GetProducts(CancellationToken cancellationToken,
            int? start,
            int? limit)
        {
            if (start == null || start <= 0) start = 1;
            if (limit == null || limit < 2 || limit <= start) limit = start + 10;
            var requestUri = $"v1/cryptocurrency/listings/latest?start={start}&limit={limit}&convert=USD";
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                requestUri);
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            using (var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var result = stream.ReadAndDeserializeFromJson<ProductCoinMarketCapViewModel>();
                return result;
            }
        }

        public async Task<decimal> GetProductPriceById(CancellationToken cancellationToken, int id)
        {
            if (id < 0) return 0;
            var requestUri = $"v1/cryptocurrency/quotes/latest?convert=USD&id={id}";
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                requestUri);
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            using (var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var resStr = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(resStr);
                var priceJObject = jsonResponse["data"]?[$"{id.ToString()}"]?["quote"]?["USD"]?["price"];
                if(priceJObject==null) throw new Exception("Could not get price information");
                var priceStr = priceJObject.ToString();
                if(string.IsNullOrEmpty(priceStr)) throw new Exception("Could not get price information");
                response.EnsureSuccessStatusCode();
                var parseResult = decimal.TryParse(priceStr, out var decimalResult);
                if (!parseResult) throw new Exception("Could not parse price information");
                return decimalResult;
            }
        }
    }
}