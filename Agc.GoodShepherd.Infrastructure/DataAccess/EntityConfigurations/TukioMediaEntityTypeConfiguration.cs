using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class TukioMediaEntityTypeConfiguration:IEntityTypeConfiguration<TukioMedia>
{
    public void Configure(EntityTypeBuilder<TukioMedia> builder)
    {
        builder.Property(x => x.MediaType).HasConversion<string>();
    }
}