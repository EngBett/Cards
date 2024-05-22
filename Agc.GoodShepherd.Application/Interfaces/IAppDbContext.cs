using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Application.Interfaces;

public interface IAppDbContext
{
    
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
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}