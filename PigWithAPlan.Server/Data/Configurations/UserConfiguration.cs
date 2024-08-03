using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(c => c.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }

}