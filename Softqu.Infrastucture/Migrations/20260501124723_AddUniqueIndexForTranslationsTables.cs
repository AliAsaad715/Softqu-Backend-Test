using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Softqu.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexForTranslationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SwiperSlideTranslations_SwiperSlideId",
                table: "SwiperSlideTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "LanguageCode",
                table: "SwiperSlideTranslations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SwiperSlideTranslations_SwiperSlideId_LanguageCode",
                table: "SwiperSlideTranslations",
                columns: new[] { "SwiperSlideId", "LanguageCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_CategoryId_LanguageCode",
                table: "CategoryTranslations",
                columns: new[] { "CategoryId", "LanguageCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SwiperSlideTranslations_SwiperSlideId_LanguageCode",
                table: "SwiperSlideTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_CategoryId_LanguageCode",
                table: "CategoryTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "LanguageCode",
                table: "SwiperSlideTranslations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_SwiperSlideTranslations_SwiperSlideId",
                table: "SwiperSlideTranslations",
                column: "SwiperSlideId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId");
        }
    }
}
