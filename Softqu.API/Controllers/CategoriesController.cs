using Microsoft.AspNetCore.Mvc;
using Softqu.Application.Features.Categories.Commands;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Features.Categories.Queries;

namespace Softqu.API.Controllers
{
    [Route("api/admin/categories")]
    public class CategoriesController : ApiClientController
    {
        // 1. GET: api/admin/categories
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoriesTreeAsync()
        {
            var result = await Mediator.Send(new GetCategoriesTreeQuery());
            return HandleResult(result);
        }

        // 2. POST: api/admin/categories
        [HttpPost]
        [ProducesResponseType(typeof(CreateNewCategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateNewCategoryAsync([FromBody] CreateNewCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 3. POST: api/admin/categories/{languageCode}
        [HttpPost("{languageCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddOrUpdateCategoryTranslationAsync([FromRoute] string languageCode, [FromBody] AddOrUpdateCategoryTranslationCommand command)
        {
            command.LanguageCode = languageCode;
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 4. PUT: api/admin/categories
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 5. DELETE: api/admin/categories
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDeleteCategoryAsync([FromBody] SoftDeleteCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
    }
}
