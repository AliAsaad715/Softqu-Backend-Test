using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softqu.Domain.SwiperSlide;
using Softqu.Domain.SwiperSlide.Entities;

namespace Softqu.Infrastructure.Configurations
{
    public class SwiperSlideConfigurations : IEntityTypeConfiguration<SwiperSlideAggregate>
    {
        public void Configure(EntityTypeBuilder<SwiperSlideAggregate> builder)
        {
            // 1. Main Table Configuration
            builder.ToTable("SwiperSlides");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SortOrder).IsRequired();
            builder.Property(s => s.ImageUrl).IsRequired();

            // 2. Relationship Configuration
            builder.HasOne(s => s.Category) 
                   .WithMany()   
                   .HasForeignKey(s => s.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 3. Translations Configuration
            builder.OwnsMany(s => s.Translations, t =>
            {
                t.ToTable("SwiperSlideTranslations");
                t.HasKey(tr => tr.Id);
                t.WithOwner().HasForeignKey("SwiperSlideId");

                t.Property<Guid>("SwiperSlideId");
                t.HasIndex("SwiperSlideId", nameof(SwiperSlideTranslation.LanguageCode)).IsUnique();

                t.Property(tr => tr.LanguageCode).HasMaxLength(10).IsRequired();

                t.OwnsOne(tr => tr.Texts, tt =>
                {
                    tt.Property(x => x.TopText).IsRequired();
                    tt.Property(x => x.BigTitle).IsRequired();
                    tt.Property(x => x.BottomText).IsRequired();
                });

                t.OwnsOne(tr => tr.Title, ht =>
                {
                    ht.Property(x => x.NormalText).IsRequired();
                    ht.Property(x => x.ColorHighlight).IsRequired();
                    ht.Property(x => x.BoldHighlight).IsRequired(false);
                });
            });
        }
    }
}
