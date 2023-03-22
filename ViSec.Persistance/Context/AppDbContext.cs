using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ViSec.Domain.Entities;
using FourPoints.FWK.Context;

namespace ViSec.Persistance.Context
{
    internal class AppDbContext<Key> : BaseContext<Key>
    {
        public AppDbContext(IHttpContextAccessor httpContext) : base(httpContext)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("test");
            }
        }
        public DbSet<Persons> Persons { get; set; }
    }
}
