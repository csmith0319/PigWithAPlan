using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PigWithAPlan.Server.Models;

public class PayeeConfiguration : IEntityTypeConfiguration<Payee>
{
    public void Configure(EntityTypeBuilder<Payee> builder)
    {
        builder.ToTable("Payees");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
