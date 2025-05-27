using CurrencyConvertion.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConvertion.Context
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
        {
        }

        public DbSet<RealTimeExchangeRate> RealTimeExchangeRates { get; set; }
        public DbSet<RealTimeExchangeRateDetails> RealTimeExchangeRateDetails { get; set; }
        public DbSet<HistoricalExchangeRate> HistoricalExchangeRates { get; set; }
        public DbSet<HistoricalExchangeRateDetails> HistoricalExchangeRateDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<RealTimeExchangeRate>().ToTable("ExchnageRates").HasKey(x => x.Id);
            
            modelBuilder.Entity<RealTimeExchangeRateDetails>().ToTable("ExchnageRatesDetails");
            
            modelBuilder.Entity<HistoricalExchangeRate>().ToTable("HistoricalExchnageRates").HasKey(x => x.Id);
            
            modelBuilder.Entity<HistoricalExchangeRateDetails>().ToTable("HistoricalExchnageRateDetails");
        }
    }
}
