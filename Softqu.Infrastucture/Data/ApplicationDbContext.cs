using Microsoft.EntityFrameworkCore;
using Softqu.Domain.Category;
using Softqu.Domain.PopularCategory;
using Softqu.Domain.SwiperSlide;

namespace Softqu.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CategoryAggregate> Categories { get; set; }

        public DbSet<PopularCategoryAggregate> PopularCategories { get; set; }

        public DbSet<SwiperSlideAggregate> SwiperSlides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
