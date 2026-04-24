using MediatR;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.Categories.Queries
{
    public class GetClientCategoriesTreeQuery : IRequest<AppResult<List<CategoryClientDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
