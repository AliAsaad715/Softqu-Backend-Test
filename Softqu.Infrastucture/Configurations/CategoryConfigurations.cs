using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softqu.Domain.Category;
using Softqu.Domain.Category.Entities;

namespace Softqu.Infrastructure.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<CategoryAggregate>, IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Slug).IsRequired();
            builder.HasIndex(x => x.Slug).IsUnique();

            builder.HasMany(c => c.ChildCategories)
                   .WithOne()
                   .HasForeignKey(x => x.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Translations)
                   .WithOne()
                   .HasForeignKey("CategoryId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }

        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");
            builder.HasKey(tr => tr.Id);
            builder.Property(tr => tr.Id).ValueGeneratedNever();
            builder.HasIndex("CategoryId", nameof(CategoryTranslation.LanguageCode)).IsUnique();
            builder.Property(tr => tr.Title).IsRequired().HasMaxLength(200);
            builder.Property(tr => tr.LanguageCode).IsRequired().HasMaxLength(10);
            builder.Property<Guid>("CategoryId").IsRequired();
        }
    }
}
