using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PigWithAPlan.Server.Models;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Payee)
               .WithMany()
               .HasForeignKey(t => t.PayeeId);

        builder.HasOne(t => t.Budget)
               .WithMany()
               .HasForeignKey(t => t.BudgetId);

        builder.Property(t => t.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(t => t.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
