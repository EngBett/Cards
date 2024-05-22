using Agc.GoodShepherd.Domain.Enums;
using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class SermonMediaEntityTypeConfiguration:IEntityTypeConfiguration<SermonMedia>
{
    public void Configure(EntityTypeBuilder<SermonMedia> builder)
    {
        builder.Property(x => x.MediaType).HasConversion<string>();
    }
}