using MediatR;
using Softqu.Application.Shared.Results;
using System.Text.Json.Serialization;

namespace Softqu.Application.Features.Categories.Commands
{
    public class AddOrUpdateCategoryTranslationCommand : IRequest<AppResult>
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public string? LanguageCode { get; set; }
    }
}
