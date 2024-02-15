using Cards.Domain.Enums;
using Cards.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.EntityConfigurations;

public class CardsEntityTypeConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
        builder.Property(x => x.Status).HasConversion<string>().IsRequired().HasDefaultValue(CardStatuses.ToDo);
        builder.Property(x => x.Name).IsRequired();
    }
}