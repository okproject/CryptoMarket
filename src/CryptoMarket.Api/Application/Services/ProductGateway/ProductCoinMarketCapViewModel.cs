using System;
using System.Collections.Generic;


namespace CryptoMarket.Api.Application.Services.ProductGateway
{
    public class ProductCoinMarketCapViewModel
    {
        public Status Status { get; set; }
        public List<Data> data { get; set; }

        public ProductCoinMarketCapViewModel()
        {
            data = new List<Data>();
        }
    }

    public class Status
    {
        public DateTime timestamp { get; set; }

        public int error_code { get; set; }

        public string error_message { get; set; }

        public int elapsed { get; set; }

        public int credit_cound { get; set; }
        
    }

    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public Quote quote { get; set; }
        
    }

    public class Quote
    {
        public Usd Usd { get; set; }
    }

    public class Usd
    {
        public decimal price { get; set; }
    }
}