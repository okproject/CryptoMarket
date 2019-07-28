using System;
using System.Collections.Generic;
using System.Text;
using CryptoMarket.Api.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CryptoMarket.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        
        }
    }
}
