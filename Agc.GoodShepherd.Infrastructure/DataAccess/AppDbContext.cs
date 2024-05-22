using System.Reflection;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Domain.Models;
using Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Infrastructure.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Congregant> Congregants { get; set; }
        public DbSet<Sermon> Sermons { get; set; }
        public DbSet<SermonMedia> SermonMedia { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementMedia> AnnouncementMedia { get; set; }
        public DbSet<Tukio> Tukios { get; set; }
        public DbSet<TukioMedia> TukioMedia { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryMedia> CategoryMedia { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Merchandise> Merchandises { get; set; }
        public DbSet<MpesaPayment> MpesaPayments { get; set; }
        public DbSet<ProjectTransaction> ProjectTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new SermonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SermonMediaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TukioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TukioMediaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnnouncementMediaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MpesaPaymentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectTransactionEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}