using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PigWithAPlan.Server.Models;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasOne(c => c.CategoryGroup)
                .WithMany()
                .HasForeignKey(c => c.CategoryGroupId);

        builder.Property(c => c.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
