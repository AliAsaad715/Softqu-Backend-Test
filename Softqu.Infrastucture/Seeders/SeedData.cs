using Microsoft.EntityFrameworkCore;
using Softqu.Domain.Category;
using Softqu.Domain.PopularCategory;
using Softqu.Domain.SwiperSlide;
using Softqu.Domain.SwiperSlide.ValueObjects;
using Softqu.Infrastructure.Data;

namespace Softqu.Infrastucture.Seeders
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // 1. Test if there are any categories already in the database to avoid seeding duplicate data
            if (await context.Categories.AnyAsync()) return;

            // 2. Adding main categories with translations
            var electronics = new CategoryAggregate("Electronics");
            electronics.AddTranslation("Electronics", "en");
            electronics.AddTranslation("الإلكترونيات", "ar");

            var fashion = new CategoryAggregate("Fashion");
            fashion.AddTranslation("Fashion", "en");
            fashion.AddTranslation("الأزياء", "ar");

            var homeDecor = new CategoryAggregate("Home Decor");
            homeDecor.AddTranslation("Home Decor", "en");
            homeDecor.AddTranslation("ديكور المنزل", "ar");

            // 3. Adding subcategories for Electronics and Fashion with translations
            var phones = new CategoryAggregate("Smart Phones", electronics.Id);
            phones.AddTranslation("Smart Phones", "en");
            phones.AddTranslation("الهواتف الذكية", "ar");

            var laptops = new CategoryAggregate("Laptops", electronics.Id);
            laptops.AddTranslation("Laptops", "en");
            laptops.AddTranslation("أجهزة المحمول", "ar");

            var dresses = new CategoryAggregate("Dresses", fashion.Id);
            dresses.AddTranslation("Dresses", "en");
            dresses.AddTranslation("الفساتين", "ar");

            await context.Categories.AddRangeAsync(electronics, fashion, homeDecor, phones, laptops, dresses);

            // 4. Adding a swiper slide
            var slide1 = new SwiperSlideAggregate(
                sortOrder: 1,
                imageUrl: "https://images.unsplash.com/photo-1519389950473-47ba0277781c",
                categoryId: electronics.Id
            );

            slide1.AddTranslation(
                lang: "en",
                texts: new SlideTexts("Flash Sale", "Get the best tech deals", "Order Now"),
                highlightedTitle: new HighlightedTitle("Tech Week", "#2196F3")
            );

            slide1.AddTranslation(
                lang: "ar",
                texts: new SlideTexts("عروض خاطفة", "احصل على أفضل صفقات التقنية", "اطلب الآن"),
                highlightedTitle: new HighlightedTitle("أسبوع التقنية", "#2196F3")
            );

            var slide2 = new SwiperSlideAggregate(
                sortOrder: 2,
                imageUrl: "https://images.unsplash.com/photo-1445205170230-053b83016050",
                categoryId: fashion.Id
            );

            slide2.AddTranslation(
                lang: "en",
                texts: new SlideTexts("New Collection", "Check out latest trends", "Explore"),
                highlightedTitle: new HighlightedTitle("Fashion 2026", "#E91E63")
            );

            slide2.AddTranslation(
                lang: "ar",
                texts: new SlideTexts("مجموعة جديدة", "اكتشف أحدث الصيحات", "استكشف"),
                highlightedTitle: new HighlightedTitle("موضة 2026", "#E91E63")
            );

            await context.SwiperSlides.AddRangeAsync(slide1, slide2);

            // 6. Adding popular categories
            var popular1 = new PopularCategoryAggregate(electronics.Id, sortOrder: 1);
            var popular2 = new PopularCategoryAggregate(fashion.Id, sortOrder: 2);

            await context.PopularCategories.AddRangeAsync(popular1, popular2);

            // 6. Save changes to the database
            await context.SaveChangesAsync();
        }
    }
}