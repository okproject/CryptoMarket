using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;
using Marvin.StreamExtensions;

namespace CryptoMarket.Api.Infrastructure.Service
{
    public class ProductGateway : IProductGateway
    {
        private readonly HttpClient _httpClient;

        public ProductGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ProductCoinMarketCapViewModel> GetProductsQuery(CancellationToken cancellationToken, int? start,
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
    }
}