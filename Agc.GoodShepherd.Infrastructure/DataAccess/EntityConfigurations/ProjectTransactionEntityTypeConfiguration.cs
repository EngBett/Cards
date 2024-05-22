using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class ProjectTransactionEntityTypeConfiguration:IEntityTypeConfiguration<ProjectTransaction>
{
    public void Configure(EntityTypeBuilder<ProjectTransaction> builder)
    {
        builder.Property(x => x.Status).HasConversion<string>();
    }
}