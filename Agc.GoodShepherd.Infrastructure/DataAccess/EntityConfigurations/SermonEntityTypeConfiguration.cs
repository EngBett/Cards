using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class SermonEntityTypeConfiguration:IEntityTypeConfiguration<Sermon>
{
    public void Configure(EntityTypeBuilder<Sermon> builder)
    {
        builder.Property(x => x.BibleVersion).HasConversion<string>();
    }
}