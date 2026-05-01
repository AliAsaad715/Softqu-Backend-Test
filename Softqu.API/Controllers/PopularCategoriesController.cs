using Microsoft.AspNetCore.Mvc;
using Softqu.Application.Features.PopularCategories.Commands;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Features.PopularCategories.Queries;

namespace Softqu.API.Controllers
{
    [Route("api/admin/popular-categories")]
    public class PopularCategoriesController : ApiClientController
    {
        // 1. GET: api/admin/popular-categories
        [HttpGet]
        [ProducesResponseType(typeof(List<PopularCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPopularCategoriesAsync()
        {
            var result = await Mediator.Send(new GetPopularCategoriesQuery());
            return HandleResult(result);
        }

        // 2. POST: api/admin/popular-categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCategoryToPopularCategoriesAsync([FromBody] AddCategoryToPopularCategoriesCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 3. PUT: api/admin/popular-categories/order
        [HttpPut("order")]
        [ProducesResponseType(typeof(List<NewItemOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ChangePopularCategoryOrderAsync([FromBody] ChangePopularCategoryOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 4. DELETE: api/admin/popular-categories
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCategoryFromPopularCategoriesAsync([FromBody] RemoveCategoryFromPopularCategoriesCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
    }
}
