using Cards.Application.Interfaces;
using Cards.Domain.Models;
using Cards.Infrastructure.DataAccess.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardsEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}