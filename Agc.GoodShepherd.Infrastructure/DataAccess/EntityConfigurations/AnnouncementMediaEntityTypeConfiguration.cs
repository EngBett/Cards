using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class AnnouncementMediaEntityTypeConfiguration:IEntityTypeConfiguration<AnnouncementMedia>
{
    public void Configure(EntityTypeBuilder<AnnouncementMedia> builder)
    {
        builder.Property(x => x.MediaType).HasConversion<string>();
    }
}