using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class TukioEntityTypeConfiguration:IEntityTypeConfiguration<Tukio>
{
    public void Configure(EntityTypeBuilder<Tukio> builder)
    {
        builder.Property(x => x.LocationType).HasConversion<string>();
    }
}