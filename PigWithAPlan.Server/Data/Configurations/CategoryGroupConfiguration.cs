using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PigWithAPlan.Server.Models;

public class CategoryGroupConfiguration : IEntityTypeConfiguration<CategoryGroup>
{
       public void Configure(EntityTypeBuilder<CategoryGroup> builder)
       {
              builder.ToTable("CategoryGroups");

              builder.HasKey(c => c.Id);

              builder.Property(c => c.Name)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.HasMany(c => c.Category)
                     .WithOne(c => c.CategoryGroup)
                     .HasForeignKey(c => c.CategoryGroupId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.Property(c => c.CreatedAt)
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

              builder.Property(c => c.UpdatedAt)
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");
       }
}
