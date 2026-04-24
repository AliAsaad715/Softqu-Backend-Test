using Softqu.Domain.Shared.Exceptions;

namespace Softqu.Domain.SwiperSlide.ValueObjects
{
    public record SlideTexts
    {
        public string TopText { get; init; }
        public string BigTitle { get; init; }
        public string BottomText { get; init; }

        public SlideTexts(string topText, string bigTitle, string bottomText)
        {
            if (string.IsNullOrWhiteSpace(topText)) throw new DomainException("Top text is required.");
            if (string.IsNullOrWhiteSpace(bigTitle)) throw new DomainException("Big title is required.");
            if (string.IsNullOrWhiteSpace(bottomText)) throw new DomainException("Bottom text is required.");

            TopText = topText;
            BigTitle = bigTitle;
            BottomText = bottomText;
        }
    }
}
