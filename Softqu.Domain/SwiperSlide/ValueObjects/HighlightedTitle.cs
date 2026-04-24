using Softqu.Domain.Shared.Exceptions;

namespace Softqu.Domain.SwiperSlide.ValueObjects
{
    public record HighlightedTitle
    {
        public string NormalText { get; init; }
        public string ColorHighlight { get; init; }
        public string? BoldHighlight { get; init; }

        public HighlightedTitle(string normalText, string colorHighlight, string? boldHighlight = null)
        {
            if (string.IsNullOrWhiteSpace(normalText))
                throw new DomainException("Normal text is required");
            if (string.IsNullOrWhiteSpace(colorHighlight))
                throw new DomainException("Color highlight is required");

            NormalText = normalText;
            ColorHighlight = colorHighlight;
            BoldHighlight = boldHighlight;
        }
    }
}
