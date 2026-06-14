using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Storage;

namespace Business.Fakes.Fabrika
{
    public sealed class FabrikaInMemory : ProjectDbContext
    {
        private static readonly InMemoryDatabaseRoot SharedDatabaseRoot = new();

        public FabrikaInMemory(DbContextOptions<FabrikaInMemory> options, IConfiguration configuration)
            : base(options, configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(
                    optionsBuilder.UseInMemoryDatabase(
                        Configuration.GetConnectionString("FabrikaInMemory"),
                        SharedDatabaseRoot));
            }
        }
    }
}