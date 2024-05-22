using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.EntityConfigurations;

public class MpesaPaymentEntityTypeConfiguration:IEntityTypeConfiguration<MpesaPayment>
{
    public void Configure(EntityTypeBuilder<MpesaPayment> builder)
    {
        builder.Property(x => x.Status).HasConversion<string>();
    }
}