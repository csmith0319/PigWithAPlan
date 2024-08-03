using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PigWithAPlan.Server.Models;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
       public void Configure(EntityTypeBuilder<Budget> builder)
       {
              builder.ToTable("Budgets");

              builder.HasKey(b => b.Id);

              builder.Property(b => b.Name)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(b => b.Color)
                     .IsRequired()
                     .HasMaxLength(50);

              builder.HasOne(b => b.User)
                     .WithMany()
                     .HasForeignKey(b => b.UserId);

              builder.Property(b => b.CreatedAt)
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

              builder.Property(b => b.UpdatedAt)
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");
       }
}
