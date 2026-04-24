using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softqu.Domain.PopularCategory;

namespace Softqu.Infrastructure.Configurations
{
    public class PopularCategoryConfigurations : IEntityTypeConfiguration<PopularCategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<PopularCategoryAggregate> builder)
        {
            // 1. Main Table Configuration
            builder.ToTable("PopularCategories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.SortOrder).IsRequired();

            // 2. Relationship Configuration
            builder.HasOne(p => p.Category) 
                   .WithMany()   
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CategoryId).IsUnique();
        }
    }
}
